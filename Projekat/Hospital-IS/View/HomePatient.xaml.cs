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
            Medicine medicine = new Medicine("Bromazepam", "Loš sastav", "Najebao si", "Kad hoćeš");
            Therapy t = new Therapy(medicine, 1, 2, new DateTime(2021, 4, 19), new DateTime(2021, 5, 1));
            Medicine medicine1 = new Medicine("Brufen", "Loš sastav", "Najebao si", "Kad hoćeš");
            Therapy t1 = new Therapy(medicine1, 1, 3, new DateTime(2021, 4, 19), new DateTime(2021, 5, 1));
            t.FirstUsageTime = 8;
            t1.FirstUsageTime = 8;
            Patient.MedicalHistory.AddTherapy(t);
            Patient.MedicalHistory.AddTherapy(t1);
            this.DataContext = this;
            PersonalData.DataContext = Patient;
            DispatcherTimer dispatcherTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMinutes(1)
            };
            dispatcherTimer.Tick += timer_Tick;
            dispatcherTimer.Start();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            DateTime time= DateTime.Now;

            foreach (Therapy therapy in Patient.MedicalHistory.Therapies)
            {
                int usageHourDifference = (int)24/therapy.TimesADay;
                for (int i = 0; i < therapy.TimesADay; i++)
                {
                    if (time.AddHours(2).Hour == (therapy.FirstUsageTime + i * usageHourDifference) && time.Minute == 0)
                    {
                        MessageBox.Show("Vreme je da popijete lek: " + therapy.Medicine.Name);
                    }
                }
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

        private void showNotifications(object sender, RoutedEventArgs e)
        {
            PatientNotifications notifications = new PatientNotifications();
            notifications.Show();
            this.Hide();
        }
    }
}
