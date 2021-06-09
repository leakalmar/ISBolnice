using Hospital_IS.DoctorViewModel;
using System.Windows.Controls;


namespace Hospital_IS.DoctorRole.DoctorView
{
    public partial class ReportView : UserControl
    {
        public ReportView(ReportViewModel reportViewModel)
        {
            InitializeComponent();
            this.DataContext = reportViewModel;

        }
    }
}
