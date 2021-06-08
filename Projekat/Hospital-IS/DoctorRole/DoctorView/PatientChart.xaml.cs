using Hospital_IS.DoctorViewModel;
using System.Windows.Controls;

namespace Hospital_IS.DoctorRole.DoctorView
{
    public partial class PatientChart : UserControl
    {
        public PatientChart()
        {
            InitializeComponent();
            PatientChartViewModel.Instance.InsideNavigationService = this.patientInfo.NavigationService;
            this.DataContext = PatientChartViewModel.Instance;
        }

    }
}
