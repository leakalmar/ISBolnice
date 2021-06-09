using Hospital_IS.ManagerViewModel;
using System.Windows;
using System.Windows.Controls;

namespace Hospital_IS.ManagerView1
{
    /// <summary>
    /// Interaction logic for MainMedicineView.xaml
    /// </summary>
    public partial class MainMedicineView : Page
    {
        public MainMedicineView()
        {
            InitializeComponent();

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = MedicineViewModel.Instance;
            MedicineViewModel.Instance.NavService = this.NavigationService;

        }

        private void OtherOptionsButton_Click(object sender, RoutedEventArgs e)
        {
            MedicineOptions.Visibility = Visibility.Hidden;
            if (OtherOptions.Visibility == Visibility.Visible)
            {
                OtherOptions.Visibility = Visibility.Hidden;
                DataGridMedicine.SelectedItem = null;
            }
            else
            {
                OtherOptions.Visibility = Visibility.Visible;
                DataGridMedicine.SelectedItem = null;
            }
        }

        private void Options_Click(object sender, RoutedEventArgs e)
        {

            OtherOptions.Visibility = Visibility.Hidden;
            if (MedicineOptions.Visibility == Visibility.Visible)
            {
                DataGridMedicine.SelectedItem = null;
                MedicineOptions.Visibility = Visibility.Hidden;
            }
            else
            {
                DataGridMedicine.SelectedItem = null;
                MedicineOptions.Visibility = Visibility.Visible;
            }
        }

        private void CloseOptions_Click(object sender, RoutedEventArgs e)
        {
            OtherOptions.Visibility = Visibility.Hidden;
        }
    }
}
