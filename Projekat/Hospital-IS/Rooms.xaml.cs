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
using Model;
using Storages;

namespace Hospital_IS
{

   
    public partial class Rooms : Window
    {

        private RoomStorage roomStorage = new RoomStorage();
        public Rooms()
        {


            InitializeComponent();

            if (Hospital.Room == null)
            {
                Hospital.Room = roomStorage.GetAll();
            }

            DateGridRooms.DataContext = Hospital.Instance;
        }

        

        private void DateGridRooms_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddNewRoom room = new AddNewRoom();
            room.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Room room = (Room)DateGridRooms.SelectedItem;
            if (room == null)
            {
                MessageBox.Show("Izaberite sobu");
            }
            else
            {
                UpdateRoom upRoom = new UpdateRoom(room);
                upRoom.Show();
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Room room = (Room)DateGridRooms.SelectedItem;
            if (room == null)
            {
                MessageBox.Show("Izaberite sobu");
            }
            else
            {
                DeleteRoom delRoom = new DeleteRoom(room);
                delRoom.Show();
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
