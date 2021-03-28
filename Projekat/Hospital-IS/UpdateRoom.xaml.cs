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
            textbox2.Text = Convert.ToString(currentRoom.RoomId);
            
           
        }
    }
}
