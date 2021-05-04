using Hospital_IS.Storages;
using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service
{
    public class MedicineService
    {
        private MedicineFileStorage mfs = new MedicineFileStorage();
        public List<Medicine> AllMedicines { get; set; }

        private static MedicineService instance = null;
        public static MedicineService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MedicineService();
                }
                return instance;
            }
        }

        private MedicineService()
        {
            AllMedicines = mfs.GetAll();
        }

        public void UpdateMedicine(Medicine medicine)
        {
            for(int i = 0; i < AllMedicines.Count; i++)
            {
                if (AllMedicines[i].Name.Equals(medicine.Name))
                {
                    AllMedicines.Remove(AllMedicines[i]);
                    AllMedicines.Insert(i, medicine);
                    mfs.Save(AllMedicines);
                }
            }
        }

        public void UpdateMedicineWithName(string oldName, Medicine medicine)
        {
            for (int i = 0; i < AllMedicines.Count; i++)
            {
                if (AllMedicines[i].Name.Equals(oldName))
                {
                    AllMedicines.Remove(AllMedicines[i]);
                    AllMedicines.Insert(i, medicine);
                    mfs.Save(AllMedicines);
                }
            }
        }

        internal void DeleteMedicine(Medicine medicine)
        {
            throw new NotImplementedException();
        }

        public void AddNewMedicine(Medicine medicine)
        {
            AllMedicines.Add(medicine);
            mfs.Save(AllMedicines);
        }

       
    }
}
