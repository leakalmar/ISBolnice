using Model;
using System.Windows;

namespace Hospital_IS
{
    /// <summary>
    /// Interaction logic for DeleteRoom.xaml
    /// </summary>
    public partial class DeleteRoom : Window
    {
        private Room roomDelete;
        public DeleteRoom(Room room)
        {
            roomDelete = room;
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Manager.Instance.RemoveRoom(roomDelete);
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
