using Hospital_IS.SecretaryView;
using System;
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
        UCAppointmentsView uca = new UCAppointmentsView();

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

        private void btnAppointments_Click(object sender, RoutedEventArgs e)
        {
            HomePage.Content = uca;
        }

    }
}