using Hospital_IS.Storages;
using Model;
using Storages;
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
using Hospital_IS.DoctorView;
using Service;
using Controllers;

namespace Hospital_IS.DoctorView
{
    public partial class DoctorHomePage : Window
    {
        private static DoctorHomePage instance = null;
        public static DoctorHomePage Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DoctorHomePage();
                }
                return instance;
            }
        }
        public Doctor Doctor { get; set; }
        public Room PrimaryRoom { get; set; }
        private int Last { get; set; } = 0;
        public ObservableCollection<DoctorAppointment> DoctorAppointment { get; set; }


        public UserControlHomePage HomePage;
        public UCAppointments Appointments;
        public UCPatients Patients;
        public UCMedicines Medicines;


        
        public DoctorHomePage()
        {
            InitializeComponent();            
            start.IsChecked = true;
        }

        public void ChangeDoctor(Doctor doctor)
        {
            Doctor = doctor;
            foreach (Room r in MainWindow.Rooms)
            {
                if (r.RoomId.Equals(Doctor.PrimaryRoom))
                {
                    PrimaryRoom = r;
                    break;
                }
            }
            DoctorAppointment = new ObservableCollection<DoctorAppointment>(DoctorAppointmentController.Instance.GetAllByDoctor(Doctor.Id));
            HomePage = new UserControlHomePage();
            Appointments = new UCAppointments();
            Patients = new UCPatients();
            Medicines = new UCMedicines();
            Home.Children.Add(HomePage);
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
                    break;
                case 7:
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
                    Last = 6;
                    break;
                case 7:
                    Last = 7;
                    break;
            }
        }

    }
}
