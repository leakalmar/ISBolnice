using Controllers;
using Hospital_IS.DTOs;
using Model;
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
        public ObservableCollection<Patient> Patients { get; set; } = new ObservableCollection<Patient>();
        public ObservableCollection<Specialty> Specializations { get; set; } = new ObservableCollection<Specialty>();
        public ObservableCollection<Room> Rooms { get; set; } = new ObservableCollection<Room>();
        public ObservableCollection<SuggestedEmergencyAppDTO> SuggestedAppointments { get; set; }
        public ObservableCollection<RescheduledAppointmentDTO> RescheduledAppointments { get; set; }

        private ScheduleAppointment sa;
        public ScheduleEmergencyAppointment(ScheduleAppointment sa)
        {
            InitializeComponent();
            this.sa = sa;

            Patients = new ObservableCollection<Patient>(PatientController.Instance.GetAll());
            Specializations = new ObservableCollection<Specialty>(SpecializationController.Instance.GetAll());

            this.DataContext = this;

        }

        private void AddNewEmergencyAppointment(object sender, RoutedEventArgs e)
        {
            foreach (RescheduledAppointmentDTO raDTO in RescheduledAppointments)
            {
                DoctorAppointmentController.Instance.UpdateAppointment(raDTO.OldDocAppointment, raDTO.DocAppointment);  //notifikacije ???
            }
            DoctorAppointmentController.Instance.AddAppointment(SuggestedAppointments[dgSuggestedAppointments.SelectedIndex].SuggestedAppointment);

            sa.uca.RefreshGrid();

            sa.Close();
            this.Close();
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

            EmergencyAppointmentDTO emerAppointmentDTO = new EmergencyAppointmentDTO();
            if (cbAppType.SelectedIndex == 0)
                emerAppointmentDTO.AppointmetType = AppointmetType.CheckUp;
            else if (cbAppType.SelectedIndex == 1)
                emerAppointmentDTO.AppointmetType = AppointmetType.Operation;

            emerAppointmentDTO.Specialty = Specializations[cbSpecialty.SelectedIndex];
            emerAppointmentDTO.Patient = Patients[cbPatient.SelectedIndex];
            emerAppointmentDTO.Room = Rooms[cbRoom.SelectedIndex];
            emerAppointmentDTO.DurationInMinutes = Int32.Parse(txtAppDuration.Text);



            SuggestedAppointments = new ObservableCollection<SuggestedEmergencyAppDTO>(DoctorAppointmentController.Instance.GenerateEmergencyAppointmentsForSecretary(emerAppointmentDTO));
            dgSuggestedAppointments.ItemsSource = SuggestedAppointments;
            


            //foreach (SuggestedEmergencyAppDTO seadto in SuggestedAppointments)
                //MessageBox.Show(seadto.TotalReshedulePeriodInHours.ToString());
        }

        private void dgSuggestedAppointments_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (dgSuggestedAppointments.SelectedIndex >= 0)
            {
                RescheduledAppointments = new ObservableCollection<RescheduledAppointmentDTO>(SuggestedAppointments[dgSuggestedAppointments.SelectedIndex].RescheduledAppointments);
                dgScheduledAppointments.ItemsSource = RescheduledAppointments;
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            sa.Visibility = Visibility.Visible;
            this.Close();
        }
    }
}
