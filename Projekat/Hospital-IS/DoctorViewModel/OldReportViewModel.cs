using Controllers;
using DTOs;
using Hospital_IS.Commands;
using Hospital_IS.DoctorView;
using Model;
using System.Collections.Generic;

namespace Hospital_IS.DoctorViewModel
{
    public class OldReportViewModel : BindableBase
    {
        #region Fields
        private string anamnesis;
        private ReportDTO report;
        private List<Prescription> prescriptions;
        private List<Test> tests;

        
        public string Anamnesis
        {
            get { return anamnesis; }
            set 
            { 
                anamnesis = value;
                OnPropertyChanged("Anamnesis");
            }
        }

        public ReportDTO Report
        {
            get { return report; }
            set { 
                report = value;
                Anamnesis = report.Anemnesis;
                Prescriptions = ChartController.Instance.GetPrescriptionsForReport(DoctorMainWindow.Instance._ViewModel.PatientChartView._ViewModel.Patient, report.AppointmentStart);
                //isto zaa testove kada budes pravila
                OnPropertyChanged("Report");
            }
        }

        public List<Prescription> Prescriptions
        {
            get { return prescriptions; }
            set
            {
                prescriptions = value;
                OnPropertyChanged("Prescriptions");
            }
        }

        public List<Test> Tests
        {
            get { return tests; }
            set
            {
                tests = value;
                OnPropertyChanged("Tests");
            }
        }
        #endregion

        #region Commands
        private RelayCommand saveCommand;

        public RelayCommand SaveCommand
        {
            get { return saveCommand; }
            set { saveCommand = value; }
        }
        #endregion

        #region Actions
        private bool CanExecute_Command(object obj)
        {
            return true;
        }

        private void Execute_SaveCommand(object obj)
        {
            this.Report.Anemnesis = this.Anamnesis;
            ChartController.Instance.UpdateReport(DoctorMainWindow.Instance._ViewModel.PatientChartView._ViewModel.Patient, Report);
        }
        #endregion

        #region Constructor
        public OldReportViewModel()
        {
            this.SaveCommand = new RelayCommand(Execute_SaveCommand,CanExecute_Command);
        }
        #endregion
    }
}
