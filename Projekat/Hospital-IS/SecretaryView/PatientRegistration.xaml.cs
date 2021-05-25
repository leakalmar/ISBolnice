using Hospital_IS.Controllers;
using Hospital_IS.DTOs.SecretaryDTOs;
using Hospital_IS.SecretaryView;
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
        public PatientDTO Patient { get; set; } = new PatientDTO();
        public UCPatientsView ucp;
        public GuestsView gv;
        public PatientRegistration(UCPatientsView ucp, GuestsView gv)
        {
            InitializeComponent();

            if (gv != null)
            {
                Patient = (PatientDTO)gv.dataGridGuests.SelectedItem;
                checkBox.Visibility = Visibility.Collapsed;
                this.gv = gv;
            }

            this.DataContext = this;
            this.ucp = ucp;
        }
        private void Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void AddPatient(object sender, RoutedEventArgs e)
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
                SecretaryManagementController.Instance.AddPatient(Patient);
            }
            else 
            {
                Patient.IsGuest = false;
                SecretaryManagementController.Instance.UpdatePatient(Patient);
                gv.RefreshGrid();
            }

            ucp.RefreshGrid();

            this.Close();
        }
    }
}
