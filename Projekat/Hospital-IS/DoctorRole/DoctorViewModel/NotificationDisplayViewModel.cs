using Hospital_IS.DoctorRole.Commands;
using Hospital_IS.DoctorRole.DoctorView;
using Hospital_IS.DoctorViewModel;
using Hospital_IS.DTOs.SecretaryDTOs;
using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital_IS.DoctorRole.DoctorViewModel
{
    public class NotificationDisplayViewModel : BindableBase
    {
        #region Fields
        private NotificationDTO selectedNotification;

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
        private RelayCommand backCommand;

        public RelayCommand BackCommand
        {
            get { return backCommand; }
            set { backCommand = value; }
        }
        #endregion

        #region Actions
        private void Execute_BackCommand(object obj)
        {
            DoctorMainWindowModel.Instance.NavigationService.Navigate(new DoctorNotifications());
        }


        private bool CanExecute_Command(object obj)
        {
            return true;
        }
        #endregion

        #region Constructor
        public NotificationDisplayViewModel()
        {
            this.BackCommand = new RelayCommand(Execute_BackCommand, CanExecute_Command);
        }
        #endregion
    }
}
