using Hospital_IS.DoctorRole.DoctorViewModel;
using System.Windows.Controls;

namespace Hospital_IS.DoctorRole.DoctorView
{
    public partial class FeedbackView : UserControl
    {

        private FeedbackViewModel viewModel;

        public FeedbackViewModel _ViewModel
        {
            get { return viewModel; }
            set { viewModel = value; }
        }

        public FeedbackView()
        {
            this._ViewModel = new FeedbackViewModel();
            InitializeComponent();
            this.DataContext = _ViewModel;
        }

    }
}
