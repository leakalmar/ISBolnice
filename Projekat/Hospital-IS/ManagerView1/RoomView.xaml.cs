using Hospital_IS.ManagerViewModel;
using System.Threading;
using System.Windows;
using System.Windows.Controls;

namespace Hospital_IS.ManagerView1
{
    /// <summary>
    /// Interaction logic for RoomView.xaml
    /// </summary>
    public partial class RoomView : Page
    {
        public RoomView()
        {
            Thread.Sleep(500);
            InitializeComponent();
           
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

       
    
       

       

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = new RoomViewModel(this.NavigationService);
        }
    }

   

}
