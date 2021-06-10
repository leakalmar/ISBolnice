using Controllers;
using DTOs;
using Hospital_IS.Adapter;
using Hospital_IS.Controllers;
using Hospital_IS.DTOs;
using Hospital_IS.Enums;
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
        public IDoctorAppointmentTarget doctorAppointmentTarget = SecretaryMainWindow.Instance.target;

        public DoctorView(DoctorDTO doctor)
        {
            InitializeComponent();
            Doctor = doctor;
            Appointments = new ObservableCollection<DoctorAppointmentDTO>(doctorAppointmentTarget.GetPreviousAppointmentsForDoctor(Doctor.Id));
            RefreshGrid();
            this.DataContext = this;

            if (Doctor.WorkShift.Equals(WorkDayShift.FirstShift))
                txtWorkShift.Text = "Prva";
            else
                txtWorkShift.Text = "Druga";

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
            sa.ShowDialog();
        }

        public void RefreshGrid()
        {
            if (FutureAppointments != null)
                FutureAppointments.Clear();

            DoctorAppointmentController.Instance.ReloadDoctorAppointments();
            FutureAppointments = new ObservableCollection<DoctorAppointmentDTO>(doctorAppointmentTarget.GetFutureAppointmentsForDoctor(Doctor.Id));

            dataGridFutureAppointments.ItemsSource = FutureAppointments;
        }
    }
}
