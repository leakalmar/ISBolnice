using Controllers;
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
using System.Windows.Shapes;

namespace Hospital_IS.ManagerView
{
    /// <summary>
    /// Interaction logic for ManagerNotificationView.xaml
    /// </summary>
    public partial class ManagerNotificationView : Window
    {
        public ObservableCollection<MedicineNotification> Notifications { get; set; } = new ObservableCollection<MedicineNotification>();
        public ManagerNotificationView()
        {
            InitializeComponent();
            Notifications = new ObservableCollection<MedicineNotification>(MedicineNotificationController.Instance.GetAll());

            ListViewNotifications.Items.Clear();
            if (Notifications.Count > 0)
                ListViewNotifications.ItemsSource = Notifications;


        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            ManagerLogout manager = new ManagerLogout();
            manager.Show();
            this.Hide();
        }

        private void ListViewNotifications_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            MedicineNotification selectedNotification = (MedicineNotification)ListViewNotifications.SelectedItem;

            NotificationInfoView infoView = new NotificationInfoView();
            infoView.Show();
        }
    }
}

