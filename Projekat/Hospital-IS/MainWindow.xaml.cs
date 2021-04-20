using Hospital_IS.Storages;
using Hospital_IS.View;
using Model;
using Storages;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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
        public static Patient PatientUser { get; set; }
        public static Doctor DoctortUser { get; set; }
        public static ObservableCollection<DoctorAppointment> doctorAppointments { get; set; }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void Login(object sender, RoutedEventArgs e)
        {
            List<Patient> patients = pfs.GetAll();
            Hospital.Instance.allAppointments = afs.GetAll();
            ObservableCollection<Doctor> doctors = dfs.GetAll();

            foreach (Patient p in patients)
            {
                if (email.Text == p.Email && password.Password.ToString() == p.Password)
                {
                    PatientUser = p;
                    HomePatient.Instance.DoctorAppointment = afs.GetAllByPatient(p.Id);
                    HomePatient.Instance.Show();
                    this.Close();
                }
            }

            foreach (Doctor d in doctors)
            {
                if (email.Text == d.Email && password.Password.ToString() == d.Password)
                {
                    DoctortUser = d;
                    DoctorHomePage.Instance.DoctorAppointment = afs.GetAllByDoctor(d.Id);
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
