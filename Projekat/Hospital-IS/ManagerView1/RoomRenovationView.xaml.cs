using Hospital_IS.ManagerViewModel;
using System.Windows.Controls;

namespace Hospital_IS.ManagerView1
{
    /// <summary>
    /// Interaction logic for RoomRenovationView.xaml
    /// </summary>
    public partial class RoomRenovationView : Page
    {
        public RoomRenovationView()
        {
            InitializeComponent();
            this.DataContext = RoomRenovationViewModel.Instance;
        }
    }
}
