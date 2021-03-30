using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Model;

namespace Hospital_IS.View
{
    /// <summary>
    /// Interaction logic for DoctorSetAppointment.xaml
    /// </summary>
    public partial class DoctorSetAppointment : Window
    {
        public ObservableCollection<DoctorAppointment> AvailableAppoitments { get; set; }
        public ObservableCollection<Doctor> TempDoctors { get; set; }
        public DoctorAppointment Appointment { get; set; }
        public ObservableCollection<Room> TempRooms { get; set; }
        public WorkDay day;
        public Doctor doctor;
        public DateTime date;
        public Patient patient;

        public DoctorSetAppointment(DoctorAppointment appointment, bool changeApp = false)
        {
            InitializeComponent();
            this.DataContext = this;
            AvailableAppoitments = new ObservableCollection<DoctorAppointment>();
            TempDoctors = new ObservableCollection<Doctor>();
            TempRooms = new ObservableCollection<Room>();


            Room tempRoom = new Room(RoomType.ConsultingRoom, true, true, 1, 5);
            day = new WorkDay(Day.Monday, new DateTime(2021, 3, 29, 8, 0, 0), new DateTime(2021, 3, 29, 16, 0, 0));
            List<WorkDay> days = new List<WorkDay>();
            days.Add(day);
            Doctor tempDoctor = new Doctor(1, "Doktor", "Doca", new DateTime(1976, 5, 5), "doca@gmail.com", null, "Laze Kostica 55,Novi Sad", 70000, new DateTime(2018, 5, 5), days, new Specialty("dermatolog"), tempRoom);
            Doctor tempDoctor1 = new Doctor(1, "Doktor2", "Doca2", new DateTime(1977, 5, 5), "doca@gmail.com", null, "Laze Kostica 55,Novi Sad", 70000, new DateTime(2018, 5, 5), days, new Specialty("dermatolog"), tempRoom);
            TempDoctors.Add(tempDoctor);
            TempDoctors.Add(tempDoctor1);
            TempDoctors.Add(DoctorHomePage.Instance.Doctor);

            TempRooms.Add(new Room(RoomType.ConsultingRoom, true, true, 1, 2));
            TempRooms.Add(new Room(RoomType.ConsultingRoom, true, true, 1, 3));
            TempRooms.Add(new Room(RoomType.ConsultingRoom, true, true, 1, 4));
            TempRooms.Add(tempRoom);
            TempRooms.Add(new Room(RoomType.OperationRoom, true, true, 1, 6));
            TempRooms.Add(new Room(RoomType.OperationRoom, true, true, 1, 7));
            TempRooms.Add(new Room(RoomType.OperationRoom, true, true, 1, 8));
            TempRooms.Add(DoctorHomePage.Instance.Doctor.PrimaryRoom);

            AvailableAppoitments.Add(new DoctorAppointment(new DateTime(date.Year, date.Month, date.Day, 8, 30, 0), AppointmetType.CheckUp, false, TempRooms[0], doctor, HomePatient.Instance.patient));
            AvailableAppoitments.Add(new DoctorAppointment(new DateTime(date.Year, date.Month, date.Day, 9, 0, 0), AppointmetType.CheckUp, false, TempRooms[1], doctor, HomePatient.Instance.patient));
            AvailableAppoitments.Add(new DoctorAppointment(new DateTime(date.Year, date.Month, date.Day, 9, 30, 0), AppointmetType.CheckUp, false, TempRooms[2], doctor, HomePatient.Instance.patient));
            AvailableAppoitments.Add(new DoctorAppointment(new DateTime(date.Year, date.Month, date.Day, 10, 0, 0), AppointmetType.CheckUp, false, TempRooms[3], doctor, HomePatient.Instance.patient));
            AvailableAppoitments.Add(new DoctorAppointment(new DateTime(date.Year, date.Month, date.Day, 10, 30, 0), AppointmetType.CheckUp, false, TempRooms[4], doctor, HomePatient.Instance.patient));
            AvailableAppoitments.Add(new DoctorAppointment(new DateTime(date.Year, date.Month, date.Day, 11, 0, 0), AppointmetType.CheckUp, false, TempRooms[5], doctor, HomePatient.Instance.patient));

            rooms.SelectedItem = appointment.Doctor.PrimaryRoom;
            Doctors.SelectedItem = appointment.Doctor;
            Calendar.SelectedDate = appointment.DateAndTime.Date;

            Appointment = appointment;

            if (changeApp)
            {
                change.Visibility = Visibility.Visible;
                reserve.Visibility = Visibility.Collapsed;
            }
        }

