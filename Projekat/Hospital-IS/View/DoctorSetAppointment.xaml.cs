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
using Storages;

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
        RoomStorage rs =  new RoomStorage();


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

            TempDoctors.Add(DoctorHomePage.Instance.Doctor);

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
