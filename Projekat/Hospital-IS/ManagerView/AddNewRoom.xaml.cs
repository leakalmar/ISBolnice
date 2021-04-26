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
using Model;


namespace Hospital_IS
{
    /// <summary>
    /// Interaction logic for AddNewRoom.xaml
    /// </summary>
    public partial class AddNewRoom : Window
    {
        public AddNewRoom()
        {
            InitializeComponent();
        }

        


        private void potvrdi_Click(object sender, RoutedEventArgs e)
        {
            
            int roomNumber = Convert.ToInt32(text1.Text);
            bool zauzeto = (bool)check1.IsChecked;
            bool renoviranje = (bool)check2.IsChecked;
            int roomFloor = Convert.ToInt32(FloorId.Text);
            RoomType tip;
            if (combo1.Text.Equals("Soba za odmor"))
            {
                tip = RoomType.RecoveryRoom;
            }
            else if (combo1.Text.Equals("Operaciona soba"))
            {
                tip = RoomType.OperationRoom;
            }
            else if (combo1.Text.Equals("Magacin"))
            {
                tip = RoomType.StorageRoom;
            }
            else
            {
                tip = RoomType.ConsultingRoom;
            }

            int id = genereteId(RoomController.Instance.getAllRooms());
            Room newRoom = new Room(roomFloor, roomNumber, id, zauzeto, renoviranje, tip);

            RoomController.Instance.AddRoom(newRoom);

            RoomOptions roomOptions = new RoomOptions();
            roomOptions.Show();
            this.Close();


           
            
        }

        private int genereteId(List<Room> room)
        {
            int id = -1;
            foreach(Room r in room)
            {
                if(r.RoomId > id)
                {
                    id = r.RoomId;
                }
            }

            return ++id;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            RoomOptions roomOptions = new RoomOptions();
            roomOptions.Show();
            this.Close();
            
        }
    }
}
