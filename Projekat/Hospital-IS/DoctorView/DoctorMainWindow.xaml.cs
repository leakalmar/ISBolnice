﻿using Model;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using Controllers;

namespace Hospital_IS.DoctorView
{
    public partial class DoctorMainWindow : Window
    {
        private static DoctorMainWindow instance = null;
        public static DoctorMainWindow Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DoctorMainWindow();
                }
                return instance;
            }
        }
        public Doctor Doctor { get; set; }
        private int Last { get; set; } = 0;
        public ObservableCollection<DoctorAppointment> DoctorAppointment { get; set; }


        public UCHomePage HomePage;
        public UCAppointments Appointments;
        public UCPatients Patients;
        public UCMedicines Medicines;
        public UCDoctorNotifications Notifications;
        public UCApproveMedicine ApproveMedicine;


        
        public DoctorMainWindow()
        {
            InitializeComponent();            
            start.IsChecked = true;
        }

        public void ChangeDoctor(Doctor doctor)
        {
            Doctor = doctor;
            DoctorAppointment = new ObservableCollection<DoctorAppointment>(DoctorAppointmentController.Instance.GetAllByDoctor(Doctor.Id));
            HomePage = new UCHomePage();
            Appointments = new UCAppointments();
            Patients = new UCPatients();
            Medicines = new UCMedicines();
            Notifications = new UCDoctorNotifications();
            ApproveMedicine = new UCApproveMedicine();
            Home.Children.Add(HomePage);
            nameSurname.Text = Doctor.Name.ToString() +" "+ Doctor.Surname.ToString();
        }

        public void ExitBtnClick(object sender, RoutedEventArgs e)
        {
            bool dialog = (bool)new ExitMess("Da li ste sigurni da želite da se odjavite?").ShowDialog();
           if (dialog)
            {
                //DoctorController.Instance..UpdateDoctor(Doctor);
                MainWindow login = new MainWindow();
                login.Show();
                this.Hide();
                
            }
         }

        public void MinBtnClick(object sender, RoutedEventArgs e)
        {
            WindowControls.DoMinimize(this);
        }

        public void MaxBtnClick(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            WindowControls.DoMaximize(this, btn);
        }

        public void FullBtnClick(object sender, RoutedEventArgs e)
        {
            WindowControls.DoFullscreen(this);
        }

        private void Meni_Clicked(object sender, RoutedEventArgs e)
        {
            int index = int.Parse(((RadioButton)e.Source).Uid);

            switch (Last)
            {
                case 0:
                    Home.Children.Clear();
                    Home.Children.Remove(HomePage);
                    break;
                case 1:
                    Home.Children.Clear();
                    Home.Children.Remove(Appointments);
                    break;
                case 2:
                    Home.Children.Clear();
                    Home.Children.Remove(Patients);
                    break;
                case 3:
                    Home.Children.Clear();
                    Home.Children.Remove(Medicines);
                    break;
                case 4:
                    break;
                case 5:
                    break;
                case 6:
                    Home.Children.Clear();
                    Home.Children.Remove(ApproveMedicine);
                    break;
                case 7:
                    Home.Children.Clear();
                    Home.Children.Remove(Notifications);
                    break;
            }

            switch (index)
            {
                case 0:
                    HomePage.Visibility = Visibility.Visible;
                    Home.Children.Add(HomePage);
                    Last = 0;
                    break;
                case 1:
                    Appointments.Visibility = Visibility.Visible;
                    Home.Children.Add(Appointments);
                    Last = 1;
                    break;
                case 2:
                    Patients.Visibility = Visibility.Visible;
                    Home.Children.Add(Patients);
                    Last = 2;
                    break;
                case 3:
                    Medicines.Visibility = Visibility.Visible;
                    Home.Children.Add(Medicines);
                    Last = 3;
                    break;
                case 4:
                    Last = 4;
                    break;
                case 5:
                    Last = 5;
                    break;
                case 6:
                    ApproveMedicine.Visibility = Visibility.Visible;
                    Home.Children.Add(ApproveMedicine);
                    Last = 6;
                    break;
                case 7:
                    Notifications.Visibility = Visibility.Visible;
                    Home.Children.Add(Notifications);
                    Last = 7;
                    break;
            }
        }

    }
}