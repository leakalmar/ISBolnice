using System;
using System.Collections.ObjectModel;

namespace Model
{
    public class Prescription
    {
        public ObservableCollection<String> Medicine { get; set; }

        public Prescription()
        {
            Medicine = new ObservableCollection<string>();
        }

        public void AddMedicine(String medicine)
        {
            Medicine.Add(medicine);
        }

        public void RemoveMedicine(String medicine)
        {
            Medicine.Remove(medicine);
        }


    }
}