using DTOs;
using Hospital_IS.DoctorRole.DoctorConverters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows;

namespace Hospital_IS.DTOs
{
    public class TherapyDTO : ValidationBase
    {
        private string name;
        private int pills;
        private int takings;
        private String therapyEnd;

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }
        public int Pills
        {
            get { return pills; }
            set
            {
                pills = value;
                OnPropertyChanged("Pills");
            }
        }

        public int Takings
        {
            get { return takings; }
            set
            {
                takings = value;
                OnPropertyChanged("Takings");
            }
        }

        public String TherapyEnd
        {
            get { return therapyEnd; }
            set
            {
                therapyEnd = value;
                OnPropertyChanged("TherapyEnd");
            }
        }

        protected override void ValidateSelf()
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                this.ValidationErrors["Medicine"] = "Lek je obavezan.";
            }

            if (new DateConverter().ConvertBack(this.TherapyEnd, null, null, CultureInfo.CurrentCulture) == DependencyProperty.UnsetValue)
            {
                this.ValidationErrors["Date"] = "dd.MM.yyyy.";
            }

        }
    }
}
