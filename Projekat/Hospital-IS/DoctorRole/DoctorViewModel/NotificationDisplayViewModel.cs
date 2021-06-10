using Hospital_IS.DoctorRole.Commands;
using Hospital_IS.DTOs.SecretaryDTOs;

//MVVM
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
            DoctorNavigationController.Instance.NavigateToNotificationsCommand();
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
