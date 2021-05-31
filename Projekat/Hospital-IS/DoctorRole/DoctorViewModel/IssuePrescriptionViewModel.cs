using Controllers;
using Hospital_IS.Controllers;
using Hospital_IS.DoctorRole.Commands;
using Hospital_IS.DoctorRole.DoctorView;
using Model;
using System;
using System.Collections.Generic;

namespace Hospital_IS.DoctorRole.DoctorViewModel
{
    public class IssuePrescriptionViewModel : BindableBase
    {
        #region Fields
        private List<Notification> notifications;
        private Notification selectedNotification;

        public List<Notification> Notifications
        {
            get { return notifications; }
            set
            {
                notifications = value;
                OnPropertyChanged("Notifications");
            }
        }

        public Notification SelectedNotification
        {
            get { return selectedNotification; }
            set
            {
                selectedNotification = value;
                OnPropertyChanged("SelectedNotification");
            }
        }
        #endregion

        #region Commands
        private RelayCommand openChartCommand;

        public RelayCommand OpenChartCommand
        {
            get { return openChartCommand; }
            set { openChartCommand = value; }
        }
        #endregion

        #region Actions
        private void Execute_OpenChartCommand(object obj)
        {
            PatientChart view = new PatientChart();
            view._ViewModel.Patient = SecretaryManagementController.Instance.GetPatientByID(1);
            view._ViewModel.PrescriptionReview = true;
            DoctorMainWindow.Instance._ViewModel.PatientChartView = view;
            DoctorMainWindow.Instance._ViewModel.NavigateToChartCommand.Execute(obj);
        }

        private bool CanExecute_Command(object obj)
        {
            return true;
        }

        #endregion

        #region Constructor
        public IssuePrescriptionViewModel(bool show)
        {
            this.OpenChartCommand = new RelayCommand(Execute_OpenChartCommand, CanExecute_Command);
            this.Notifications = NotificationController.Instance.GetPrescriptionNotification(DoctorMainWindow.Instance._ViewModel.Doctor.Id);
            if (show)
            {
                this.Notifications = new List<Notification>();
                this.Notifications.Add(new Notification("Izdavanje recepta: Simona Stantic", "Neophodna obnova recepta za Brufen", DateTime.Now));
            }
            
        }
        #endregion
    }
}
