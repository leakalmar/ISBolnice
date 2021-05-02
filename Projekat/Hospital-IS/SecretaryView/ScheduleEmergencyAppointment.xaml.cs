using Controllers;
using Model;
using Service;
using System;
using System.Collections.ObjectModel;
using System.Windows;


namespace Hospital_IS.SecretaryView
{
    /// <summary>
    /// Interaction logic for ScheduleEmergencyAppointment.xaml
    /// </summary>
    public partial class ScheduleEmergencyAppointment : Window
    {
        public DoctorAppointment DocAppointment { get; set; } = new DoctorAppointment();
        public ObservableCollection<Patient> Patients { get; set; } = new ObservableCollection<Patient>();
        public ObservableCollection<Specialty> Specializations { get; set; } = new ObservableCollection<Specialty>();
        public ObservableCollection<Room> Rooms { get; set; } = new ObservableCollection<Room>();
        public ObservableCollection<SuggestedEmergencyAppDTO> SuggestedAppointments { get; set; }
        public ObservableCollection<DoctorAppointment> ScheduledAppointments { get; set; }
        public ScheduleEmergencyAppointment()
        {
            InitializeComponent();

            Patients = new ObservableCollection<Patient>(PatientController.Instance.GetAll());
            Specializations = new ObservableCollection<Specialty>(SpecializationController.Instance.GetAll());

            this.DataContext = this;

        }

        private void cbAppType_LostFocus(object sender, RoutedEventArgs e)
        {
            if (cbAppType.SelectedIndex == 0)
            {
                cbSpecialty.IsEnabled = false;
                Rooms = new ObservableCollection<Room>(RoomController.Instance.getRoomByType(RoomType.ConsultingRoom));
                cbRoom.ItemsSource = Rooms;
            }
            else
            {
                cbSpecialty.IsEnabled = true;
                Rooms = new ObservableCollection<Room>(RoomController.Instance.getRoomByType(RoomType.OperationRoom));
                cbRoom.ItemsSource = Rooms;
            }
        }

        private void txtAppDuration_LostFocus(object sender, RoutedEventArgs e)
        {
            SuggestedAppointments = new ObservableCollection<SuggestedEmergencyAppDTO>(DoctorAppointmentService.Instance.GenerateEmergencyAppointmentsForSecretary(Rooms[cbRoom.SelectedIndex],
                Specializations[cbSpecialty.SelectedIndex], Int32.Parse(txtAppDuration.Text)));
            dataGridAppointments.ItemsSource = SuggestedAppointments;

            foreach (SuggestedEmergencyAppDTO seadto in SuggestedAppointments)
                MessageBox.Show(seadto.RescheduledAppointments.Count.ToString());
        }
    }
}