        private void showAvailableApp(object sender, RoutedEventArgs e)
        {
            Room tempRoom = (Room)rooms.SelectedItem;
            doctor = (Doctor)Doctors.SelectedItem;
            date = Calendar.SelectedDate.Value;

            //foreach (DoctorAppointment r in AvailableAppoitments)
            //{
            //   if(tempRoom.RoomId == r.Room.RoomId && tempRoom.RoomFloor == r.Room.RoomFloor && tempRoom.Type.Equals(r.Room.Type))
            //  AvailableAppoitments.Add(r);
            //}
            AvailableAppoitments.Clear();
            AvailableAppoitments.Add(new DoctorAppointment(new DateTime(date.Year, date.Month, date.Day, 8, 0, 0), AppointmetType.CheckUp, false, tempRoom, doctor, HomePatient.Instance.patient));
            AvailableAppoitments.Add(new DoctorAppointment(new DateTime(date.Year, date.Month, date.Day, 8, 30, 0), AppointmetType.CheckUp, false, tempRoom, doctor, HomePatient.Instance.patient));
            AvailableAppoitments.Add(new DoctorAppointment(new DateTime(date.Year, date.Month, date.Day, 9, 0, 0), AppointmetType.CheckUp, false, tempRoom, doctor, HomePatient.Instance.patient));
            AvailableAppoitments.Add(new DoctorAppointment(new DateTime(date.Year, date.Month, date.Day, 9, 30, 0), AppointmetType.CheckUp, false, tempRoom, doctor, HomePatient.Instance.patient));
            AvailableAppoitments.Add(new DoctorAppointment(new DateTime(date.Year, date.Month, date.Day, 10, 0, 0), AppointmetType.CheckUp, false, tempRoom, doctor, HomePatient.Instance.patient));
            AvailableAppoitments.Add(new DoctorAppointment(new DateTime(date.Year, date.Month, date.Day, 10, 30, 0), AppointmetType.CheckUp, false, tempRoom, doctor, HomePatient.Instance.patient));
            AvailableAppoitments.Add(new DoctorAppointment(new DateTime(date.Year, date.Month, date.Day, 11, 0, 0), AppointmetType.CheckUp, false, tempRoom, doctor, HomePatient.Instance.patient));
        }

        private void reserveAppointment(object sender, RoutedEventArgs e)
        {
            DoctorAppointment docApp = (DoctorAppointment)listOfAppointments.SelectedItem;
            DoctorHomePage.Instance.Doctor.AddDoctorAppointment(docApp);
            Appointment.Patient.AddDoctorAppointment(docApp);
            AvailableAppoitments.Remove(docApp);
            this.Close();
        }

        private void changeAppointmentButton(object sender, RoutedEventArgs e)
        {
            DoctorAppointment docApp = (DoctorAppointment)listOfAppointments.SelectedItem;
            DoctorHomePage.Instance.Doctor.RemoveDoctorAppointment(Appointment);
            DoctorHomePage.Instance.Doctor.AddDoctorAppointment(docApp);
            Appointment.Patient.AddDoctorAppointment(docApp);
            Appointment.Patient.RemoveDoctorAppointment(Appointment);
            AvailableAppoitments.Remove(docApp);
            this.Close();
        }

        public void ExitBtnClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public void MinimizeBtnClick(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        public void MaximizeBtnClick(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Maximized;
        }
    }
}
