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

        internal void CreateNotification(string nameClass, string sideEffectClass, string therapeuticClass, List<ReplaceMedicineName> medicineNamesClass, List<MedicineComponent> medicineComponentsClass, List<int> doctorsIds)
        {
            Medicine medicine = new Medicine(nameClass, medicineComponentsClass, sideEffectClass, therapeuticClass, medicineNamesClass);

            MedicineNotification medicineNotification = new MedicineNotification("Odobrenje" + " " + nameClass, medicine, doctorsIds);
            medicineNotification.SenderId.Add(6);
            medicineNotification.DateSent = DateTime.Now;

            MedicineNotificationService.Instance.AddNotification(medicineNotification);
        }

        public void DisapproveMedicine(MedicineNotification reviewdNotification, string text)
        {
            reviewdNotification.Note = text;
            reviewdNotification.Title = "Odbijen " + reviewdNotification.Medicine.Name;
            reviewdNotification.SenderId.Clear();
            reviewdNotification.SenderId.AddRange(reviewdNotification.RecieverIds);
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
            MedicineService.Instance.AddNewMedicine(reviewdNotification.Medicine);
            MedicineNotificationService.Instance.DeleteNotification(reviewdNotification);
        }

        public List<MedicineNotification> GetAll()
        {
            return MedicineNotificationService.Instance.GetAll();
        }

        internal void CreateReNotification(string nameClass, string sideEffectClass, string therapeuticClass, List<ReplaceMedicineName> medicineNamesClass, List<MedicineComponent> medicineComponentsClass, List<int> doctorsIds)
        {
            Medicine medicine = new Medicine(nameClass, medicineComponentsClass, sideEffectClass, therapeuticClass, medicineNamesClass);

            MedicineNotification medicineNotification = new MedicineNotification("RE:Odobrenje" + " " + nameClass, medicine, doctorsIds);
            medicineNotification.SenderId.Add(6);
            medicineNotification.DateSent = DateTime.Now;

            MedicineNotificationService.Instance.AddNotification(medicineNotification);
        }
    }
}
