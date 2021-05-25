﻿using Hospital_IS.Controllers;
using Hospital_IS.DTOs.SecretaryDTOs;
using Hospital_IS.View.PatientViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace Hospital_IS.View
{
    /// <summary>
    /// Interaction logic for PatientNotifications.xaml
    /// </summary>
    public partial class PatientNotificationsView : UserControl
    {
        public ObservableCollection<NotificationDTO> NotificationMessages { get; set; } = new ObservableCollection<NotificationDTO>();
        public PatientNotificationsView()
        {
            List<NotificationDTO> notifications = SecretaryManagementController.Instance.GetAllByUser(PatientMainWindowViewModel.Patient.Id);
            NotificationMessages = new ObservableCollection<NotificationDTO>(notifications);
            InitializeComponent();
            /*List<NotificationDTO> notifications = SecretaryManagementController.Instance.GetAllByUser(HomePatient.Instance.Patient.Id);
            NotificationMessages = new ObservableCollection<NotificationDTO>(notifications);


            if (NotificationMessages.Count > 0)
                ListViewNotifications.ItemsSource = NotificationMessages;*/
        }
        private void ShowNotification(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            NotificationDTO n = findNotification(Int32.Parse(button.Tag.ToString()));
            NotificationView nv = new NotificationView(n);
            nv.Show();

        }

        private NotificationDTO findNotification(int id)
        {
            for (int i = 0; i < NotificationMessages.Count; i++)
            {
                if (id == NotificationMessages[i].Id)
                    return NotificationMessages[i];
            }
            return null;
        }
    }
}