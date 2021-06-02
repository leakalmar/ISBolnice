using Enums;
using Hospital_IS.Controllers;
using Hospital_IS.DTOs.SecretaryDTOs;
using Hospital_IS.SecretaryView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace Hospital_IS
{
    /// <summary>
    /// Interaction logic for UpdatePatient.xaml
    /// </summary>
    public partial class UpdatePatientView : Window
    {
        public ObservableCollection<String> Allergies { get; set; }
        public PatientDTO Patient { get; set; }
        public UCPatientsView ucp;

        public UpdatePatientView(PatientDTO patient, UCPatientsView ucp)
        {
            InitializeComponent();
            Patient = patient;
            this.ucp = ucp;
            SetPatientInfo();

            this.DataContext = this;
        }

        private void SetPatientInfo()
        {
            nameTxt.Text = Patient.Name;
            surnameTxt.Text = Patient.Surname;
            addressTxt.Text = Patient.Address;
            phoneTxt.Text = Patient.Phone;
            emailTxt.Text = Patient.Email;
            relationshipTxt.Text = Patient.Relationship;
            employerTxt.Text = Patient.Employer;
            if (Patient.Gender != null)
            {
                if (Patient.Gender.Equals("Žensko"))
                    genComboBox.SelectedIndex = 0;
                else if (Patient.Gender.Equals("Muško"))
                    genComboBox.SelectedIndex = 1;
            }

            if (Patient.Education.Equals(EducationCategory.GradeSchool))
                eduComboBox.SelectedIndex = 0;
            else if (Patient.Education.Equals(EducationCategory.HighSchool))
                eduComboBox.SelectedIndex = 1;
            else if (Patient.Education.Equals(EducationCategory.College))
                eduComboBox.SelectedIndex = 2;

            if (Patient.Alergies != null)
                Allergies = new ObservableCollection<String>(Patient.Alergies);

            birthdateTxt.Text = Patient.BirthDate.ToString("dd.MM.yyyy.");
        }

        private void UpdatePatient(object sender, RoutedEventArgs e)
        {
            string name = nameTxt.Text;
            string surname = surnameTxt.Text;
            string address = addressTxt.Text;
            string phone = phoneTxt.Text;
            string email = emailTxt.Text;
            string relationship = relationshipTxt.Text;
            string employer = employerTxt.Text;
            string gender = "";
            DateTime birthDate = Patient.BirthDate;
            EducationCategory education;

            if (genComboBox.SelectedIndex == 0)
                gender = "Žensko";
            else if (genComboBox.SelectedIndex == 1)
                gender = "Muško";

            if (eduComboBox.SelectedIndex == 0)
                education = EducationCategory.GradeSchool;
            else if (eduComboBox.SelectedIndex == 1)
                education = EducationCategory.HighSchool;
            else
                education = EducationCategory.College;


            try
            {
                birthDate = DateTime.ParseExact(birthdateTxt.Text, "dd.MM.yyyy.", System.Globalization.CultureInfo.InvariantCulture);
            }
            catch (Exception ex)
            {
            }

            List<String> allergies = new List<string>(Allergies);
            PatientDTO updatedPatient = new PatientDTO(Patient.Id, name, surname, gender, birthDate, phone, email, education, relationship, employer, Patient.Password, address, allergies, Patient.IsGuest);

            ucp.dataGridPatients.ItemsSource = null;
            ucp.dataGridPatients.ItemsSource = ucp.Patients;

            SecretaryManagementController.Instance.UpdatePatient(updatedPatient);

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
            AddAllergy aa = new AddAllergy(this);
            aa.ShowDialog();
        }

        private void ChangeAllergy(object sender, RoutedEventArgs e)
        {
            if ((string)dataGridAllergies.SelectedItem != null)
            {
                UpdateAllergy ua = new UpdateAllergy(this);
                ua.ShowDialog();
            }
        }

        private void DeleteAllergy(object sender, RoutedEventArgs e)
        {
            if ((string)dataGridAllergies.SelectedItem != null)
            {
                RemoveAllergy ra = new RemoveAllergy(this);
                ra.ShowDialog();
            }
        }

        private void DeletePatient(object sender, RoutedEventArgs e)
        {
            DeletePatientView dpv = new DeletePatientView(Patient, this);
            dpv.ShowDialog();
        }

        private void UndoAllChanges(object sender, RoutedEventArgs e)
        {
            ucp.RefreshGrid();
            Patient = SecretaryManagementController.Instance.GetPatientByID(Patient.Id);
            SetPatientInfo();
            this.DataContext = this;
        }
    }
}
