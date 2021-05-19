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
using Hospital_IS.ManagerView;
using Hospital_IS.ManagerViewModel;

namespace Hospital_IS
{
  
    public partial class Window1 : Window
    {
       
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
           
        }

       
       

       

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (RoomOptions.Visibility == Visibility.Visible)
            {
                RoomOptions.Visibility = Visibility.Collapsed;
            }
            else
            {
                RoomOptions.Visibility = Visibility.Visible;
            }
        }

        private void Eqiupment_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            EquipmentWindow.Instance.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ManagerLogout managerLogout = new ManagerLogout("room");
            managerLogout.Show();
            this.Hide();
        }

        private void sobe_Click(object sender, RoutedEventArgs e)
        {

        }

        private void OtherOptionsButton_Click(object sender, RoutedEventArgs e)
        {
            if (OtherOptions.Visibility == Visibility.Visible)
            {
                OtherOptions.Visibility = Visibility.Hidden;
            }
            else
            {
                OtherOptions.Visibility = Visibility.Visible;
            }
        }

        private void CloseOptions_Click(object sender, RoutedEventArgs e)
        {
            OtherOptions.Visibility = Visibility.Hidden;
        }

        private void MedicineView_Click(object sender, RoutedEventArgs e)
        {
            MedicineView medicineView = new MedicineView();
            OtherOptions.Visibility = Visibility.Hidden;
            medicineView.Show();
            this.Hide();
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddRoom_Click(object sender, RoutedEventArgs e)
        {
            AddNewRoom room = new AddNewRoom();
            room.Show();
            this.Hide();
        }

        private void EditRoom_Click(object sender, RoutedEventArgs e)
        {
            Room room = (Room)DataGridRooms.SelectedItem;
            if (room == null)
            {
                MessageBox.Show("Izaberite sobu");
            }
            else
            {
                UpdateRoom upRoom = new UpdateRoom(room);
                upRoom.Show();
                this.Hide();
            }
        }

       

        private void RenovationRoom_Click(object sender, RoutedEventArgs e)
        {
            Room room = (Room)DataGridRooms.SelectedItem;
            if (room == null)
            {
                MessageBox.Show("Izaberite sobu");
            }
            else
            {
                RenovationView renView = new RenovationView(room);
                renView.Show();
            }

        }
    }
}
