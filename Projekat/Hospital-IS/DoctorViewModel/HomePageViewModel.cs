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
using System.Windows.Input;
using System.Windows.Navigation;

namespace Hospital_IS.DoctorViewModel
{
    public class HomePageViewModel : DoctorViewModelClass
    {
        private ICollectionView appointmentsView;
        private ObservableCollection<StartAppointmentDTO> doctorAppointments;
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

        public ObservableCollection<StartAppointmentDTO> DoctorAppointments
        {
            get { return doctorAppointments; }
            set
            {
                doctorAppointments = value;
                OnPropertyChanged();
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

        public void DetailsWindow(object sender, KeyEventArgs e)
        {
            if (SelectedAppointment != null && e.Key == Key.Enter)
            {
                AppDetail appDetail = new AppDetail(NavigationService);
                appDetail._ViewModel.SelectedAppointment = SelectedAppointment;
                appDetail._ViewModel.AppointmentsView = AppointmentsView;
                this.NavigationService.Navigate(appDetail);
            }
        }


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

        private void Execute_NavigateToDetailsCommand(object obj)
        {
            if(selectedAppointment != null)
            {
                AppDetail appDetail = new AppDetail(NavigationService);
                appDetail._ViewModel.SelectedAppointment = SelectedAppointment;
                appDetail._ViewModel.AppointmentsView = AppointmentsView;
                this.NavigationService.Navigate(appDetail);
            }
        }

        private void Execute_SelectionChangedCommand(object obj)
        {
            StartAppointmentDTO selected = (StartAppointmentDTO)obj;
            SelectedAppointment = selected;
        }

        private bool CanExecute_NavigateCommand(object obj)
        {
            return true;
        }

        public HomePageViewModel(NavigationService navigation)
        {
            this.NavigateToDetailsCommand = new RelayCommand(Execute_NavigateToDetailsCommand, CanExecute_NavigateCommand);
            this.SelectionChangedCommand = new RelayCommand(Execute_SelectionChangedCommand, CanExecute_NavigateCommand);
            this.navigationService = navigation;
            List<DoctorAppointment> appointments = DoctorAppointmentController.Instance.GetAllByDoctor(DoctorMainWindow.Instance._ViewModel.Doctor.Id);
            DoctorAppointments = new DoctorAppointmentConverter().ConvertCollectionToViewModel(appointments);

            appointmentsView = new CollectionViewSource { Source = DoctorAppointments }.View;

            appointmentsView.Filter = delegate (object item)
            {
                return ((StartAppointmentDTO)item).DoctorAppointment.AppointmentStart.Date == DateTime.Now.Date;
            };
            appointmentsView.SortDescriptions.Add(new SortDescription("DoctorAppointment.AppointmentStart", ListSortDirection.Ascending));
        }

    }
}
