using Hospital_IS.DoctorViewModel;
using System.Windows.Controls;

namespace Hospital_IS.DoctorRole.DoctorView
{
    public partial class DoctorNotifications : UserControl
    {
        private DoctorNotificationsViewModel viewModel;

        public DoctorNotificationsViewModel _ViewModel
        {
            get { return viewModel; }
            set { viewModel = value; }
        }

        public DoctorNotifications()
        {
            InitializeComponent();
            this._ViewModel = new DoctorNotificationsViewModel();
            this.DataContext = _ViewModel;

        }
    }
}
