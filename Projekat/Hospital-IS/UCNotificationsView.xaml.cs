using Hospital_IS.Storages;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;


namespace Hospital_IS
{
    /// <summary>
    /// Interaction logic for UCNotificationsView.xaml
    /// </summary>
    public partial class UCNotificationsView : UserControl
    {
        public ObservableCollection<Notification> Notifications { get; set; } = new ObservableCollection<Notification>();
        private NotificationFileStorage nfs = new NotificationFileStorage();

        public UCNotificationsView()
        {
            InitializeComponent();
            List<Notification> notifications = nfs.GetAll();
            Notifications = new ObservableCollection<Notification>(notifications);


            if (Notifications.Count > 0)
                ListViewNotifications.ItemsSource = Notifications;

        }

        private void ShowNotification(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Notification n = findNotification(Int32.Parse(button.Tag.ToString()));
            NotificationView nv = new NotificationView(n);
            nv.Show();

        }

        private void CreateNotification(object sender, RoutedEventArgs e)
        {
            CreateNotification cn = new CreateNotification(this);
            cn.Show();
        }

        private Notification findNotification(int id)
        {
            for (int i = 0; i < Notifications.Count; i++)
            {
                if (id == Notifications[i].Id)
                    return Notifications[i];
            }
            return null;
        }

    }
}
