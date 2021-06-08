using Hospital_IS.DoctorRole.DoctorView;
using Hospital_IS.DoctorViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Navigation;

namespace Hospital_IS.DoctorRole.Commands
{
    class DoctorNavigationController : NavigationController
    {
        private static DoctorNavigationController instance;
        private NavigationService navigaitonService;

        public static DoctorNavigationController Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DoctorNavigationController();
                }
                return instance;
            }
        }

        public NavigationService NavigationService
        {
            set { navigaitonService = value; }
            get { return navigaitonService; }
        }

        public DoctorNavigationController() { }


        public override void NavigateBackCommand()
        {
            throw new NotImplementedException();
        }

        public override void NavigateBackWithoutCheckCommand()
        {
            throw new NotImplementedException();
        }

        public override void NavigateToAppointmentsCommand()
        {
            throw new NotImplementedException();
        }

        public override void NavigateToApprovemedicineCommand()
        {
            throw new NotImplementedException();
        }

        public override void NavigateToChartCommand()
        {
            DoctorMainWindowModel.Instance.NavigationService.Navigate(new PatientChart(PatientChartViewModel.Instance));
        }

        public override void NavigateToEquipCommand()
        {
            throw new NotImplementedException();
        }

        public override void NavigateToEquipmentCommand()
        {
            throw new NotImplementedException();
        }

        public override void NavigateToHomeCommand()
        {
            this.NavigationService.Navigate(
                    new Uri("DoctorRole/DoctorView/HomePage.xaml", UriKind.Relative));
        }

        public override void NavigateToLogInCommand()
        {
            throw new NotImplementedException();
        }

        public override void NavigateToMedicinesCommand()
        {
            throw new NotImplementedException();
        }

        public override void NavigateToNotificationsCommand()
        {
            throw new NotImplementedException();
        }

        public override void NavigateToPatientsCommand()
        {
            throw new NotImplementedException();
        }

        public override void NavigateToPrescriptionsCommand()
        {
            throw new NotImplementedException();
        }

        public override void NavigateToRoomsCommand()
        {
            throw new NotImplementedException();
        }

        public override void NavigateToSettingsCommand()
        {
            throw new NotImplementedException();
        }

        public override void NavigateToUpdateMedicineCommand()
        {
            throw new NotImplementedException();
        }

        public override void NavigateToViewMedicineCommand()
        {
            throw new NotImplementedException();
        }
    }
}
