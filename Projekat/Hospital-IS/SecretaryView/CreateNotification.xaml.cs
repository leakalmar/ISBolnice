using Hospital_IS.Storages;
using Model;
using System;
using System.Windows;


namespace Hospital_IS
{
    /// <summary>
    /// Interaction logic for CreateNotification.xaml
    /// </summary>
    public partial class CreateNotification : Window
    {
        public Notification Notification { get; set; } = new Notification();
        NotificationFileStorage nfs = new NotificationFileStorage();

        public UCNotificationsView ucn;
        public CreateNotification(UCNotificationsView ucn)
        {
            InitializeComponent();
            this.DataContext = this;
            this.ucn = ucn;
        }

        private void Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void postNotification(object sender, RoutedEventArgs e)
        {
            Notification.DatePosted = DateTime.Now;
            Notification.LastChanged = DateTime.Now;
            ucn.Notifications.Insert(0, Notification);
 
            nfs.SaveNotification(Notification);

            this.Close();
        }
    }
}
