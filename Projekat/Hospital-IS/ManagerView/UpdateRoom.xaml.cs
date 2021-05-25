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
using Hospital_IS.ManagerViewModel;
using Model;

namespace Hospital_IS
{
    /// <summary>
    /// Interaction logic for UpdateRoom.xaml
    /// </summary>
    public partial class UpdateRoom : Window
    {
       
        public UpdateRoom(Room room)
        {
            InitializeComponent(); 
        }

       

        private void RoomNumber_GotFocus(object sender, RoutedEventArgs e)
        {
            if (RoomNumberBox.Text.Equals("Unesite broj sobe"))
            {
                RoomNumberBox.Text = string.Empty;
            }
        }

        private void RoomFloor_GotFocus(object sender, RoutedEventArgs e)
        {

            if (RoomFloorBox.Text.Equals("Unesite sprat sobe"))
            {
                RoomFloorBox.Text = string.Empty;
            }
        }

        private void SurfaceArea_GotFocus(object sender, RoutedEventArgs e)
        {

            if (SurfaceAreaBox.Text.Equals("Unesite površinu sobe"))
            {
                SurfaceAreaBox.Text = string.Empty;
            }
        }

        private void BedNumber_GotFocus(object sender, RoutedEventArgs e)
        {

            if (BedNumberBox.Text.Equals("Unesite broj kreveta"))
            {
                BedNumberBox.Text = string.Empty;
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Window1.Instance.Show();
            this.Hide();
        }
    }
}
