using Hospital_IS.DoctorViewModel;
using System.Windows.Controls;

namespace Hospital_IS.DoctorView
{
    public partial class Hospitalizations : UserControl
    {
        private HospitalizationsViewModel viewModel;

        public HospitalizationsViewModel _ViewModel
        {
            get { return viewModel; }
            set { viewModel = value; }
        }

        public Hospitalizations()
        {
            InitializeComponent();
            this._ViewModel = new HospitalizationsViewModel();
            this.DataContext = _ViewModel;

        }
    }
}
