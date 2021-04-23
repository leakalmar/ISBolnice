using Hospital_IS.Storages;
using Model;
using Storages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows;
using System.Windows.Media;

namespace Hospital_IS.SecretaryView
{
    /// <summary>
    /// Interaction logic for UpdateAppointment.xaml
    /// </summary>
    public partial class UpdateAppointment : Window
    {
        public DoctorAppointment DocAppointment { get; set; }

        AppointmentFileStorage afs = new AppointmentFileStorage();
        ClassicAppointmentStorage cas = new ClassicAppointmentStorage();

        UCAppointmentsView uca;

        public ObservableCollection<Doctor> Doctors { get; set; } = new ObservableCollection<Doctor>();
        public ObservableCollection<Room> Rooms { get; set; } = new ObservableCollection<Room>();

        public DoctorAppointment OldApp { get; set; } = new DoctorAppointment();  //zameniti sa id-em posle

        public UpdateAppointment(DoctorAppointment appointment, UCAppointmentsView uca)
        {
            InitializeComponent();
            DocAppointment = appointment;
            this.DataContext = this;
            this.uca = uca;

            if (DocAppointment.Type == AppointmetType.CheckUp)
            {
                txtAppType.Text = "Pregled";
                txtEndOfApp.IsEnabled = false;
            }
            else if (DocAppointment.Type == AppointmetType.Operation)
            {
                txtAppType.Text = "Operacija";
            }

            txtRoom.Text = DocAppointment.Room.ToString();

            txtAppDate.Text = DocAppointment.AppointmentStart.ToString("dd.MM.yyyy.");

            OldApp.AppointmentStart = DocAppointment.AppointmentStart;

            FSDoctor fsd = new FSDoctor();
            Doctors = new ObservableCollection<Doctor>(fsd.GetAll());

            RoomStorage rs = new RoomStorage();
            Rooms = new ObservableCollection<Room>(rs.GetAll());

        }

        private void ChangeAppointment(object sender, RoutedEventArgs e)
        {
            try
            {
                DateTime appDate = DateTime.ParseExact(txtAppDate.Text, "dd.MM.yyyy.", CultureInfo.InvariantCulture);
                DocAppointment.AppointmentStart = appDate;

                DateTime appStart = DateTime.ParseExact(txtStartOfApp.Text, "HH:mm", CultureInfo.InvariantCulture);
                DocAppointment.AppointmentStart = appDate.Date.Add(appStart.TimeOfDay);

                if (DocAppointment.Type == AppointmetType.CheckUp)
                {
                    DocAppointment.AppointmentEnd = DocAppointment.AppointmentStart.AddMinutes(30);
                }
                else if (DocAppointment.Type == AppointmetType.Operation)
                {
                    DateTime appEnd = DateTime.ParseExact(txtEndOfApp.Text, "HH:mm", CultureInfo.InvariantCulture);
                    DocAppointment.AppointmentEnd = appDate.Date.Add(appEnd.TimeOfDay);
                }

            }
            catch (Exception ex)
            {
            }

            /*uca.dataGridAppointments.ItemsSource = null;          --Ubaciti id u Appointment
            uca.dataGridAppointments.ItemsSource = uca.Appointments;
            afs.UpdateAppointment(DocAppointment);*/

            sendNotification(OldApp, DocAppointment);

            Hospital.Instance.RemoveAppointment(OldApp);
            Hospital.Instance.AddAppointment(DocAppointment);
            afs.SaveAppointment(Hospital.Instance.allAppointments);
            uca.RefreshGrid();
            this.Close();
        }

        private void sendNotification(DoctorAppointment oldApp, DoctorAppointment appointment)
        {
            string title = "Pomeren pregled";

            string text = "Pregled koji ste imali " + oldApp.AppointmentStart.ToString("dd.MM.yyyy.") + " u "
                + oldApp.AppointmentStart.ToString("HH:mm") + "h je pomeren za "
                + appointment.AppointmentStart.ToString("dd.MM.yyyy.") + " u " + appointment.AppointmentStart.ToString("HH:mm");

            Notification notification = new Notification(title, text, DateTime.Now);

            NotificationFileStorage nfs = new NotificationFileStorage();
            nfs.SaveNotification(notification);

            PatientFileStorage pfs = new PatientFileStorage();
            Patient patient = pfs.GetPatientById(appointment.Patient.Id);
            patient.addNotification(notification.Id);
            pfs.UpdatePatient(patient);

            FSDoctor fsd = new FSDoctor();
            Doctor doctor = fsd.GetByEmail(appointment.Doctor.Email);
            doctor.addNotification(notification.Id);
            fsd.UpdateDoctor(doctor);
        }

