using Controllers;
using Model;
using Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
namespace Hospital_IS.SecretaryView
{
    /// <summary>
    /// Interaction logic for SelectGuestView.xaml
    /// </summary>
    public partial class SelectGuestView : Window
    {
        public ObservableCollection<Patient> Guests { get; set; }

        public ScheduleEmergencyAppointment sea;
        public SelectGuestView(ScheduleEmergencyAppointment sea)
        {
            InitializeComponent();
            RefreshGrid();
            this.sea = sea;

            this.DataContext = this;
        }

        public void RefreshGrid()
        {
            Guests = new ObservableCollection<Patient>(PatientController.Instance.GetAllGuests());
            dataGridGuests.ItemsSource = Guests;
        }

        private void AddNewGuest(object sender, RoutedEventArgs e)
        {
            Patient patient = new Patient();                    //prebaciti u servis
            patient.Id = UserService.Instance.GenerateUserID();
            patient.IsGuest = true;
            patient.BirthDate = DateTime.Now;
            PatientController.Instance.AddPatient(patient);

            RefreshGrid();
        }

        private void Select(object sender, RoutedEventArgs e)
        {
            Patient patient = (Patient)dataGridGuests.SelectedItem;
            sea.txtGuest.Text = patient.Id.ToString();

            sea.txtGuest.Visibility = Visibility.Visible;
            sea.cbPatient.IsEnabled = false;
            sea.cbPatient.Visibility = Visibility.Collapsed;
            this.Close();
        }

        private void Close(object sender, RoutedEventArgs e)
        {
            sea.txtGuest.Visibility = Visibility.Collapsed;
            sea.cbPatient.IsEnabled = true;
            sea.cbPatient.Visibility = Visibility.Visible;
            this.Close();
        }
    }
}
