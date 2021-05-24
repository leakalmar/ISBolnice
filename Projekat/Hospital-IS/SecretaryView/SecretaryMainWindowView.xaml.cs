using Hospital_IS.SecretaryView.SecretaryViewModel;
using System;
using System.Windows;
using System.Windows.Threading;

namespace Hospital_IS.SecretaryView
{
    /// <summary>
    /// Interaction logic for SecretaryMainWindowView.xaml
    /// </summary>
    public partial class SecretaryMainWindowView : Window
    {
        UCPatientsView ucp = new UCPatientsView();
        UCNotificationsView ucn = new UCNotificationsView();
        UCAppointmentsView uca = new UCAppointmentsView();

        private static SecretaryMainWindowView instance = null;

        public static SecretaryMainWindowView Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SecretaryMainWindowView();
                }
                return instance;
            }
        }

        public SecretaryMainWindowViewModel SecretaryMainView { get; set; }

        public SecretaryMainWindowView()
        {
            SecretaryMainView = new SecretaryMainWindowViewModel();
            this.DataContext = SecretaryMainView;
            InitializeComponent();

            CurrentTimeLabel.Content = DateTime.Now.ToString("HH:mm  dd.MM.yyyy.");
            DispatcherTimer dispatcherTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            dispatcherTimer.Tick += timer_Tick;
            dispatcherTimer.Start();

            //HomePage.Content = ucp;
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
            //HomePage.Content = ucp;
        }

        private void btnNotifications_Click(object sender, RoutedEventArgs e)
        {
            //HomePage.Content = ucn;
        }

        private void btnAppointments_Click(object sender, RoutedEventArgs e)
        {
            //HomePage.Content = uca;
        }

    }
}