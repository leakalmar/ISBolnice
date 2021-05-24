using Controllers;
using Hospital_IS.SecretaryView.SecretaryViewModel;
using Model;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace Hospital_IS.SecretaryView
{
    /// <summary>
    /// Interaction logic for UCPatientsView.xaml
    /// </summary>
    public partial class UCPatientsView : UserControl
    {
        //public ObservableCollection<Patient> Patients { get; set; }

        public UCPatientsView()
        {
            InitializeComponent();
            RefreshGrid();
            //this.DataContext = this;
        }

        
        public void RefreshGrid()
        {
            /*if (Patients != null)
                Patients.Clear();

            PatientController.Instance.ReloadPatients();
            Patients = new ObservableCollection<Patient>(PatientController.Instance.GetAllRegisteredPatients());
            dataGridPatients.ItemsSource = Patients;*/
        }
        
        private void AddNewPatient(object sender, RoutedEventArgs e)
        {
        //    PatientRegistrationView pr = new PatientRegistrationView(this, null);
          //  pr.Show();
        }

        /*private void ShowPatient(object sender, RoutedEventArgs e)
        {
            if ((Patient)dataGridPatients.SelectedItem != null)
            {
                Patient patient = (Patient)dataGridPatients.SelectedItem;
                PatientView pv = new PatientView(patient);
                pv.Show();
            }
        }*/

        private void UpdatePatient(object sender, RoutedEventArgs e)
        {
           /* if ((Patient)dataGridPatients.SelectedItem != null)
            {
                Patient patient = (Patient)dataGridPatients.SelectedItem;
                UpdatePatientView upv = new UpdatePatientView(patient, this);
                upv.Show();
            }*/
        }

        private void DeletePatient(object sender, RoutedEventArgs e)
        {/*if ((Patient)dataGridPatients.SelectedItem != null)
            {
                Patient patient = (Patient)dataGridPatients.SelectedItem;
                Patients.Remove(patient);
                PatientController.Instance.DeletePatient(patient);
            }*/
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

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            /*ICollectionView view = new CollectionViewSource { Source = Patients }.View;
            view.Filter = delegate (object item)
            {
                Patient patient = item as Patient;
                return CheckIfPatientMeetsSearchCriteria(patient);
            };
            dataGridPatients.ItemsSource = view;*/
        }

        private bool CheckIfPatientMeetsSearchCriteria(Patient patient)
        {
            string[] search = txtSearch.Text.ToLower().Split(" ");
            if (txtSearch.Text.Equals("Pretraži..."))
                search[0] = string.Empty;

            if (search.Length <= 1)
                return patient.Name.ToLower().Contains(search[0]) | patient.Surname.ToLower().Contains(search[0]) | patient.BirthDate.ToString("dd.MM.yyyy.").Contains(search[0]);
            else
            { 
            bool FirstName = true;
            bool LastName = true;
            bool BirthDate = true;
            int cnt = 0;

                for (int i = 0; i < search.Length; i++)
                {
                    if (patient.Name.ToLower().Contains(search[i]) && FirstName)
                    {
                        FirstName = false;
                        cnt++;
                        continue;
                    }
                    if (patient.Surname.ToLower().Contains(search[i]) && LastName)
                    {
                        LastName = false;
                        cnt++;
                        continue;
                    }
                    if (patient.BirthDate.ToString("dd.MM.yyyy.").Contains(search[i]) && BirthDate)
                    {
                        BirthDate = false;
                        cnt++;
                        continue;
                    }
                }

                return cnt == search.Length;
            }

        }

        private void SearchBox_GotFocus(object sender, RoutedEventArgs e)
        {
            /*if (txtSearch.Text.Equals("Pretraži..."))
            {
                txtSearch.Text = string.Empty;
                txtSearch.Foreground = new SolidColorBrush(Colors.Black);
            }*/
        }

        private void SearchBox_LostFocus(object sender, RoutedEventArgs e)
        {
            /*if (string.IsNullOrEmpty(txtSearch.Text))
            {
                txtSearch.Foreground = new SolidColorBrush(Colors.Gray);
                txtSearch.Text = "Pretraži...";
            }*/
        }

        private void btnGuests_Click(object sender, RoutedEventArgs e)
        {
         //   GuestsView gv = new GuestsView(this);
           // gv.Show();
        }

    }
}
