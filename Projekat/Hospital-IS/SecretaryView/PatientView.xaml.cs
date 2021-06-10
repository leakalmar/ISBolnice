using Controllers;
using DTOs;
using Enums;
using Hospital_IS.Adapter;
using Hospital_IS.Controllers;
using Hospital_IS.DTOs.SecretaryDTOs;
using Hospital_IS.SecretaryView;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Threading;

namespace Hospital_IS
{
    /// <summary>
    /// Interaction logic for PatientView.xaml
    /// </summary>
    public partial class PatientView : Window
    {
        public PatientDTO Patient { get; set; }
        public ObservableCollection<DoctorAppointmentDTO> Appointments { get; set; }
        public IDoctorAppointmentTarget doctorAppointmentTarget = SecretaryMainWindow.Instance.target;
        public PatientView(PatientDTO patient)
        {
            InitializeComponent();
            Patient = patient;

            RefreshGrid();

            this.DataContext = this;
            if (patient.Education.Equals(EducationCategory.NA))
                eduTxt.Text = " ";
            else if (patient.Education.Equals(EducationCategory.GradeSchool))
                eduTxt.Text = "Osnovna škola";
            else if (patient.Education.Equals(EducationCategory.HighSchool))
                eduTxt.Text = "Srednja škola";
            else if (patient.Education.Equals(EducationCategory.College))
                eduTxt.Text = "Fakultet";
        }

        private void Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ShowAppointment(object sender, RoutedEventArgs e)
        {
            if ((DoctorAppointmentDTO)dataGridAppointments.SelectedItem != null)
            {
                AppointmentView av = new AppointmentView((DoctorAppointmentDTO)dataGridAppointments.SelectedItem);
                av.Show();
            }
            else
                MessageBox.Show("Izaberite termin!");
        }

        private void ScheduleNewAppointment(object sender, RoutedEventArgs e)
        {
            ScheduleAppointment sa = new ScheduleAppointment(SecretaryMainWindow.Instance.uca, Patient, this);
            sa.ShowDialog();
        }

        public void RefreshGrid()
        {
            if (Appointments != null)
                Appointments.Clear();

            DoctorAppointmentController.Instance.ReloadDoctorAppointments();
            Appointments = new ObservableCollection<DoctorAppointmentDTO>(doctorAppointmentTarget.GetDoctorAppointmentsByPatientId(Patient.Id));

            dataGridAppointments.ItemsSource = Appointments;
        }
    }
}
