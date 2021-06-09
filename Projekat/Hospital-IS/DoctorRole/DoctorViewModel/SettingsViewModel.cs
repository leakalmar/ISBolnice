using Hospital_IS.DoctorRole.Commands;
using Hospital_IS.DoctorViewModel;
using Hospital_IS.DTOs;

//MVVM
namespace Hospital_IS.DoctorRole.DoctorViewModel
{
    public class SettingsViewModel : BindableBase
    {
        #region Feilds
        private bool isEnabled;
        private DoctorDTO doctor;
        private string newPassword;
        private string oldPassword;
        private string newAgainPassword;

        public bool IsEnabled
        {
            get { return isEnabled; }
            set 
            {
                isEnabled = value;
                OnPropertyChanged("IsEnabled");
            }
        }
        public DoctorDTO Doctor
        {
            get { return doctor; }
            set
            {
                doctor = value;
                OnPropertyChanged("Doctor");
            }
        }
        public string NewPassword
        {
            get { return newPassword; }
            set
            {
                newPassword = value;
                OnPropertyChanged("NewPassword");
            }
        }
        public string OldPassword
        {
            get { return oldPassword; }
            set
            {
                oldPassword = value;
                OnPropertyChanged("OldPassword");
            }
        }
        public string NewAgainPassword
        {
            get { return newAgainPassword; }
            set
            {
                newAgainPassword = value;
                OnPropertyChanged("NewAgainPassword");
            }
        }
        #endregion

        #region Commands
        private RelayCommand changePasswordCommand;
        private RelayCommand savePasswordCommand;
        private RelayCommand cancelPasswordCommand;
        private RelayCommand feedbackCommand;


        public RelayCommand ChangePasswordCommand
        {
            get { return changePasswordCommand; }
            set { changePasswordCommand = value; }
        }
        public RelayCommand SavePasswordCommand
        {
            get { return savePasswordCommand; }
            set { savePasswordCommand = value; }
        }
        public RelayCommand CancelPasswordCommand
        {
            get { return cancelPasswordCommand; }
            set { cancelPasswordCommand = value; }
        }
        public RelayCommand FeedBackCommand
        {
            get { return feedbackCommand; }
            set { feedbackCommand = value; }
        }
        #endregion

        #region Actions
        private void Execute_ChangePasswordCommand(object obj)
        {
            IsEnabled = true;
        }
        private void Execute_SavePasswordCommand(object obj)
        {
            this.NewPassword = "";
            this.OldPassword = "";
            this.NewAgainPassword = "";
            IsEnabled = false;
        }
        private void Execute_CancelPasswordCommand(object obj)
        {
            this.NewPassword = "";
            this.OldPassword = "";
            this.NewAgainPassword = "";
            IsEnabled = false;
        }
        private void Execute_FeedBackCommand(object obj)
        {
            DoctorNavigationController.Instance.NavigateToFeedbackCommand();
        }
        private bool CanExecute_Command(object obj)
        {
            return true;
        }
        #endregion

        #region Constructor
        public SettingsViewModel()
        {
            this.IsEnabled = false;
            this.NewPassword = "";
            this.OldPassword = "";
            this.NewAgainPassword = "";
            this.Doctor = DoctorMainWindowModel.Instance.Doctor;
            this.CancelPasswordCommand = new RelayCommand(Execute_CancelPasswordCommand, CanExecute_Command);
            this.ChangePasswordCommand = new RelayCommand(Execute_ChangePasswordCommand, CanExecute_Command);
            this.SavePasswordCommand = new RelayCommand(Execute_SavePasswordCommand, CanExecute_Command);
            this.FeedBackCommand = new RelayCommand(Execute_FeedBackCommand, CanExecute_Command);
        }
        #endregion

    }
}
