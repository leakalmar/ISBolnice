using Hospital_IS.DoctorViewModel;
using Model;
using System.Windows.Controls;

namespace Hospital_IS.DoctorView
{
    public partial class History : UserControl
    {
        private HistoryViewModel viewModel;

        public HistoryViewModel _ViewModel
        {
            get { return viewModel; }
            set { viewModel = value; }
        }

        public History()
        {
            InitializeComponent();
            this._ViewModel = new HistoryViewModel();
            InitializeComponent();
            this.DataContext = _ViewModel;
        }

    }
}
