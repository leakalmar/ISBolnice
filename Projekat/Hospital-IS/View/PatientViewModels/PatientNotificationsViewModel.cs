using Hospital_IS.Controllers;
using Hospital_IS.DTOs.SecretaryDTOs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace Hospital_IS.View.PatientViewModels
{
    public class PatientNotificationsViewModel : BindableBase
    {
        public ObservableCollection<NotificationDTO> NotificationMessages { get; set; }
        private bool showNotificationInfo = false;
        private string notification;
        private string heading;
        private string date;
        private Thickness notificationMargin;
        public MyICommand<int> ShowNotification { get; set; }
        public PatientNotificationsViewModel()
        {
            List<NotificationDTO> notifications = SecretaryManagementController.Instance.GetAllByUser(PatientMainWindowViewModel.Patient.Id);
            NotificationMessages = new ObservableCollection<NotificationDTO>(notifications);
            ShowNotification = new MyICommand<int>(ShowNot);
            NotificationMargin = new Thickness(10, 10, 10, 10);
        }

        public bool ShowNotificationInfo
        {
            get { return showNotificationInfo; }
            set
            {
                if(showNotificationInfo != value)
                {
                    showNotificationInfo = value;
                    OnPropertyChanged("ShowNotificationInfo");
                }
            }
        }

        public Thickness NotificationMargin
        {
            get { return notificationMargin; }
            set
            {
                if (notificationMargin != value)
                {
                    notificationMargin = value;
                    OnPropertyChanged("NotificationMargin");
                }
            }
        }

        public string Notification
        {
            get { return notification; }
            set
            {
                if(notification != value)
                {
                    notification = value;
                    OnPropertyChanged("Notification");
                }
            }
        }

        public string Heading
        {
            get { return heading; }
            set
            {
                if (heading != value)
                {
                    heading = value;
                    OnPropertyChanged("Heading");
                }
            }
        }

        public string Date
        {
            get { return date; }
            set
            {
                if (date != value)
                {
                    date = value;
                    OnPropertyChanged("Date");
                }
            }
        }

        private void ShowNot(int id)
        {
            NotificationDTO notificationDTO = findNotification(id);
            Heading = notificationDTO.Title;
            Date = notificationDTO.DatePosted.ToString("dd.MM.yyyy.");
            Notification = notificationDTO.Text;
            NotificationMargin = new Thickness(10, 10, 500, 10);
            ShowNotificationInfo = true;
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
