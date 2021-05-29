using Hospital_IS.DoctorRole.Commands;
using Hospital_IS.DTOs;
using System.Collections.Generic;

namespace DTOs
{
    public class MedicineDTO : BindableBase
    {
        private string name;
        public List<MedicineComponentDTO> Composition { get; set; }
        public string SideEffects { get; set; }
        public string Usage { get; set; }
        public List<ReplaceMedicineNameDTO> ReplaceMedicine { get; set; }
        private bool check;
        private bool allergic;

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Name");
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

        public MedicineDTO(List<MedicineComponentDTO> composition, string sideEffects, string usage, List<ReplaceMedicineNameDTO> replaceMedicine, string name, bool check, bool allergic)
        {
            Composition = composition;
            SideEffects = sideEffects;
            Usage = usage;
            ReplaceMedicine = replaceMedicine;
            Name = name;
            Check = check;
            Allergic = allergic;
        }
    }
}
