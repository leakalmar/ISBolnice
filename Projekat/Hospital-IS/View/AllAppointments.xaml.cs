using Model;
using Storages;
using System.Windows;

namespace Hospital_IS.View
{
    /// <summary>
    /// Interaction logic for AllAppointments.xaml
    /// </summary>
    public partial class AllAppointments : Window
    {
        public AllAppointments()
        {
            InitializeComponent();
        }

        private void home(object sender, RoutedEventArgs e)
        {
            HomePatient.Instance.Show();
            this.Close();
        }


        private void reserveApp(object sender, RoutedEventArgs e)
        {
            AppointmentPatient ap = new AppointmentPatient();
            ap.Show();
            this.Close();
        }

        private void showTherapy(object sender, RoutedEventArgs e)
        {
            TherapyPatient doc = new TherapyPatient();
            doc.Show();
            this.Close();
        }

        private void logout(object sender, RoutedEventArgs e)
        {
            MainWindow login = new MainWindow();
            login.Show();
            this.Close();
            AppointmentFileStorage afs = new AppointmentFileStorage();
            afs.SaveAppointment(Hospital.Instance.allAppointments);
            Storages.PatientFileStorage pfs = new Storages.PatientFileStorage();
            pfs.UpdatePatient(HomePatient.Instance.Patient);
        }

        private void showNotifications(object sender, RoutedEventArgs e)
        {
            PatientNotifications notifications = new PatientNotifications();
            notifications.Show();
            this.Close();
        }
    }
}
