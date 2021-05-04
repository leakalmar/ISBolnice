using Controllers;
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
        public DoctorAppointment DocAppointment { get; set; } = new DoctorAppointment();
        public ObservableCollection<Patient> Patients { get; set; } = new ObservableCollection<Patient>();
        public ObservableCollection<Doctor> Doctors { get; set; } = new ObservableCollection<Doctor>();
        public ObservableCollection<Room> Rooms { get; set; } = new ObservableCollection<Room>();

        public UCAppointmentsView uca;

        public ScheduleAppointment(UCAppointmentsView uca)
        {
            InitializeComponent();
            this.uca = uca;

            Patients = new ObservableCollection<Patient>(PatientController.Instance.GetAllRegisteredPatients());
            Doctors = new ObservableCollection<Doctor>(DoctorController.Instance.GetAll());

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

            DoctorAppointmentController.Instance.AddAppointment(DocAppointment);

            uca.RefreshGrid();

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
                Rooms = new ObservableCollection<Room>(RoomController.Instance.GetRoomByType(RoomType.ConsultingRoom));
                cbRoom.ItemsSource = Rooms;
            }
            else
            {
                txtEndOfApp.IsEnabled = true;
                Rooms = new ObservableCollection<Room>(RoomController.Instance.GetRoomByType(RoomType.OperationRoom));
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

            EnableAppointmentConfirmation(DoctorAppointmentController.Instance.VerifyAppointment(DocAppointment));
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
            ScheduleEmergencyAppointment sea = new ScheduleEmergencyAppointment(this);
            sea.Show();
            this.Visibility = Visibility.Collapsed;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
