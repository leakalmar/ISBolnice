using Hospital_IS.DoctorViewModel;
using System.Windows.Controls;

namespace Hospital_IS.DoctorRole.DoctorView
{
    public partial class ReviewMedicine : UserControl
    {
        private ReviewMedicineViewModel viewModel;

        public ReviewMedicineViewModel _ViewModel
        {
            get { return viewModel; }
            set { viewModel = value; }
        }

        public ReviewMedicine()
        {
            InitializeComponent();
            this._ViewModel = new ReviewMedicineViewModel();
            this.DataContext = _ViewModel;

        }
    }
}
