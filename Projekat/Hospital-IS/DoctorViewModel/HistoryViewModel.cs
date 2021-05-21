using Controllers;
using Hospital_IS.Commands;
using Hospital_IS.DoctorView;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Navigation;

namespace Hospital_IS.DoctorViewModel
{
    public class HistoryViewModel : BindableBase
    {
        #region
        private DateTime appointmentStart;
        private bool started;
        private Patient patient;
        private ObservableCollection<Report> reports;
        private Report selectedReport;
        private NavigationService insideNavigationService;

        public NavigationService InsideNavigationService
        {
            get { return insideNavigationService; }
            set
            {
                insideNavigationService = value;
            }
        }
        public DateTime AppointmentStart
        {
            get { return appointmentStart; }
            set
            {
                appointmentStart = value;
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

        public Patient Patient
        {
            get { return patient; }
            set
            {
                patient = value;
                this.Reports = new ObservableCollection<Report>(ChartController.Instance.GetReportsByPatient(Patient));
                OnPropertyChanged("Patient");

            }
        }
        public ObservableCollection<Report> Reports
        {
            get { return reports; }
            set
            {
                reports = value;
                OnPropertyChanged("Reports");
            }
        }

        public Report SelectedReport
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

            UCOldReport view = new UCOldReport();
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
