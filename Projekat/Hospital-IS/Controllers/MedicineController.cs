﻿using Model;
using Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace Controllers
{
    public class MedicineController
    {
        private static MedicineController instance = null;
        public static MedicineController Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MedicineController();
                }
                return instance;
            }
        }

        private MedicineController()
        {
        }

        public List<Medicine> GetAll()
        {
            return MedicineService.Instance.AllMedicines;
        }

        public void UpdateMedicine(Medicine medicine)
        {
            MedicineService.Instance.UpdateMedicine(medicine);
        }
    }
}