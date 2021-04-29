using Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;


namespace Hospital_IS
{
    /// <summary>
    /// Interaction logic for UCPatientsView.xaml
    /// </summary>
    public partial class UCPatientsView : UserControl
    {
        public ObservableCollection<Model.Patient> Patients { get; set; }
        Storages.PatientFileStorage pfs = new Storages.PatientFileStorage();

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
            List<Model.Patient> patients = pfs.GetAll();
            Patients = new ObservableCollection<Model.Patient>(patients);
            dataGridPatients.ItemsSource = Patients;
        }

        private void AddNewPatient(object sender, RoutedEventArgs e)
        {
            PatientRegistration pr = new PatientRegistration(this);
            pr.Show();
        }

        private void ShowPatient(object sender, RoutedEventArgs e)
        {
            if ((Model.Patient)dataGridPatients.SelectedItem != null)
            {
                Model.Patient patient = (Model.Patient)dataGridPatients.SelectedItem;
                PatientView pv = new PatientView(patient);
                pv.Show();
            }
        }

        private void UpdatePatient(object sender, RoutedEventArgs e)
        {
            if ((Model.Patient)dataGridPatients.SelectedItem != null)
            {
                Model.Patient patient = (Model.Patient)dataGridPatients.SelectedItem;
                UpdatePatientView upv = new UpdatePatientView(patient, this);
                upv.Show();
            }
        }

        private void DeletePatient(object sender, RoutedEventArgs e)
        {
            if ((Model.Patient)dataGridPatients.SelectedItem != null)
            {
                Model.Patient patient = (Model.Patient)dataGridPatients.SelectedItem;
                Patients.Remove(patient);
                pfs.DeletePatient(patient);
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
