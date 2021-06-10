using Hospital_IS.DoctorViewModel;
using System.Windows.Controls;

namespace Hospital_IS.DoctorRole.DoctorView
{
    public partial class Patients : UserControl
    {
        private PatientsViewModel viewModel;

        public PatientsViewModel _ViewModel
        {
            get { return viewModel; }
            set { viewModel = value; }
        }

        public Patients()
        {
            InitializeComponent();
            this._ViewModel = new PatientsViewModel();
            this.DataContext = _ViewModel;

        }
    }
}
