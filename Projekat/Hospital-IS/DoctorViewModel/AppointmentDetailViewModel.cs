﻿using Hospital_IS.Commands;
using Hospital_IS.DoctorView;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Navigation;

namespace Hospital_IS.DoctorViewModel
{
    public class AppointmentDetailViewModel : DoctorViewModelClass
    {
        #region Feilds
        private ICollectionView appointmentsView;
        private DoctorAppointmentViewModel selectedAppointment;
        private NavigationService navigationService;

        public NavigationService NavigationService
        {
            get { return navigationService; }
            set
            {
                navigationService = value;
            }
        }
        public DoctorAppointmentViewModel SelectedAppointment
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
            this.NavigationService.Navigate(new UCPatientChart(NavigationService));
        }

        private void Execute_StartAppointmentCommand(object obj)
        {
            this.NavigationService.Navigate(new UCPatientChart(NavigationService));
        }

        private bool CanExecute_NavigateCommand(object obj)
        {
            return true;
        }
        #endregion

        public AppointmentDetailViewModel(NavigationService navigationService)
        {
            this.OpenChartCommand= new RelayCommand(Execute_OpenChartCommand, CanExecute_NavigateCommand);
            this.StartAppointmentCommand = new RelayCommand(Execute_StartAppointmentCommand, CanExecute_NavigateCommand);
            this.NavigationService = navigationService;
        }
    }
}
