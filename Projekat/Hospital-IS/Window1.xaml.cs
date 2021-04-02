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
            if (Hospital.Room == null)
            {
                Hospital.Room = roomStorage.GetAll();
                Room room = new Room(RoomType.StorageRoom, true, true, 12, 15);
                Equipment eqip = new Equipment(EquiptType.Dynamic, 50, "makaze", 50);
                room.AddEquipment(eqip);
                Hospital.Room.Add(room);
            }
            InitializeComponent();
            DataGridRooms.DataContext = Hospital.Instance;
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
            EquipmentWindow.Instance.Show();
           
            this.Hide();
        }
    }
}
