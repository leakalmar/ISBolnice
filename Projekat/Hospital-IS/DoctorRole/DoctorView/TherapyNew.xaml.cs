using Hospital_IS.DoctorRole.DoctorViewModel;
using System.Windows.Controls;
using System.Windows.Input;

namespace Hospital_IS.DoctorRole.DoctorView
{
    public partial class TherapyNew : UserControl
    {
        private TherapyNewViewModel viewModel;

        public TherapyNewViewModel _ViewModel
        {
            get { return viewModel; }
            set { viewModel = value; }
        }

        public int _noOfErrorsOnScreen { get; private set; } = 0;

        public TherapyNew()
        {
            InitializeComponent();
            this._ViewModel = new TherapyNewViewModel();
            this.DataContext = _ViewModel;

        }
    }
}
