using Hospital_IS.DoctorViewModel;
using System.Windows.Controls;
using System.Windows.Input;

namespace Hospital_IS.DoctorRole.DoctorView
{
    public partial class SearchMedicine : UserControl
    {
        public SearchMedicine(SearchMedicineViewModel searchMedicineViewModel)
        {
            InitializeComponent();
            this.DataContext = searchMedicineViewModel;
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
