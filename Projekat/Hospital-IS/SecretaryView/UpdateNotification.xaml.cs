using Hospital_IS.Controllers;
using Hospital_IS.DTOs.SecretaryDTOs;
using System;
using System.ComponentModel;
using System.Windows;

namespace Hospital_IS.SecretaryView
{
    /// <summary>
    /// Interaction logic for UpdateNotification.xaml
    /// </summary>
    public partial class UpdateNotification : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private NotificationDTO _notification { get; set; }
        public NotificationDTO Notification
        {
            get { return _notification; }
            set
            {
                if (value != _notification)
                {
                    _notification = value;
                    OnPropertyChanged("Notification");
                }
            }
        }

        public UCNotificationsView ucn;
        public UpdateNotification(NotificationDTO notification, UCNotificationsView ucn)
        {
            InitializeComponent();
            this._notification = notification;
            this.ucn = ucn;

            this.DataContext = this;
        }

        private void Close(object sender, RoutedEventArgs e)
        {
            ucn.RefreshList();
            this.Close();
        }

        private void EditNotification(object sender, RoutedEventArgs e)
        {
            _notification.LastChanged = DateTime.Now;
            SecretaryManagementController.Instance.UpdateNotification(_notification);
            this.Close();
        }

        private void DeleteNotification(object sender, RoutedEventArgs e)
        {
            DeleteNotification dn = new DeleteNotification(_notification, this);
            dn.ShowDialog();
        }
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
