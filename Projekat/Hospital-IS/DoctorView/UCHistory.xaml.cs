using Hospital_IS.DoctorViewModel;
using Model;
using System.Windows.Controls;
using System.Windows.Input;

namespace Hospital_IS.DoctorView
{
    public partial class UCHistory : UserControl
    {
        private HistoryViewModel viewModel;

        public HistoryViewModel _ViewModel
        {
            get { return viewModel; }
            set { viewModel = value; }
        }

        public UCHistory()
        {
            InitializeComponent();
            this._ViewModel = new HistoryViewModel();
            InitializeComponent();
            this.DataContext = _ViewModel;
        }

    }
}
