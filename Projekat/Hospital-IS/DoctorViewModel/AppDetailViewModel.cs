using Hospital_IS.Commands;
using Hospital_IS.DoctorView;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Navigation;

namespace Hospital_IS.DoctorViewModel
{
    public class AppDetailViewModel : DoctorViewModelClass
    {
        #region Feilds
        private ICollectionView appointmentsView;
        private StartAppointmentDTO selectedAppointment;
        private NavigationService navigationService;

        public NavigationService NavigationService
        {
            get { return navigationService; }
            set
            {
                navigationService = value;
            }
        }
        public StartAppointmentDTO SelectedAppointment
        {
            get { return selectedAppointment; }
            set
            {
                selectedAppointment = value;
                OnPropertyChanged();
            }
        }

        public ICollectionView AppointmentsView
        {
            get { return appointmentsView; }
            set
            {
                appointmentsView = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Commands
        private RelayCommand openChartCommand;
        private RelayCommand startAppointmentCommand;

        public RelayCommand OpenChartCommand
        {
            get { return openChartCommand; }
            set
            {
                openChartCommand = value;
            }
        }

        public RelayCommand StartAppointmentCommand
        {
            get { return startAppointmentCommand; }
            set
            {
                startAppointmentCommand = value;
            }
        }

        #endregion

        #region Actions

        private void Execute_OpenChartCommand(object obj)
        {
            UCPatientChart chart = new UCPatientChart();
            chart._ViewModel.MainNavigationService = NavigationService;
            chart._ViewModel.SelectedAppointment = SelectedAppointment;
            this.NavigationService.Navigate(chart);
        }

        private void Execute_StartAppointmentCommand(object obj)
        {
            SelectedAppointment.Started = true;
            UCPatientChart chart = new UCPatientChart();
            chart._ViewModel.MainNavigationService = NavigationService;
            chart._ViewModel.SelectedAppointment = SelectedAppointment;
            this.NavigationService.Navigate(chart);
        }

        private bool CanExecute_NavigateCommand(object obj)
        {
            return true;
        }
        #endregion

        public AppDetailViewModel(NavigationService navigationService)
        {
            this.OpenChartCommand = new RelayCommand(Execute_OpenChartCommand, CanExecute_NavigateCommand);
            this.StartAppointmentCommand = new RelayCommand(Execute_StartAppointmentCommand, CanExecute_NavigateCommand);
            this.NavigationService = navigationService;
        }
    }
}
