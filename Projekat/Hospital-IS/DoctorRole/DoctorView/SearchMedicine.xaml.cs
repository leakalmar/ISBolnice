using Hospital_IS.DoctorViewModel;
using System.Windows.Controls;
using System.Windows.Input;

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

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                grid.ScrollIntoView(grid.SelectedItem);
            }
        }
    }
}
