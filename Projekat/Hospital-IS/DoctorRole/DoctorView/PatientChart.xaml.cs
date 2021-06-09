using Hospital_IS.DoctorRole.Commands;
using Hospital_IS.DoctorViewModel;
using System.Windows.Controls;

namespace Hospital_IS.DoctorRole.DoctorView
{
    public partial class PatientChart : UserControl
    {
        public PatientChart()
        {
            InitializeComponent();
            DoctorInsideNavigationController.Instance.NavigationService = this.patientInfo.NavigationService;
            this.DataContext = PatientChartViewModel.Instance;
        }

    }
}
