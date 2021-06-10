using Hospital_IS.DoctorViewModel;
using System.Windows.Controls;


namespace Hospital_IS.DoctorRole.DoctorView
{
    public partial class ChangeApp : UserControl
    {
        private ChangeAppViewModel viewModel;

        public ChangeAppViewModel _ViewModel
        {
            get { return viewModel; }
            set { viewModel = value; }
        }
        public ChangeApp()
        {
            InitializeComponent();
            this._ViewModel = new ChangeAppViewModel();
            this.DataContext = _ViewModel;

        }
    }
}
