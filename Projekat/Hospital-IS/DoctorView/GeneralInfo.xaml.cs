using Hospital_IS.DoctorViewModel;
using System.Windows.Controls;

namespace Hospital_IS.DoctorView
{
    public partial class GeneralInfo : UserControl
    {
        private GeneralInfoViewModel viewModel;

        public GeneralInfoViewModel _ViewModel
        {
            get { return viewModel; }
            set { viewModel = value; }
        }

        public GeneralInfo()
        {
            this._ViewModel = new GeneralInfoViewModel();
            InitializeComponent();
            this.DataContext = _ViewModel;
        }

    }
}
