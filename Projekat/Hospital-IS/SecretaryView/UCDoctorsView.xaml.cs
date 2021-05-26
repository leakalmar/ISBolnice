using Controllers;
using Hospital_IS.Controllers;
using Hospital_IS.DTOs;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

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
            dr.Show();
        }

        private void ShowDoctor(object sender, RoutedEventArgs e)
        {
            if ((DoctorDTO)dataGridDoctors.SelectedItem != null)
            {
                DoctorDTO doctor = (DoctorDTO)dataGridDoctors.SelectedItem;
                DoctorView dv = new DoctorView(doctor);
                dv.Show();
            }
        }

        private void UpdateDoctor(object sender, RoutedEventArgs e)
        {
            if ((DoctorDTO)dataGridDoctors.SelectedItem != null)
            {
                DoctorDTO doctor = (DoctorDTO)dataGridDoctors.SelectedItem;
                UpdateDoctorView upv = new UpdateDoctorView(doctor, this);
                upv.Show();
            }
        }

        private void DeleteDoctor(object sender, RoutedEventArgs e)
        {
            if ((DoctorDTO)dataGridDoctors.SelectedItem != null)
            {
                DoctorDTO doctor = (DoctorDTO)dataGridDoctors.SelectedItem;
                Doctors.Remove(doctor);
                DoctorController.Instance.DeleteDoctor(doctor);
            }
        }
    }
}
