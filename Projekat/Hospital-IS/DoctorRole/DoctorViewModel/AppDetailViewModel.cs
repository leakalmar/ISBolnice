using DTOs;
using Hospital_IS.Controllers;
using Hospital_IS.DoctorRole.Commands;
using Hospital_IS.DoctorRole.DoctorConverters;
using Hospital_IS.DoctorRole.DoctorView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using System.Windows.Navigation;

namespace Hospital_IS.DoctorViewModel
{
    public class AppDetailViewModel : BindableBase
    {
        #region Feilds
        private ICollectionView appointmentsView;
        private AppointmentRowDTO selectedAppointment;
        private NavigationService navigationService;

        public NavigationService NavigationService
        {
            get { return navigationService; }
            set
            {
                navigationService = value;
            }
        }
        public AppointmentRowDTO SelectedAppointment
        {
            get { return selectedAppointment; }
            set
            {
                selectedAppointment = value;
                OnPropertyChanged("SelectedAppointment");
            }
        }

        public ICollectionView AppointmentsView
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
            SelectedAppointment.IsStarted = false;
            PatientChart chart = new PatientChart();
            chart._ViewModel.SelectedAppointment = SelectedAppointment;
            DoctorMainWindow.Instance._ViewModel.PatientChartView = chart;
            this.NavigationService.Navigate(chart);
        }

        private void Execute_StartAppointmentCommand(object obj)
        {
            SelectedAppointment.IsStarted = true;
            PatientChart chart = new PatientChart();
            chart._ViewModel.SelectedAppointment = SelectedAppointment;
            DoctorMainWindow.Instance._ViewModel.PatientChartView = chart;
            this.NavigationService.Navigate(chart);
        }

        private bool CanExecute_NavigateCommand(object obj)
        {
            return true;
        }
        #endregion

        #region Methods
        private void SetAppointmentFeilds()
        {
            List<DoctorAppointmentDTO> appointmentDTOs = DoctorAppointmentManagementController.Instance.GetAppointmentByDoctorId(DoctorMainWindow.Instance._ViewModel.Doctor.Id);
            ObservableCollection<AppointmentRowDTO> doctorAppointments = new DoctorAppointmentConverter().ConvertNewAppointmentsToDTO(appointmentDTOs);

            appointmentsView = new CollectionViewSource { Source = doctorAppointments }.View;

            appointmentsView.Filter = delegate (object item)
            {
                return ((AppointmentRowDTO)item).Appointment.AppointmentStart.Date == DateTime.Now.Date;
            };
            appointmentsView.SortDescriptions.Add(new SortDescription("Appointment.AppointmentStart", ListSortDirection.Ascending));

            AppointmentsView = appointmentsView;
            if (doctorAppointments.Count == 0)
            {
                DoctorMainWindow.Instance._ViewModel.NavigateToHomePageCommand.Execute(null);
            }
            else
            {
                SelectedAppointment = doctorAppointments[0];
            }
        }
        #endregion

        #region Constructor
        public AppDetailViewModel()
        {
            this.OpenChartCommand = new RelayCommand(Execute_OpenChartCommand, CanExecute_NavigateCommand);
            this.StartAppointmentCommand = new RelayCommand(Execute_StartAppointmentCommand, CanExecute_NavigateCommand);
            this.NavigationService = DoctorMainWindow.Instance._ViewModel.NavigationService;
            SetAppointmentFeilds();
        }
        #endregion


    }
}
