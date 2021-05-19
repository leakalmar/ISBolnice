using Hospital_IS.ManagerViewModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Hospital_IS.ManagerView1
{
    /// <summary>
    /// Interaction logic for AddRoom.xaml
    /// </summary>
    public partial class AddRoom : Page
    {
        public AddRoom(RoomViewModel roomView)
        {
            InitializeComponent();
            this.DataContext = roomView;
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
    }

}
