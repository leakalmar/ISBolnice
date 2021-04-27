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
using Controllers;
using Model;

namespace Hospital_IS
{
    /// <summary>
    /// Interaction logic for UpdateRoom.xaml
    /// </summary>
    public partial class UpdateRoom : Window
    {
        private Room currentRoom;
        public UpdateRoom(Room room)
        {
            InitializeComponent();
            this.currentRoom = room;

            textbox1.Text = Convert.ToString(currentRoom.RoomFloor);
            textbox2.Text = Convert.ToString(currentRoom.RoomNumber);
            check1.IsChecked = currentRoom.IsFree;
            check2.IsChecked = currentRoom.IsUsable;
            if (currentRoom.Type.Equals(RoomType.RecoveryRoom))
            {
                combo1.SelectedIndex = 0;
            } else if (currentRoom.Type.Equals(RoomType.OperationRoom)){
                combo1.SelectedIndex = 1;
            }
             else if (currentRoom.Type.Equals(RoomType.ConsultingRoom)){
                combo1.SelectedIndex = 2;
            } else if (currentRoom.Type.Equals(RoomType.StorageRoom))
            {
                combo1.SelectedIndex = 3;
            }

            
           
        }

        private void potvrdi_Click(object sender, RoutedEventArgs e)
        {
            int roomNumber = Convert.ToInt32(textbox2.Text);
            bool zauzeto = (bool)check1.IsChecked;
            bool renoviranje = (bool)check2.IsChecked;
            int roomFloor = Convert.ToInt32(textbox1.Text);
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
            Room room = new Room(roomFloor,roomNumber,currentRoom.RoomId,zauzeto,renoviranje,tip);
            RoomController.Instance.UpdateRoom(room);


            RoomOptions roomOptions = new RoomOptions();
            roomOptions.Show();
            this.Close();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            RoomOptions roomOptions = new RoomOptions();
            roomOptions.Show();
            this.Close();

        }
    }
}
