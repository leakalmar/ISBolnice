using Controllers;
using Enums;
using Hospital_IS.Controllers;
using Hospital_IS.DTOs;
using Hospital_IS.DTOs.SecretaryDTOs;
using Model;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;


namespace Hospital_IS.SecretaryView
{
    /// <summary>
    /// Interaction logic for ScheduleEmergencyAppointment.xaml
    /// </summary>
    public partial class ScheduleEmergencyAppointment : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<PatientDTO> Patients { get; set; } = new ObservableCollection<PatientDTO>();
        public ObservableCollection<string> Specializations { get; set; } = new ObservableCollection<string>();
        public ObservableCollection<RoomDTO> Rooms { get; set; } = new ObservableCollection<RoomDTO>();
        public ObservableCollection<SuggestedEmergencyAppDTO> SuggestedAppointments { get; set; }
        public ObservableCollection<RescheduledAppointmentDTO> RescheduledAppointments { get; set; }

        private ScheduleAppointment sa;

        private int _appDuration;
        public int AppDuration
        {
            get { return _appDuration; }
            set
            {
                if (value != _appDuration)
                {
                    _appDuration = value;
                    OnPropertyChanged("AppDuration");
                }
            }
        }
        public ScheduleEmergencyAppointment(ScheduleAppointment sa)
        {
            InitializeComponent();
            this.sa = sa;

            Patients = new ObservableCollection<PatientDTO>(SecretaryManagementController.Instance.GetAllRegisteredPatients());
            Specializations = new ObservableCollection<string>(SpecializationController.Instance.GetAllNames());

            this.DataContext = this;

        }
        public ScheduleEmergencyAppointment(ScheduleAppointment sa, PatientDTO patient)
        {
            InitializeComponent();
            this.sa = sa;

            Patients = new ObservableCollection<PatientDTO>(SecretaryManagementController.Instance.GetAllRegisteredPatients());
            Specializations = new ObservableCollection<string>(SpecializationController.Instance.GetAllNames());

            cbPatient.SelectedItem = patient;
            cbPatient.IsEnabled = false;

            this.DataContext = this;

        }

        private void AddNewEmergencyAppointment(object sender, RoutedEventArgs e)
        {
            if (dgSuggestedAppointments.SelectedIndex != -1)
            {
                foreach (RescheduledAppointmentDTO raDTO in RescheduledAppointments)
                {
                    NotificationController.Instance.SendNAppointmentRescheduledNotification(raDTO.OldDocAppointment, raDTO.NewDocAppointment);
                    sa.uca.doctorAppointmentTarget.UpdateDoctorAppointment(raDTO.OldDocAppointment, raDTO.NewDocAppointment);
                }
                sa.uca.doctorAppointmentTarget.AddDoctorAppointment(SuggestedAppointments[dgSuggestedAppointments.SelectedIndex].SuggestedAppointment);
                sa.uca.RefreshGrid();

                sa.Close();
                this.Close();
            }
            else 
            {
                MessageBox.Show("Izaberite jedan od predloženih termina!");
            }
        }

        private void txtAppDuration_LostFocus(object sender, RoutedEventArgs e)
        {

            EmergencyAppointmentDTO emerAppointmentDTO = new EmergencyAppointmentDTO();
            if (cbAppType.SelectedIndex == 0)
                emerAppointmentDTO.AppointmetType = AppointmentType.CheckUp;
            else if (cbAppType.SelectedIndex == 1)
                emerAppointmentDTO.AppointmetType = AppointmentType.Operation;

            if (cbSpecialty.SelectedIndex == -1)
                emerAppointmentDTO.Specialty = Specializations[0];
            else
                emerAppointmentDTO.Specialty = Specializations[cbSpecialty.SelectedIndex];
                
            if (cbPatient.IsEnabled)
                emerAppointmentDTO.Patient = Patients[cbPatient.SelectedIndex];
            else
                emerAppointmentDTO.Patient = SecretaryManagementController.Instance.GetPatientByID(Int32.Parse(txtGuest.Text));
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

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            sa.Visibility = Visibility.Visible;
            this.Close();
        }

        private void SelectGuest(object sender, RoutedEventArgs e)
        {
            SelectGuestView sg = new SelectGuestView(this);
            sg.ShowDialog();
        }

        private void cbAppType_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (cbAppType.SelectedIndex == 0)
            {
                cbSpecialty.IsEnabled = false;
                cbSpecialty.SelectedIndex = 0;
                Rooms = new ObservableCollection<RoomDTO>(DoctorAppointmentManagementController.Instance.GetRoomByType(RoomType.ConsultingRoom));
                cbRoom.ItemsSource = Rooms;
            }
            else
            {
                cbSpecialty.IsEnabled = true;
                Rooms = new ObservableCollection<RoomDTO>(DoctorAppointmentManagementController.Instance.GetRoomByType(RoomType.OperationRoom));
                cbRoom.ItemsSource = Rooms;
            }
        }

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
