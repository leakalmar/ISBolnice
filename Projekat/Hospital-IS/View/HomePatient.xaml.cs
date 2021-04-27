using Controllers;
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
        private HomePatient()
        {
            InitializeComponent();

            Patient = MainWindow.PatientUser;
            DoctorAppointment = new ObservableCollection<DoctorAppointment>(DoctorAppointmentController.Instance.GetFutureAppointmentsByPatient(Patient.Id));
            /*Medicine medicine = new Medicine("Bromazepam", "1 tableta sadrži 1,5 mg, 3 mg, odnosno 6 mg bromazepama.",
                "U osetljivih bolesnika, naročito kod većih doza, može se javiti blagi umor, pospanost i vrtoglavica," +
                " a povremeno slabost mišića i ataksija. Ova neželjena dejstva mogu se izbeći prilagođavanjem doze.", "Doziranje je individualno." +
                " Prosečna doza za ambulantne pacijente je 1,5-3 mg, do 3 puta na dan." +
                " U težim oblicima bolesti, naročito kod hospitalizovanih bolesnika, daje se 6-12 mg, 2 do 3 puta na dan.");
            Therapy t = new Therapy(medicine, 1, 2, new DateTime(2021, 4, 19), new DateTime(2021, 5, 1));
            Medicine medicine1 = new Medicine("Brufen", "1 film tableta sadrži 400 mg ibuprofena 5 ml sirupa sadrži 100 mg Ibuprofena", "Ibuprofen se dobro podnosi, a neželjena dejstva su veoma retka i nestaju prekidom terapije." +
                " Kod manjeg broja bolesnika mogu se, javiti gastrointestinalne smetnje.", "Ibuprofen se uzima posle jela." +
                " Doziranje treba individualno uskladiti tako da se sa najmanjom mogućom dozom postigne željeni terapijski efekat.");
            Therapy t1 = new Therapy(medicine1, 1, 3, new DateTime(2021, 4, 19), new DateTime(2021, 5, 1));
            t.FirstUsageTime = 11;
            t1.FirstUsageTime = 8;
            Patient.MedicalHistory.AddTherapy(t);
            Patient.MedicalHistory.AddTherapy(t1);*/
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
                    if (time.AddHours(2).Hour == (therapy.FirstUsageTime + i * usageHourDifference) && time.Minute == 38)
                    {
                        MessageBox.Show("Za 2 sata treba da popijete lek: " + therapy.Medicine.Name);
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
            else if (doctorApp.AppointmentStart.Date < today.AddDays(3))
            {
                MessageBox.Show("Ne možete otkazati termin na manje od 3 dana do termina!");
            }
            else
            {
                DoctorAppointmentController.Instance.RemoveAppointment(doctorApp);
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
            Storages.PatientFileStorage pfs = new Storages.PatientFileStorage();
            pfs.UpdatePatient(Patient);
        }

        private void showNotifications(object sender, RoutedEventArgs e)
        {
            PatientNotifications notifications = new PatientNotifications();
            notifications.Show();
            this.Hide();
        }
    }
}
