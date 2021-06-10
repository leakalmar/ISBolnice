using Hospital_IS.Adapter;
using Hospital_IS.DoctorRole.Commands;
using Hospital_IS.DTOs;
using System.Windows.Navigation;

//MVVM
namespace Hospital_IS.DoctorViewModel
{
    public class DoctorMainWindowModel : BindableBase
    {
        #region Feilds
        //slika
        private static DoctorMainWindowModel instance = null;
        private NavigationService navigationService;
        private DoctorDTO doctor;
        private bool focused;
        public bool DemoRunning { get; set; }

        public IDoctorAppointmentTarget Adapter { get; } = new DoctorAppointmentAdapter();


        public static DoctorMainWindowModel Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DoctorMainWindowModel();
                }
                return instance;
            }
        }

        public bool Focused
        {
            get { return focused; }
            set
            {
                focused = value;
                OnPropertyChanged("Focused");
            }
        }

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
        #endregion

        #region Commands
        private RelayCommand navigateToHomePageCommand;
        private RelayCommand navigateToAppointmentsCommand;
        private RelayCommand navigateToPatientsCommand;
        private RelayCommand navigateToChartCommand;
        private RelayCommand navigateToMedicinesCommand;
        private RelayCommand navigateToRoomsCommand;
        private RelayCommand navigateToPrescriptionsCommand;
        private RelayCommand navigateToApproveMedicineCommand;
        private RelayCommand navigateToNotificationsCommand;
        private RelayCommand navigateBackCommand;
        private RelayCommand onLoadedCommand;
        private RelayCommand navigateBackWithoutCheckCommand;
        private RelayCommand navigateToEquipmentCommand;
        private RelayCommand navigateToSettingsCommand;

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
        public RelayCommand NavigateToEquipmentCommand
        {
            get { return navigateToEquipmentCommand; }
            set { navigateToEquipmentCommand = value; }
        }

        public RelayCommand NavigateToSettingsCommand
        {
            get { return navigateToSettingsCommand; }
            set { navigateToSettingsCommand = value; }
        }
        #endregion

        #region Actions
        private void Execute_NavigateToHomePageCommand(object obj)
        {
            DoctorNavigationController.Instance.NavigateToHomeCommand();
        }

        private bool CanExecute_NavigateCommand(object obj)
        {
            if (DemoRunning)
            {
                return false;
            }
            return true;
        }

        private void Execute_NavigateToAppointmentsCommand(object obj)
        {
            DoctorNavigationController.Instance.NavigateToAppointmentsCommand();
        }

        private void Execute_NavigateToPatientsCommand(object obj)
        {
            DoctorNavigationController.Instance.NavigateToPatientsCommand();
        }

        private void Execute_NavigateToChartCommand(object obj)
        {
            DoctorNavigationController.Instance.NavigateToChartCommand();
        }

        private void Execute_NavigateToMedicinesCommand(object obj)
        {
            DoctorNavigationController.Instance.NavigateToMedicinesCommand();
        }

        private void Execute_NavigateToRoomsCommand(object obj)
        {
            DoctorNavigationController.Instance.NavigateToRoomsCommand();
        }
        private void Execute_NavigateToEquipmentCommand(object obj)
        {
            DoctorNavigationController.Instance.NavigateToEquipmentCommand();
        }

        private void Execute_NavigateToPrescriptionsCommand(object obj)
        {
            DoctorNavigationController.Instance.NavigateToPrescriptionsCommand();
        }

        private void Execute_NavigateToApprovemedicineCommand(object obj)
        {
            DoctorNavigationController.Instance.NavigateToApprovemedicineCommand();
        }

        private void Execute_NavigateToNotificationsCommand(object obj)
        {
            DoctorNavigationController.Instance.NavigateToNotificationsCommand();
        }

        private void Execute_NavigateBackCommand(object obj)
        {
            DoctorNavigationController.Instance.NavigateBackCommand();
        }

        private void Execute_NavigateBackWithoutCheckCommand(object obj)
        {

            this.NavigationService.GoBack();
            this.NavigationService.Refresh();

        }

        private void Execute_OnLoadedCommand(object obj)
        {
            DoctorNavigationController.Instance.NavigateToHomeCommand();
        }

        private void Execute_NavigateToSettingsCommand(object obj)
        {
            DoctorNavigationController.Instance.NavigateToSettingsCommand();
        }

        #endregion

        #region Methods
        public void SetDoctor(DoctorDTO doctor)
        {
            Doctor = doctor;
        }
        #endregion

        #region Constructor
        public DoctorMainWindowModel()
        {
            this.NavigationService = DoctorNavigationController.Instance.NavigationService;
            this.Focused = true;
            this.NavigateToHomePageCommand = new RelayCommand(Execute_NavigateToHomePageCommand, CanExecute_NavigateCommand);
            this.NavigateToAppointmentsCommand = new RelayCommand(Execute_NavigateToAppointmentsCommand, CanExecute_NavigateCommand);
            this.NavigateToPatientsCommand = new RelayCommand(Execute_NavigateToPatientsCommand, CanExecute_NavigateCommand);
            this.NavigateToChartCommand = new RelayCommand(Execute_NavigateToChartCommand, CanExecute_NavigateCommand);
            this.NavigateToMedicinesCommand = new RelayCommand(Execute_NavigateToMedicinesCommand, CanExecute_NavigateCommand);
            this.NavigateToRoomsCommand = new RelayCommand(Execute_NavigateToRoomsCommand, CanExecute_NavigateCommand);
            this.NavigateToPrescriptionsCommand = new RelayCommand(Execute_NavigateToPrescriptionsCommand, CanExecute_NavigateCommand);
            this.NavigateToApproveMedicineCommand = new RelayCommand(Execute_NavigateToApprovemedicineCommand, CanExecute_NavigateCommand);
            this.NavigateToNotificationsCommand = new RelayCommand(Execute_NavigateToNotificationsCommand, CanExecute_NavigateCommand);
            this.NavigateBackCommand = new RelayCommand(Execute_NavigateBackCommand, CanExecute_NavigateCommand);
            this.OnLoadedCommand = new RelayCommand(Execute_OnLoadedCommand, CanExecute_NavigateCommand);
            this.NavigateBackWithoutCheckCommand = new RelayCommand(Execute_NavigateBackWithoutCheckCommand, CanExecute_NavigateCommand);
            this.NavigateToEquipmentCommand = new RelayCommand(Execute_NavigateToEquipmentCommand, CanExecute_NavigateCommand);
            this.NavigateToSettingsCommand = new RelayCommand(Execute_NavigateToSettingsCommand, CanExecute_NavigateCommand);
        }
        #endregion
    }
}
