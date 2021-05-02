using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Hospital_IS.ManagerView;
using Storages;
using Model;

namespace Hospital_IS
{
   
    public partial class ManagerLogout : Window
    {
        public ManagerLogout()
        {
            InitializeComponent();
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
           
            this.Close();
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Window1.Instance.Show();
            this.Hide();
        }

        private void Notifications_Click(object sender, RoutedEventArgs e)
        {

            ManagerNotificationView managerNotification = new ManagerNotificationView();
            managerNotification.Show();
            this.Hide();
        }
    }
}
