using Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace Hospital_IS.SecretaryView.SecretaryViewModel
{
    class PatientRegistrationViewModel : BindableBase
    {
        public Patient Patient { get; set; } = new Patient();

        private string birthDate;
        public string BirthDate
        {
            get { return birthDate; }
            set
            {
                if (birthDate != value)
                {
                    birthDate = value;
                    OnPropertyChanged("BirthDate");
                }
            }
        }
        public MyICommand AddPatient { get; set; }
        public PatientRegistrationViewModel()
        {
            AddPatient = new MyICommand(RegisterPatient);
        }

        private void RegisterPatient()
        {
            if (checkBox.IsChecked == true)
            {
                Patient.IsGuest = true;
                Patient.BirthDate = DateTime.Now;
            }

            if (comboBox.SelectedIndex == 0)
                Patient.Gender = "Žensko";
            else if (comboBox.SelectedIndex == 1)
                Patient.Gender = "Muško";

            try
            {
                DateTime birthDate = DateTime.ParseExact(birthdateTxt.Text, "dd.MM.yyyy.", System.Globalization.CultureInfo.InvariantCulture);
                Patient.BirthDate = birthDate;
            }
            catch (Exception ex)
            {
            }

            Patient.Password = passwordBox.ToString();

            if (checkBox.Visibility != Visibility.Collapsed)
            {
                Patient.Id = UserService.Instance.GenerateUserID();
                PatientController.Instance.AddPatient(Patient);
            }
            else
            {
                Patient.IsGuest = false;
                PatientController.Instance.UpdatePatient(Patient);
                gv.RefreshGrid();
            }

            ucp.RefreshGrid();

            this.Close();
        }
    }
}
