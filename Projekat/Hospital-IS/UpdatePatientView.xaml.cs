using System;
using System.Windows;

namespace Hospital_IS
{
    /// <summary>
    /// Interaction logic for UpdatePatient.xaml
    /// </summary>
    public partial class UpdatePatientView : Window
    {
        public Model.Patient Patient { get; set; }
        public UCPatientsView ucp;

        public UpdatePatientView(Model.Patient patient, UCPatientsView ucp)
        {
            InitializeComponent();
            Patient = patient;
            this.ucp = ucp;

            if (Patient.Gender != null)
            {
                if (Patient.Gender.Equals("Žensko"))
                    genComboBox.SelectedIndex = 0;
                else if (Patient.Gender.Equals("Muško"))
                    genComboBox.SelectedIndex = 1;
            }

            if (Patient.Education.Equals(Model.EducationCategory.GradeSchool))
                eduComboBox.SelectedIndex = 0;
            else if (Patient.Education.Equals(Model.EducationCategory.HighSchool))
                eduComboBox.SelectedIndex = 1;
            else if (Patient.Education.Equals(Model.EducationCategory.College))
                eduComboBox.SelectedIndex = 2;

            this.DataContext = this;
        }

        private void UpdatePatient(object sender, RoutedEventArgs e)
        {
            if (genComboBox.SelectedIndex == 0)
                Patient.Gender = "Žensko";
            else if (genComboBox.SelectedIndex == 1)
                Patient.Gender = "Muško";

            if (eduComboBox.SelectedIndex == 0)
                Patient.Education = Model.EducationCategory.GradeSchool;
            else if (eduComboBox.SelectedIndex == 1)
                Patient.Education = Model.EducationCategory.HighSchool;
            else if (eduComboBox.SelectedIndex == 2)
                Patient.Education = Model.EducationCategory.College;


            try
            {
                DateTime birthDate = DateTime.ParseExact(birthdateTxt.Text, "dd.MM.yyyy.", System.Globalization.CultureInfo.InvariantCulture);
                Patient.BirthDate = birthDate;
            }
            catch (Exception ex)
            {
            }

            ucp.dataGridPatients.ItemsSource = null;
            ucp.dataGridPatients.ItemsSource = ucp.Patients;
            Storages.PatientFileStorage pfs = new Storages.PatientFileStorage();
            pfs.UpdatePatient(Patient);

            this.Close();
        }

        private void Close(object sender, RoutedEventArgs e)
        {
            ucp.RefreshGrid();
            this.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ucp.RefreshGrid();
        }

        private void AddNewAllergy(object sender, RoutedEventArgs e)
        {
            AddAllergy aa = new AddAllergy();
            aa.Show();
        }

        private void ChangeAllergy(object sender, RoutedEventArgs e)
        {
            UpdateAllergy ua = new UpdateAllergy();
            ua.Show();
        }

        private void DeleteAllergy(object sender, RoutedEventArgs e)
        {
            RemoveAllergy ra = new RemoveAllergy();
            ra.Show();
        }
    }
}
