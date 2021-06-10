using DTOs;
using Hospital_IS.Adapter;
using Hospital_IS.Controllers;
using Hospital_IS.DoctorRole.Commands;
using Hospital_IS.DTOs;
using Model;
using System;

//MVVM
namespace Hospital_IS.DoctorViewModel
{
    public class IssueInstructionViewModel : BindableBase
    {
        #region Fields
        private AppointmentRowDTO selectedAppointment;
        private SuggestedEmergencyAppDTO selectedEmergencyAppointment;
        private string appointmentCause;
        private DoctorDTO logedInDoctor;

        public AppointmentRowDTO SelectedAppointment
        {
            get { return selectedAppointment; }
            set
            {
                selectedAppointment = value;
                OnPropertyChanged("SelectedAppointment");
            }
        }

        public SuggestedEmergencyAppDTO SelectedEmergencyAppointment
        {
            get { return selectedEmergencyAppointment; }
            set
            {
                selectedEmergencyAppointment = value;
                OnPropertyChanged("SelectedEmergencyAppointment");
            }
        }

        public string AppointmentCause
        {
            get { return appointmentCause; }
            set
            {
                appointmentCause = value;
                OnPropertyChanged("AppointmentCause");
            }
        }

        public DoctorDTO LogedInDoctor
        {
            get { return logedInDoctor; }
            set
            {
                logedInDoctor = value;
                OnPropertyChanged("LogedInDoctor");
            }
        }

        #endregion

        #region Commands
        private RelayCommand issueInstructionCommand;
        private RelayCommand backCommand;
        private RelayCommand navigateToPatientChartCommand;

        public RelayCommand IssueInstructionCommand
        {
            get { return issueInstructionCommand; }
            set { issueInstructionCommand = value; }
        }

        public RelayCommand BackCommand
        {
            get { return backCommand; }
            set { backCommand = value; }
        }

        public RelayCommand NavigateToPatientChartCommand
        {
            get { return navigateToPatientChartCommand; }
            set { navigateToPatientChartCommand = value; }
        }
        #endregion

        #region Actions

        private bool CanExecute_Command(object obj)
        {
            return true;
        }

        private void Execute_IssueInstructionCommand(object obj)
        {
            ScheduleAppointment();
            DoctorMainWindowModel.Instance.NavigateToChartCommand.Execute(null);
            PatientChartViewModel.Instance.ChangeCommand.Execute("3");
        }

        private void Execute_NavigateToPatientChartCommand(object obj)
        {
            DoctorNavigationController.Instance.NavigateToChartCommand();
        }

        #endregion

        #region Methods
        private void SendRescheduleNotification(DoctorAppointmentDTO oldApp, DoctorAppointmentDTO appointment)
        {
            string title = "Pomeren pregled";

            string text = "Pregled koji ste imali " + oldApp.AppointmentStart.ToString("dd.MM.yyyy.") + " u "
                + oldApp.AppointmentStart.ToString("HH:mm") + "h je pomeren za "
                + appointment.AppointmentStart.ToString("dd.MM.yyyy.") + " u " + appointment.AppointmentStart.ToString("HH:mm") + "h.";

            Notification notification = new Notification(title, text, DateTime.Now);
            notification.Recipients.Add(appointment.Patient.Id);
            notification.Recipients.Add(appointment.Doctor.Id);

            NotificationController.Instance.AddNotification(notification);
        }

        private void SendScheduledNotification(DoctorAppointmentDTO appointment)
        {
            string title = "Zakazan pregled";

            string text = "Zakazan Vam je novi pregled " + appointment.AppointmentStart.ToString("dd.MM.yyyy.") + " u "
                + appointment.AppointmentStart.ToString("HH:mm") + " do " + appointment.AppointmentStart.ToString("HH:mm") + "h.";

            Notification notification = new Notification(title, text, DateTime.Now);
            notification.Recipients.Add(appointment.Patient.Id);
            //notification.Recipients.Add(appointment.Doctor.Id);

            NotificationController.Instance.AddNotification(notification);
        }

        private void ScheduleAppointment()
        {
            if (SelectedEmergencyAppointment == null)
            {
                SelectedAppointment.Appointment.AppointmentCause = AppointmentCause;
                SendScheduledNotification(SelectedAppointment.Appointment);
                DoctorMainWindowModel.Instance.Adapter.AddDoctorAppointment(SelectedAppointment.Appointment);
            }
            else
            {
                foreach (RescheduledAppointmentDTO rescheduled in SelectedEmergencyAppointment.RescheduledAppointments)
                {
                    SendRescheduleNotification(rescheduled.OldDocAppointment, rescheduled.NewDocAppointment);
                    DoctorMainWindowModel.Instance.Adapter.UpdateDoctorAppointment(rescheduled.OldDocAppointment, rescheduled.NewDocAppointment);
                }
                SelectedEmergencyAppointment.SuggestedAppointment.AppointmentCause = AppointmentCause;
                DoctorMainWindowModel.Instance.Adapter.AddDoctorAppointment(SelectedEmergencyAppointment.SuggestedAppointment);
            }
        }

        #endregion

        #region Constructor
        public IssueInstructionViewModel()
        {
            this.LogedInDoctor = DoctorMainWindowModel.Instance.Doctor;
            this.IssueInstructionCommand = new RelayCommand(Execute_IssueInstructionCommand, CanExecute_Command);
            this.BackCommand = DoctorMainWindowModel.Instance.NavigateBackCommand;
            this.NavigateToPatientChartCommand = new RelayCommand(Execute_NavigateToPatientChartCommand, CanExecute_Command);
        }

        #endregion
    }
}
