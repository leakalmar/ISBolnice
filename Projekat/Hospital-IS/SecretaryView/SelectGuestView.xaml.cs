using Hospital_IS.Controllers;
using Hospital_IS.DTOs.SecretaryDTOs;
using Service;
using System;
using System.Collections.ObjectModel;
using System.Windows;
namespace Hospital_IS.SecretaryView
{
    /// <summary>
    /// Interaction logic for SelectGuestView.xaml
    /// </summary>
    public partial class SelectGuestView : Window
    {
        public ObservableCollection<PatientDTO> Guests { get; set; }

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
            Guests = new ObservableCollection<PatientDTO>(SecretaryManagementController.Instance.GetAllGuests());
            dataGridGuests.ItemsSource = Guests;
        }

        private void AddNewGuest(object sender, RoutedEventArgs e)
        {
            PatientDTO patient = new PatientDTO();
            patient.Id = UserService.Instance.GenerateUserID();
            patient.IsGuest = true;
            patient.BirthDate = DateTime.Now;
            SecretaryManagementController.Instance.AddPatient(patient);

            RefreshGrid();
        }

        private void Select(object sender, RoutedEventArgs e)
        {
            PatientDTO patient = (PatientDTO)dataGridGuests.SelectedItem;
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
