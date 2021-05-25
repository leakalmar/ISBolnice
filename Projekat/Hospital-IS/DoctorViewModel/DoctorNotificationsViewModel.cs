using Hospital_IS.Commands;
using Hospital_IS.Controllers;
using Hospital_IS.DoctorView;
using Hospital_IS.DTOs.SecretaryDTOs;
using Model;
using System.Collections.Generic;

namespace Hospital_IS.DoctorViewModel
{
    public class DoctorNotificationsViewModel : BindableBase
    {
        #region Fields
        private List<NotificationDTO> notifications;
        private NotificationDTO selectedNotification;

        public List<NotificationDTO> Notifications
        {
            get { return notifications; }
            set
            {
                notifications = value;
                OnPropertyChanged("Notifications");
            }
        }

        public NotificationDTO SelectedNotification
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
        private RelayCommand showNotificationCommand;

        public RelayCommand ShowNotificationCommand
        {
            get { return showNotificationCommand; }
            set { showNotificationCommand = value; }
        }

        #endregion

        #region Actions
        private void Execute_ShowNotificationCommand(object obj)
        {
            if (SelectedNotification != null)
            {
                NotificationView nv = new NotificationView(SelectedNotification);
                nv.Show();
            }
        }

        private bool CanExecute_NavigateCommand(object obj)
        {
            return true;
        }
        #endregion

        #region Constructor
        public DoctorNotificationsViewModel()
        {
            this.Notifications = SecretaryManagementController.Instance.GetAllNotifications();
            //Notifications = NotificationController.Instance.GetAllByUser(DoctorHomePage.Instance.Doctor.Id);
            this.ShowNotificationCommand = new RelayCommand(Execute_ShowNotificationCommand, CanExecute_NavigateCommand);
        }
        #endregion
    }
}
