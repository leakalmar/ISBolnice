using Hospital_IS.DoctorRole.DoctorViewModel;
using System.Windows.Controls;


namespace Hospital_IS.DoctorRole.DoctorView
{
    public partial class Rooms : UserControl
    {
        private RoomsViewModel viewModel;

        public RoomsViewModel _ViewModel
        {
            get { return viewModel; }
            set { viewModel = value; }
        }

        public Rooms()
        {
            InitializeComponent();
            this._ViewModel = new RoomsViewModel();
            _ViewModel.InsideNavigation = this.frame.NavigationService;
            this.DataContext = _ViewModel;

        }
    }
}
