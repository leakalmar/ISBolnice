using Hospital_IS.DoctorViewModel;
using System.Windows.Controls;
namespace Hospital_IS.DoctorView
{
    public partial class Tests : UserControl
    {
        private TestsViewModel viewModel;

        public TestsViewModel _ViewModel
        {
            get { return viewModel; }
            set { viewModel = value; }
        }
        public Tests()
        {
            InitializeComponent();
            this._ViewModel = new TestsViewModel();
            this.DataContext = _ViewModel;

        }
    }
}
