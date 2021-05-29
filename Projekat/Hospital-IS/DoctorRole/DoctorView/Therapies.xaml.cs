using Hospital_IS.DoctorViewModel;
using System.Windows.Controls;

namespace Hospital_IS.DoctorRole.DoctorView
{
    public partial class Therapies : UserControl
    {
        private TherapyViewModel viewModel;

        public TherapyViewModel _ViewModel
        {
            get { return viewModel; }
            set { viewModel = value; }
        }
        public Therapies()
        {
            InitializeComponent();
            this._ViewModel = new TherapyViewModel();
            this.DataContext = _ViewModel;

        }
    }
}
