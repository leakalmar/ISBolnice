using Hospital_IS.Controllers;
using Hospital_IS.DTOs.SecretaryDTOs;
using System.Windows;

namespace Hospital_IS.SecretaryView
{
    /// <summary>
    /// Interaction logic for DeleteNotification.xaml
    /// </summary>
    public partial class DeleteNotification : Window
    {
        public NotificationDTO Notification { get; set; }
        public UpdateNotification un;
        public DeleteNotification(NotificationDTO notification, UpdateNotification un)
        {
            InitializeComponent();
            this.Notification = notification;
            this.un = un;

            this.DataContext = this;
        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            SecretaryManagementController.Instance.DeleteNotification(Notification);
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
