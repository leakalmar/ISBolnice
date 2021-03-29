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
    /// Interaction logic for HomePatient.xaml
    /// </summary>
    public partial class HomePatient : Window
    {
        private static HomePatient instance = null;

        public static HomePatient Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new HomePatient();
                }
                return instance;
            }
        }
        public Patient patient { get; set; }
        public ObservableCollection<DoctorAppointment> doctorAppointment { get; set; }
        public DoctorAppointment changedApp;
        public HomePatient()
        {
            InitializeComponent();

            patient = new Patient(1, "Marko", "Petrovic", new DateTime(1999, 5, 12), "Strazilovska 25,Novi Sad", "mare_pera@gmail.com", null, new DateTime(2020, 5, 12), "SAM SVOJ GAZDA", null);
            doctorAppointment = new ObservableCollection<DoctorAppointment>();
            this.DataContext = this;
            PersonalData.DataContext = patient;
            
            /*          WorkDay day = new WorkDay("Monday", new DateTime(2021, 3, 29, 8, 0, 0), new DateTime(2021, 3, 29, 16, 0, 0));
                      List<WorkDay> days = new List<WorkDay>();
                      days.Add(day);
                      Doctor tempDoctor = new Doctor(1, "Doktor", "Doca", new DateTime(1976, 5, 5), "doca@gmail.com", null, "Laze Kostica 55,Novi Sad", 70000, new DateTime(2018, 5, 5), days, new Specialty("dermatolog"));
                      p.DoctorAppointment.Add(new DoctorAppointment(new DateTime(2021,3,30,8,0,0), AppointmetType.CheckUp, false, new Room(RoomType.ConsultingRoom, true, true, 1, 5),tempDoctor));*/
            doctorAppointment = patient.DoctorAppointment;
        }

        private void reserveApp(object sender, RoutedEventArgs e)
        {
            
            AppointmentPatient ap = new AppointmentPatient();
            ap.Show();
            this.Hide();
        }

        private void allApp(object sender, RoutedEventArgs e)
        {

            AllAppointments ap = new AllAppointments();
            ap.Show();
            this.Hide();
        }

        private void showDoc(object sender, RoutedEventArgs e)
        {
            DocumentationPatient doc = new DocumentationPatient();
            doc.Show();
            this.Hide();
        }

        private void deleteAppointment(object sender, RoutedEventArgs e)
        {
            DoctorAppointment doctorApp = (DoctorAppointment)dataGridAppointment.SelectedItem;
            patient.DoctorAppointment.Remove(doctorApp);
        }

        private void changeAppointment(object sender, RoutedEventArgs e)
        {
            changedApp = (DoctorAppointment)dataGridAppointment.SelectedItem;
            AppointmentPatient ap = new AppointmentPatient();
            ap.Show();
            ap.changeAppointment(changedApp);
        }
    }
}
