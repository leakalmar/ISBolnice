using Hospital_IS.Storages;
using Hospital_IS.View;
using Model;
using Storages;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using Hospital_IS.DoctorView;
using Controllers;
using Service;

namespace Hospital_IS
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        FSDoctor dfs = new FSDoctor();
        RoomStorage rfs = new RoomStorage();
        PatientFileStorage pfs = new PatientFileStorage();
        AppointmentFileStorage afs = new AppointmentFileStorage();
        public static Patient PatientUser { get; set; }

        //Dodala jer mi treba ista referenca svuda kako bi mogla da postavim selektovanje za combobox
        public static List<Doctor> Doctors { get; set; }
        public static List<Room> Rooms { get; set; }
        public static List<Specialty> Specialties { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            Doctors = DoctorController.Instance.GetAll();
            Rooms = RoomController.Instance.GetAllRooms();
            Specialties = SpecializationController.Instance.GetAll();
            UserService.Instance.GetAllUsersIDs();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }
        
        private void Login(object sender, RoutedEventArgs e)
        {
            List<Patient> patients = PatientController.Instance.GetAll();

            foreach (Patient p in patients)
            {
                if (email.Text == p.Email && password.Password.ToString() == p.Password)
                {
                    PatientUser = p;
                    //PatientMainWindowView patientView = new PatientMainWindowView();
                    //patientView.Show();
                    PatientMainWindowView.Instance.Show();
                    this.Close();
                }
            }

            foreach (Doctor doctor in Doctors)
            {
                if (email.Text == doctor.Email && password.Password.ToString() == doctor.Password)
                {
                    DoctorHomePage.Instance.ChangeDoctor(doctor);
                    DoctorHomePage.Instance.Show();
                    this.Close();
                }
            }

            if (email.Text == "manager@gmail.com" && password.Password.ToString() == "manager")
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
