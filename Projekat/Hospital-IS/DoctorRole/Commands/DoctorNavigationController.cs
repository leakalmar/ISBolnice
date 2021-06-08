using Hospital_IS.DoctorRole.DoctorView;
using Hospital_IS.DoctorViewModel;
using Model;
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
            if (NavigationService.Content.ToString().Contains("PatientChart") && PatientChartViewModel.Instance.Started == true)
            {
                PatientChartViewModel.Instance.EndAppointmentCommand.Execute(null);
            }
            else
            {
                try
                { this.NavigationService.GoBack(); }
                catch (Exception e) { }
            }
        }

        public override void NavigateBackWithoutCheckCommand()
        {
            throw new NotImplementedException();
        }

        public override void NavigateToAppointmentsCommand()
        {
            this.NavigationService.Navigate(new Appointments());
        }

        public override void NavigateToApprovemedicineCommand()
        {
            this.NavigationService.Navigate(
                new Uri("DoctorRole/DoctorView/ApproveMedicine.xaml", UriKind.Relative));
        }

        public override void NavigateToChartCommand()
        {
            this.NavigationService.Navigate(new PatientChart());
        }

        public override void NavigateToEquipmentCommand()
        {
            this.NavigationService.Navigate(
               new Uri("DoctorRole/DoctorView/EquipmentUse.xaml", UriKind.Relative));
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
            this.NavigationService.Navigate(
                new Uri("DoctorRole/DoctorView/Medicines.xaml", UriKind.Relative));
        }

        public override void NavigateToNotificationsCommand()
        {
            this.NavigationService.Navigate(
                new Uri("DoctorRole/DoctorView/DoctorNotifications.xaml", UriKind.Relative));
        }

        public override void NavigateToPatientsCommand()
        {
            this.NavigationService.Navigate(
               new Uri("DoctorRole/DoctorView/Patients.xaml", UriKind.Relative));
        }

        public override void NavigateToPrescriptionsCommand()
        {
            //nije implementirano, samo zbog prikatya je bool da li da pokaze notifikaciju ili ne kada se odobri
            this.NavigationService.Navigate(new IssuePrescription(true));
        }

        public override void NavigateToRoomsCommand()
        {
            this.NavigationService.Navigate(
               new Uri("DoctorRole/DoctorView/Rooms.xaml", UriKind.Relative));
        }

        public override void NavigateToSettingsCommand()
        {
            this.NavigationService.Navigate(
               new Uri("DoctorRole/DoctorView/Settings.xaml", UriKind.Relative));
        }

        public override void NavigateToUpdateMedicineCommand(Medicine medicine)
        {
            UpdateMedicine updateMedicine = new UpdateMedicine();
            updateMedicine._ViewModel.MedicineWhichIsUpdated = medicine;
            this.NavigationService.Navigate(updateMedicine);
        }

        public override void NavigateToViewMedicineCommand(MedicineNotification medicineNotification)
        {
            ReviewMedicine view = new ReviewMedicine();
            view._ViewModel.MedicineNotification = medicineNotification;
            this.NavigationService.Navigate(view);
        }
    }
}
