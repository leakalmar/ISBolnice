using Hospital_IS.DoctorView;
using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DTOs
{
    public class MedicineDTO : BindableBase
    {
        private Medicine medicine;
        private bool check;
        private bool allergic;

        public Medicine Medicine
        {
            get { return medicine; }
            set
            {
                medicine = value;
                OnPropertyChanged("Medicine");
            }
        }

        public bool Check
        {
            get { return check; }
            set
            {
                check = value;
                OnPropertyChanged("Check");
            }
        }

        public bool Allergic
        {
            get { return allergic; }
            set
            {
                allergic = value;
                OnPropertyChanged("Allergic");
            }
        }

        public MedicineDTO(Medicine medicine, bool check, bool allergic)
        {
            Medicine = medicine;
            Check = check;
            Allergic = allergic;
        }
    }
}
