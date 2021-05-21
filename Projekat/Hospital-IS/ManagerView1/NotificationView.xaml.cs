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
    /// Interaction logic for NotificationView.xaml
    /// </summary>
    public partial class NotificationView : Page
    {
        public NotificationView()
        {
            InitializeComponent();
            this.DataContext = NotificationViewModel.Instance;
            ListViewNotifications.Items.Clear();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            NotificationViewModel.Instance.NavService = this.NavigationService;
        }     
    }
}
