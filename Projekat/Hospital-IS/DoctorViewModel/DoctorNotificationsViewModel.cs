using Hospital_IS.Commands;
using Hospital_IS.Controllers;
using Hospital_IS.DoctorView;
using Model;
using System.Collections.Generic;

namespace Hospital_IS.DoctorViewModel
{
    public class DoctorNotificationsViewModel : BindableBase
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
            this.Notifications = NotificationController.Instance.GetAll();
            //Notifications = NotificationController.Instance.GetAllByUser(DoctorHomePage.Instance.Doctor.Id);
            this.ShowNotificationCommand = new RelayCommand(Execute_ShowNotificationCommand, CanExecute_NavigateCommand);
        }
        #endregion
    }
}
