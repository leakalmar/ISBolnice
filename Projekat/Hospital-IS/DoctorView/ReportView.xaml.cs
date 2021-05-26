using Hospital_IS.DoctorViewModel;
using System.Windows.Controls;


namespace Hospital_IS.DoctorView
{
    public partial class ReportView : UserControl
    {
        private ReportViewModel viewModel;

        public ReportViewModel _ViewModel
        {
            get { return viewModel; }
            set { viewModel = value; }
        }

        public ReportView()
        {
            InitializeComponent();
            this._ViewModel = new ReportViewModel();
            this.DataContext = _ViewModel;

        }
    }
}
