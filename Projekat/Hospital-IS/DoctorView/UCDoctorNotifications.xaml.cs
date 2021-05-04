using Hospital_IS.Controllers;
using Model;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Hospital_IS.DoctorView
{
    public partial class UCDoctorNotifications : UserControl
    {
        public ObservableCollection<Notification> Notifications { get; set; } = new ObservableCollection<Notification>();

        public UCDoctorNotifications()
        {
            InitializeComponent();
            Notifications = new ObservableCollection<Notification>(NotificationController.Instance.GetAll());
            //Notifications = new ObservableCollection<Notification>(NotificationController.Instance.GetAllByUser(DoctorHomePage.Instance.Doctor.Id));


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

        private Notification findNotification(int id)
        {
            for (int i = 0; i < Notifications.Count; i++)
            {
                if (id == Notifications[i].Id)
                    return Notifications[i];
            }
            return null;
        }

        private void notification_selected(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                int idNotification = Int32.Parse(id.Text);
                Notification n = findNotification(idNotification);
                NotificationView nv = new NotificationView(n);
                nv.Show();
            }
        }
    }
}
