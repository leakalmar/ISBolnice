using Hospital_IS.Controllers;
using Model;
using System.Windows;

namespace Hospital_IS.SecretaryView
{
    /// <summary>
    /// Interaction logic for DeleteNotification.xaml
    /// </summary>
    public partial class DeleteNotification : Window
    {
        public Notification Notification { get; set; }
        public UpdateNotification un;
        public DeleteNotification(Notification notification, UpdateNotification un)
        {
            InitializeComponent();
            this.Notification = notification;
            this.un = un;

            this.DataContext = this;
        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            NotificationController.Instance.DeleteNotification(Notification);
            un.ucn.RefreshList();
            this.Close();
            un.Close();
        }

        private void Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
