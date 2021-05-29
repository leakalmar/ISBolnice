using Hospital_IS.DoctorRole.Commands;
using Hospital_IS.DoctorRole.DoctorView;

namespace Hospital_IS.DoctorViewModel
{
    public class TherapyViewModel : BindableBase
    {
        #region Fields
        private bool started;
        public bool Started
        {
            get { return started; }
            set
            {
                started = value;
                OnPropertyChanged("Started");
            }
        }
        #endregion

        #region Commands
        private RelayCommand newTherapyCommand;

        public RelayCommand NewTherapyCommand
        {
            get { return newTherapyCommand; }
            set
            {
                newTherapyCommand = value;
                OnPropertyChanged("NewTherapyCommand");
            }
        }
        #endregion

        #region Actions
        private void Execute_NewTherapyCommand(object obj)
        {
            DoctorMainWindow.Instance._ViewModel.PatientChartView._ViewModel.InsideNavigationService.Navigate(new TherapyNew());
        }

        private bool CanExecute_NavigateCommand(object obj)
        {
            return true;
        }
        #endregion

        #region Constructor
        public TherapyViewModel()
        {
            this.NewTherapyCommand = new RelayCommand(Execute_NewTherapyCommand, CanExecute_NavigateCommand);
        }
        #endregion
    }
}
