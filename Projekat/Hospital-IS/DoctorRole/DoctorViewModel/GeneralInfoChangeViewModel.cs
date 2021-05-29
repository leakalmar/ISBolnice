using Hospital_IS.Controllers;
using Hospital_IS.DoctorRole.Commands;
using Hospital_IS.DoctorRole.DoctorView;
using Hospital_IS.DTOs.SecretaryDTOs;

namespace Hospital_IS.DoctorRole.DoctorViewModel
{
    public class GeneralInfoChangeViewModel : BindableBase
    {
        #region Feilds
        private PatientDTO patient;

        public PatientDTO Patient
        {
            get { return patient; }
            set
            {
                patient = value;
                OnPropertyChanged("Patient");
            }
        }

        #endregion

        #region Commands
        private RelayCommand saveCommand;
        private RelayCommand cancelCommand;

        public RelayCommand SaveCommand
        {
            get { return saveCommand; }
            set
            {
                saveCommand = value;
            }
        }

        public RelayCommand CancelCommand
        {
            get { return cancelCommand; }
            set
            {
                cancelCommand = value;
            }
        }
        #endregion

        #region Actions

        private void Execute_SaveCommand(object obj)
        {
            SecretaryManagementController.Instance.UpdatePatient(Patient);
            DoctorMainWindow.Instance._ViewModel.PatientChartView._ViewModel.ChangeCommand.Execute("1");
        }
        private void Execute_CancelCommand(object obj)
        {
            DoctorMainWindow.Instance._ViewModel.PatientChartView._ViewModel.ChangeCommand.Execute("1");
        }


        private bool CanExecute_NavigateCommand(object obj)
        {
            return true;
        }
        #endregion

        #region Constructor
        public GeneralInfoChangeViewModel()
        {
            this.SaveCommand = new RelayCommand(Execute_SaveCommand, CanExecute_NavigateCommand);
            this.CancelCommand = new RelayCommand(Execute_CancelCommand, CanExecute_NavigateCommand);
            Patient = DoctorMainWindow.Instance._ViewModel.PatientChartView._ViewModel.Patient;
        }
        #endregion
    }
}
