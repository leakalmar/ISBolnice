using Hospital_IS.DoctorViewModel;
using System.Windows.Controls;

namespace Hospital_IS.DoctorRole.DoctorView
{
    public partial class ApproveMedicine : UserControl
    {
        private ApproveMedicineViewModel viewModel;

        public ApproveMedicineViewModel _ViewModel
        {
            get { return viewModel; }
            set { viewModel = value; }
        }

        public ApproveMedicine()
        {
            InitializeComponent();
            this._ViewModel = new ApproveMedicineViewModel();
            this.DataContext = _ViewModel;

        }
    }
}
