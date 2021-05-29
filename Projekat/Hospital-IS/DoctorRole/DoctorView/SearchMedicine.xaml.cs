using Hospital_IS.DoctorViewModel;
using System.Windows.Controls;

namespace Hospital_IS.DoctorRole.DoctorView
{
    public partial class SearchMedicine : UserControl
    {

        private SearchMedicineViewModel viewModel;

        public SearchMedicineViewModel _ViewModel
        {
            get { return viewModel; }
            set
            {
                viewModel = value;
            }
        }
        public SearchMedicine()
        {
            InitializeComponent();
            this.viewModel = new SearchMedicineViewModel();
            this.DataContext = this.viewModel;
        }

    }
}
