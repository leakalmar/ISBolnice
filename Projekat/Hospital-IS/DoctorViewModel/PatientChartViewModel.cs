using Controllers;
using DTOs;
using Hospital_IS.Commands;
using Hospital_IS.Controllers;
using Hospital_IS.DoctorView;
using Hospital_IS.DTOs.SecretaryDTOs;
using Model;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Navigation;

namespace Hospital_IS.DoctorViewModel
{
    public class PatientChartViewModel : BindableBase
    {
        #region Feilds
        private PatientDTO patient;
        private AppointmentRowDTO selectedAppointment;
        private NavigationService mainNavigationService;
        private NavigationService insideNavigationService;
        private int lastTab = 0;
        private Thickness margin = new Thickness(0);

        private bool started;

        public NavigationService MainNavigationService
        {
            get { return mainNavigationService; }
            set
            {
                mainNavigationService = value;
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
                SetFields();
                OnPropertyChanged("SelectedAppointment");
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

        public bool Started
        {
            get { return started; }
            set
            {
                started = value;
                OnPropertyChanged("Started");
            }
        }
        #endregion

        #region Views
        private SearchMedicine searchMedicineView;
        private ReportView reportView;
        public SearchMedicine SearchMedicineView
        {
            get { return searchMedicineView; }
            set
            {
                searchMedicineView = value;
                OnPropertyChanged("SearchMedicineView");
            }
        }
        public ReportView ReportView
        {
            get { return reportView; }
            set
            {
                reportView = value;
                OnPropertyChanged("ReportView");
            }
        }


        #endregion

        #region Commands
        private RelayCommand endAppointmentCommand;
        private RelayCommand changeCommand;
        private RelayCommand addCommand;

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


        #endregion

        #region Actions
        private void Execute_ChangeCommand(object obj)
        {

            int index = Int32.Parse(((string)obj));

            Started = false;
            if (SelectedAppointment == null)
            {
                Margin = new Thickness(110 * index - 110, 0, 0, 0);
            }
            else
            {

                if (SelectedAppointment.IsStarted == true)
                {
                    Started = true;
                    Margin = new Thickness(110 * index, 0, 0, 0);
                }
                else if (SelectedAppointment.IsStarted == false)
                {
                    Margin = new Thickness(110 * index - 110, 0, 0, 0);
                }
            }

            switch (index)
            {
                case 0:
                    this.InsideNavigationService.Navigate(reportView);
                    break;
                case 1:
                    GeneralInfo view = new GeneralInfo();
                    view._ViewModel.Started = Started;
                    //view._ViewModel.Patient = Patient;
                    this.InsideNavigationService.Navigate(view);
                    break;
                case 2:
                    History history = new History();
                    history._ViewModel.InsideNavigationService = InsideNavigationService;
                    //history._ViewModel.Patient = Patient;
                    this.InsideNavigationService.Navigate(history);
                    break;
                case 3:
                    ScheduledApp scheduledApp = new ScheduledApp();
                    scheduledApp._ViewModel.Started = Started;
                    this.InsideNavigationService.Navigate(scheduledApp);
                    break;
                case 4:
                    Therapies therapy = new Therapies();
                    therapy._ViewModel.Started = Started;
                    this.InsideNavigationService.Navigate(therapy);
                    break;
                case 5:
                    Tests tests = new Tests();
                    tests._ViewModel.Started = Started;
                    this.InsideNavigationService.Navigate(tests);
                    break;
                case 6:
                    Hospitalizations hospitalizations = new Hospitalizations();
                    hospitalizations._ViewModel.Started = Started;
                    this.InsideNavigationService.Navigate(hospitalizations);
                    break;
            }
        }
        private bool CanExecute_Command(object obj)
        {
            return true;
        }

        private void Execute_EndAppointmentCommand(object obj)
        {
            bool dialog = (bool)new ExitMess("Da li želite da završite termin?").ShowDialog();
            if (dialog)
            {
                DoctorAppointmentDTO selectedAppointment = SelectedAppointment.Appointment;
                DoctorAppointmentController.Instance.EndAppointment(selectedAppointment);
                DoctorAppointmentManagementController.Instance.EndAppointment(selectedAppointment);
                ReportDTO reportDTO = new ReportDTO(selectedAppointment.AppointmentStart, selectedAppointment.Doctor.Name, selectedAppointment.Doctor.Surname, selectedAppointment.Type, selectedAppointment.AppointmentCause, ReportView.reportDetail.Text, ReportView._ViewModel.Prescriptions.Count, selectedAppointment.Patient.Id);
                ChartController.Instance.AddReport(reportDTO);
                ChartController.Instance.AddPrescriptions(new List<PrescriptionDTO>(ReportView._ViewModel.Prescriptions), SelectedAppointment.Appointment.Patient);
                this.MainNavigationService.Navigate(new AppDetail());
            }
        }

        private void Execute_AddCommand(object obj)
        {
            switch (InsideNavigationService.Content.GetType().Name)
            {
                case "UCReport":
                    SearchMedicineView._ViewModel.Prescriptions = ReportView._ViewModel.Prescriptions;
                    MainNavigationService.Navigate(SearchMedicineView);
                    break;
                default:
                    break;
            }

        }

        #endregion

        #region Methods
        private void SetFirstPageNavigation()
        {
            if (SelectedAppointment == null)
            {
                GeneralInfo view = new GeneralInfo();
                view._ViewModel.Started = Started;
                //view._ViewModel.Patient = Patient;
                InsideNavigationService.Navigate(view);
            }
            else
            {
                if (SelectedAppointment.IsStarted == true)
                {
                    InsideNavigationService.Navigate(ReportView);
                }
                else
                {
                    GeneralInfo view = new GeneralInfo();
                    view._ViewModel.Started = Started;
                    //view._ViewModel.Patient = Patient;
                    InsideNavigationService.Navigate(view);
                }
            }
        }

        private void SetFields()
        {
            Patient = SelectedAppointment.Appointment.Patient;
            SearchMedicineView = new SearchMedicine();
            SearchMedicineView._ViewModel.Patient = Patient;
            SearchMedicineView._ViewModel.DatePrescribed = SelectedAppointment.Appointment.AppointmentStart;
            SearchMedicineView._ViewModel.MainNavigationService = MainNavigationService;
            Started = SelectedAppointment.IsStarted;
        }
        #endregion

        #region Constructor
        public PatientChartViewModel()
        {
            this.ReportView = new ReportView();
            Started = false;
            this.MainNavigationService = DoctorMainWindow.Instance._ViewModel.NavigationService;
            this.AddCommand = new RelayCommand(Execute_AddCommand, CanExecute_Command);
            this.ChangeCommand = new RelayCommand(Execute_ChangeCommand, CanExecute_Command);
            this.EndAppointmentCommand = new RelayCommand(Execute_EndAppointmentCommand, CanExecute_Command);
        }
        #endregion

    }
}
