using Hospital_IS.DoctorViewModel;
using System.Windows.Controls;

namespace Hospital_IS.DoctorRole.DoctorView
{
    public partial class ScheduledApp : UserControl
    {
        public ScheduledApp(ScheduleAppointmentViewModel scheduleAppointmentViewModel)
        {
            InitializeComponent();
            this.DataContext = scheduleAppointmentViewModel;
        }

    }
}
