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
        public List<MedicineNotification> allNotification { get; set; } = new List<MedicineNotification>();

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
        public List<MedicineNotification> GetAllByDoctorId(int idDoctor)
        {
            List<MedicineNotification> notificationForDoctor = new List<MedicineNotification>();
            foreach(MedicineNotification medicineNotification in allNotification)
            {
                if (medicineNotification.RecieverIds.Contains(idDoctor))
                {
                    notificationForDoctor.Add(medicineNotification);
                }
            }
            return notificationForDoctor;
        }

        public void AddNotification(MedicineNotification medicineNotification)
        {
            allNotification.Add(medicineNotification);
            mnfs.Save(allNotification);
        }

        public void UpdateMedicineNotification(MedicineNotification reviewdNotification)
        {
            for(int i = 0; i< allNotification.Count; i++)
            {
                if (allNotification[i].Medicine.Name.Equals(reviewdNotification.Medicine.Name))
                {
                    allNotification.RemoveAt(i);
                    allNotification.Insert(i, reviewdNotification);
                    mnfs.Save(allNotification);
                }
            }
        }

        

        public void DeleteMedicineNotification(MedicineNotification reviewdNotification)
        {
            for (int i = 0; i < allNotification.Count; i++)
            {
                if (allNotification[i].Medicine.Name.Equals(reviewdNotification.Medicine.Name))
                {
                    allNotification.RemoveAt(i);
                    mnfs.Save(allNotification);
                }
            }
        }

        public void DeleteNotification(MedicineNotification medicineNotification)
        {
            allNotification.Remove(medicineNotification);
            mnfs.Save(allNotification);
        }

        public List<MedicineNotification> GetAll()
        {
            return allNotification;
        }
    }
}
