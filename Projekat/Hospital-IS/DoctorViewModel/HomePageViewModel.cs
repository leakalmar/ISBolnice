using Controllers;
using DTOs;
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
    public class HomePageViewModel : BindableBase
    {
        #region Feilds
        private ICollectionView appointmentsView;
        private ObservableCollection<AppointmentRowDTO> doctorAppointments;
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

        public ObservableCollection<AppointmentRowDTO> DoctorAppointments
        {
            get { return doctorAppointments; }
            set
            {
                doctorAppointments = value;
                OnPropertyChanged("DoctorAppointments");
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
        private RelayCommand navigateToDetailsCommand;
        private RelayCommand selectionChangedCommand;

        public RelayCommand NavigateToDetailsCommand
        {
            get { return navigateToDetailsCommand; }
            set
            {
                navigateToDetailsCommand = value;
            }
        }

        public RelayCommand SelectionChangedCommand
        {
            get { return selectionChangedCommand; }
            set
            {
                selectionChangedCommand = value;
            }
        }
        #endregion

        #region Actions
        private void Execute_NavigateToDetailsCommand(object obj)
        {
            if(selectedAppointment != null)
            {
                AppDetail appDetail = new AppDetail();
                appDetail._ViewModel.SelectedAppointment = SelectedAppointment;
                appDetail._ViewModel.AppointmentsView = AppointmentsView;
                this.NavigationService.Navigate(appDetail);
            }
        }

        private void Execute_SelectionChangedCommand(object obj)
        {
            AppointmentRowDTO selected = (AppointmentRowDTO)obj;
            SelectedAppointment = selected;
        }

        private bool CanExecute_NavigateCommand(object obj)
        {
            return true;
        }
        #endregion

        #region Constructor
        public HomePageViewModel(NavigationService navigation)
        {
            this.NavigateToDetailsCommand = new RelayCommand(Execute_NavigateToDetailsCommand, CanExecute_NavigateCommand);
            this.SelectionChangedCommand = new RelayCommand(Execute_SelectionChangedCommand, CanExecute_NavigateCommand);
            this.navigationService = navigation;
            DoctorAppointments = new DoctorAppointmentConverter().ConvertCollectionToDTO(DoctorAppointmentController.Instance.GetAllByDoctor(DoctorMainWindow.Instance._ViewModel.Doctor.Id));

            appointmentsView = new CollectionViewSource { Source = DoctorAppointments }.View;

            appointmentsView.Filter = delegate (object item)
            {
                return ((AppointmentRowDTO)item).Appointment.AppointmentStart.Date == DateTime.Now.Date;
            };
            appointmentsView.SortDescriptions.Add(new SortDescription("Appointment.AppointmentStart", ListSortDirection.Ascending));
        }
        #endregion
    }
}
