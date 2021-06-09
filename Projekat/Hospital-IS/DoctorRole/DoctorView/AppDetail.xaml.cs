using Hospital_IS.DoctorViewModel;
using System.Windows.Controls;

namespace Hospital_IS.DoctorRole.DoctorView
{
    public partial class AppDetail : UserControl
    {
        private AppDetailViewModel viewModel;

        public AppDetailViewModel _ViewModel
        {
            get { return viewModel; }
            set { viewModel = value; }
        }


        public AppDetail()
        {
            InitializeComponent();
            AppDetailViewModel vm = new AppDetailViewModel();
            this._ViewModel = vm;
            this.DataContext = vm;
            
        }
    }
}
