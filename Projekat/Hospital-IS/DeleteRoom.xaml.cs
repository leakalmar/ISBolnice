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
