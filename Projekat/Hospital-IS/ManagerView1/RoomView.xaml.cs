using Hospital_IS.ManagerHelp;
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
            if (HelpViewModel.Instance.isShowed == false)
            {
                Thread.Sleep(500);
               
            }
            InitializeComponent();
        }

        private void OtherOptionsButton_Click(object sender, RoutedEventArgs e)
        {
            RoomOptions.Visibility = Visibility.Collapsed;
            if (OtherOptions.Visibility == Visibility.Visible)
            {
                OtherOptions.Visibility = Visibility.Hidden;
            }
            else
            {
                OtherOptions.Visibility = Visibility.Visible;
            }
            DataGridRooms.SelectedItem = null;
        }

        private void RoomOptions_Click(object sender, RoutedEventArgs e)
        {
            OtherOptions.Visibility = Visibility.Hidden;
            if (RoomOptions.Visibility == Visibility.Visible)
            {
                RoomOptions.Visibility = Visibility.Collapsed;
            }
            else
            {
                RoomOptions.Visibility = Visibility.Visible;
            }
            DataGridRooms.SelectedItem = null;
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            RoomViewModel roomView = new RoomViewModel(this.NavigationService);
            HelpWindow helpWindow = new HelpWindow();
            if(HelpViewModel.Instance.isShowed == false)
            {
                helpWindow.ShowDialog();
                HelpViewModel.Instance.isShowed = true;
            }
            this.DataContext = roomView;       
        }


        private void SearchBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (SearchBox.Text.Equals("Unesite broj sobe"))
            {
                SearchBox.Text = "";
            }
        }

        private void CloseOptions_Click(object sender, RoutedEventArgs e)
        {

            OtherOptions.Visibility = Visibility.Hidden;
        }
    }

   

}
