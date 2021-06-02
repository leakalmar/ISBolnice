using Hospital_IS.DoctorRole.DoctorViewModel;
using System.Windows.Controls;

namespace Hospital_IS.DoctorRole.DoctorView
{
    public partial class NotificationDisplay : UserControl
    {
        private NotificationDisplayViewModel viewModel;

        public NotificationDisplayViewModel _ViewModel
        {
            get { return viewModel; }
            set { viewModel = value; }
        }

        public NotificationDisplay()
        {
            InitializeComponent();
            this._ViewModel = new NotificationDisplayViewModel();
            this.DataContext = _ViewModel;

        }
    }
}
