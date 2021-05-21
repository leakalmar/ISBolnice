﻿using Controllers;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Navigation;

namespace Hospital_IS.ManagerViewModel
{
    public class NotificationViewModel: ViewModel
    {

        private ObservableCollection<MedicineNotification> notifications;
        private MedicineNotification selectedNotification;
        private NavigationService navService;
        private RelayCommand navigateToSelectedNotification;
        private RelayCommand navigateToPreviousPage;
        private RelayCommand deleteMedicineNotificationCommand;



        public MedicineNotification SelectedNotification
        {
            get
            {
                return selectedNotification;
            }
            set
            {
                if (value != selectedNotification)
                {

                    selectedNotification = value;
                    OnPropertyChanged("SelectedNotification");

                }
            }
        }
        public NavigationService NavService
        {
            get { return navService; }
            set
            {
                navService = value;
            }
        }

        private static NotificationViewModel instance = null;
        public static NotificationViewModel Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new NotificationViewModel();
                }
                return instance;
            }
        }

        public RelayCommand NavigateToSelectedNotification
        {
            get { return navigateToSelectedNotification; }
            set
            {
                navigateToSelectedNotification = value;
            }
        }

        public RelayCommand DeleteMedicineNotificationCommand
        {
            get { return deleteMedicineNotificationCommand; }
            set
            {
                deleteMedicineNotificationCommand = value;
            }
        }

        public RelayCommand NavigateToPreviousPage
        {
            get { return navigateToPreviousPage; }
            set
            {
                navigateToPreviousPage = value;
            }
        }


        public ObservableCollection<MedicineNotification> Notifications
        {
            get
            {
                return notifications;
            }
            set
            {
                if (value != notifications)
                {

                    notifications = value;
                    OnPropertyChanged("Notifications");

                }
            }
        }

      
        private NotificationViewModel()
        {

            Notifications = new ObservableCollection<MedicineNotification>(MedicineNotificationController.Instance.GetAllByDoctorId(6));
            this.NavigateToSelectedNotification = new RelayCommand(Execute_NavigateToMedicineRegistrationPage,CanExecute_IfNotificationIsSelected);
            this.DeleteMedicineNotificationCommand = new RelayCommand(Execute_NavigateToPreviousPage, CanExecute_Navigation);
           

        }



        private bool CanExecute_IfNotificationIsSelected(object obj)
        {
            return !(SelectedNotification == null);
        }

        private bool CanExecute_Navigation(object obj)
        {
            return true;
        }

        private void Execute_NavigateToMedicineRegistrationPage(object obj)
        {

            this.NavService.Navigate(
                   new Uri("ManagerView1/NotificationInformationView.xaml", UriKind.Relative));
        }

        private void Execute_NavigateToPreviousPage(object obj)
        {
            MedicineNotificationController.Instance.DeleteNotification(SelectedNotification);
            Notifications = new ObservableCollection<MedicineNotification>(MedicineNotificationController.Instance.GetAllByDoctorId(6));
            this.NavService.GoBack();
                   
        }



    }
}
