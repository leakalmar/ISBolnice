using Controllers;
using Hospital_IS.Adapter;
using Hospital_IS.Controllers;
using Hospital_IS.DTOs;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace Hospital_IS.SecretaryView
{
    /// <summary>
    /// Interaction logic for UCDoctorsView.xaml
    /// </summary>
    public partial class UCDoctorsView : UserControl
    {
        public ObservableCollection<DoctorDTO> Doctors { get; set; }
        public UCDoctorsView()
        {
            InitializeComponent();
            RefreshGrid();

            this.DataContext = this;
        }

        public void RefreshGrid()
        {
            if (Doctors != null)
                Doctors.Clear();

            SecretaryManagementController.Instance.ReloadDoctors();
            Doctors = new ObservableCollection<DoctorDTO>(SecretaryManagementController.Instance.GetAllDoctors());
            dataGridDoctors.ItemsSource = Doctors;
        }

        private void AddNewDoctor(object sender, RoutedEventArgs e)
        {
            DoctorRegistration dr = new DoctorRegistration(this);
            dr.ShowDialog();
        }

        private void ShowDoctor(object sender, RoutedEventArgs e)
        {
            if ((DoctorDTO)dataGridDoctors.SelectedItem != null)
            {
                DoctorDTO doctor = (DoctorDTO)dataGridDoctors.SelectedItem;
                DoctorView dv = new DoctorView(doctor);
                dv.Show();
            }
            else
                MessageBox.Show("Izaberite doktora!");
        }

        private void UpdateDoctor(object sender, RoutedEventArgs e)
        {
            if ((DoctorDTO)dataGridDoctors.SelectedItem != null)
            {
                DoctorDTO doctor = (DoctorDTO)dataGridDoctors.SelectedItem;
                UpdateDoctorView upv = new UpdateDoctorView(doctor, this);
                upv.ShowDialog();
            }
            else
                MessageBox.Show("Izaberite doktora!");
        }

        //private void DeleteDoctor(object sender, RoutedEventArgs e)
        //{
        //    if ((DoctorDTO)dataGridDoctors.SelectedItem != null)
        //    {
        //        DoctorDTO doctor = (DoctorDTO)dataGridDoctors.SelectedItem;
        //        Doctors.Remove(doctor);
        //        DoctorController.Instance.DeleteDoctor(doctor);
        //    }
        //}

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            ICollectionView view = new CollectionViewSource { Source = Doctors }.View;
            view.Filter = delegate (object item)
            {
                DoctorDTO doctor = item as DoctorDTO;
                return CheckIfDoctorMeetsSearchCriteria(doctor);
            };
            dataGridDoctors.ItemsSource = view;
        }

        private bool CheckIfDoctorMeetsSearchCriteria(DoctorDTO doctor)
        {
            string[] search = txtSearch.Text.ToLower().Split(" ");
            if (txtSearch.Text.Equals("Pretraži...")||txtSearch.Text.Equals("Search..."))
                search[0] = string.Empty;

            if (search.Length <= 1)
                return doctor.Name.ToLower().Contains(search[0]) | doctor.Surname.ToLower().Contains(search[0]) | 
                    doctor.BirthDate.ToString("dd.MM.yyyy.").Contains(search[0]) | doctor.Specialty.ToLower().Contains(search[0]);
            else
            {
                bool firstName = true;
                bool lastName = true;
                bool birthDate = true;
                bool specialty = true;
                int cnt = 0;

                for (int i = 0; i < search.Length; i++)
                {
                    if (doctor.Name.ToLower().Contains(search[i]) && firstName)
                    {
                        firstName = false;
                        cnt++;
                        continue;
                    }
                    if (doctor.Surname.ToLower().Contains(search[i]) && lastName)
                    {
                        lastName = false;
                        cnt++;
                        continue;
                    }
                    if (doctor.BirthDate.ToString("dd.MM.yyyy.").Contains(search[i]) && birthDate)
                    {
                        birthDate = false;
                        cnt++;
                        continue;
                    }
                    if (doctor.Specialty.ToLower().Contains(search[i]) && specialty)
                    {
                        specialty = false;
                        cnt++;
                        continue;
                    }
                }

                return cnt == search.Length;
            }

        }

        private void SearchBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtSearch.Text.Equals("Pretraži...")||txtSearch.Text.Equals("Search..."))
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
    }
}
