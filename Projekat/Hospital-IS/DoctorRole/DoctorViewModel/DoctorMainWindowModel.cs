using Hospital_IS.DoctorRole.Commands;
using Hospital_IS.DoctorRole.DoctorView;
using Hospital_IS.DTOs;
using Model;
using System;
using System.Windows;
using System.Windows.Navigation;

namespace Hospital_IS.DoctorViewModel
{
    public class DoctorMainWindowModel : BindableBase
    {
        #region Feilds
        //slika
        private NavigationService navigationService;
        private PatientChart patientChartView;
        private Appointments appointmentsView;
        private DoctorDTO doctor;

        public NavigationService NavigationService
        {
            get { return navigationService; }
            set
            {
                navigationService = value;
            }
        }
        public DoctorDTO Doctor
        {
            get { return doctor; }
            set
            {
                doctor = value;
                OnPropertyChanged("Doctor");
            }
        }

        public PatientChart PatientChartView
        {
            get { return patientChartView; }
            set
            {
                patientChartView = value;
                OnPropertyChanged("PatientChartView");
            }
        }

        public Appointments AppointmentsView
        {
            get { return appointmentsView; }
            set
            {
                appointmentsView = value;
                OnPropertyChanged("AppointmentsView");
            }
        }
        #endregion

        #region Commands
        private RelayCommand navigateToHomePageCommand;
        private RelayCommand navigateToAppointmentsCommand;
        private RelayCommand navigateToPatientsCommand;
        private RelayCommand navigateToChartCommand;
        private RelayCommand navigateToMedicinesCommand;
        private RelayCommand navigateToUpdateMedicineCommand;
        private RelayCommand navigateToViewMedicineCommand;
        private RelayCommand navigateToRoomsCommand;
        private RelayCommand navigateToPrescriptionsCommand;
        private RelayCommand navigateToApproveMedicineCommand;
        private RelayCommand navigateToNotificationsCommand;
        private RelayCommand navigateBackCommand;
        private RelayCommand navigateToLogInCommand;
        private RelayCommand minimizeCommand;
        private RelayCommand maximizeCommand;
        private RelayCommand onLoadedCommand;
        private RelayCommand navigateBackWithoutCheckCommand;

        public RelayCommand NavigateToHomePageCommand
        {
            get { return navigateToHomePageCommand; }
            set { navigateToHomePageCommand = value; }
        }

        public RelayCommand NavigateToAppointmentsCommand
        {
            get { return navigateToAppointmentsCommand; }
            set { navigateToAppointmentsCommand = value; }
        }
        public RelayCommand NavigateToPatientsCommand
        {
            get { return navigateToPatientsCommand; }
            set { navigateToPatientsCommand = value; }
        }

        public RelayCommand NavigateToChartCommand
        {
            get { return navigateToChartCommand; }
            set { navigateToChartCommand = value; }
        }

        public RelayCommand NavigateToMedicinesCommand
        {
            get { return navigateToMedicinesCommand; }
            set { navigateToMedicinesCommand = value; }
        }

        public RelayCommand NavigateToViewMedicineCommand
        {
            get { return navigateToViewMedicineCommand; }
            set { navigateToViewMedicineCommand = value; }
        }

        public RelayCommand NavigateToUpdateMedicineCommand
        {
            get { return navigateToUpdateMedicineCommand; }
            set { navigateToUpdateMedicineCommand = value; }
        }
        public RelayCommand NavigateToRoomsCommand
        {
            get { return navigateToRoomsCommand; }
            set { navigateToRoomsCommand = value; }
        }

        public RelayCommand NavigateToPrescriptionsCommand
        {
            get { return navigateToPrescriptionsCommand; }
            set { navigateToPrescriptionsCommand = value; }
        }
        public RelayCommand NavigateToApproveMedicineCommand
        {
            get { return navigateToApproveMedicineCommand; }
            set { navigateToApproveMedicineCommand = value; }
        }

        public RelayCommand NavigateToNotificationsCommand
        {
            get { return navigateToNotificationsCommand; }
            set { navigateToNotificationsCommand = value; }
        }

        public RelayCommand NavigateBackCommand
        {
            get { return navigateBackCommand; }
            set { navigateBackCommand = value; }
        }

        public RelayCommand NavigateToLogInCommand
        {
            get { return navigateToLogInCommand; }
            set { navigateToLogInCommand = value; }
        }

        public RelayCommand MinimizeCommand
        {
            get { return minimizeCommand; }
            set { minimizeCommand = value; }
        }

        public RelayCommand MaximizeCommand
        {
            get { return maximizeCommand; }
            set { maximizeCommand = value; }
        }

        public RelayCommand OnLoadedCommand
        {
            get { return onLoadedCommand; }
            set { onLoadedCommand = value; }
        }

        public RelayCommand NavigateBackWithoutCheckCommand
        {
            get { return navigateBackWithoutCheckCommand; }
            set { navigateBackWithoutCheckCommand = value; }
        }
        #endregion

        #region Actions
        private void Execute_NavigateToHomePageCommand(object obj)
        {

            this.NavigationService.Navigate(new HomePage(NavigationService));
        }

        private bool CanExecute_NavigateCommand(object obj)
        {
            return true;
        }

        private void Execute_NavigateToAppointmentsCommand(object obj)
        {
            if (AppointmentsView == null)
            {
                AppointmentsView = new Appointments();
            }
            this.NavigationService.Navigate(AppointmentsView);
        }

        private void Execute_NavigateToPatientsCommand(object obj)
        {
            this.NavigationService.Navigate(
                new Uri("DoctorRole/DoctorView/Patients.xaml", UriKind.Relative));
        }

        private void Execute_NavigateToChartCommand(object obj)
        {
            this.NavigationService.Navigate(PatientChartView);
        }

