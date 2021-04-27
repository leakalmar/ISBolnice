using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital_IS.DoctorView
{
    public class MedicineObject
    {
        public Medicine Medicine { get; set; }
        public bool Check { get; set; }
        public bool Allergic { get; set; }

        public MedicineObject(Medicine medicine, bool check, bool allergic)
        {
            Medicine = medicine;
            Check = check;
            Allergic = allergic;
        }
    }
}
