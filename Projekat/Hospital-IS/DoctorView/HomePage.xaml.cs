using System.Windows.Controls;
using System.Windows.Navigation;
using Hospital_IS.DoctorViewModel;


namespace Hospital_IS.DoctorView
{
    public partial class HomePage : UserControl
    {
        private HomePageViewModel viewModel;
        public HomePageViewModel _ViewModel
        {
            get { return viewModel; }
            set
            {
                viewModel = value;
            }
        }
        public HomePage(NavigationService navigation)
        {
            InitializeComponent();
            this.viewModel = new HomePageViewModel(navigation);
            this.DataContext = this.viewModel;
            this.Focus();
        }
    }
}
