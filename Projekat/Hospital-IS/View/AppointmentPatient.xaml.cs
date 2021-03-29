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


            Room tempRoom = new Room(RoomType.ConsultingRoom, true, true, 1, 5);
            day = new WorkDay("Monday", new DateTime(2021, 3, 29, 8, 0, 0), new DateTime(2021, 3, 29, 16, 0, 0));
            List<WorkDay> days = new List<WorkDay>();
            days.Add(day);
            Doctor tempDoctor = new Doctor(1, "Doktor", "Doca", new DateTime(1976, 5, 5), "doca@gmail.com", null, "Laze Kostica 55,Novi Sad", 70000, new DateTime(2018, 5, 5), days, new Specialty("dermatolog"), tempRoom);
            Doctor tempDoctor1 = new Doctor(1, "Doktor2", "Doca2", new DateTime(1977, 5, 5), "doca@gmail.com", null, "Laze Kostica 55,Novi Sad", 70000, new DateTime(2018, 5, 5), days, new Specialty("dermatolog"), tempRoom);
            TempDoctors.Add(tempDoctor);
            TempDoctors.Add(tempDoctor1);
            
        }

        private void home(object sender, RoutedEventArgs e)
        {

            HomePatient.Instance.Show();
            this.Close();
            
        }

        private void allApp(object sender, RoutedEventArgs e)
        {
            AllAppointments all = new AllAppointments();
            all.Show();
            this.Close();
        }

        private void showDoc(object sender, RoutedEventArgs e)
        {
            DocumentationPatient doc = new DocumentationPatient();
            doc.Show();
            this.Close();
        }

        private void showAvailableApp(object sender, RoutedEventArgs e)
        {
            doctor = (Doctor)Doctors.SelectedItem;
            date = Calendar.SelectedDate.Value;
           
            AvailableAppoitments.Clear();
            AvailableAppoitments.Add(new DoctorAppointment(new DateTime(date.Year,date.Month,date.Day,8,0,0),AppointmetType.CheckUp,false,doctor.PrimaryRoom,doctor, HomePatient.Instance.patient));
            AvailableAppoitments.Add(new DoctorAppointment(new DateTime(date.Year, date.Month, date.Day, 8, 30, 0), AppointmetType.CheckUp, false, doctor.PrimaryRoom, doctor, HomePatient.Instance.patient));
            AvailableAppoitments.Add(new DoctorAppointment(new DateTime(date.Year, date.Month, date.Day, 9, 0, 0), AppointmetType.CheckUp, false, doctor.PrimaryRoom, doctor, HomePatient.Instance.patient));
            AvailableAppoitments.Add(new DoctorAppointment(new DateTime(date.Year, date.Month, date.Day, 9, 30, 0), AppointmetType.CheckUp, false, doctor.PrimaryRoom, doctor, HomePatient.Instance.patient));
            AvailableAppoitments.Add(new DoctorAppointment(new DateTime(date.Year, date.Month, date.Day, 10, 0, 0), AppointmetType.CheckUp, false, doctor.PrimaryRoom, doctor, HomePatient.Instance.patient));
            AvailableAppoitments.Add(new DoctorAppointment(new DateTime(date.Year, date.Month, date.Day, 10, 30, 0), AppointmetType.CheckUp, false, doctor.PrimaryRoom, doctor, HomePatient.Instance.patient));
            AvailableAppoitments.Add(new DoctorAppointment(new DateTime(date.Year, date.Month, date.Day, 11, 0, 0), AppointmetType.CheckUp, false, doctor.PrimaryRoom, doctor, HomePatient.Instance.patient));
        }

        private void reserveAppointment(object sender, RoutedEventArgs e)
        {
            DoctorAppointment docApp = (DoctorAppointment)listOfAppointments.SelectedItem;
            HomePatient.Instance.patient.AddDoctorAppointment(docApp);
            docApp.Reserved = true;
            AvailableAppoitments.Remove(docApp);
        }

        public void changeAppointment(DoctorAppointment docApp)
        {
            Room tempRoom = new Room(RoomType.ConsultingRoom, true, true, 1, 5);
            doctor = docApp.Doctor;
            Doctors.SelectedIndex = TempDoctors.IndexOf(doctor);
            date = docApp.DateAndTime;
            Calendar.SelectedDate = date;
            change.Visibility = Visibility.Visible;
            reserve.Visibility = Visibility.Collapsed;

            AvailableAppoitments.Clear();
            AvailableAppoitments.Add(new DoctorAppointment(new DateTime(date.Year, date.Month, date.Day, 8, 0, 0), AppointmetType.CheckUp, false, doctor.PrimaryRoom, doctor, HomePatient.Instance.patient));
            AvailableAppoitments.Add(new DoctorAppointment(new DateTime(date.Year, date.Month, date.Day, 8, 30, 0), AppointmetType.CheckUp, false, doctor.PrimaryRoom, doctor, HomePatient.Instance.patient));
            AvailableAppoitments.Add(new DoctorAppointment(new DateTime(date.Year, date.Month, date.Day, 9, 0, 0), AppointmetType.CheckUp, false, doctor.PrimaryRoom, doctor, HomePatient.Instance.patient));
            AvailableAppoitments.Add(new DoctorAppointment(new DateTime(date.Year, date.Month, date.Day, 9, 30, 0), AppointmetType.CheckUp, false, doctor.PrimaryRoom, doctor, HomePatient.Instance.patient));
            AvailableAppoitments.Add(new DoctorAppointment(new DateTime(date.Year, date.Month, date.Day, 10, 0, 0), AppointmetType.CheckUp, false, doctor.PrimaryRoom, doctor, HomePatient.Instance.patient));
            AvailableAppoitments.Add(new DoctorAppointment(new DateTime(date.Year, date.Month, date.Day, 10, 30, 0), AppointmetType.CheckUp, false, doctor.PrimaryRoom, doctor, HomePatient.Instance.patient));
            AvailableAppoitments.Add(new DoctorAppointment(new DateTime(date.Year, date.Month, date.Day, 11, 0, 0), AppointmetType.CheckUp, false, doctor.PrimaryRoom, doctor, HomePatient.Instance.patient));
        }

        private void changeAppointmentButton(object sender, RoutedEventArgs e)
        {
            DoctorAppointment docApp = (DoctorAppointment)listOfAppointments.SelectedItem;
            HomePatient.Instance.patient.AddDoctorAppointment(docApp);
            HomePatient.Instance.patient.RemoveDoctorAppointment(HomePatient.Instance.changedApp);
            docApp.Reserved = true;
            AvailableAppoitments.Remove(docApp);
        }
    }
}
