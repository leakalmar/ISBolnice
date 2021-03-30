using System;
using System.Windows;

namespace Hospital_IS
{
    /// <summary>
    /// Interaction logic for PatientRegistration.xaml
    /// </summary>
    public partial class PatientRegistration : Window
    {
        public Model.Patient Patient { get; set; } = new Model.Patient();
        public PatientRegistration()
        {
            InitializeComponent();
            this.DataContext = this;
        }
        private void Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void AddPatient(object sender, RoutedEventArgs e)
        {
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
            SecretaryMainWindow.Instance.Patients.Add(Patient);

            Storages.PatientFileStorage pfs = new Storages.PatientFileStorage();
            pfs.SavePatient(Patient);

            this.Close();
        }
    }
}
