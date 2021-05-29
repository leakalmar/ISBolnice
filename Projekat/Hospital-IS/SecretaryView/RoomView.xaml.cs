using Enums;
using Hospital_IS.DTOs.SecretaryDTOs;
using System.Windows;

namespace Hospital_IS.SecretaryView
{
    /// <summary>
    /// Interaction logic for RoomView.xaml
    /// </summary>
    public partial class RoomView : Window
    {
        public RoomDTO Room { get; set; }
        public RoomView(RoomDTO room)
        {
            InitializeComponent();
            Room = room;
            this.DataContext = this;

            if (room.Type.Equals(RoomType.ConsultingRoom) || room.Type.Equals(RoomType.OperationRoom))
            {
                tbIsInUse.Visibility = Visibility.Visible;
                rbInUse.Visibility = Visibility.Visible;
                rbNotInUse.Visibility = Visibility.Visible;

                if (room.IsUsable)
                    rbNotInUse.IsChecked = true;
                else
                    rbInUse.IsChecked = true;
            }
            else
            {
                tbIsInUse.Visibility = Visibility.Collapsed;
                rbInUse.Visibility = Visibility.Collapsed;
                rbNotInUse.Visibility = Visibility.Collapsed;
            }

        }
        private void Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
