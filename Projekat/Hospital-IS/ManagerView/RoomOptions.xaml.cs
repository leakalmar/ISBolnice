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
using Controllers;
using Hospital_IS.ManagerView;
using Model;

namespace Hospital_IS
{
    
    public partial class RoomOptions : Window
    {
        public RoomOptions()
        {
          
            InitializeComponent();
            DataGridRooms1.DataContext = new ObservableCollection<Room>(RoomController.Instance.getAllRooms());
        }


        public void refresh()
        {
            DataGridRooms1.DataContext = new ObservableCollection<Room>(RoomController.Instance.getAllRooms());
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Window1.Instance.refresh();
            Window1.Instance.Show();
            this.Hide();

        }

        private void AddRoom_Click(object sender, RoutedEventArgs e)
        {
            AddNewRoom room = new AddNewRoom();
            room.Show();
            this.Hide();
        }

        private void EditRoom_Click(object sender, RoutedEventArgs e)
        {
            Room room = (Room)DataGridRooms1.SelectedItem;
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

        private void DeleteRoom_Click(object sender, RoutedEventArgs e)
        {
            Room room = (Room)DataGridRooms1.SelectedItem;
            if (room == null)
            {
                MessageBox.Show("Izaberite sobu");
            }
            else
            {
                DeleteRoom delRoom = new DeleteRoom(room);
                delRoom.Show();
                refresh();
            }

        }

        private void RenovationRoom_Click(object sender, RoutedEventArgs e)
        {
            Room room = (Room)DataGridRooms1.SelectedItem;
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
