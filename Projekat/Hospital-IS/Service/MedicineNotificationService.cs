using Model;
using Storages;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service
{
    public class MedicineNotificationService
    {
        private MedcineNotificationStorage mnfs = new MedcineNotificationStorage();
        public List<MedicineNotification> allNotification { get; set; }

        private static MedicineNotificationService instance = null;
        public static MedicineNotificationService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MedicineNotificationService();
                }
                return instance;
            }
        }

        private MedicineNotificationService()
        {
            allNotification = mnfs.GetAll();
        }

        internal void AddNotification(MedicineNotification medicineNotification)
        {
            allNotification.Add(medicineNotification);
            mnfs.Save(allNotification);
        }
    }
}