        private void Execute_NavigateToMedicinesCommand(object obj)
        {
            this.NavigationService.Navigate(
                new Uri("DoctorRole/DoctorView/Medicines.xaml", UriKind.Relative));
        }

        private void Execute_NavigateToViewMedicineCommand(object obj)
        {
            ReviewMedicine view = new ReviewMedicine();
            view._ViewModel.MedicineNotification = (MedicineNotification)obj;
            this.NavigationService.Navigate(view);
        }

        private void Execute_NavigateToUpdateMedicineCommand(object obj)
        {
            UpdateMedicine updateMedicine = new UpdateMedicine();
            updateMedicine._ViewModel.MedicineWhichIsUpdated = (Medicine)obj;
            this.NavigationService.Navigate(updateMedicine);
        }

        private void Execute_NavigateToRoomsCommand(object obj)
        {
            //this.NavigationService.Navigate(
            //   new Uri("DoctorView/Rooms.xaml", UriKind.Relative));
        }

        private void Execute_NavigateToPrescriptionsCommand(object obj)
        {
            // this.NavigationService.Navigate(
            //    new Uri("DoctorView/Appointments.xaml", UriKind.Relative));
        }

        private void Execute_NavigateToApprovemedicineCommand(object obj)
        {
            this.NavigationService.Navigate(
                new Uri("DoctorRole/DoctorView/ApproveMedicine.xaml", UriKind.Relative));
        }

        private void Execute_NavigateToNotificationsCommand(object obj)
        {
            this.NavigationService.Navigate(
                new Uri("DoctorRole/DoctorView/DoctorNotifications.xaml", UriKind.Relative));
        }

        private void Execute_NavigateBackCommand(object obj)
        {
            if (NavigationService.Content.ToString().Contains("PatientChart") && PatientChartView._ViewModel.Started == true)
            {

                PatientChartView._ViewModel.EndAppointmentCommand.Execute(null);

            }
            else
            {
                this.NavigationService.GoBack();
                this.NavigationService.Refresh();
            }
        }

        private void Execute_NavigateBackWithoutCheckCommand(object obj)
        {

            this.NavigationService.GoBack();
            this.NavigationService.Refresh();

        }

        private void Execute_NavigateToLogInCommand(object obj)
        {
            bool dialog = (bool)new ExitMess("Da li ste sigurni da želite da se odjavite?").ShowDialog();
            if (dialog)
            {
                //DoctorController.Instance..UpdateDoctor(Doctor);
                MainWindow login = new MainWindow();
                login.Show();
                DoctorMainWindow.Instance.Hide();

            }
        }

        private void Execute_MinimizeCommand(object obj)
        {
            WindowControls.DoMinimize(DoctorMainWindow.Instance);
        }

        private void Execute_MaximizeCommand(object obj)
        {
            WindowControls.DoMaximize(DoctorMainWindow.Instance, DoctorMainWindow.Instance.full);
        }

        private void Execute_OnLoadedCommand(object obj)
        {
            this.NavigationService.Navigate(new HomePage(NavigationService));
        }


        #endregion

        #region Methods
        public void SetDoctor(DoctorDTO doctor)
        {
            Doctor = doctor;
        }
        #endregion

        #region Constructor
        public DoctorMainWindowModel(NavigationService navigationService)
        {
            this.NavigateToHomePageCommand = new RelayCommand(Execute_NavigateToHomePageCommand, CanExecute_NavigateCommand);
            this.NavigateToAppointmentsCommand = new RelayCommand(Execute_NavigateToAppointmentsCommand, CanExecute_NavigateCommand);
            this.NavigateToPatientsCommand = new RelayCommand(Execute_NavigateToPatientsCommand, CanExecute_NavigateCommand);
            this.NavigateToChartCommand = new RelayCommand(Execute_NavigateToChartCommand, CanExecute_NavigateCommand);
            this.NavigateToUpdateMedicineCommand = new RelayCommand(Execute_NavigateToUpdateMedicineCommand, CanExecute_NavigateCommand);
            this.NavigateToMedicinesCommand = new RelayCommand(Execute_NavigateToMedicinesCommand, CanExecute_NavigateCommand);
            this.NavigateToRoomsCommand = new RelayCommand(Execute_NavigateToRoomsCommand, CanExecute_NavigateCommand);
            this.NavigateToViewMedicineCommand = new RelayCommand(Execute_NavigateToViewMedicineCommand, CanExecute_NavigateCommand);
            this.NavigateToPrescriptionsCommand = new RelayCommand(Execute_NavigateToPrescriptionsCommand, CanExecute_NavigateCommand);
            this.NavigateToApproveMedicineCommand = new RelayCommand(Execute_NavigateToApprovemedicineCommand, CanExecute_NavigateCommand);
            this.NavigateToNotificationsCommand = new RelayCommand(Execute_NavigateToNotificationsCommand, CanExecute_NavigateCommand);
            this.NavigateBackCommand = new RelayCommand(Execute_NavigateBackCommand, CanExecute_NavigateCommand);
            this.NavigateToLogInCommand = new RelayCommand(Execute_NavigateToLogInCommand, CanExecute_NavigateCommand);
            this.MaximizeCommand = new RelayCommand(Execute_MaximizeCommand, CanExecute_NavigateCommand);
            this.MinimizeCommand = new RelayCommand(Execute_MinimizeCommand, CanExecute_NavigateCommand);
            this.OnLoadedCommand = new RelayCommand(Execute_OnLoadedCommand, CanExecute_NavigateCommand);
            this.NavigateBackWithoutCheckCommand = new RelayCommand(Execute_NavigateBackWithoutCheckCommand, CanExecute_NavigateCommand);
            this.navigationService = navigationService;
        }
        #endregion
    }
}
