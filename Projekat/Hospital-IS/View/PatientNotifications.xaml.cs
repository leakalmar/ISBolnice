﻿using Hospital_IS.Controllers;
using Hospital_IS.DTOs.SecretaryDTOs;
using Hospital_IS.Storages;
using Model;
using Storages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Hospital_IS.View
{
    /// <summary>
    /// Interaction logic for PatientNotifications.xaml
    /// </summary>
    public partial class PatientNotifications : Window
    {
        public ObservableCollection<NotificationDTO> NotificationMessages { get; set; } = new ObservableCollection<NotificationDTO>();
        public PatientNotifications()
        {
            InitializeComponent();
            List<NotificationDTO> notifications = SecretaryManagementController.Instance.GetAllByUser(HomePatient.Instance.Patient.Id);
            NotificationMessages = new ObservableCollection<NotificationDTO>(notifications);


            if (NotificationMessages.Count > 0)
                ListViewNotifications.ItemsSource = NotificationMessages;
        }

        private void logout(object sender, RoutedEventArgs e)
        {
            MainWindow login = new MainWindow();
            login.Show();
            this.Hide();
        }

        private void home(object sender, RoutedEventArgs e)
        {
            HomePatient.Instance.Show();
            this.Close();
        }

        private void reserveApp(object sender, RoutedEventArgs e)
        {
            AppointmentPatient ap = new AppointmentPatient();
            ap.Show();
            this.Close();
        }

        private void allApp(object sender, RoutedEventArgs e)
        {
            AllAppointments all = new AllAppointments();
            all.Show();
            this.Close();
        }

        private void showTherapy(object sender, RoutedEventArgs e)
        {
            TherapyPatient therapy = new TherapyPatient();
            therapy.Show();
            this.Close();
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
