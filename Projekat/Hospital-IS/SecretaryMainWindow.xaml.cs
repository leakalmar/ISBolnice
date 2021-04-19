using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Threading;

namespace Hospital_IS
{
    /// <summary>
    /// Interaction logic for SecretaryMainWindow.xaml
    /// </summary>
    public partial class SecretaryMainWindow : Window
    {
        UCPatientsView ucp = new UCPatientsView();
        UCNotificationsView ucn = new UCNotificationsView();

        private static SecretaryMainWindow instance = null;

        public static SecretaryMainWindow Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SecretaryMainWindow();
                }
                return instance;
            }
        }

        public SecretaryMainWindow()
        {
            InitializeComponent();

            CurrentTimeLabel.Content = DateTime.Now.ToString("HH:mm  dd.MM.yyyy.");
            DispatcherTimer dispatcherTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            dispatcherTimer.Tick += timer_Tick;
            dispatcherTimer.Start();

            HomePage.Content = ucp;

            // this.DataContext = this;

            /*ObservableCollection<String> alergije = new ObservableCollection<String>();
            alergije.Add("Tetanus");
            alergije.Add("Paracetamol");
            Model.Patient p1 = new Model.Patient(1111, "Marko", "Marković", new DateTime(1998, 12, 18), "markoa@gmail.com", "sifra", "Cara Dušana 4", DateTime.Now, "", alergije);
            Model.Patient p2 = new Model.Patient(1212, "Nikolina", "Perić", new DateTime(1972, 10, 12), "nikolina@gmail.com", "sifra", "Bulevar Evrope 11", DateTime.Now, "", null);
            Model.Patient p3 = new Model.Patient(2314, "Ana", "Zorić", new DateTime(1995, 11, 12), "ana@gmail.com", "sifra", "Bulevar Evrope 21", DateTime.Now, "", null);
            Model.Patient p4 = new Model.Patient(1234, "Petar", "Petrović", new DateTime(2000, 12, 9), "petar@gmail.com", "sifra", "Šekspirova 18", DateTime.Now, "", null);
            Patients.Add(p1);
            Patients.Add(p2);
            Patients.Add(p3);
            Patients.Add(p4);*/

        }



        private void timer_Tick(object sender, EventArgs e)
        {
            CurrentTimeLabel.Content = DateTime.Now.ToString("HH:mm  dd.MM.yyyy.");
        }

        

        private void Logout(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            mw.Show();
            this.Visibility = Visibility.Hidden;
        }

        private void btnPatients_Click(object sender, RoutedEventArgs e)
        {
            HomePage.Content = ucp;
        }

        private void btnNotifications_Click(object sender, RoutedEventArgs e)
        {
            HomePage.Content = ucn;
        }
    }
}