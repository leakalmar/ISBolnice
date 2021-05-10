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
        private String activeButton;
        public ManagerLogout(string activeButton)
        {
            InitializeComponent();
            this.activeButton = activeButton;
            if (activeButton.Equals("room"))
            {
              
                Back.Visibility = Visibility.Visible;
            }else if (activeButton.Equals("equipment"))
            {
                BackEquipment.Visibility = Visibility.Visible;
            }
            else if(activeButton.Equals("medicine"))
            {
                BackMedicine.Visibility = Visibility.Visible;
            }
          
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
            Back.Visibility = Visibility.Collapsed;
            this.Hide();
        }

        private void Notifications_Click(object sender, RoutedEventArgs e)
        {

            ManagerNotificationView.Instance.activeButton = activeButton;
            ManagerNotificationView.Instance.Show();
           
            this.Hide();
        }

        private void BackMedicine_Click(object sender, RoutedEventArgs e)
        {
            MedicineView medicine = new MedicineView();
            medicine.Show();
            BackMedicine.Visibility = Visibility.Collapsed;
            this.Hide();
        }

        private void BackEquipment_Click(object sender, RoutedEventArgs e)
        {
            EquipmentWindow.Instance.Show();
            BackEquipment.Visibility = Visibility.Visible;
            this.Hide();
        }
    }
}