        private void Close(object sender, RoutedEventArgs e)
        {
            uca.RefreshGrid();
            this.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            uca.RefreshGrid();
        }


        private void txtEndOfApp_LostFocus(object sender, RoutedEventArgs e)
        {
            List<DoctorAppointment> appsByRoom = afs.GetAllByRoom(DocAppointment.Room);
            List<DoctorAppointment> appsByDoctor = afs.GetAllByDoctor(DocAppointment.Doctor.Id);

            List<DoctorAppointment> ClassicAppsByRoom = cas.GetAllDocAppointmentsById(DocAppointment.Room);
            appsByRoom = ConcatCollections(appsByRoom, ClassicAppsByRoom);

            confirmAppointmentDate(checkAppointment(appsByRoom, appsByDoctor));
        }

        private void txtStartOfApp_LostFocus(object sender, RoutedEventArgs e)
        {
            if (DocAppointment.Type == AppointmetType.CheckUp && !string.IsNullOrEmpty(txtStartOfApp.Text))
            {
                DateTime appStart = DateTime.ParseExact(txtStartOfApp.Text, "HH:mm", CultureInfo.InvariantCulture);
                DateTime appEnd = appStart.AddMinutes(30);
                txtEndOfApp.Text = appEnd.ToString("t", DateTimeFormatInfo.InvariantInfo);


                List<DoctorAppointment> appsByDoctor = afs.GetAllByDoctor(DocAppointment.Doctor.Id);
                List<DoctorAppointment> appsByRoom = afs.GetAllByRoom(DocAppointment.Room);

                List<DoctorAppointment> ClassicAppsByRoom = cas.GetAllDocAppointmentsById(DocAppointment.Room);
                appsByRoom = ConcatCollections(appsByRoom, ClassicAppsByRoom);

                confirmAppointmentDate(checkAppointment(appsByRoom, appsByDoctor));
            }
        }
        private List<DoctorAppointment> ConcatCollections(List<DoctorAppointment> apps1, List<DoctorAppointment> apps2)
        {
            foreach (DoctorAppointment appointment in apps2)
            {
                apps1.Add(appointment);
            }
            return apps1;
        }

        private void confirmAppointmentDate(bool isValid)
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

        private bool checkAppointment(List<DoctorAppointment> RoomAppointments, List<DoctorAppointment> DoctorAppointments)
        {
            DateTime start;
            DateTime end;
            DateTime date = DateTime.ParseExact(txtAppDate.Text, "dd.MM.yyyy.", CultureInfo.InvariantCulture);
            start = DateTime.ParseExact(txtStartOfApp.Text, "HH:mm", CultureInfo.InvariantCulture);
            start = date.Date.Add(start.TimeOfDay);

            end = DateTime.ParseExact(txtEndOfApp.Text, "HH:mm", CultureInfo.InvariantCulture);
            end = date.Date.Add(end.TimeOfDay);

            foreach (Appointment appointment in RoomAppointments)
            {

                bool between = IsBetweenDates(start, end, appointment);
                if (between || start < appointment.AppointmentStart && end > appointment.AppointmentEnd)
                {
                    return false;
                }

            }

            if (DoctorAppointments.Count == 0)
                return true;



            foreach (Appointment appointment in DoctorAppointments)
            {

                bool between = IsBetweenDates(start, end, appointment);
                if (between || (start <= appointment.AppointmentStart && end >= appointment.AppointmentEnd))
                {
                    return false;
                }

            }

            return true;

        }

        private static bool IsBetweenDates(DateTime start, DateTime end, Appointment appointment)
        {
            return (start >= appointment.AppointmentStart && start < appointment.AppointmentEnd) || (end > appointment.AppointmentStart && end <= appointment.AppointmentEnd);
        }

        private int findRoomNumber(int roomId)
        {
            foreach (Room room in Rooms)
            {
                if (room.RoomId == roomId)
                    return room.RoomNumber;
            }
            return 0;
        }
    }
}
