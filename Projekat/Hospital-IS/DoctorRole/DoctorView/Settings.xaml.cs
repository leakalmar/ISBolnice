using Hospital_IS.DoctorRole.DoctorViewModel;
using System.Windows.Controls;

namespace Hospital_IS.DoctorRole.DoctorView
{
    public partial class Settings : UserControl
    {

        private SettingsViewModel viewModel;

        public SettingsViewModel _ViewModel
        {
            get { return viewModel; }
            set
            {
                viewModel = value;
            }
        }
        public Settings()
        {
            InitializeComponent();
            this.viewModel = new SettingsViewModel();
            this.DataContext = this.viewModel;
        }
    }
}
