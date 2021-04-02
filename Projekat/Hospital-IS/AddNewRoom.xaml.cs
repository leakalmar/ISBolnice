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
            
            int roomId = Convert.ToInt32(text1.Text);
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
            Room newRoom = new Room(tip,zauzeto, renoviranje,roomFloor,roomId);
            Manager.Instance.AddRoom(newRoom);
    

          
            this.Close();
           
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
