using Controllers;
using Hospital_IS.Commands;
using Hospital_IS.Converters;
using Hospital_IS.DoctorView;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Data;
using System.Windows.Navigation;

namespace Hospital_IS.DoctorViewModel
{
    public class HomePageViewModel : DoctorViewModelClass
    {
        private ICollectionView appointmentsView;
        private ObservableCollection<DoctorAppointmentViewModel> doctorAppointments;
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

        public ObservableCollection<DoctorAppointmentViewModel> DoctorAppointments
        {
            get { return doctorAppointments; }
            set
            {
                doctorAppointments = value;
                OnPropertyChanged();
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



        private RelayCommand navigateToDetailsCommand;

        public RelayCommand NavigateToDetailsCommand
        {
            get { return navigateToDetailsCommand; }
            set
            {
                navigateToDetailsCommand = value;
            }
        }

        private void Execute_NavigateToDetailsCommand(object obj)
        {
            if(selectedAppointment != null)
            {
                UCAppDetail appDetail = new UCAppDetail(selectedAppointment);
                this.NavigationService.Navigate(appDetail);
            }
        }

        private bool CanExecute_NavigateCommand(object obj)
        {
            return true;
        }

        public HomePageViewModel(NavigationService navigationService)
        {
            this.NavigateToDetailsCommand = new RelayCommand(Execute_NavigateToDetailsCommand, CanExecute_NavigateCommand);
            this.navigationService = navigationService;
            List<DoctorAppointment> appointments = DoctorAppointmentController.Instance.GetAllByDoctor(DoctorMainWindow.Instance._ViewModel.DoctorId);
            DoctorAppointments = new DoctorAppointmentConverter().ConvertCollectionToViewModel(appointments);

            appointmentsView = new CollectionViewSource { Source = DoctorAppointments }.View;

            appointmentsView.Filter = delegate (object item)
            {
                return ((DoctorAppointmentViewModel)item).DoctorAppointment.AppointmentStart.Date == DateTime.Now.Date;
            };
            appointmentsView.SortDescriptions.Add(new SortDescription("AppointmentStart", ListSortDirection.Ascending));
        }

    }
}
