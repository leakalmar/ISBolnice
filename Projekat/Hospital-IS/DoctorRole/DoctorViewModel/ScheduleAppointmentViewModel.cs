using Controllers;
using DTOs;
using Hospital_IS.DoctorRole.Commands;
using Hospital_IS.DoctorRole.DoctorConverters;
using Hospital_IS.DTOs.SecretaryDTOs;
using System.Collections.ObjectModel;

//MVVM
namespace Hospital_IS.DoctorViewModel
{
    public class ScheduleAppointmentViewModel : BindableBase
    {
        #region Fields
        private ObservableCollection<DoctorAppointmentDTO> scheduledAppointments;
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

        public ObservableCollection<DoctorAppointmentDTO> ScheduledAppointments
        {
            get { return scheduledAppointments; }
            set
            {
                scheduledAppointments = value;
                OnPropertyChanged("ScheduledAppointment");
            }
        }
        #endregion

        #region Commands
        private RelayCommand addNewCommand;

        public RelayCommand AddNewCommand
        {
            get { return addNewCommand; }
            set { addNewCommand = value; }
        }
        #endregion

        #region Actions

        private void Execute_AddNewCommand(object obj)
        {
            DoctorNavigationController.Instance.NavigateToNewAppointment();
        }

        private bool CanExecute_AddNewCommand(object obj)
        {
            return true;
        }
        #endregion

        #region Constructor
        public ScheduleAppointmentViewModel()
        
        {
            this.AddNewCommand = new RelayCommand(Execute_AddNewCommand, CanExecute_AddNewCommand);
            PatientDTO patient = PatientChartViewModel.Instance.Patient;
            this.ScheduledAppointments = new DoctorAppointmentConverter().ConvertAppointmentsToDTO(DoctorAppointmentController.Instance.GetFutureAppointmentsByPatient(patient.Id));
        }
        #endregion
    }
}
