using Hospital_IS.Controllers;
using Hospital_IS.DoctorRole.Commands;
using Hospital_IS.DoctorViewModel;
using Hospital_IS.DTOs.SecretaryDTOs;

//MVVM
namespace Hospital_IS.DoctorRole.DoctorViewModel
{
    public class GeneralInfoChangeViewModel : BindableBase
    {
        #region Feilds
        private PatientDTO patient;
        private bool focused;

        public bool Focused
        {
            get { return focused; }
            set
            {
                focused = value;
                OnPropertyChanged("Focused");
            }
        }

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
            patient.Validate();
            if (patient.IsValid)
            {
                SecretaryManagementController.Instance.UpdatePatient(Patient);
                PatientChartViewModel.Instance.ChangeCommand.Execute("1");
            }
        }
        private void Execute_CancelCommand(object obj)
        {
            PatientChartViewModel.Instance.ChangeCommand.Execute("1");
        }


        private bool CanExecute_NavigateCommand(object obj)
        {
            return true;
        }
        #endregion

        #region Constructor
        public GeneralInfoChangeViewModel()
        {
            this.Focused = true;
            this.SaveCommand = new RelayCommand(Execute_SaveCommand, CanExecute_NavigateCommand);
            this.CancelCommand = new RelayCommand(Execute_CancelCommand, CanExecute_NavigateCommand);
            Patient = PatientChartViewModel.Instance.Patient;
        }
        #endregion
    }
}
