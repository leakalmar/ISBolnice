using Controllers;
using Hospital_IS.Adapter;
using Hospital_IS.Controllers;
using Hospital_IS.DTOs.SecretaryDTOs;
using Hospital_IS.SecretaryView;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace Hospital_IS
{
    /// <summary>
    /// Interaction logic for UCPatientsView.xaml
    /// </summary>
    public partial class UCPatientsView : UserControl
    {
        public ObservableCollection<PatientDTO> Patients { get; set; }

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
            SecretaryManagementController.Instance.ReloadPatients();
            Patients = new ObservableCollection<PatientDTO>(SecretaryManagementController.Instance.GetAllRegisteredPatients());
            dataGridPatients.ItemsSource = Patients;
        }

        private void AddNewPatient(object sender, RoutedEventArgs e)
        {
            PatientRegistration pr = new PatientRegistration(this, null);
            pr.ShowDialog();
        }

        private void ShowPatient(object sender, RoutedEventArgs e)
        {
            if ((PatientDTO)dataGridPatients.SelectedItem != null)
            {
                PatientDTO patient = (PatientDTO)dataGridPatients.SelectedItem;
                PatientView pv = new PatientView(patient);
                pv.Show();
            }
            else
                MessageBox.Show("Izaberite pacijenta!");
        }

        private void UpdatePatient(object sender, RoutedEventArgs e)
        {
            if ((PatientDTO)dataGridPatients.SelectedItem != null)
            {
                PatientDTO patient = (PatientDTO)dataGridPatients.SelectedItem;
                UpdatePatientView upv = new UpdatePatientView(patient, this);
                upv.ShowDialog();
            }
            else
                MessageBox.Show("Izaberite pacijenta!");
        }

        //private void DeletePatient(object sender, RoutedEventArgs e)
        //{
        //    if ((PatientDTO)dataGridPatients.SelectedItem != null)
        //    {
        //        PatientDTO patient = (PatientDTO)dataGridPatients.SelectedItem;
        //        Patients.Remove(patient);
        //        SecretaryManagementController.Instance.DeletePatient(patient);
        //    }
        //}

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            ICollectionView view = new CollectionViewSource { Source = Patients }.View;
            view.Filter = delegate (object item)
            {
                PatientDTO patient = item as PatientDTO;
                return CheckIfPatientMeetsSearchCriteria(patient);
            };
            dataGridPatients.ItemsSource = view;
        }

        private bool CheckIfPatientMeetsSearchCriteria(PatientDTO patient)
        {
            string[] search = txtSearch.Text.ToLower().Split(" ");
            if (txtSearch.Text.Equals("Pretraži...") || txtSearch.Text.Equals("Search..."))
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
            if (txtSearch.Text.Equals("Pretraži...") || txtSearch.Text.Equals("Search..."))
            {
                txtSearch.Text = string.Empty;
                txtSearch.Foreground = new SolidColorBrush(Colors.Black);
            }
        }

        private void SearchBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtSearch.Text))
            {
                txtSearch.Foreground = new SolidColorBrush(Colors.Gray);
                if (SecretaryMainWindow.Instance.miSerbian.IsChecked)
                    txtSearch.Text = "Pretraži...";
                else
                    txtSearch.Text = "Search...";
            }
        }

        private void btnGuests_Click(object sender, RoutedEventArgs e)
        {
            GuestsView gv = new GuestsView(this);
            gv.ShowDialog();
        }
    }
}
