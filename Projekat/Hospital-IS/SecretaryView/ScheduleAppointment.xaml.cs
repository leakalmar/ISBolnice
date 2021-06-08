using Controllers;
using DTOs;
using Enums;
using Hospital_IS.Controllers;
using Hospital_IS.DTOs;
using Hospital_IS.DTOs.SecretaryDTOs;
using Model;
using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows;
using System.Windows.Media;

namespace Hospital_IS.SecretaryView
{
    /// <summary>
    /// Interaction logic for ScheduleAppointment.xaml
    /// </summary>
    public partial class ScheduleAppointment : Window
    {
        public DoctorAppointmentDTO DocAppointment { get; set; } = new DoctorAppointmentDTO();
        public ObservableCollection<PatientDTO> Patients { get; set; } = new ObservableCollection<PatientDTO>();
        public ObservableCollection<DoctorDTO> Doctors { get; set; } = new ObservableCollection<DoctorDTO>();
        public ObservableCollection<RoomDTO> Rooms { get; set; } = new ObservableCollection<RoomDTO>();

        public UCAppointmentsView uca;

        public PatientDTO patient = null;
        public PatientView pv;
        public DoctorDTO doctor = null;
        public DoctorView dv;

        public ScheduleAppointment(UCAppointmentsView uca)
        {
            InitializeComponent();
            this.uca = uca;

            Patients = new ObservableCollection<PatientDTO>(SecretaryManagementController.Instance.GetAllRegisteredPatients());
            Doctors = new ObservableCollection<DoctorDTO>(SecretaryManagementController.Instance.GetAllDoctors());

            this.DataContext = this;
        }
        public ScheduleAppointment(UCAppointmentsView uca, PatientDTO patient, PatientView pv)
        {
            InitializeComponent();
            this.uca = uca;
            this.patient = patient;
            this.pv = pv;
            Patients = new ObservableCollection<PatientDTO>(SecretaryManagementController.Instance.GetAllRegisteredPatients());
            Doctors = new ObservableCollection<DoctorDTO>(SecretaryManagementController.Instance.GetAllDoctors());

            cbPatient.SelectedItem = patient;
            cbPatient.IsEnabled = false;

            this.DataContext = this;
        }

        public ScheduleAppointment(UCAppointmentsView uca, DoctorDTO doctor, DoctorView dv)
        {
            InitializeComponent();
            this.uca = uca;
            this.doctor = doctor;
            this.dv = dv;
            Patients = new ObservableCollection<PatientDTO>(SecretaryManagementController.Instance.GetAllRegisteredPatients());
            Doctors = new ObservableCollection<DoctorDTO>(SecretaryManagementController.Instance.GetAllDoctors());

            cbDoctor.SelectedItem = doctor;
            cbDoctor.IsEnabled = false;

            this.DataContext = this;
        }

        private void NewAppointment(object sender, RoutedEventArgs e)
        {
            //  pacijent
            DocAppointment.Patient = Patients[cbPatient.SelectedIndex];

            // tip pregleda
            if (cbAppType.SelectedIndex == 0)
            {
                DocAppointment.Type = AppointmentType.CheckUp;
                DocAppointment.AppTypeText = "Pregled";
            }
            else if (cbAppType.SelectedIndex == 1)
            {
                DocAppointment.Type = AppointmentType.Operation;
                DocAppointment.AppTypeText = "Operacija";
            }

            DocAppointment.Reserved = true;

            DoctorAppointmentManagementController.Instance.AddAppointment(DocAppointment);

            uca.RefreshGrid();

            if (patient != null)
                pv.RefreshGrid();
            if (doctor != null)
                dv.RefreshGrid();

            this.Close();
        }

        private void Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void cbAppType_LostFocus(object sender, RoutedEventArgs e)
        {
            if (cbAppType.SelectedIndex == 0)
            {
                txtEndOfApp.IsEnabled = false;
                Rooms = new ObservableCollection<RoomDTO>(DoctorAppointmentManagementController.Instance.GetRoomByType(RoomType.ConsultingRoom));
                cbRoom.ItemsSource = Rooms;
            }
            else
            {
                txtEndOfApp.IsEnabled = true;
                Rooms = new ObservableCollection<RoomDTO>(DoctorAppointmentManagementController.Instance.GetRoomByType(RoomType.OperationRoom));
                cbRoom.ItemsSource = Rooms;
            }
        }

        private void txtEndOfApp_LostFocus(object sender, RoutedEventArgs e)
        {
            VerifyAppointment();
        }

        private void txtStartOfApp_LostFocus(object sender, RoutedEventArgs e)
        {
            if (cbAppType.SelectedIndex == 0 && !string.IsNullOrEmpty(txtStartOfApp.Text))
            {
                DateTime appStart = DateTime.ParseExact(txtStartOfApp.Text, "HH:mm", CultureInfo.InvariantCulture);
                DateTime appEnd = appStart.AddMinutes(30);
                txtEndOfApp.Text = appEnd.ToString("t", DateTimeFormatInfo.InvariantInfo);

                VerifyAppointment();
            }
        }

        private void VerifyAppointment()
        {
            // doktor
            DocAppointment.Doctor = Doctors[cbDoctor.SelectedIndex];

            // soba
            DocAppointment.Room = Rooms[cbRoom.SelectedIndex].RoomId;

            //pacijent
            DocAppointment.Patient = Patients[cbPatient.SelectedIndex];

            // datum, vreme i trajanje pregleda
            try
            {
                DateTime appDate = DateTime.ParseExact(txtAppDate.Text, "dd.MM.yyyy.", CultureInfo.InvariantCulture);
                DocAppointment.AppointmentStart = appDate;

                DateTime appStart = DateTime.ParseExact(txtStartOfApp.Text, "HH:mm", CultureInfo.InvariantCulture);
                DocAppointment.AppointmentStart = appDate.Date.Add(appStart.TimeOfDay);

                if (cbAppType.SelectedIndex == 0)
                {
                    DocAppointment.AppointmentEnd = DocAppointment.AppointmentStart.AddMinutes(30);
                }
                else if (cbAppType.SelectedIndex == 1)
                {
                    DateTime appEnd = DateTime.ParseExact(txtEndOfApp.Text, "HH:mm", CultureInfo.InvariantCulture);
                    DocAppointment.AppointmentEnd = appDate.Date.Add(appEnd.TimeOfDay);
                }

            }
            catch (Exception ex)
            {
            }

            EnableAppointmentConfirmation(DoctorAppointmentManagementController.Instance.VerifyAppointment(DocAppointment));
        }

        private void EnableAppointmentConfirmation(bool isValid)
        {
            if (isValid)
            {
                btnConfirm.IsEnabled = true;
                txtStartOfApp.Background = new SolidColorBrush(Colors.White);
                txtEndOfApp.Background = new SolidColorBrush(Colors.White);
            }
            else
            {
                txtStartOfApp.Background = new SolidColorBrush(Colors.Red);
                txtEndOfApp.Background = new SolidColorBrush(Colors.Red);
                btnConfirm.IsEnabled = false;
            }
        }

        private void btnEmergency_Click(object sender, RoutedEventArgs e)
        {
            ScheduleEmergencyAppointment sea;
            if (patient == null)
            {
                sea = new ScheduleEmergencyAppointment(this);
            }
            else 
            {
                sea = new ScheduleEmergencyAppointment(this, patient);
            }
            sea.ShowDialog();
            this.Visibility = Visibility.Collapsed;
        }

        
    }
}
