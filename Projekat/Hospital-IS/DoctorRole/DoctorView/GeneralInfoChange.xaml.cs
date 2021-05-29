using Hospital_IS.DoctorRole.DoctorViewModel;
using System.Windows.Controls;


namespace Hospital_IS.DoctorRole.DoctorView
{
    public partial class GeneralInfoChange : UserControl
    {
        private GeneralInfoChangeViewModel viewModel;

        public GeneralInfoChangeViewModel _ViewModel
        {
            get { return viewModel; }
            set { viewModel = value; }
        }

        public GeneralInfoChange()
        {
            this._ViewModel = new GeneralInfoChangeViewModel();
            InitializeComponent();
            this.DataContext = _ViewModel;
        }
    }
}
