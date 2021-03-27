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
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            CurrentTimeLabel.Content = DateTime.Now.ToString("HH:mm  dd.MM.yyyy.");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PatientRegistration pr = new PatientRegistration();
            pr.Show();
        }
    }
}
