using Hospital_IS.DoctorViewModel;
using System.Windows.Controls;

namespace Hospital_IS.DoctorRole.DoctorView
{
    public partial class Appointments : UserControl
    {
        private AppointmentsViewModel viewModel;

        public AppointmentsViewModel _ViewModel
        {
            get { return viewModel; }
            set { viewModel = value; }
        }

        public Appointments()
        {
            this._ViewModel = new AppointmentsViewModel();
            InitializeComponent();
            _ViewModel.InsideNavigation = this.frame.NavigationService;
            this.DataContext = _ViewModel;
        }
    }
}
