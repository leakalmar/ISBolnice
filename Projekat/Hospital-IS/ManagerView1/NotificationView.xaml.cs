using Hospital_IS.ManagerViewModel;
using System.Windows;
using System.Windows.Controls;

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
