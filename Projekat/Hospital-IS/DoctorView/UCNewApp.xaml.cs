using Hospital_IS.DoctorViewModel;
using System.Windows.Controls;

namespace Hospital_IS.DoctorView
{
    public partial class UCNewApp : UserControl
    {
        private NewAppViewModel viewModel;

        public NewAppViewModel _ViewModel
        {
            get { return viewModel; }
            set { viewModel = value; }
        }

        public UCNewApp()
        {
            InitializeComponent();
            this._ViewModel = new NewAppViewModel();
            this.DataContext = _ViewModel;
        }

    }

}
