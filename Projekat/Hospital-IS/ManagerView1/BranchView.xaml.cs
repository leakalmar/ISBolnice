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
    /// Interaction logic for BranchView.xaml
    /// </summary>
    public partial class BranchView : Page
    {
        public BranchView()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = new BranchViewModel(this.NavigationService);
        }

        private void OtherOptionsButton_Click(object sender, RoutedEventArgs e)
        {

            BranchOptions.Visibility = Visibility.Collapsed;

            if (OtherOptions.Visibility == Visibility.Collapsed)
            {
                OtherOptions.Visibility = Visibility.Visible;
            }
            else
            {
                OtherOptions.Visibility = Visibility.Collapsed;
            }
        }

        private void BranchOptions_Click(object sender, RoutedEventArgs e)
        {
            OtherOptions.Visibility = Visibility.Collapsed;
            if (BranchOptions.Visibility == Visibility.Collapsed)
            {
                BranchOptions.Visibility = Visibility.Visible;
            }
            else
            {
                BranchOptions.Visibility = Visibility.Collapsed;
            }

        }
    }
}
