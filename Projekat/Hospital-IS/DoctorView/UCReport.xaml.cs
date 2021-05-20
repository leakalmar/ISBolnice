using Hospital_IS.DoctorViewModel;
using System.Windows.Controls;


namespace Hospital_IS.DoctorView
{
    public partial class UCReport : UserControl
    {
        private ReportViewModel viewModel;

        public ReportViewModel _ViewModel
        {
            get { return viewModel; }
            set { viewModel = value; }
        }

        public UCReport()
        {
            this._ViewModel = new ReportViewModel();
            InitializeComponent();
            this.DataContext = _ViewModel;

        }
    }
}
