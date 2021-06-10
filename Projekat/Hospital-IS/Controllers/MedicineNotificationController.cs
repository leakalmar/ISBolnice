using Hospital_IS.DoctorRole.DoctorView;
using Hospital_IS.DoctorViewModel;
using Hospital_IS.DTOs;
using Model;
using Service;
using System;
using System.Collections.Generic;

namespace Controllers
{
    public class MedicineNotificationController
    {

        private static MedicineNotificationController instance = null;
        public static MedicineNotificationController Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MedicineNotificationController();
                }
                return instance;
            }
        }

        public List<MedicineNotification> GetAllByDoctorId(int idDoctor)
        {
            return MedicineNotificationService.Instance.GetAllByDoctorId(idDoctor);
        }



        private MedicineNotificationController()
        {

        }

        public void CreateNotification(MedicineNotificationDTO notificationDTO)
        {

            Medicine medicine = new Medicine(notificationDTO.Name, notificationDTO.MedicineComponents, notificationDTO.SideEffect,
                notificationDTO.Usage, notificationDTO.MedicineNames);

            MedicineNotification medicineNotification = new MedicineNotification("Odobrenje" + " " + notificationDTO.Name, medicine, notificationDTO.RecieverIds);
            medicineNotification.ApprovalCounter = 0;
            medicineNotification.SenderId.Add(6);
            medicineNotification.DateSent = DateTime.Now;

            MedicineNotificationService.Instance.AddNotification(medicineNotification);
        }

        public void DisapproveMedicine(MedicineNotification reviewdNotification, string text)
        {
            reviewdNotification.Note = text;
            reviewdNotification.Title = "Odbijen " + reviewdNotification.Medicine.Name;
            reviewdNotification.SenderId.Clear();
            reviewdNotification.SenderId.Add(DoctorMainWindowModel.Instance.Doctor.Id);
            reviewdNotification.RecieverIds.Clear();
            reviewdNotification.RecieverIds.Add(6);
            reviewdNotification.DateSent = DateTime.Now;
            MedicineNotificationService.Instance.UpdateMedicineNotification(reviewdNotification);
        }

        public void DeleteNotification(MedicineNotification medicineNotification)
        {
            MedicineNotificationService.Instance.DeleteNotification(medicineNotification);
        }

        public void ApproveMedicine(MedicineNotification reviewdNotification)
        {
            MedicineNotificationService.Instance.AddApprovalDeleteDoctor(reviewdNotification, DoctorMainWindowModel.Instance.Doctor.Id);
        }

        public List<MedicineNotification> GetAll()
        {
            return MedicineNotificationService.Instance.GetAll();
        }

        internal void CreateReNotification(MedicineNotificationDTO notificationDTO)
        {
            Medicine medicine = new Medicine(notificationDTO.Name, notificationDTO.MedicineComponents, notificationDTO.SideEffect,
                notificationDTO.Usage, notificationDTO.MedicineNames);

            MedicineNotification medicineNotification = new MedicineNotification("RE:Odobrenje" + " " + notificationDTO.Name, medicine, notificationDTO.RecieverIds);
            medicineNotification.SenderId.Add(6);
            medicineNotification.DateSent = DateTime.Now;

            MedicineNotificationService.Instance.AddNotification(medicineNotification);
        }
    }
}
