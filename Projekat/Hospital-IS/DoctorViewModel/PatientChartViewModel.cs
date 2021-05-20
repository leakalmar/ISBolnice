using Controllers;
using Hospital_IS.Commands;
using Hospital_IS.DoctorView;
using Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Hospital_IS.DoctorViewModel
{
    public class PatientChartViewModel : BindableBase
    {
        #region Feilds
        private Patient patient;
        private DoctorAppointmentViewModel selectedAppointment;
        private UCReport reportView;
        private NavigationService mainNavigationService;
        private NavigationService insideNavigationService;
        private int lastTab = 0;
        private Thickness margin = new Thickness(0);

        public NavigationService MainNavigationService
        {
            get { return mainNavigationService; }
            set
            {
                mainNavigationService = value;
            }
        }

        public NavigationService InsideNavigationService
        {
            get { return insideNavigationService; }
            set
            {
                insideNavigationService = value;
            }
        }

        public Patient Patient
        {
            get { return patient; }
            set { patient = value; }
        }

        public DoctorAppointmentViewModel SelectedAppointment
        {
            get { return selectedAppointment; }
            set
            {
                selectedAppointment = value;
                Patient = selectedAppointment.DoctorAppointment.Patient;
            }
        }

        public UCReport ReportView
        {
            get { return reportView; }
            set { reportView = value; }
        }

        public int LastTab
        {
            get { return lastTab; }
            set { lastTab = value;
                OnPropertyChanged("LastTab");
            }
        }

        public Thickness Margin
        {
            get { return margin; }
            set
            {
                margin = value;
                OnPropertyChanged("Margin");
            }
        }
        #endregion

        #region Commands
        private RelayCommand endAppointmentCommand;
        private RelayCommand changeCommand;


        public RelayCommand EndAppointmentCommand
        {
            get { return endAppointmentCommand; }
            set
            {
                endAppointmentCommand = value;
            }
        }

        public RelayCommand ChangeCommand
        {
            get { return changeCommand; }
            set { changeCommand = value; }
        }

        #endregion

        #region Actions
        private void Execute_ChangeCommand(object obj)
        {

            int index = Int32.Parse(((string)obj));

            if (SelectedAppointment.Started == Visibility.Visible)
            {
                Margin = new Thickness(120 * index, 0, 0, 0);
            }
            else
            {
                Margin = new Thickness(120 * index - 120, 0, 0, 0);
            }

            switch (index)
            {
                case 0:
                    this.InsideNavigationService.Navigate(new UCReport());
                    break;
                case 1:
                    this.InsideNavigationService.Navigate(new UCGeneralInfo());
                    break;
                case 2:
                    this.InsideNavigationService.Navigate(new UCHistory());
                    break;
                case 3:
                    this.InsideNavigationService.Navigate(new UCScheduledApp());
                    break;
                case 4:
                    this.InsideNavigationService.Navigate(new UCTherapy());
                    break;
                case 5:
                    this.InsideNavigationService.Navigate(new UCTest());
                    break;
            }
        }
        private bool CanExecute_Command(object obj)
        {
            return true;
        }

        private void Execute_EndAppointmentCommand(object obj)
        {
            DoctorAppointmentController.Instance.EndAppointment(SelectedAppointment.DoctorAppointment);
            ChartController.Instance.AddReport(SelectedAppointment.DoctorAppointment, ReportView.reportDetail.Text, ReportView.Prescriptions.Count, SelectedAppointment.DoctorAppointment.Patient);
            ChartController.Instance.AddPrescriptions(new List<Prescription>(ReportView.Prescriptions), SelectedAppointment.DoctorAppointment.Patient);
            this.MainNavigationService.Navigate(new UCAppDetail(MainNavigationService));
        }

        #endregion

        #region Constructor
        public PatientChartViewModel()
        {
            this.ChangeCommand = new RelayCommand(Execute_ChangeCommand, CanExecute_Command);
            this.EndAppointmentCommand = new RelayCommand(Execute_EndAppointmentCommand, CanExecute_Command);
        }
        #endregion

    }
}
