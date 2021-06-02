using Controllers;
using DTOs;
using Hospital_IS.DoctorRole.Commands;
using Hospital_IS.DoctorRole.DoctorView;
using Hospital_IS.DTOs.SecretaryDTOs;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Navigation;

namespace Hospital_IS.DoctorViewModel
{
    public class HistoryViewModel : BindableBase
    {
        #region Fields
        private bool started;
        private PatientDTO patient;
        private ICollectionView reports;
        private ReportDTO selectedReport;
        private NavigationService insideNavigationService;
        private DateTime fromDate;
        private DateTime toDate;

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
                InitializeFields();
                OnPropertyChanged("Patient");

            }
        }
        public ICollectionView Reports
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

        public DateTime FromDate
        {
            get { return fromDate; }
            set
            {
                fromDate = value;
                FilterHistory();
                OnPropertyChanged("FromDate");
            }
        }

        public DateTime ToDate
        {
            get { return toDate; }
            set
            {
                toDate = value;
                FilterHistory();
                OnPropertyChanged("ToDate");
            }
        }
        #endregion

        #region Commands
        private RelayCommand oldReportCommand;
        private RelayCommand printCommand;

        public RelayCommand OldReportCommand
        {
            get { return oldReportCommand; }
            set { oldReportCommand = value; }
        }

        public RelayCommand PrintCommand
        {
            get { return printCommand; }
            set { printCommand = value; }
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

        private void Execute_PrintCommand(object obj)
        {
        }
        #endregion

        #region Methods
        private void FilterHistory()
        {
            if (FromDate != null && ToDate != null)
            {
                List<ReportDTO> app = ChartController.Instance.GetReportsByPatient(Patient.Id);
                ICollectionView view = new CollectionViewSource { Source = app }.View;
                view.Filter = null;
                view.Filter = delegate (object item)
                {
                    return ((ReportDTO)item).AppointmentStart.Date <= ToDate.Date & ((ReportDTO)item).AppointmentStart.Date >= FromDate.Date;
                };

                Reports = view;
            }
        }

        private void InitializeFields()
        {
            ToDate = DateTime.Now;
            FromDate = DateTime.Now.AddMonths(-1);
            FilterHistory();
        }
        #endregion

        #region Constructor
        public HistoryViewModel()
        {
            this.OldReportCommand = new RelayCommand(Execute_OldReportCommand, CanExecute_Command);
            this.PrintCommand = new RelayCommand(Execute_PrintCommand, CanExecute_Command);
        }
        #endregion
    }
}
