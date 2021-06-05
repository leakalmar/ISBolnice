using Hospital_IS.Controllers;
using Hospital_IS.DTOs.SecretaryDTOs;
using Hospital_IS.SecretaryView;
using System;
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
        public ObservableCollection<NotificationDTO> Notifications { get; set; } = new ObservableCollection<NotificationDTO>();

        public UCNotificationsView()
        {
            InitializeComponent();
            Notifications = new ObservableCollection<NotificationDTO>(SecretaryManagementController.Instance.GetAllNotifications());


            if (Notifications.Count > 0)
                ListViewNotifications.ItemsSource = Notifications;

        }

        public void RefreshList()
        {
            if (Notifications != null)
                Notifications.Clear();

            SecretaryManagementController.Instance.ReloadNotifications();
            Notifications = new ObservableCollection<NotificationDTO>(SecretaryManagementController.Instance.GetAllNotifications());
            ListViewNotifications.ItemsSource = Notifications;
        }

        private void ShowNotification(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            NotificationDTO n = FindNotification(Int32.Parse(button.Tag.ToString()));
            NotificationView nv = new NotificationView(n);
            nv.Show();

        }

        private void CreateNotification(object sender, RoutedEventArgs e)
        {
            CreateNotification cn = new CreateNotification(this);
            cn.ShowDialog();
        }

        private NotificationDTO FindNotification(int id)
        {
            for (int i = 0; i < Notifications.Count; i++)
            {
                if (id == Notifications[i].Id)
                    return Notifications[i];
            }
            return null;
        }

        private void UpdateNotification(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            NotificationDTO n = FindNotification(Int32.Parse(button.Tag.ToString()));
            UpdateNotification un = new UpdateNotification(n, this);
            un.ShowDialog();
        }
    }
}
