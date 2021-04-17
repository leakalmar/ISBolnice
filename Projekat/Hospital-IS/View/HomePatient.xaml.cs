using Model;
using Storages;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Threading;

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

        public Patient Patient { get; set; }
        public DoctorAppointment changedApp;
        public ObservableCollection<DoctorAppointment> DoctorAppointment { get; set; }
        public HomePatient()
        {
            InitializeComponent();

            Patient = MainWindow.PatientUser;
            this.DataContext = this;
            PersonalData.DataContext = Patient;
            DispatcherTimer dispatcherTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMinutes(5)
            };
            dispatcherTimer.Tick += timer_Tick;
            dispatcherTimer.Start();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            DateTime time= DateTime.Now;

            foreach (Therapy t in Patient.Therapies)
            {
                
            }
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

        private void showTherapy(object sender, RoutedEventArgs e)
        {
            TherapyPatient doc = new TherapyPatient();
            doc.Show();
            this.Hide();
        }

        private void deleteAppointment(object sender, RoutedEventArgs e)
        {
            DoctorAppointment doctorApp = (DoctorAppointment)dataGridAppointment.SelectedItem;
            DateTime today = DateTime.Today;
            if (doctorApp == null)
            {
                MessageBox.Show("Izaberite termin!");
            }
            else if (doctorApp.DateAndTime.Date < today.AddDays(3))
            {
                MessageBox.Show("Ne možete otkazati termin na manje od 3 dana do termina!");
            }
            else
            {
                Hospital.Instance.RemoveAppointment(doctorApp);
                DoctorAppointment.Remove(doctorApp);
                doctorApp.Reserved = false;
            }

        }

        private void changeAppointment(object sender, RoutedEventArgs e)
        {
            changedApp = (DoctorAppointment)dataGridAppointment.SelectedItem;
            if (changedApp == null)
            {
                MessageBox.Show("Izaberite termin!");
            }
            else
            {
                AppointmentPatient ap = new AppointmentPatient();
                ap.Show();
                ap.changeAppointment(changedApp);
            }
        }

        private void logout(object sender, RoutedEventArgs e)
        {
            MainWindow login = new MainWindow();
            login.Show();
            this.Hide();
            AppointmentFileStorage afs = new AppointmentFileStorage();
            afs.SaveAppointment(Hospital.Instance.allAppointments);
        }
    }
}
