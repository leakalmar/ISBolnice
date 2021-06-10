using Controllers;
using DTOs;
using Hospital_IS.Controllers;
using Hospital_IS.DoctorRole.Commands;
using Hospital_IS.DoctorRole.DoctorView;
using Hospital_IS.DTOs.SecretaryDTOs;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Navigation;

//MVVM
namespace Hospital_IS.DoctorViewModel
{
    public class PatientChartViewModel : BindableBase
    {
        #region Feilds
        private PatientDTO patient;
        private AppointmentRowDTO selectedAppointment;
        private NavigationService insideNavigationService;
        private int lastTab = 0;
        private Thickness margin = new Thickness(0);
        private bool reportFocused;
        private bool generalFocused;
        private bool backButton;
        private bool started = false;
        private bool prescriptionReview;
        private int patientYears;

        private static PatientChartViewModel instance;

        public static PatientChartViewModel Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new PatientChartViewModel();
                }
                return instance;
            }
        }

        public bool ReportFocused
        {
            get { return reportFocused; }
            set
            {
                reportFocused = value;
                OnPropertyChanged("ReportFocused");
            }
        }

        public bool GeneralFocused
        {
            get { return generalFocused; }
            set
            {
                generalFocused = value;
                OnPropertyChanged("GeneralFocused");
            }
        }


        public NavigationService InsideNavigationService
        {
            get { return insideNavigationService; }
            set
            {
                insideNavigationService = value;
            }
        }

        public PatientDTO Patient
        {
            get { return patient; }
            set
            {
                patient = value;
                PatientYears = (new DateTime(1, 1, 1).Add(DateTime.Now - Patient.BirthDate)).Year - 1;
                SetFirstPageNavigation();
                OnPropertyChanged("Patient");
            }
        }

        public AppointmentRowDTO SelectedAppointment
        {
            get { return selectedAppointment; }
            set
            {
                selectedAppointment = value;
                OnPropertyChanged("SelectedAppointment");
                if(value  != null)
                {
                    SetFields();
                }
            }
        }

        public int LastTab
        {
            get { return lastTab; }
            set
            {
                lastTab = value;
                OnPropertyChanged("LastTab");
            }
        }

        public Thickness Margin
        {
            get { return margin; }
            set
            {
                margin = value;
                OnPropertyChanged("Margin");
            }
        }

        public bool BackButton
        {
            get { return backButton; }
            set
            {
                backButton = value;
                OnPropertyChanged("BackButton");
            }
        }

        public bool Started
        {
            get { return started; }
            set
            {
                started = value;
                BackButton = !value;
                PrescriptionReview = false;

                OnPropertyChanged("Started");
            }
        }

        public bool PrescriptionReview
        {
            get { return prescriptionReview; }
            set
            {
                prescriptionReview = value;
                if (value)
                {
                    BackButton = false;
                }
                OnPropertyChanged("PrescriptionReview");
            }
        }
        public int PatientYears
        {
            get { return patientYears; }
            set
            {
                patientYears = value;
                OnPropertyChanged("PatientYears");
            }
        }
        #endregion

        #region ViewModels
        private SearchMedicineViewModel searchMedicineViewModel;
        private ReportViewModel reportViewModel;
        public SearchMedicineViewModel SearchMedicineViewModel
        {
            get { return searchMedicineViewModel; }
            set
            {
                searchMedicineViewModel = value;
                OnPropertyChanged("SearchMedicineView");
            }
        }
        public ReportViewModel ReportViewModel
        {
            get { return reportViewModel; }
            set
            {
                reportViewModel = value;
                OnPropertyChanged("ReportView");
            }
        }


        #endregion

        #region Commands
        private RelayCommand endAppointmentCommand;
        private RelayCommand changeCommand;
        private RelayCommand addCommand;
        private RelayCommand navigateBackCommand;
        private RelayCommand prescriptionReviewCommand;

        public RelayCommand EndAppointmentCommand
        {
            get { return endAppointmentCommand; }
            set
            {
                endAppointmentCommand = value;
            }
        }

        public RelayCommand ChangeCommand
        {
            get { return changeCommand; }
            set { changeCommand = value; }
        }

        public RelayCommand AddCommand
        {
            get { return addCommand; }
            set { addCommand = value; }
        }

        public RelayCommand NavigateBackCommand
        {
            get { return navigateBackCommand; }
            set { navigateBackCommand = value; }
        }

        public RelayCommand PrescriptionReviewCommand
        {
            get { return prescriptionReviewCommand; }
            set { prescriptionReviewCommand = value; }
        }


        #endregion

        #region Actions
        private void Execute_ChangeCommand(object obj)
        {

            int index = Int32.Parse(((string)obj));

            bool tempStarted = false;
            if (SelectedAppointment == null)
            {
                Margin = new Thickness(110 * index - 110, 0, 0, 0);
            }
            else
            {

                if (SelectedAppointment.IsStarted == true)
                {
                    tempStarted = true;
                    Margin = new Thickness(110 * index, 0, 0, 0);
                }
                else if (SelectedAppointment.IsStarted == false)
                {
                    Margin = new Thickness(110 * index - 110, 0, 0, 0);
                }
            }
            GeneralFocused = false;
            ReportFocused = false;

            switch (index)
            {
                case 0:
                    ReportFocused = true;
                    DoctorInsideNavigationController.Instance.NavigateToReportCommand(reportViewModel);
                    break;
                case 1:
                    GeneralFocused = true;
                    GeneralInfoViewModel viewModel = new GeneralInfoViewModel();
                    viewModel.Started = tempStarted;
                    viewModel.Patient = Patient;
                    DoctorInsideNavigationController.Instance.NavigateToGeneralCommand(viewModel);
                    break;
                case 2:
                    HistoryViewModel historyViewModel = new HistoryViewModel();
                    historyViewModel.Patient = Patient;
                    DoctorInsideNavigationController.Instance.NavigateToHistoryCommand(historyViewModel);
                    break;
                case 3:
                    ScheduleAppointmentViewModel scheduleAppointmentViewModel = new ScheduleAppointmentViewModel();
                    scheduleAppointmentViewModel.Started = tempStarted;
                    DoctorInsideNavigationController.Instance.NavigateToSheduledAppointmentsCommand(scheduleAppointmentViewModel);
                    break;
                case 4:
                    TherapyViewModel therapyViewModel = new TherapyViewModel();
                    therapyViewModel.Started = tempStarted;
                    DoctorInsideNavigationController.Instance.NavigateToTherapiesCommand(therapyViewModel);
                    break;
                case 5:
                    TestsViewModel testsViewModel = new TestsViewModel();
                    testsViewModel.Started = tempStarted;
                    DoctorInsideNavigationController.Instance.NavigateToTestsCommand(testsViewModel);
                    break;
                case 6:
                    HospitalizationsViewModel hospitalizationsViewModel = new HospitalizationsViewModel();
                    hospitalizationsViewModel.Started = tempStarted;
                    DoctorInsideNavigationController.Instance.NavigateToHospitalizationsCommand(hospitalizationsViewModel);
                    break;
            }
        }
        private bool CanExecute_Command(object obj)
        {
            return true;
        }

        private void Execute_EndAppointmentCommand(object obj)
        {
            bool dialog = (bool)new ExitMess("Da li želite da završite termin?", "yesNo").ShowDialog();
            if (dialog)
            {
                DoctorAppointmentDTO selectedAppointment = SelectedAppointment.Appointment;
                DoctorAppointmentController.Instance.EndAppointment(selectedAppointment);
                selectedAppointment.IsFinished = true;
                DoctorMainWindowModel.Instance.Adapter.UpdateDoctorAppointment(selectedAppointment, selectedAppointment);
                ReportDTO reportDTO = new ReportDTO(selectedAppointment.AppointmentStart, selectedAppointment.Doctor.Name, selectedAppointment.Doctor.Surname, selectedAppointment.Type, selectedAppointment.AppointmentCause, ReportViewModel.Anemnesis, ReportViewModel.Prescriptions.Count, selectedAppointment.Patient.Id);
                ChartController.Instance.AddReport(reportDTO);
                ChartController.Instance.AddPrescriptions(new List<PrescriptionDTO>(ReportViewModel.Prescriptions), SelectedAppointment.Appointment.Patient);
                DoctorNavigationController.Instance.NavigateToAppDetailCommand();
                this.Started = false;
            }
        }

        private void Execute_PrescriptionReviewCommand(object obj)
        {
            DoctorNavigationController.Instance.NavigateTIssuePrescriptionCommand();
        }

        private void Execute_AddCommand(object obj)
        {
            switch (DoctorInsideNavigationController.Instance.NavigationService.Content.GetType().Name)
            {
                case "Report":
                    DoctorNavigationController.Instance.NavigateToSearchMedicineCommand(ReportViewModel.Prescriptions);
                    break;
                default:
                    break;
            }

        }

        private void Execute_NavigateBackCommand(object obj)
        {
            switch (DoctorInsideNavigationController.Instance.NavigationService.Content.GetType().Name)
            {
                case "Report":
                case "GeneralInfo":
                case "History":
                case "ScheduledApp":
                case "Therapies":
                case "Tests":
                case "Hospitalizations":
                    DoctorNavigationController.Instance.NavigateBackCommand();
                    break;
                default:
                    DoctorInsideNavigationController.Instance.NavigationService.GoBack();
                    break;
            }

        }

        #endregion

        #region Methods
        private void SetFirstPageNavigation()
        {
            if (SelectedAppointment == null)
            {
                GeneralFocused = true;
                ReportFocused = false;
                GeneralInfoViewModel viewModel = new GeneralInfoViewModel();
                viewModel.Started = Started;
                viewModel.Patient = Patient;
                DoctorInsideNavigationController.Instance.NavigateToGeneralCommand(viewModel);
            }
            else
            {
                if (SelectedAppointment.IsStarted == true)
                {
                    GeneralFocused = false;
                    ReportFocused = true;
                    DoctorInsideNavigationController.Instance.NavigateToReportCommand(ReportViewModel);
                }
                else
                {
                    GeneralFocused = true;
                    ReportFocused = false;
                    GeneralInfoViewModel viewModel = new GeneralInfoViewModel();
                    viewModel.Started = Started;
                    viewModel.Patient = Patient;
                    DoctorInsideNavigationController.Instance.NavigateToGeneralCommand(viewModel);
                }
            }
        }

        private void SetFields()
        {
            Patient = SelectedAppointment.Appointment.Patient;
            SearchMedicineViewModel.Patient = Patient;
            SearchMedicineViewModel.DatePrescribed = SelectedAppointment.Appointment.AppointmentStart;
            Started = SelectedAppointment.IsStarted;
        }
        #endregion

        #region Constructor
        public PatientChartViewModel()
        {
            this.ReportViewModel = new ReportViewModel();
            this.SearchMedicineViewModel = new SearchMedicineViewModel();
            Started = false;
            this.NavigateBackCommand = new RelayCommand(Execute_NavigateBackCommand, CanExecute_Command);
            this.AddCommand = new RelayCommand(Execute_AddCommand, CanExecute_Command);
            this.ChangeCommand = new RelayCommand(Execute_ChangeCommand, CanExecute_Command);
            this.EndAppointmentCommand = new RelayCommand(Execute_EndAppointmentCommand, CanExecute_Command);
            this.PrescriptionReviewCommand = new RelayCommand(Execute_PrescriptionReviewCommand, CanExecute_Command);
        }
        #endregion

    }
}
