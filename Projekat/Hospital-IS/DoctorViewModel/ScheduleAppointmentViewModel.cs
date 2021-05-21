using Controllers;
using Hospital_IS.Commands;
using Hospital_IS.DoctorView;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Hospital_IS.DoctorViewModel
{
    public class ScheduleAppointmentViewModel : BindableBase
    {
        #region Fields
        private ObservableCollection<DoctorAppointment> scheduledAppointments;

        public ObservableCollection<DoctorAppointment> ScheduledAppointments
        {
            get { return scheduledAppointments; }
            set { scheduledAppointments = value; }
        }
        #endregion

        #region Commands
        private RelayCommand addNewCommand;

        public RelayCommand AddNewCommand
        {
            get { return addNewCommand; }
            set { addNewCommand = value; }
        }
        #endregion

        #region Actions

        private void Execute_AddNewCommand(object obj)
        {

            //this.NavigationService.Navigate(new UCHomePage(NavigationService));
        }

        private bool CanExecute_AddNewCommand(object obj)
        {
            return true;
        }
        #endregion
        
        #region Constructor
        public ScheduleAppointmentViewModel()
        {
            this.AddNewCommand = new RelayCommand(Execute_AddNewCommand, CanExecute_AddNewCommand);
            Patient patient = DoctorMainWindow.Instance._ViewModel.PatientChartView._ViewModel.Patient;
            this.ScheduledAppointments = new ObservableCollection<DoctorAppointment>(DoctorAppointmentController.Instance.GetFutureAppointmentsByPatient(patient.Id));
        }
        #endregion
    }
}
