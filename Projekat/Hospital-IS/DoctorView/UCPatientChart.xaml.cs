using Hospital_IS.DoctorViewModel;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Hospital_IS.DoctorView
{
    public partial class UCPatientChart : UserControl
    {
        private PatientChartViewModel viewModel;

        public PatientChartViewModel _ViewModel
        {
            get { return viewModel; }
            set { viewModel = value; }
        }
        public UCPatientChart(NavigationService navigation)
        {
            InitializeComponent();
            PatientChartViewModel vm = new PatientChartViewModel();
            vm.InsideNavigationService = this.patientInfo.NavigationService;
            vm.MainNavigationService = navigation;
            this._ViewModel = vm;
            this.DataContext = vm;
        }

    }
}
