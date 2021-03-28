using Model;
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

namespace Hospital_IS.View
{
    /// <summary>
    /// Interaction logic for AppointmentPatient.xaml
    /// </summary>
    public partial class AppointmentPatient : Window
    {

        public ObservableCollection<DoctorAppointment> AvailableAppoitments { get; set; }
        public ObservableCollection<Doctor> TempDoctors { get; set; }
        public WorkDay day;
        public Doctor doctor;
        public DateTime date;
        public Patient patient;


        public AppointmentPatient()
        {
            InitializeComponent();
            this.DataContext = this;
            AvailableAppoitments = new ObservableCollection<DoctorAppointment>();
            TempDoctors = new ObservableCollection<Doctor>();
            

            day = new WorkDay("Monday", new DateTime(2021, 3, 29, 8, 0, 0), new DateTime(2021, 3, 29, 16, 0, 0));
            List<WorkDay> days = new List<WorkDay>();
            days.Add(day);
            Doctor tempDoctor = new Doctor(1, "Doktor", "Doca", new DateTime(1976, 5, 5), "doca@gmail.com", null, "Laze Kostica 55,Novi Sad", 70000, new DateTime(2018, 5, 5), days);
            Doctor tempDoctor1 = new Doctor(1, "Doktor2", "Doca2", new DateTime(1977, 5, 5), "doca@gmail.com", null, "Laze Kostica 55,Novi Sad", 70000, new DateTime(2018, 5, 5), days);
            TempDoctors.Add(tempDoctor);
            TempDoctors.Add(tempDoctor1);

        }

        private void home(object sender, RoutedEventArgs e)
        {
            HomePatient home = new HomePatient();
            home.Show();
        }

        private void allApp(object sender, RoutedEventArgs e)
        {
            AllAppointments all = new AllAppointments();
            all.Show();
        }

        private void showDoc(object sender, RoutedEventArgs e)
        {
            DocumentationPatient doc = new DocumentationPatient();
            doc.Show();
        }

        private void showAvailableApp(object sender, RoutedEventArgs e)
        {
            Room tempRoom = new Room(RoomType.ConsultingRoom,true,true,1, 5);
            doctor = (Doctor)Doctors.SelectedItem;
            date = Calendar.SelectedDate.Value;
           
            AvailableAppoitments.Clear();
            AvailableAppoitments.Add(new DoctorAppointment(date,30,AppointmetType.CheckUp,false,tempRoom));
            AvailableAppoitments.Add(new DoctorAppointment(date, 30, AppointmetType.CheckUp, false, tempRoom));
            AvailableAppoitments.Add(new DoctorAppointment(date, 30, AppointmetType.CheckUp, false, tempRoom));
            AvailableAppoitments.Add(new DoctorAppointment(date, 30, AppointmetType.CheckUp, false, tempRoom));
            AvailableAppoitments.Add(new DoctorAppointment(date, 30, AppointmetType.CheckUp, false, tempRoom));
        }

     
    }
}
