using Hospital_IS.DoctorViewModel;
using System.Windows.Controls;

namespace Hospital_IS.DoctorRole.DoctorView
{
    public partial class ScheduledApp : UserControl
    {
        private ScheduleAppointmentViewModel viewModel;

        public ScheduleAppointmentViewModel _ViewModel
        {
            get { return viewModel; }
            set { viewModel = value; }
        }

        public ScheduledApp()
        {
            InitializeComponent();
            this._ViewModel = new ScheduleAppointmentViewModel();
            this.DataContext = _ViewModel;
        }

    }
}
