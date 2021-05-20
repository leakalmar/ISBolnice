using Hospital_IS.DoctorViewModel;
using System.Windows.Controls;

namespace Hospital_IS.DoctorView
{
    public partial class UCSearchMedicine : UserControl
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
        public UCSearchMedicine()
        {
            InitializeComponent();
            this.viewModel = new SearchMedicineViewModel();
            this.DataContext = this.viewModel;
        }

    }
}
