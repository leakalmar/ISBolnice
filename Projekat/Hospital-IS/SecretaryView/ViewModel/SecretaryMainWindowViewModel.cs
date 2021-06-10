using Hospital_IS.Adapter;
using Hospital_IS.DoctorRole.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Navigation;

namespace Hospital_IS.SecretaryView.ViewModel
{
    public class SecretaryMainWindowViewModel
    {
        #region Fields
        private NavigationService navService;
        public IDoctorAppointmentTarget DoctorAppointmentTarget { get; } = new DoctorAppointmentAdapter();

        private string currentTheme;
        public NavigationService NavService
        {
            get { return navService; }
            set
            {
                navService = value;
            }
        }
        public string CurrentTheme
        {
            get { return currentTheme; }
            set
            {
                currentTheme = value;
            }
        }
        #endregion

        #region Commands
        private RelayCommand navigateToPatientsUCCommand;

        private RelayCommand navigateToAppointmentsUCCommand;

        private RelayCommand navigateToDoctorsUCCommand;

        private RelayCommand navigateToRoomsUCCommand;

        private RelayCommand navigateToNotificationUCCommand;

        private RelayCommand navigateToSettingsUCCommand;

        private RelayCommand switchThemeCommand;

        public RelayCommand NavigateToPatientsUCCommand
        {
            get { return navigateToPatientsUCCommand; }
            set
            {
                navigateToPatientsUCCommand = value;
            }
        }

        public RelayCommand NavigateToAppointmentsUCCommand
        {
            get { return navigateToAppointmentsUCCommand; }
            set
            {
                navigateToAppointmentsUCCommand = value;
            }
        }

        public RelayCommand NavigateToDoctorsUCCommand
        {
            get { return navigateToDoctorsUCCommand; }
            set
            {
                navigateToDoctorsUCCommand = value;
            }
        }

        public RelayCommand NavigateToRoomsUCCommand
        {
            get { return navigateToRoomsUCCommand; }
            set
            {
                navigateToRoomsUCCommand = value;
            }
        }

        public RelayCommand NavigateToNotificationUCCommand
        {
            get { return navigateToNotificationUCCommand; }
            set
            {
                navigateToNotificationUCCommand = value;
            }
        }

        public RelayCommand NavigateToSettingsUCCommand
        {
            get { return navigateToSettingsUCCommand; }
            set
            {
                navigateToSettingsUCCommand = value;
            }
        }

        public RelayCommand SwitchThemeCommand
        {
            get { return switchThemeCommand; }
            set
            {
                switchThemeCommand = value;
            }
        }
        #endregion

        #region Actions
        private bool CanExecute_NavigateCommand(object obj)
        {
            return true;
        }

        private void Execute_NavigateToPatientsUCCommand(object obj)
        {
            this.NavService.Navigate(
                new Uri("SecretaryView/UCPatientsView.xaml", UriKind.Relative));
        }

        private void Execute_NavigateToAppointmentssUCCommand(object obj)
        {
            this.NavService.Navigate(
                new Uri("SecretaryView/UCAppointmentsView.xaml", UriKind.Relative));
        }

        private void Execute_NavigateToDoctorsUCCommand(object obj)
        {
            this.NavService.Navigate(
                new Uri("SecretaryView/UCDoctorsView.xaml", UriKind.Relative));
        }

        private void Execute_NavigateToNotificationsUCCommand(object obj)
        {
            this.NavService.Navigate(
                new Uri("SecretaryView/UCNotificationsView.xaml", UriKind.Relative));
        }

        private void Execute_NavigateToRoomsUCCommand(object obj)
        {
            this.NavService.Navigate(
                new Uri("SecretaryView/UCRoomsView.xaml", UriKind.Relative));
        }

        private void Execute_NavigateToSettingsUCCommand(object obj)
        {
            this.NavService.Navigate(
                new Uri("SecretaryView/UCSettings.xaml", UriKind.Relative));
        }

        private void Execute_SwitchThemeCommand(object obj)
        {
            var app = (App)Application.Current;
            if (CurrentTheme.Equals("Light"))
            {
                CurrentTheme = "Light";
                app.ChangeTheme(new Uri("SecretaryView/Themes/LightTheme.xaml", UriKind.Relative));
            }
            else
            {
                CurrentTheme = "Dark";
                app.ChangeTheme(new Uri("SecretaryView/Themes/DarkTheme.xaml", UriKind.Relative));
            }
        }
        #endregion

        #region Constructors
        public SecretaryMainWindowViewModel(NavigationService navService)
        {
            this.NavigateToPatientsUCCommand = new RelayCommand(Execute_NavigateToPatientsUCCommand, CanExecute_NavigateCommand);
            this.NavigateToAppointmentsUCCommand = new RelayCommand(Execute_NavigateToAppointmentssUCCommand, CanExecute_NavigateCommand);
            this.NavigateToDoctorsUCCommand= new RelayCommand(Execute_NavigateToDoctorsUCCommand, CanExecute_NavigateCommand);
            this.NavigateToRoomsUCCommand = new RelayCommand(Execute_NavigateToRoomsUCCommand, CanExecute_NavigateCommand);
            this.NavigateToNotificationUCCommand = new RelayCommand(Execute_NavigateToNotificationsUCCommand, CanExecute_NavigateCommand);
            this.NavigateToSettingsUCCommand = new RelayCommand(Execute_NavigateToSettingsUCCommand, CanExecute_NavigateCommand);
            


            this.SwitchThemeCommand = new RelayCommand(Execute_SwitchThemeCommand);
            this.CurrentTheme = "Light";

            this.navService = navService;
        }
        #endregion
    }
}
