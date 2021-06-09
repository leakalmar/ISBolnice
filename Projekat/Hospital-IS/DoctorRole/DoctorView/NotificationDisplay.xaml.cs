using Hospital_IS.DoctorRole.DoctorViewModel;
using System.Windows.Controls;

namespace Hospital_IS.DoctorRole.DoctorView
{
    public partial class NotificationDisplay : UserControl
    {
        public NotificationDisplay(NotificationDisplayViewModel notificationDisplayViewModel)
        {
            InitializeComponent();
            this.DataContext = notificationDisplayViewModel;

        }
    }
}
