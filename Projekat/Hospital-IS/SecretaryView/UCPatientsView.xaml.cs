using Controllers;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;


namespace Hospital_IS
{
    /// <summary>
    /// Interaction logic for UCPatientsView.xaml
    /// </summary>
    public partial class UCPatientsView : UserControl
    {
        public ObservableCollection<Patient> Patients { get; set; }

        public UCPatientsView()
        {
            InitializeComponent();
            RefreshGrid();

            this.DataContext = this;
        }


        public void RefreshGrid()
        {
            if (Patients != null)
                Patients.Clear();

            PatientController.Instance.ReloadPatients();
            Patients = new ObservableCollection<Patient>(PatientController.Instance.GetAll());
            dataGridPatients.ItemsSource = Patients;
        }

        private void AddNewPatient(object sender, RoutedEventArgs e)
        {
            PatientRegistration pr = new PatientRegistration(this);
            pr.Show();
        }

        private void ShowPatient(object sender, RoutedEventArgs e)
        {
            if ((Patient)dataGridPatients.SelectedItem != null)
            {
                Patient patient = (Patient)dataGridPatients.SelectedItem;
                PatientView pv = new PatientView(patient);
                pv.Show();
            }
        }

        private void UpdatePatient(object sender, RoutedEventArgs e)
        {
            if ((Patient)dataGridPatients.SelectedItem != null)
            {
                Patient patient = (Patient)dataGridPatients.SelectedItem;
                UpdatePatientView upv = new UpdatePatientView(patient, this);
                upv.Show();
            }
        }

        private void DeletePatient(object sender, RoutedEventArgs e)
        {
            if ((Patient)dataGridPatients.SelectedItem != null)
            {
                Patient patient = (Patient)dataGridPatients.SelectedItem;
                Patients.Remove(patient);
                PatientController.Instance.DeletePatient(patient);
            }
        }


        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null && value is DateTime && (DateTime)value < new DateTime(2, 1, 1))
            {
                return "";
            }
            else
                return value;
        }

    }
}
