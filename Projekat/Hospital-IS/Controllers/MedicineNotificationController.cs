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

        private MedicineNotificationController()
        {
          
        }

        internal void CreateNotification(string nameClass, string sideEffectClass, string therapeuticClass, List<ReplaceMedicineName> medicineNamesClass, List<MedicineComponent> medicineComponentsClass, List<int> doctorsIds)
        {
            Medicine medicine = new Medicine(nameClass, medicineComponentsClass, sideEffectClass, therapeuticClass, medicineNamesClass);

            MedicineNotification medicineNotification = new MedicineNotification("Odobrenje" + " " + nameClass, medicine, doctorsIds);

            MedicineNotificationService.Instance.AddNotification(medicineNotification);
        }
    }
}
