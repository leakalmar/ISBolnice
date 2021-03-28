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

namespace Hospital_IS
{

   
    public partial class Rooms : Window
    {
        public static ObservableCollection<Room> Room { get; set; }
        public Rooms()
        {
            
           
            InitializeComponent();
            Room room = new Room(RoomType.ConsultingRoom, true, true, 5, 10);
            Manager.Instance.AddRoom(room);
            this.DataContext = this;
            Room = new ObservableCollection<Room>();
            foreach (Room r in Hospital.Instance.room)
            {
                Room.Add(r);
            }
            
            int broj = Room.Count;
            String mess = Convert.ToString(broj);
            MessageBox.Show(mess);


        }

        private void DateGridRooms_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddNewRoom room = new AddNewRoom();
            room.Show();
        }
    }
}
