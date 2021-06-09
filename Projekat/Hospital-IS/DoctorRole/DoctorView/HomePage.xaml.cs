using Hospital_IS.DoctorViewModel;
using System.Windows.Controls;


namespace Hospital_IS.DoctorRole.DoctorView
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
        public HomePage()
        {
            InitializeComponent();
            this.viewModel = new HomePageViewModel();
            this.DataContext = this.viewModel;
        }
    }
}
