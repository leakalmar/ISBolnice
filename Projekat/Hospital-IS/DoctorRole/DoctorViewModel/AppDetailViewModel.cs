using DTOs;
using Hospital_IS.Controllers;
using Hospital_IS.DoctorRole.Commands;
using Hospital_IS.DoctorRole.DoctorConverters;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;

//MVVM
namespace Hospital_IS.DoctorViewModel
{
    public class AppDetailViewModel : BindableBase
    {
        #region Feilds
        private ICollectionView appointmentsView;
        private AppointmentRowDTO selectedAppointment;
        private bool focused;

        public bool Focused
        {
            get { return focused; }
            set
            {
                focused = value;
                OnPropertyChanged("Focused");
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
        private RelayCommand setFocusCommand;
        private RelayCommand lostFocusCommand;

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
        public RelayCommand SetFocusCommand
        {
            get { return setFocusCommand; }
            set
            {
                setFocusCommand = value;
            }
        }
        public RelayCommand LostFocusCommand
        {
            get { return lostFocusCommand; }
            set
            {
                lostFocusCommand = value;
            }
        }
        #endregion

        #region Actions

        private void Execute_OpenChartCommand(object obj)
        {
            SelectedAppointment.IsStarted = false;
            DoctorNavigationController.Instance.NavigateToChartCommand();
            PatientChartViewModel.Instance.SelectedAppointment = SelectedAppointment;
        }

        private void Execute_StartAppointmentCommand(object obj)
        {
            SelectedAppointment.IsStarted = true;
            DoctorNavigationController.Instance.NavigateToChartCommand();
            PatientChartViewModel.Instance.SelectedAppointment = SelectedAppointment;
        }
        private void Execute_SetFocusCommand(object obj)
        {
            Focused = true;
            OnPropertyChanged("Focused");
        }
        private void Execute_LostFocusCommand(object obj)
        {
            Focused = false;
            OnPropertyChanged("Focused");
        }

        private bool CanExecute_NavigateCommand(object obj)
        {
            return true;
        }
        #endregion

        #region Methods
        private void SetAppointmentFeilds()
        {
            List<DoctorAppointmentDTO> appointmentDTOs = DoctorMainWindowModel.Instance.Adapter.GetAppointmentByDoctorId(DoctorMainWindowModel.Instance.Doctor.Id);
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
                DoctorMainWindowModel.Instance.NavigateToHomePageCommand.Execute(null);
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
            this.Focused = true;
            this.OpenChartCommand = new RelayCommand(Execute_OpenChartCommand, CanExecute_NavigateCommand);
            this.StartAppointmentCommand = new RelayCommand(Execute_StartAppointmentCommand, CanExecute_NavigateCommand);
            this.SetFocusCommand = new RelayCommand(Execute_SetFocusCommand, CanExecute_NavigateCommand);
            this.LostFocusCommand = new RelayCommand(Execute_LostFocusCommand, CanExecute_NavigateCommand);
            SetAppointmentFeilds();
        }
        #endregion


    }
}
