using Controllers;
using DTOs;
using Hospital_IS.DoctorRole.Commands;
using Hospital_IS.DoctorRole.DoctorView;
using Hospital_IS.DTOs.SecretaryDTOs;
using System.Collections.ObjectModel;
using System.Windows.Navigation;

namespace Hospital_IS.DoctorViewModel
{
    public class HistoryViewModel : BindableBase
    {
        #region Fields
        private bool started;
        private PatientDTO patient;
        private ObservableCollection<ReportDTO> reports;
        private ReportDTO selectedReport;
        private NavigationService insideNavigationService;

        public NavigationService InsideNavigationService
        {
            get { return insideNavigationService; }
            set
            {
                insideNavigationService = value;
            }
        }

        public bool Started
        {
            get { return started; }
            set
            {
                started = value;
                OnPropertyChanged("Started");
            }
        }

        public PatientDTO Patient
        {
            get { return patient; }
            set
            {
                patient = value;
                this.Reports = new ObservableCollection<ReportDTO>(ChartController.Instance.GetReportsByPatient(Patient.Id));
                OnPropertyChanged("Patient");

            }
        }
        public ObservableCollection<ReportDTO> Reports
        {
            get { return reports; }
            set
            {
                reports = value;
                OnPropertyChanged("Reports");
            }
        }

        public ReportDTO SelectedReport
        {
            get { return selectedReport; }
            set
            {
                selectedReport = value;
                OnPropertyChanged("SelectedReport");
            }
        }
        #endregion

        #region Commands
        private RelayCommand oldReportCommand;

        public RelayCommand OldReportCommand
        {
            get { return oldReportCommand; }
            set
            {
                oldReportCommand = value;
            }
        }

        #endregion

        #region Actions

        private bool CanExecute_Command(object obj)
        {
            return true;
        }

        private void Execute_OldReportCommand(object obj)
        {

            OldReport view = new OldReport();
            view._ViewModel.Report = SelectedReport;
            this.InsideNavigationService.Navigate(view);
        }

        #endregion

        #region Constructor
        public HistoryViewModel()
        {
            this.OldReportCommand = new RelayCommand(Execute_OldReportCommand, CanExecute_Command);

        }
        #endregion
    }
}
