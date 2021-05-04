using Controllers;
using Model;
using Storages;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Hospital_IS.View
{
    /// <summary>
    /// Interaction logic for DocumentationPatient.xaml
    /// </summary>
    public partial class TherapyPatient : Window
    {

        public ObservableCollection<Therapy> Therapies { get; set; }

        public TherapyPatient()
        {
            InitializeComponent();
            Therapies = new ObservableCollection<Therapy>(ChartController.Instance.GetTherapiesByPatient(HomePatient.Instance.Patient));
            this.DataContext = this;
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

        private void showAll(object sender, RoutedEventArgs e)
        {
            AllAppointments allApp = new AllAppointments();
            allApp.Show();
            this.Close();
        }

        private void showNotifications(object sender, RoutedEventArgs e)
        {
            PatientNotifications notifications = new PatientNotifications();
            notifications.Show();
            this.Close();
        }

        private void logout(object sender, RoutedEventArgs e)
        {
            MainWindow login = new MainWindow();
            login.Show();
            this.Hide();
            Storages.PatientFileStorage pfs = new Storages.PatientFileStorage();
            pfs.UpdatePatient(HomePatient.Instance.Patient);
        }

        private void showRow(object sender, MouseButtonEventArgs e)
        {
            Therapy therapyInfo = (Therapy)dataGridTherapy.SelectedItem;
            int usageHourDifference = (int)24 / therapyInfo.TimesADay;
            Name.Content = therapyInfo.Medicine.Name;
            Quantity.Content = therapyInfo.Quantity;
            TimesADay.Content = therapyInfo.TimesADay;
            TimeSpan.Content = usageHourDifference.ToString() + "h";
            StartTherapy.Text = therapyInfo.TherapyStart.ToString("dd.MM.yyyy.");
            EndTherapy.Text = therapyInfo.TherapyEnd.ToString("dd.MM.yyyy.");
            Usage.Text = therapyInfo.Medicine.Usage;
            SideEffects.Text = therapyInfo.Medicine.SideEffects;
            TherapyInfo.Visibility = Visibility.Visible;
        }
    }
}
