using DoctorView;
using Hospital_IS.DoctorView;
using Model;
using Service;
using System;
using System.Collections.Generic;
using System.Text;

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

        public void CreateNotification(string name, string sideEffect, string usage, List<ReplaceMedicineName> medicineNames, List<MedicineComponent> medicineComponents, List<int> recieverIds)
        {
            Medicine medicine = new Medicine(name, medicineComponents, sideEffect, usage, medicineNames);

            MedicineNotification medicineNotification = new MedicineNotification("Odobrenje" + " " + name, medicine, recieverIds);
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
            reviewdNotification.SenderId.Add(DoctorHomePage.Instance.Doctor.Id);
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
            MedicineNotificationService.Instance.AddApprovalDeleteDoctor(reviewdNotification, DoctorHomePage.Instance.Doctor.Id);
        }

        public List<MedicineNotification> GetAll()
        {
            return MedicineNotificationService.Instance.GetAll();
        }

        internal void CreateReNotification(string name, string sideEffect, string therapeutic, List<ReplaceMedicineName> medicineNames, List<MedicineComponent> medicineComponents, List<int> recieverIds)
        {
            Medicine medicine = new Medicine(name, medicineComponents, sideEffect, therapeutic, medicineNames);

            MedicineNotification medicineNotification = new MedicineNotification("RE:Odobrenje" + " " + name, medicine, recieverIds);
            medicineNotification.SenderId.Add(6);
            medicineNotification.DateSent = DateTime.Now;

            MedicineNotificationService.Instance.AddNotification(medicineNotification);
        }
    }
}
