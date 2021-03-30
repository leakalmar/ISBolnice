using Hospital_IS.Storages;
using Hospital_IS.View;
using Model;
using Storages;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
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
            doctorAppointments = new ObservableCollection<DoctorAppointment>();
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
            ObservableCollection<Doctor> doctors = dfs.GetAll();

            foreach (Patient p in patients)
            {
                if (email.Text == p.Email && password.Password.ToString() == p.Password)
                {
                    PatientUser = p;
                    //    PatientUser.DoctorAppointment = afs.GetAllByPatient(p);
                    HomePatient.Instance.DoctorAppointment = afs.GetAllByPatient(p.Id);
                    //doctorAppointments = afs.GetAllByPatient();
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
                Window1 win = new Window1();
                win.Show();
            }
            else if (email.Text == "sekretar@gmail.com" && password.Password.ToString() == "sekretar")
            {
                SecretaryMainWindow smw = new SecretaryMainWindow();
                smw.Show();
                this.Close();
            }
        }
    }
}
