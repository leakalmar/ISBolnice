using Hospital_IS.DoctorViewModel;
using System.Windows.Controls;

namespace Hospital_IS.DoctorRole.DoctorView
{
    public partial class OldReport : UserControl
    {
        public OldReport(OldReportViewModel oldReportViewModel)
        {
            InitializeComponent();
            this.DataContext = oldReportViewModel;

        }

    }
}
