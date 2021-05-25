using Controllers;
using Hospital_IS.Commands;
using Hospital_IS.DoctorView;
using Model;
using System.Collections.ObjectModel;
using System.Windows.Navigation;

namespace Hospital_IS.DoctorViewModel
{
    public class ScheduleAppointmentViewModel : BindableBase
    {
        #region Fields
        private ObservableCollection<DoctorAppointment> scheduledAppointments;
        private NavigationService mainNavigationService;
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

        public NavigationService MainNavigationService
        {
            get { return mainNavigationService; }
            set
            {
                mainNavigationService = value;
            }
        }

        public ObservableCollection<DoctorAppointment> ScheduledAppointments
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

            this.MainNavigationService.Navigate(new NewApp());
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
            Patient patient = DoctorMainWindow.Instance._ViewModel.PatientChartView._ViewModel.Patient;
            this.ScheduledAppointments = new ObservableCollection<DoctorAppointment>(DoctorAppointmentController.Instance.GetFutureAppointmentsByPatient(patient.Id));
            this.MainNavigationService = DoctorMainWindow.Instance._ViewModel.NavigationService;
        }
        #endregion
    }
}
