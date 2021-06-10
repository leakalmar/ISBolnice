using Controllers;
using DTOs;
using Hospital_IS.DoctorRole.Commands;
using Model;
using System.Collections.Generic;

//MVVM
namespace Hospital_IS.DoctorViewModel
{
    public class OldReportViewModel : BindableBase
    {
        #region Fields
        private string anamnesis;
        private ReportDTO report;
        private List<PrescriptionDTO> prescriptions;
        private List<Test> tests;
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
                Prescriptions = ChartController.Instance.GetPrescriptionsForReport(PatientChartViewModel.Instance.Patient.Id, report.AppointmentStart);
                Tests = ChartController.Instance.GetTestsForReport(PatientChartViewModel.Instance.Patient.Id, report.AppointmentStart);
                OnPropertyChanged("Report");
            }
        }

        public List<PrescriptionDTO> Prescriptions
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
        private RelayCommand navigateBackCommand;

        public RelayCommand SaveCommand
        {
            get { return saveCommand; }
            set { saveCommand = value; }
        }

        public RelayCommand NavigateBackCommand
        {
            get { return navigateBackCommand; }
            set { navigateBackCommand = value; }
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
            ChartController.Instance.UpdateReport(PatientChartViewModel.Instance.Patient.Id, Report);
        }

        private void Execute_NavigateBackCommand(object obj)
        {
            DoctorNavigationController.Instance.NavigateBackWithoutCheckCommand();
        }
        #endregion

        #region Constructor
        public OldReportViewModel()
        {
            this.Focused = true;
            this.SaveCommand = new RelayCommand(Execute_SaveCommand,CanExecute_Command);
            this.NavigateBackCommand = new RelayCommand(Execute_NavigateBackCommand, CanExecute_Command);
        }
        #endregion
    }
}
