using Hospital_IS.DoctorViewModel;
using System.Windows.Controls;

namespace Hospital_IS.DoctorView
{
    public partial class UCScheduledApp : UserControl
    {
        private ScheduleAppointmentViewModel viewModel;

        public ScheduleAppointmentViewModel _ViewModel
        {
            get { return viewModel; }
            set { viewModel = value; }
        }

        public UCScheduledApp()
        {
            InitializeComponent();
            this._ViewModel = new ScheduleAppointmentViewModel();
            this.DataContext = _ViewModel;
        }

    }
}
