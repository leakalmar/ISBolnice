using Hospital_IS.Controllers;
using Model;
using System;
using System.Windows;

namespace Hospital_IS.SecretaryView
{
    /// <summary>
    /// Interaction logic for UpdateNotification.xaml
    /// </summary>
    public partial class UpdateNotification : Window
    {
        public Notification Notification { get; set; }
        public UCNotificationsView ucn;
        public UpdateNotification(Notification notification, UCNotificationsView ucn)
        {
            InitializeComponent();
            this.Notification = notification;
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
            Notification.LastChanged = DateTime.Now;
            NotificationController.Instance.UpdateNotification(Notification);
            this.Close();
        }

        private void DeleteNotification(object sender, RoutedEventArgs e)
        {
            DeleteNotification dn = new DeleteNotification(Notification, this);
            dn.Show();
        }
    }
}
