using Hospital_IS.DoctorViewModel;
using System;
using System.Windows.Controls;

namespace Hospital_IS.DoctorRole.DoctorView
{
    public partial class NewApp : UserControl
    {
        private NewAppViewModel viewModel;

        public NewAppViewModel _ViewModel
        {
            get { return viewModel; }
            set { viewModel = value; }
        }

        public NewApp()
        {
            InitializeComponent();
            this._ViewModel = new NewAppViewModel();
            this.DataContext = _ViewModel;
        }

    }

}
