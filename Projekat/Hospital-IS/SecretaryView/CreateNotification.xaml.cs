using Hospital_IS.Controllers;
using Hospital_IS.SecretaryView;
using Hospital_IS.Storages;
using Model;
using System;
using System.Collections.Generic;
using System.Windows;


namespace Hospital_IS
{
    /// <summary>
    /// Interaction logic for CreateNotification.xaml
    /// </summary>
    public partial class CreateNotification : Window
    {
        public Notification Notification { get; set; } = new Notification();

        public List<int> Ids { get; set; } = new List<int>();

        public UCNotificationsView ucn;
        public CreateNotification(UCNotificationsView ucn)
        {
            InitializeComponent();
            this.DataContext = this;
            this.ucn = ucn;

            rbSelectSome.IsEnabled = false;
        }

        private void Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void postNotification(object sender, RoutedEventArgs e)
        {
            Notification.DatePosted = DateTime.Now;
            Notification.LastChanged = DateTime.Now;
            
            if (rbSelectSome.IsChecked == true)
            {
                foreach (int id in Ids)
                    Notification.Recipients.Add(id);
            }

            ucn.Notifications.Insert(0, Notification);

            NotificationController.Instance.AddNotification(Notification);

            this.Close();
        }

        private void btnSelect_Click(object sender, RoutedEventArgs e)
        {
            RecipientSelection rp = new RecipientSelection(this);
            rp.Show();
        }

        private void rbSelectAll_Click(object sender, RoutedEventArgs e)
        {
            rbSelectAll.IsChecked = true;
            rbSelectSome.IsChecked = false;
        }
    }
}
