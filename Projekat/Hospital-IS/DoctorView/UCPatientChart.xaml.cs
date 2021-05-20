using Hospital_IS.DoctorViewModel;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Hospital_IS.DoctorView
{
    public partial class UCPatientChart : UserControl
    {
        private PatientChartViewModel viewModel;
        private static UCPatientChart instance = null;

        public static UCPatientChart Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new UCPatientChart();
                }
                return instance;
            }
        }

        public PatientChartViewModel _ViewModel
        {
            get { return viewModel; }
            set { viewModel = value; }
        }
        public UCPatientChart()
        {
            InitializeComponent();
            PatientChartViewModel vm = new PatientChartViewModel();
            vm.InsideNavigationService = this.patientInfo.NavigationService;
            this._ViewModel = vm;
            this.DataContext = vm;
        }

    }
}
