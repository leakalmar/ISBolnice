using Controllers;
using Model;
using Service;
using System;
using System.Windows;

namespace Hospital_IS
{
    /// <summary>
    /// Interaction logic for PatientRegistration.xaml
    /// </summary>
    public partial class PatientRegistration : Window
    {
        public Patient Patient { get; set; } = new Patient();
        public UCPatientsView ucp;
        public PatientRegistration(UCPatientsView ucp)
        {
            InitializeComponent();
            this.DataContext = this;
            this.ucp = ucp;
        }
        private void Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void AddPatient(object sender, RoutedEventArgs e)
        {
            Patient.Id = UserService.Instance.GenerateUserID();

            if (checkBox.IsChecked == true)
                Patient.IsGuest = true;

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
            ucp.Patients.Add(Patient);

            PatientController.Instance.AddPatient(Patient);

            this.Close();
        }
    }
}
