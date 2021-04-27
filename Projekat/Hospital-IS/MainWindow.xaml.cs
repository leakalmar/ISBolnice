using Hospital_IS.Storages;
using Hospital_IS.View;
using Model;
using Storages;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Hospital_IS.DoctorView;
using Controllers;

namespace Hospital_IS
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        PatientFileStorage pfs = new PatientFileStorage();
        AppointmentFileStorage afs = new AppointmentFileStorage();
        FSDoctor dfs = new FSDoctor();
        RoomStorage rfs = new RoomStorage();
        public static Patient PatientUser { get; set; }
        public static Doctor DoctortUser { get; set; }

        //Dodala jer mi treba ista referenca svuda kako bi mogla da postavim selektovanje za combobox
        public static List<Doctor> Doctors { get; set; }
        public static List<Room> Rooms { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            Doctors = dfs.GetAll();
            Rooms = rfs.GetAll();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void Login(object sender, RoutedEventArgs e)
        {
            List<Patient> patients = pfs.GetAll();
            Hospital.Instance.allAppointments=afs.GetAll();

            foreach (Patient p in patients)
            {
                if (email.Text == p.Email && password.Password.ToString() == p.Password)
                {
                    PatientUser = p;
                    HomePatient.Instance.Show();
                    this.Close();
                }
            }

            foreach (Doctor d in Doctors)
            {
                if (email.Text == d.Email && password.Password.ToString() == d.Password)
                {
                    DoctortUser = d;
                    DoctorHomePage.Instance.DoctorAppointment = new ObservableCollection<DoctorAppointment>(DoctorAppointmentController.Instance.GetAllByDoctor(d.Id));
                    DoctorHomePage.Instance.HomePage = new UserControlHomePage();
                    DoctorHomePage.Instance.Appointments = new UCAppointments();
                    DoctorHomePage.Instance.Patients = new UCPatients();
                    DoctorHomePage.Instance.Home.Children.Add(DoctorHomePage.Instance.HomePage);
                    DoctorHomePage.Instance.Show();
                    this.Close();
                }
            }

            if (email.Text == "upravnik@gmail.com" && password.Password.ToString() == "upravnik")
            {
                Window1.Instance.Show();
                this.Close();
            }
            else if (email.Text == "sekretar@gmail.com" && password.Password.ToString() == "sekretar")
            {
                SecretaryMainWindow.Instance.Show();

                this.Close();
            }
        }

        private void email_GotFocus(object sender, RoutedEventArgs e)
        {
            if (email.Text.Equals("Email"))
                email.Text = string.Empty;
        }
        private void email_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(email.Text))
                email.Text = "Email";
        }

        private void password_GotFocus(object sender, RoutedEventArgs e)
        {
            if (password.Password.ToString().Equals("Password"))
                password.Password = string.Empty;
        }

    }
}
