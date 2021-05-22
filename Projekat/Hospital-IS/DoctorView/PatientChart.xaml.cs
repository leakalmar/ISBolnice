using Hospital_IS.DoctorViewModel;
using System.Windows.Controls;

namespace Hospital_IS.DoctorView
{
    public partial class PatientChart : UserControl
    {
        private PatientChartViewModel viewModel;
        private static PatientChart instance;

        public static PatientChart Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new PatientChart();
                }
                return instance;
            }
        }

        public PatientChartViewModel _ViewModel
        {
            get { return viewModel; }
            set { viewModel = value; }
        }
        public PatientChart()
        {
            InitializeComponent();
            PatientChartViewModel vm = new PatientChartViewModel();
            vm.InsideNavigationService = this.patientInfo.NavigationService;
            this._ViewModel = vm;
            this.DataContext = vm;
        }

    }
}
