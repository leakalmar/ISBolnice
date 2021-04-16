using Model;
using System;
using System.Windows;

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
            textbox2.Text = Convert.ToString(currentRoom.RoomId);
            check1.IsChecked = currentRoom.IsFree;
            check2.IsChecked = currentRoom.IsUsable;
            if (currentRoom.Type.Equals(RoomType.RecoveryRoom))
            {
                combo1.SelectedIndex = 0;
            }
            else if (currentRoom.Type.Equals(RoomType.OperationRoom))
            {
                combo1.SelectedIndex = 1;
            }
            else if (currentRoom.Type.Equals(RoomType.ConsultingRoom))
            {
                combo1.SelectedIndex = 2;
            }



        }

        private void potvrdi_Click(object sender, RoutedEventArgs e)
        {
            int roomId = Convert.ToInt32(textbox2.Text);
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
            else
            {
                tip = RoomType.ConsultingRoom;
            }
            Room room = new Room(tip, zauzeto, renoviranje, roomFloor, roomId);
            Manager.Instance.UpdateRoom(currentRoom.RoomId, room);


            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
