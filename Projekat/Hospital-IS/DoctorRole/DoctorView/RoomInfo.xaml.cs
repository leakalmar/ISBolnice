using Hospital_IS.DoctorRole.DoctorViewModel;
using System.Windows.Controls;

namespace Hospital_IS.DoctorRole.DoctorView
{
    public partial class RoomInfo : UserControl
    {
        private RoomInfoViewModel viewModel;

        public RoomInfoViewModel _ViewModel
        {
            get { return viewModel; }
            set { viewModel = value; }
        }
        public RoomInfo()
        {
            InitializeComponent();
            this._ViewModel = new RoomInfoViewModel();
            this.DataContext = _ViewModel;

        }
    }
}
