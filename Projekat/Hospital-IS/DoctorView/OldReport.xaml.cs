using Hospital_IS.DoctorViewModel;
using System.Windows.Controls;

namespace Hospital_IS.DoctorView
{
    public partial class OldReport : UserControl
    {
        private OldReportViewModel viewModel;

        public OldReportViewModel _ViewModel
        {
            get { return viewModel; }
            set { viewModel = value; }
        }

        public OldReport()
        {
            InitializeComponent();
            this._ViewModel = new OldReportViewModel();
            this.DataContext = _ViewModel;

        }

    }
}
