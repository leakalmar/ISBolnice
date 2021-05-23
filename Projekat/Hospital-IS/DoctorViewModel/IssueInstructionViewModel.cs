﻿using Controllers;
using DTOs;
using Hospital_IS.Commands;
using Hospital_IS.Controllers;
using Hospital_IS.DoctorView;
using Model;
using System;
using System.Windows.Navigation;

namespace Hospital_IS.DoctorViewModel
{
    public class IssueInstructionViewModel : BindableBase
    {
        #region Fields
        private AppointmentRowDTO selectedAppointment;
        private SuggestedEmergencyAppDTO selectedEmergencyAppointment;
        private string appointmentCause;
        private Doctor logedInDoctor;

        private NavigationService mainNavigationService;

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

        public Doctor LogedInDoctor
        {
            get { return logedInDoctor; }
            set
            {
                logedInDoctor = value;
                OnPropertyChanged("LogedInDoctor");
            }
        }

        public NavigationService MainNavigationService
        {
            get { return mainNavigationService; }
            set
            {
                mainNavigationService = value;
                OnPropertyChanged("MainNavigationService");
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
            if (SelectedEmergencyAppointment == null)
            {
                SelectedAppointment.Appointment.AppointmentCause = AppointmentCause;
                SendScheduledNotification(SelectedAppointment.Appointment);
                DoctorAppointmentController.Instance.AddAppointment(SelectedAppointment.Appointment);
                //DoctorMainWindow.Instance._ViewModel.DoctorAppointments.Add(SelectedAppointment);
                // mislim da nece trebati
            }
            else
            {
                foreach (RescheduledAppointmentDTO rescheduled in SelectedEmergencyAppointment.RescheduledAppointments)
                {
                    SendRescheduleNotification(rescheduled.OldDocAppointment, rescheduled.NewDocAppointment);
                    DoctorAppointmentController.Instance.UpdateAppointment(rescheduled.OldDocAppointment, rescheduled.NewDocAppointment);
                }
                SelectedEmergencyAppointment.SuggestedAppointment.AppointmentCause = AppointmentCause;
                DoctorAppointmentController.Instance.AddAppointment(SelectedEmergencyAppointment.SuggestedAppointment);
            }

            DoctorMainWindow.Instance._ViewModel.PatientChartView._ViewModel.InsideNavigationService.Navigate(new ScheduledApp());
            this.MainNavigationService.Navigate(DoctorMainWindow.Instance._ViewModel.PatientChartView);
        }

        private void Execute_NavigateToPatientChartCommand(object obj)
        {
            this.MainNavigationService.Navigate(DoctorMainWindow.Instance._ViewModel.PatientChartView);
        }

        #endregion

        #region Methods
        private void SendRescheduleNotification(DoctorAppointment oldApp, DoctorAppointment appointment)
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

        private void SendScheduledNotification(DoctorAppointment appointment)
        {
            string title = "Zakazan pregled";

            string text = "Zakazan Vam je novi pregled " + appointment.AppointmentStart.ToString("dd.MM.yyyy.") + " u "
                + appointment.AppointmentStart.ToString("HH:mm") + " u " + appointment.AppointmentStart.ToString("HH:mm") + "h.";

            Notification notification = new Notification(title, text, DateTime.Now);
            notification.Recipients.Add(appointment.Patient.Id);
            //notification.Recipients.Add(appointment.Doctor.Id);

            NotificationController.Instance.AddNotification(notification);
        }

        #endregion

        #region Constructor
        public IssueInstructionViewModel()
        {
            this.LogedInDoctor = DoctorMainWindow.Instance._ViewModel.Doctor;
            this.IssueInstructionCommand = new RelayCommand(Execute_IssueInstructionCommand, CanExecute_Command);
            this.BackCommand = DoctorMainWindow.Instance._ViewModel.NavigateBackCommand;
            this.NavigateToPatientChartCommand = new RelayCommand(Execute_NavigateToPatientChartCommand, CanExecute_Command);
        }

        #endregion
    }
}