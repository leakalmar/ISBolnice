using Hospital_IS.DoctorViewModel;
using System.Windows.Controls;

namespace Hospital_IS.DoctorView
{
    public partial class UCGeneralInfo : UserControl
    {
        private GeneralInfoViewModel viewModel;

        public GeneralInfoViewModel _ViewModel
        {
            get { return viewModel; }
            set { viewModel = value; }
        }

        public UCGeneralInfo()
        {
            this._ViewModel = new GeneralInfoViewModel();
            InitializeComponent();
            this.DataContext = _ViewModel;
        }

    }
}
