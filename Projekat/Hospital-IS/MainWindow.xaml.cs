using Controllers;
using Hospital_IS.Controllers;
using Hospital_IS.DoctorRole.DoctorView;
using Hospital_IS.DTOs;
using Hospital_IS.ManagerView1;
using Hospital_IS.SecretaryView;
using Hospital_IS.Storages;
using Hospital_IS.View;
using Model;
using Service;
using Storages;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace Hospital_IS
{
    public partial class MainWindow : Window
    {
        FSDoctor dfs = new FSDoctor();
        RoomStorage rfs = new RoomStorage();
        PatientFileStorage pfs = new PatientFileStorage();
        AppointmentFileStorage afs = new AppointmentFileStorage();
        public static Patient PatientUser { get; set; }

        public MainWindow()
        {
            InitializeComponent();
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

            foreach (DoctorDTO doctor in SecretaryManagementController.Instance.GetAllDoctors())
            {
                if (email.Text == doctor.Email && password.Password.ToString() == doctor.Password)
                {
                    new DoctorMainWindow(doctor).Show();
                    this.Close();
                    return;
                }
            }

            if (email.Text == "manager@gmail.com" && password.Password.ToString() == "manager")
            {
                ManagerMainView managerMainView = new ManagerMainView(this);
                managerMainView.Show();
           
            }
            else if (email.Text == "sekretar@gmail.com" && password.Password.ToString() == "sekretar")
            {
                SecretaryMainWindow.Instance.Show();

                this.Close();
            }
            else
            {
                message.Visibility = Visibility.Visible;
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
