using DTOs;
using Hospital_IS.Controllers;
using Hospital_IS.DTOs;
using System.Collections.ObjectModel;
using System.Windows;

namespace Hospital_IS.SecretaryView
{
    /// <summary>
    /// Interaction logic for DoctorView.xaml
    /// </summary>
    public partial class DoctorView : Window
    {
        public DoctorDTO Doctor { get; set; }
        public ObservableCollection<DoctorAppointmentDTO> Appointments { get; set; }
        public ObservableCollection<DoctorAppointmentDTO> FutureAppointments { get; set; }
        public DoctorView(DoctorDTO doctor)
        {
            InitializeComponent();
            Doctor = doctor;
            Appointments = new ObservableCollection<DoctorAppointmentDTO>(DoctorAppointmentManagementController.Instance.GetPreviousAppointmentsForDoctor(Doctor.Id));
            RefreshGrid();
            this.DataContext = this;
        }

        private void Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ShowAppointment(object sender, RoutedEventArgs e)
        {
            if (tab.SelectedIndex == 1)
            {
                if ((DoctorAppointmentDTO)dataGridAppointments.SelectedItem != null)
                {
                    AppointmentView av = new AppointmentView((DoctorAppointmentDTO)dataGridAppointments.SelectedItem);
                    av.Show();
                }
                else
                    MessageBox.Show("Izaberite termin!");
            }
            else
            {
                if ((DoctorAppointmentDTO)dataGridFutureAppointments.SelectedItem != null)
                {
                    AppointmentView av = new AppointmentView((DoctorAppointmentDTO)dataGridFutureAppointments.SelectedItem);
                    av.Show();
                }
                else
                    MessageBox.Show("Izaberite termin!");
            }
        }

        private void ScheduleNewAppointment(object sender, RoutedEventArgs e)
        {
            ScheduleAppointment sa = new ScheduleAppointment(SecretaryMainWindow.Instance.uca, Doctor, this);
            sa.Show();
        }

        public void RefreshGrid()
        {
            if (FutureAppointments != null)
                FutureAppointments.Clear();

            DoctorAppointmentManagementController.Instance.ReloadAppointments();
            FutureAppointments = new ObservableCollection<DoctorAppointmentDTO>(DoctorAppointmentManagementController.Instance.GetFutureAppointmentsForDoctor(Doctor.Id));

            dataGridFutureAppointments.ItemsSource = FutureAppointments;
        }
    }
}
