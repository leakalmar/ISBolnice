using Hospital_IS.DoctorViewModel;
using System.Windows.Controls;

namespace Hospital_IS.DoctorView
{
    public partial class Therapys : UserControl
    {
        private TherapyViewModel viewModel;

        public TherapyViewModel _ViewModel
        {
            get { return viewModel; }
            set { viewModel = value; }
        }
        public Therapys()
        {
            InitializeComponent();
            this._ViewModel = new TherapyViewModel();
            this.DataContext = _ViewModel;

        }
    }
}
