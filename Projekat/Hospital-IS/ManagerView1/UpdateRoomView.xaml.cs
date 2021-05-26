using Hospital_IS.ManagerViewModel;
using System.Windows.Controls;

namespace Hospital_IS.ManagerView1
{
    /// <summary>
    /// Interaction logic for UpdateRoomView.xaml
    /// </summary>
    public partial class UpdateRoomView : Page
    {
        public UpdateRoomView(RoomViewModel roomViewModel)
        {
            InitializeComponent();
            this.DataContext = roomViewModel;
        }
    }
}
