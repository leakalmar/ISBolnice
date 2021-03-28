using Model;
using System;
using System.Collections.Generic;
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
        public Patient p;
        public List<DoctorAppointment> doctorAppointment { get; set; }
        public HomePatient()
        {
            p = new Patient(1, "Marko", "Petrovic", new DateTime(1999, 5, 12), "Strazilovska 25,Novi Sad", "alo@gmail.com", null, 69);
            InitializeComponent();
            doctorAppointment = new List<DoctorAppointment>();
            this.DataContext = this;
            Name.Content = p.Name;
            Surname.Content = p.Surname;
            Id.Content = p.Id;
            CharId.Content = p.ChartId;
            Birth.Content = p.BirthDate;
            Address.Content = p.Address;
            FileDate.Content = p.FileDate;
            Phone.Content = p.Phone;
            Email.Content = p.Email;
            p.DoctorAppointment.Add(new DoctorAppointment(new DateTime(2021,3,30,8,0,0), 30, AppointmetType.CheckUp, false, new Room(RoomType.ConsultingRoom, true, true, 1, 5)));
            doctorAppointment = p.DoctorAppointment;
        }

        private void reserveApp(object sender, RoutedEventArgs e)
        {
            AppointmentPatient ap = new AppointmentPatient();
            ap.Show();
        }

        private void allApp(object sender, RoutedEventArgs e)
        {

            AllAppointments ap = new AllAppointments();
            ap.Show();
        }

        private void showDoc(object sender, RoutedEventArgs e)
        {
            DocumentationPatient dp = new DocumentationPatient();
            dp.Show();
        }
    }
}
