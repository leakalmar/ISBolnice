using Model;
using Storages;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Model;
using Storages;
using Hospital_IS.DoctorView;

namespace Hospital_IS.DoctorView
{
    /// <summary>
    /// Interaction logic for DoctorSetAppointment.xaml
    /// </summary>
    public partial class DoctorSetAppointment : Window
    {
        public ObservableCollection<DoctorAppointment> AvailableAppoitments { get; set; }
        public ObservableCollection<Doctor> TempDoctors { get; set; }
        public DoctorAppointment Appointment { get; set; }
        RoomStorage rs = new RoomStorage();


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
            TempRooms = rs.GetAll();


            Room tempRoom = new Room(RoomType.ConsultingRoom, true, true, 1, 5);
            day = new WorkDay(Day.Monday, new DateTime(2021, 3, 29, 8, 0, 0), new DateTime(2021, 3, 29, 16, 0, 0));
            List<WorkDay> days = new List<WorkDay>();
            days.Add(day);
            Doctor tempDoctor = new Doctor(1, "Doktor", "Doca", new DateTime(1976, 5, 5), "doca@gmail.com", null, "Laze Kostica 55,Novi Sad", 70000, new DateTime(2018, 5, 5), days, new Specialty("dermatolog"), tempRoom.RoomId);
            Doctor tempDoctor1 = new Doctor(1, "Doktor2", "Doca2", new DateTime(1977, 5, 5), "doca@gmail.com", null, "Laze Kostica 55,Novi Sad", 70000, new DateTime(2018, 5, 5), days, new Specialty("dermatolog"), tempRoom.RoomId);
            TempDoctors.Add(tempDoctor);
            TempDoctors.Add(tempDoctor1);
            TempDoctors.Add(DoctorHomePage.Instance.GetDoctor());

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
            ObservableCollection<DoctorAppointment> appList = new ObservableCollection<DoctorAppointment>();
            DoctorAppointment app1 = new DoctorAppointment(new DateTime(date.Year, date.Month, date.Day, 8, 0, 0), AppointmetType.CheckUp, false, doctor.PrimaryRoom, doctor, Appointment.Patient);
            appList.Add(app1);
            DoctorAppointment app2 = new DoctorAppointment(new DateTime(date.Year, date.Month, date.Day, 8, 30, 0), AppointmetType.CheckUp, false, doctor.PrimaryRoom, doctor, Appointment.Patient);
            appList.Add(app2);
            DoctorAppointment app3 = new DoctorAppointment(new DateTime(date.Year, date.Month, date.Day, 9, 0, 0), AppointmetType.CheckUp, false, doctor.PrimaryRoom, doctor, Appointment.Patient);
            appList.Add(app3);
            DoctorAppointment app4 = new DoctorAppointment(new DateTime(date.Year, date.Month, date.Day, 9, 30, 0), AppointmetType.CheckUp, false, doctor.PrimaryRoom, doctor, Appointment.Patient);
            appList.Add(app4);
            DoctorAppointment app5 = new DoctorAppointment(new DateTime(date.Year, date.Month, date.Day, 10, 0, 0), AppointmetType.CheckUp, false, doctor.PrimaryRoom, doctor, Appointment.Patient);
            appList.Add(app5);
            DoctorAppointment app6 = new DoctorAppointment(new DateTime(date.Year, date.Month, date.Day, 10, 30, 0), AppointmetType.CheckUp, false, doctor.PrimaryRoom, doctor, Appointment.Patient);
            appList.Add(app6);
            DoctorAppointment app7 = new DoctorAppointment(new DateTime(date.Year, date.Month, date.Day, 11, 0, 0), AppointmetType.CheckUp, false, doctor.PrimaryRoom, doctor, Appointment.Patient);
            appList.Add(app7);

            AvailableAppoitments.Clear();
            bool flag = false;
            foreach (DoctorAppointment ap in appList)
            {
                foreach (DoctorAppointment hospital in Hospital.Instance.allAppointments)
                {
                    if (ap.DateAndTime == hospital.DateAndTime)
                        flag = true;
                }
                if (flag == false)
                {
                    AvailableAppoitments.Add(ap);
                }
                else
                {
                    flag = false;
                }
            }
        }

        private void reserveAppointment(object sender, RoutedEventArgs e)
        {
            DoctorAppointment docApp = (DoctorAppointment)listOfAppointments.SelectedItem;
            Hospital.Instance.AddAppointment(docApp);
            DoctorHomePage.Instance.DoctorAppointment.Add(docApp);
            Appointment.Reserved = true;
            AvailableAppoitments.Remove(docApp);
            MainWindow login = new MainWindow();
            AppointmentFileStorage afs = new AppointmentFileStorage();
            afs.SaveAppointment(Hospital.Instance.allAppointments);
            this.Close();
        }

        private void changeAppointmentButton(object sender, RoutedEventArgs e)
        {
            DoctorAppointment docApp = (DoctorAppointment)listOfAppointments.SelectedItem;

            Hospital.Instance.RemoveAppointment(Appointment);


            DoctorHomePage.Instance.DoctorAppointment.Remove(Appointment);
            Appointment.Reserved = false;
            AvailableAppoitments.Add(Appointment);

            Hospital.Instance.AddAppointment(docApp);
            DoctorHomePage.Instance.DoctorAppointment.Add(docApp);
            docApp.Reserved = true;
            AvailableAppoitments.Remove(docApp);

            MainWindow login = new MainWindow();
            AppointmentFileStorage afs = new AppointmentFileStorage();
            afs.SaveAppointment(Hospital.Instance.allAppointments);

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
