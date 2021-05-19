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
    /// Interaction logic for RoomView.xaml
    /// </summary>
    public partial class RoomView : Page
    {
        public RoomView()
        {
            InitializeComponent();
            this.DataContext = new RoomViewModel(null);
        }

        private void OtherOptionsButton_Click(object sender, RoutedEventArgs e)
        {
            if (OtherOptions.Visibility == Visibility.Visible)
            {
                OtherOptions.Visibility = Visibility.Hidden;
            }
            else
            {
                OtherOptions.Visibility = Visibility.Visible;
            }
        }

        private void RoomOptions_Click(object sender, RoutedEventArgs e)
        {
            if (RoomOptions.Visibility == Visibility.Visible)
            {
                RoomOptions.Visibility = Visibility.Collapsed;
            }
            else
            {
                RoomOptions.Visibility = Visibility.Visible;
            }
        }

        private void Eqiupment_Click(object sender, RoutedEventArgs e)
        {
            EquipmentView equipmentView = new EquipmentView();
            this.NavigationService.Navigate(equipmentView);
        }

        private void AddRoom_Click(object sender, RoutedEventArgs e)
        {
            RoomViewModel roomViewModel = new RoomViewModel(this.NavigationService);
            AddRoom addRoom = new AddRoom(roomViewModel);
            this.NavigationService.Navigate(addRoom);
        }

       

        private void EditRoom_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridRooms.SelectedItem != null)
            {
                RoomViewModel roomViewModel = new RoomViewModel(this.NavigationService);
                roomViewModel.SetFields(DataGridRooms.SelectedItem);
                UpdateRoomView updateRoom = new UpdateRoomView(roomViewModel);
                this.NavigationService.Navigate(updateRoom);
            }
        }
    }

   

}
