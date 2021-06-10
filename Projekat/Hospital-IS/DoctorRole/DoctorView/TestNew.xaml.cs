using Hospital_IS.DoctorRole.DoctorViewModel;
using System.Windows.Controls;

namespace Hospital_IS.DoctorRole.DoctorView
{
    public partial class TestNew : UserControl
    {
        private TestNewViewModel viewModel;

        public TestNewViewModel _ViewModel
        {
            get { return viewModel; }
            set { viewModel = value; }
        }
        public TestNew()
        {
            InitializeComponent();
            this._ViewModel = new TestNewViewModel();
            this.DataContext = _ViewModel;

        }
    }
}
