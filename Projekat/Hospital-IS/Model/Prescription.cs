using System;
using System.Collections.ObjectModel;

namespace Model
{
    public class Prescription
    {
        public Medicine Medicine { get; set; }
        public DateTime DatePrescribed { get; set; }


        public Prescription(Medicine med, DateTime datePrescribed)
        {
            Medicine = med;
            DatePrescribed = datePrescribed;
        }
    }
}