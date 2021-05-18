﻿using Controllers;
using Hospital_IS.Commands;
using Hospital_IS.DoctorView;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Navigation;

namespace Hospital_IS.DoctorViewModel
{
    public class DoctorMainWindowModel : DoctorViewModelClass
    {
        #region Feilds
        private int doctorId;
        private String doctorNameSurname;
        private Specialty doctorSpecialty;
        private int doctorPrimeryRoom;
        private ObservableCollection<DoctorAppointment> doctorAppointments;
        //slika
        private NavigationService navigationService;

        public NavigationService NavigationService
        {
            get { return navigationService; }
            set
            {
                navigationService = value;
            }
        }
        public int DoctorId
        {
            get { return doctorId; }
            set {
                doctorId = value;
                OnPropertyChanged();
            }
        }

        public String DoctorNameSurname
        {
            get { return doctorNameSurname; }
            set {
                doctorNameSurname = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<DoctorAppointment> DoctorAppointments
        {
            get { return doctorAppointments; }
            set
            {
                doctorAppointments = value;
                OnPropertyChanged();
            }
        }

        public Specialty DoctorSpecialty
        {
            get { return doctorSpecialty; }
            set
            {
                doctorSpecialty = value;
                OnPropertyChanged();
            }
        }

        public int DoctorPrimaryRoom
        {
            get { return doctorPrimeryRoom; }
            set
            {
                doctorPrimeryRoom = value;
                OnPropertyChanged();
            }
        }


        #endregion

        #region Commands
        private RelayCommand navigateToHomePageCommand;
        private RelayCommand navigateToAppointmentsCommand;
        private RelayCommand navigateToPatientsCommand;
        private RelayCommand navigateToMedicinesCommand;
        private RelayCommand navigateToRoomsCommand;
        private RelayCommand navigateToPrescriptionsCommand;
        private RelayCommand navigateToApproveMedicineCommand;
        private RelayCommand navigateToNotificationsCommand;
        private RelayCommand navigateBackCommand;
        private RelayCommand navigateToLogInCommand;
        private RelayCommand minimizeCommand;
        private RelayCommand maximizeCommand;




        public RelayCommand NavigateToHomePageCommand
        {
            get { return navigateToHomePageCommand; }
            set
            {
                navigateToHomePageCommand = value;
            }
        }

        public RelayCommand NavigateToAppointmentsCommand
        {
            get { return navigateToAppointmentsCommand; }
            set
            {
                navigateToAppointmentsCommand = value;
            }
        }
        public RelayCommand NavigateToPatientsCommand
        {
            get { return navigateToPatientsCommand; }
            set
            {
                navigateToPatientsCommand = value;
            }
        }

        public RelayCommand NavigateToMedicinesCommand
        {
            get { return navigateToMedicinesCommand; }
            set
            {
                navigateToMedicinesCommand = value;
            }
        }
        public RelayCommand NavigateToRoomsCommand
        {
            get { return navigateToRoomsCommand; }
            set
            {
                navigateToRoomsCommand = value;
            }
        }

        public RelayCommand NavigateToPrescriptionsCommand
        {
            get { return navigateToPrescriptionsCommand; }
            set
            {
                navigateToPrescriptionsCommand = value;
            }
        }
        public RelayCommand NavigateToApproveMedicineCommand
        {
            get { return navigateToApproveMedicineCommand; }
            set
            {
                navigateToApproveMedicineCommand = value;
            }
        }

        public RelayCommand NavigateToNotificationsCommand
        {
            get { return navigateToNotificationsCommand; }
            set
            {
                navigateToNotificationsCommand = value;
            }
        }

        public RelayCommand NavigateBackCommand
        {
            get { return navigateBackCommand; }
            set
            {
                navigateBackCommand = value;
            }
        }

        public RelayCommand NavigateToLogInCommand
        {
            get { return navigateToLogInCommand; }
            set
            {
                navigateToLogInCommand = value;
            }
        }

        public RelayCommand MinimizeCommand
        {
            get { return minimizeCommand; }
            set
            {
                minimizeCommand = value;
            }
        }

        public RelayCommand MaximizeCommand
        {
            get { return maximizeCommand; }
            set
            {
                maximizeCommand = value;
            }
        }
        #endregion

        #region Actions
        private void Execute_NavigateToHomePageCommand(object obj)
        {
            this.NavigationService.Navigate(
                new Uri("DoctorView/UCHomePage.xaml", UriKind.Relative));
        }

        private bool CanExecute_NavigateCommand(object obj)
        {
            return true;
        }

        private void Execute_NavigateToAppointmentsCommand(object obj)
        {
            this.NavigationService.Navigate(
                new Uri("DoctorView/UCAppointments.xaml", UriKind.Relative));
        }

        private void Execute_NavigateToPatientsCommand(object obj)
        {
            this.NavigationService.Navigate(
                new Uri("DoctorView/UCPatients.xaml", UriKind.Relative));
        }

        private void Execute_NavigateToMedicinesCommand(object obj)
        {
            this.NavigationService.Navigate(
                new Uri("DoctorView/UCMedicines.xaml", UriKind.Relative));
        }

        private void Execute_NavigateToRoomsCommand(object obj)
        {
            //this.NavigationService.Navigate(
            //   new Uri("DoctorView/UCRooms.xaml", UriKind.Relative));
        }

        private void Execute_NavigateToPrescriptionsCommand(object obj)
        {
            // this.NavigationService.Navigate(
            //    new Uri("DoctorView/UCAppointments.xaml", UriKind.Relative));
        }

        private void Execute_NavigateToApprovemedicineCommand(object obj)
        {
            this.NavigationService.Navigate(
                new Uri("DoctorView/UCApproveMedicine.xaml", UriKind.Relative));
        }

        private void Execute_NavigateToNotificationsCommand(object obj)
        {
            this.NavigationService.Navigate(
                new Uri("DoctorView/UCDoctorNotifications.xaml", UriKind.Relative));
        }

        private void Execute_NavigateBackCommand(object obj)
        {
            this.NavigationService.GoBack();
        }

        private void Execute_NavigateToLogInCommand(object obj)
        {
            bool dialog = (bool)new ExitMess("Da li ste sigurni da želite da se odjavite?").ShowDialog();
            if (dialog)
            {
                //DoctorController.Instance..UpdateDoctor(Doctor);
                MainWindow login = new MainWindow();
                login.Show();
                DoctorMainWindow.Instance.Hide();

            }
        }

        private void Execute_MinimizeCommand(object obj)
        {
            WindowControls.DoMinimize(DoctorMainWindow.Instance);
        }

        private void Execute_MaximizeCommand(object obj)
        {
            WindowControls.DoMaximize(DoctorMainWindow.Instance, DoctorMainWindow.Instance.full);
        }


        #endregion

        #region Methods
        public void SetDoctor(Doctor doctor)
        {
            DoctorId = doctor.Id;
            DoctorNameSurname = doctor.Name + " " + doctor.Surname;
            DoctorSpecialty = doctor.Specialty;
            DoctorPrimaryRoom = doctor.PrimaryRoom;
            DoctorAppointments = new ObservableCollection<DoctorAppointment>(DoctorAppointmentController.Instance.GetAllByDoctor(doctor.Id));
            // Home.Children.Add(HomePage);
        }
        #endregion

        #region Constructor
        public DoctorMainWindowModel (NavigationService navigationService)
        {
            this.NavigateToHomePageCommand = new RelayCommand(Execute_NavigateToHomePageCommand, CanExecute_NavigateCommand);
            this.NavigateToAppointmentsCommand = new RelayCommand(Execute_NavigateToAppointmentsCommand, CanExecute_NavigateCommand);
            this.NavigateToPatientsCommand = new RelayCommand(Execute_NavigateToPatientsCommand, CanExecute_NavigateCommand);
            this.NavigateToMedicinesCommand = new RelayCommand(Execute_NavigateToMedicinesCommand, CanExecute_NavigateCommand);
            this.NavigateToRoomsCommand = new RelayCommand(Execute_NavigateToRoomsCommand, CanExecute_NavigateCommand);
            this.NavigateToPrescriptionsCommand = new RelayCommand(Execute_NavigateToPrescriptionsCommand, CanExecute_NavigateCommand);
            this.NavigateToApproveMedicineCommand = new RelayCommand(Execute_NavigateToApprovemedicineCommand, CanExecute_NavigateCommand);
            this.NavigateToNotificationsCommand = new RelayCommand(Execute_NavigateToNotificationsCommand, CanExecute_NavigateCommand);
            this.NavigateBackCommand = new RelayCommand(Execute_NavigateBackCommand, CanExecute_NavigateCommand);
            this.NavigateToLogInCommand = new RelayCommand(Execute_NavigateToLogInCommand, CanExecute_NavigateCommand);
            this.MaximizeCommand = new RelayCommand(Execute_MaximizeCommand, CanExecute_NavigateCommand);
            this.MinimizeCommand = new RelayCommand(Execute_MinimizeCommand, CanExecute_NavigateCommand);
            this.navigationService = navigationService;
        }
        #endregion
    }
}
