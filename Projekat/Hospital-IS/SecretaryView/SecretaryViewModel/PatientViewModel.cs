using Controllers;
using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital_IS.SecretaryView.SecretaryViewModel
{
    class PatientViewModel : BindableBase
    {
        public Patient Patient { get; set; }

        private string education;
        public string Education
        {
            get { return education; }
            set
            {
                if (education != value)
                {
                    education = value;
                    OnPropertyChanged("Education");
                }
            }
        }

        public PatientViewModel(int patientId)
        {
            Patient = PatientController.Instance.GetPatientByID(patientId);
            SetEducation();
        }

        public void SetEducation()
        {
            if (Patient.Education.Equals(EducationCategory.NA))
                Education = " ";
            else if (Patient.Education.Equals(EducationCategory.GradeSchool))
                Education = "Osnovna škola";
            else if (Patient.Education.Equals(EducationCategory.HighSchool))
                Education = "Srednja škola";
            else if (Patient.Education.Equals(EducationCategory.College))
                Education = "Fakultet";
        }
    }
}
