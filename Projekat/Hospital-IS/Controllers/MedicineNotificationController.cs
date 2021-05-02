using DoctorView;
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

            MedicineNotificationService.Instance.AddNotification(medicineNotification);
        }

        

        public void DisapproveMedicine(MedicineNotification reviewdNotification, string text)
        {
            reviewdNotification.Note = text;
            reviewdNotification.Title = "Odbijen " + reviewdNotification.Medicine.Name;
            reviewdNotification.DoctorIds.Clear();
            MedicineNotificationService.Instance.UpdateMedicineNotification(reviewdNotification);
        }

        public void ApproveMedicine(MedicineNotification reviewdNotification)
        {
            MedicineService.Instance.AddNewMedicine(reviewdNotification.Medicine);
            MedicineNotificationService.Instance.DeleteMedicineNotification(reviewdNotification);
        }

        public List<MedicineNotification> GetAll()
        {
            return MedicineNotificationService.Instance.GetAll();
        }
    }
}
