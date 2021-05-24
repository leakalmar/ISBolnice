using Controllers;
using Hospital_IS.SecretaryView;
using Hospital_IS.SecretaryView.SecretaryViewModel;
using Model;
using Service;
using System;
using System.Windows;

namespace Hospital_IS.SecretaryView
{
    /// <summary>
    /// Interaction logic for PatientRegistrationView.xaml
    /// </summary>
    public partial class PatientRegistrationView : Window
    {
        public Patient Patient { get; set; } = new Patient();
        public UCPatientsView ucp;
        public GuestsView gv;

        public PatientRegistrationView()
        {
            InitializeComponent();
            this.DataContext = new PatientRegistrationViewModel();
        }

        public PatientRegistrationView(UCPatientsView ucp, GuestsView gv)
        {
            InitializeComponent();

            if (gv != null)
            {
                Patient = (Patient)gv.dataGridGuests.SelectedItem;
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
       /*     if (checkBox.IsChecked == true)
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

            this.Close();*/
        }
    }
}
