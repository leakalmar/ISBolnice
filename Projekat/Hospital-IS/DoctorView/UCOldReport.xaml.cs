using Hospital_IS.DoctorViewModel;
using System.Windows.Controls;

namespace Hospital_IS.DoctorView
{
    public partial class UCOldReport : UserControl
    {
        private OldReportViewModel viewModel;

        public OldReportViewModel _ViewModel
        {
            get { return viewModel; }
            set { viewModel = value; }
        }

        public UCOldReport()
        {
            InitializeComponent();
            this._ViewModel = new OldReportViewModel();
            this.DataContext = _ViewModel;

        }

    }
}
