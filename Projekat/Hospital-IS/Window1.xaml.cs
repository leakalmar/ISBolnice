using Model;
using Storages;
using System.Windows;
using System.Windows.Controls;

namespace Hospital_IS
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        private RoomStorage roomStorage = new RoomStorage();
        public Window1()
        {
            InitializeComponent();
        }


        private void RoomsButton_Click_1(object sender, RoutedEventArgs e)
        {
            Rooms room = new Rooms();
            room.Show();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            roomStorage.SaveRooms(Hospital.Room);
            this.Close();
        }
    }
}
