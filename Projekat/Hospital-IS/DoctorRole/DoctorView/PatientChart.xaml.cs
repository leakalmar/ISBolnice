using Hospital_IS.DoctorViewModel;
using System.Windows.Controls;

namespace Hospital_IS.DoctorRole.DoctorView
{
    public partial class PatientChart : UserControl
    {
        public PatientChart(PatientChartViewModel patientChartViewModel)
        {
            InitializeComponent();
            patientChartViewModel.InsideNavigationService = this.patientInfo.NavigationService;
            this.DataContext = patientChartViewModel;
        }

    }
}
