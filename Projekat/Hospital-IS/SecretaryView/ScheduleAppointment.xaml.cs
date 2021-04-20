using Hospital_IS.Storages;
using Model;
using Storages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;


namespace Hospital_IS.SecretaryView
{
    /// <summary>
    /// Interaction logic for ScheduleAppointment.xaml
    /// </summary>
    public partial class ScheduleAppointment : Window
    {
        public DoctorAppointment DocAppointment { get; set; } = new DoctorAppointment();
        private UCAppointmentsView uca;
        public ObservableCollection<string> PatientNames { get; set; } = new ObservableCollection<string>();
        public ObservableCollection<string> DoctorNames { get; set; } = new ObservableCollection<string>();
        public ObservableCollection<int> RoomNumbers { get; set; } = new ObservableCollection<int>();

        public ObservableCollection<Patient> Patients { get; set; } = new ObservableCollection<Patient>();
        public ObservableCollection<Doctor> Doctors { get; set; } = new ObservableCollection<Doctor>();

        public ScheduleAppointment(UCAppointmentsView uca)
        {
            InitializeComponent();
            this.uca = uca;

            PatientFileStorage pfs = new PatientFileStorage();
            List<Patient> patients = pfs.GetAll();
            Patients = new ObservableCollection<Patient>(patients);
            for (int i = 0; i < patients.Count; i++)
                PatientNames.Add(patients[i].Name + " " + patients[i].Surname);

            FSDoctor fsd = new FSDoctor();
            Doctors = fsd.GetAll();
            for (int i = 0; i < Doctors.Count; i++)
                DoctorNames.Add(Doctors[i].Name + " " + Doctors[i].Surname);

            RoomStorage rs = new RoomStorage();
            ObservableCollection<Room> Rooms = rs.GetAll();
            for (int i = 0; i < Rooms.Count; i++)
                RoomNumbers.Add(Rooms[i].RoomNumber);


            this.DataContext = this;
        }

        private void NewAppointment(object sender, RoutedEventArgs e)
        {
            DocAppointment.Patient = Patients[cbPatient.SelectedIndex];
            DocAppointment.Doctor = Doctors[cbDoctor.SelectedIndex];
            if (cbRoom.IsEnabled)
                DocAppointment.Room = RoomNumbers[cbRoom.SelectedIndex];
            else
                DocAppointment.Room = DocAppointment.Doctor.PrimaryRoom;

            if (cbAppType.SelectedIndex == 0)
                DocAppointment.Type = AppointmetType.CheckUp;
            else if (cbAppType.SelectedIndex == 1)
                DocAppointment.Type = AppointmetType.Operation;

            try
            {
                DateTime appDate = DateTime.ParseExact(txtAppDate.Text, "dd.MM.yyyy.", System.Globalization.CultureInfo.InvariantCulture);
                DocAppointment.DateAndTime = appDate;

                DateTime appStart = new DateTime();

                if (cbAppType.SelectedIndex == 0)
                {
                    appStart = DateTime.ParseExact(txtStartOfApp.Text, "HH:mm", System.Globalization.CultureInfo.InvariantCulture);
                    DocAppointment.AppointmentStart = appStart;

                    DocAppointment.AppointmentEnd = appStart.AddMinutes(30);
                }
                else if (cbAppType.SelectedIndex == 1)
                {
                    appStart = DateTime.ParseExact(txtStartOfApp.Text, "HH:mm", System.Globalization.CultureInfo.InvariantCulture);
                    DocAppointment.AppointmentStart = appStart;

                    DateTime appEnd = DateTime.ParseExact(txtEndOfApp.Text, "HH:mm", System.Globalization.CultureInfo.InvariantCulture);
                    DocAppointment.AppointmentEnd = appEnd;
                }

                DocAppointment.DateAndTime = appDate.Date.Add(appStart.TimeOfDay);
            }
            catch (Exception ex)
            {
            }

            DocAppointment.NameSurnamePatient = cbPatient.SelectedValue.ToString();
            DocAppointment.DoctorFullName = cbDoctor.SelectedValue.ToString();

            uca.Appointments.Add(DocAppointment);

            AppointmentFileStorage afs = new AppointmentFileStorage();
            afs.SaveAppointment(uca.Appointments);

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
                cbRoom.IsEnabled = false;
            }
            else 
            {
                txtEndOfApp.IsEnabled = true;
                cbRoom.IsEnabled = true;
            }
        }
    }
}
