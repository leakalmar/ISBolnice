using Hospital_IS.DoctorRole.DoctorViewModel;
using System.Windows.Controls;


namespace Hospital_IS.DoctorRole.DoctorView
{
    public partial class IssuePrescription : UserControl
    {
        private IssuePrescriptionViewModel viewModel;

        public IssuePrescriptionViewModel _ViewModel
        {
            get { return viewModel; }
            set { viewModel = value; }
        }

        public IssuePrescription(bool show)
        {
            InitializeComponent();
            this._ViewModel = new IssuePrescriptionViewModel(show);
            this.DataContext = _ViewModel;
        }
    }
}
