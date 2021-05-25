using Hospital_IS.ManagerViewModel;
using System.Windows.Controls;

namespace Hospital_IS.ManagerView1
{
    /// <summary>
    /// Interaction logic for NotificationInformationView.xaml
    /// </summary>
    public partial class NotificationInformationView : Page
    {
        public NotificationInformationView()
        {
            InitializeComponent();
            this.DataContext = NotificationViewModel.Instance;
        }

       
    }
}
