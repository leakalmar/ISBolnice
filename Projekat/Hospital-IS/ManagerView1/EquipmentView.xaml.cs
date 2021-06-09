using Hospital_IS.ManagerViewModel;
using System.Windows;
using System.Windows.Controls;

namespace Hospital_IS.ManagerView1
{
    /// <summary>
    /// Interaction logic for EquipmentView.xaml
    /// </summary>
    public partial class EquipmentView : Page
    {
        public EquipmentView()
        {
            InitializeComponent();
           
        }

      

        private void Options_Click(object sender, RoutedEventArgs e)
        {
            OtherOptions.Visibility = Visibility.Hidden;
        
            if (OptionsPanel.Visibility == Visibility.Collapsed)
            {
                OptionsPanel.Visibility = Visibility.Visible;

            }
            else
            {
                OptionsPanel.Visibility = Visibility.Collapsed;
            }
        }

        private void MoreOptions_Click(object sender, RoutedEventArgs e)
        {
            OptionsPanel.Visibility = Visibility.Collapsed;
            if (OtherOptions.Visibility == Visibility.Visible)
            {
                OtherOptions.Visibility = Visibility.Hidden;
            }
            else
            {
                OtherOptions.Visibility = Visibility.Visible;
            }
        }

       

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = EquipmentViewModel.Instance;
            EquipmentViewModel.Instance.NavService = this.NavigationService;
        }

        private void CloseOptions_Click(object sender, RoutedEventArgs e)
        {
            OtherOptions.Visibility = Visibility.Hidden;
        }

     
    }
}
