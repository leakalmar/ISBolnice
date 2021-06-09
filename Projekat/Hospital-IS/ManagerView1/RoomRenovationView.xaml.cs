using Hospital_IS.ManagerViewModel;
using System.Windows;
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

        private void AffirmRenovation_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if(RenovationEnd.Text.Equals("") || RenovationStart.Text.Equals(""))
            {
                MessageBox.Show("Unesite datum u validnom obliku");
            }
            else
            {
                RoomRenovationViewModel.Instance.DoRoomRenovation();
            }
        }
    }
}
