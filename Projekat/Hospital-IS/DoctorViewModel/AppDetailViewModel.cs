using Controllers;
using Hospital_IS.Commands;
using Hospital_IS.DoctorConverters;
using Hospital_IS.DoctorView;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
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
            SelectedAppointment.Started = false;
            PatientChart chart = new PatientChart();
            chart._ViewModel.SelectedAppointment = SelectedAppointment;
            DoctorMainWindow.Instance._ViewModel.PatientChartView = chart;
            this.NavigationService.Navigate(chart);
        }

        private void Execute_StartAppointmentCommand(object obj)
        {
            SelectedAppointment.Started = true;
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

        public AppDetailViewModel()
        {
            this.OpenChartCommand = new RelayCommand(Execute_OpenChartCommand, CanExecute_NavigateCommand);
            this.StartAppointmentCommand = new RelayCommand(Execute_StartAppointmentCommand, CanExecute_NavigateCommand);
            this.NavigationService = DoctorMainWindow.Instance._ViewModel.NavigationService;
            List<DoctorAppointment> appointments = DoctorAppointmentController.Instance.GetAllByDoctor(DoctorMainWindow.Instance._ViewModel.Doctor.Id);
            ObservableCollection<StartAppointmentDTO> doctorAppointments = new DoctorAppointmentConverter().ConvertCollectionToViewModel(appointments);

            appointmentsView = new CollectionViewSource { Source = doctorAppointments }.View;

            appointmentsView.Filter = delegate (object item)
            {
                return ((StartAppointmentDTO)item).DoctorAppointment.AppointmentStart.Date == DateTime.Now.Date;
            };
            appointmentsView.SortDescriptions.Add(new SortDescription("DoctorAppointment.AppointmentStart", ListSortDirection.Ascending));

            AppointmentsView = appointmentsView;
            if(appointments.Count == 0)
            {
                DoctorMainWindow.Instance._ViewModel.NavigateToHomePageCommand.Execute(null);
            }
            else
            {
                SelectedAppointment = doctorAppointments[0];
            }
        }
    }
}
