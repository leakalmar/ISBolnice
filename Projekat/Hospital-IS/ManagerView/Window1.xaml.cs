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
using Storages;
using Model;
using System.Collections.ObjectModel;
using Controllers;

namespace Hospital_IS
{
  
    public partial class Window1 : Window
    {
        private RoomStorage roomStorage = new RoomStorage();
        private static Window1 instance = null;
        public static Window1 Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Window1();
                }
                return instance;
            }
        }
        private Window1()
        {
               
            InitializeComponent();
            DataGridRooms.DataContext = new ObservableCollection<Room>(RoomController.Instance.getAllRooms());
        }

        
        public void refresh()
        {
            DataGridRooms.DataContext = new ObservableCollection<Room>(RoomController.Instance.getAllRooms());
        }
       

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            RoomOptions roomOptions = new RoomOptions();
            roomOptions.Show();
            this.Hide();
        }

        private void Eqiupment_Click(object sender, RoutedEventArgs e)
        {
            EquipmentWindow.Instance.refresh();
            EquipmentWindow.Instance.Show();
           
            this.Hide();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ManagerLogout managerLogout = new ManagerLogout();
            managerLogout.Show();
            this.Hide();
        }

        private void sobe_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
