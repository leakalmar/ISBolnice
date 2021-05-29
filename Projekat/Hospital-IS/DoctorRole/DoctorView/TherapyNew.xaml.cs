using Hospital_IS.DoctorRole.DoctorViewModel;
using System.Windows.Controls;

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
        public TherapyNew()
        {
            InitializeComponent();
            this._ViewModel = new TherapyNewViewModel();
            this.DataContext = _ViewModel;

        }
    }
}
