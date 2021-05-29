using Hospital_IS.DoctorViewModel;
using System.Windows.Controls;

namespace Hospital_IS.DoctorRole.DoctorView
{
    public partial class IssueInstruction : UserControl
    {
        private IssueInstructionViewModel viewModel;

        public IssueInstructionViewModel _ViewModel
        {
            get { return viewModel; }
            set { viewModel = value; }
        }

        public IssueInstruction()
        {
            InitializeComponent();
            this._ViewModel = new IssueInstructionViewModel();
            this.DataContext = _ViewModel;
        }
    }
}
