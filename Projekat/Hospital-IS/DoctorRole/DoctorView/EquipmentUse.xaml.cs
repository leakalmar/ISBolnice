using Hospital_IS.DoctorRole.DoctorViewModel;
using System.Windows.Controls;

namespace Hospital_IS.DoctorRole.DoctorView
{
    public partial class EquipmentUse : UserControl
    {
        private EquipmentUseViewModel viewModel;

        public EquipmentUseViewModel _ViewModel
        {
            get { return viewModel; }
            set { viewModel = value; }
        }

        public EquipmentUse()
        {
            this._ViewModel = new EquipmentUseViewModel();
            InitializeComponent();
            this.DataContext = _ViewModel;
        }
    }
}
