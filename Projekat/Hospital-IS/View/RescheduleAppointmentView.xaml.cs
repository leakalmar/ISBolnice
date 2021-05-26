using Hospital_IS.View.PatientViewModels;
using System.Windows.Controls;

namespace Hospital_IS.View
{
    /// <summary>
    /// Interaction logic for RescheduleAppointmentView.xaml
    /// </summary>
    public partial class RescheduleAppointmentView : UserControl
    {
        public RescheduleAppointmentViewModel Res { get; set; }
        public RescheduleAppointmentView()
        {
            Res = new RescheduleAppointmentViewModel();
            this.DataContext = Res;
            InitializeComponent();
            Calendar.DisplayDateStart = Res.Date;
            Calendar.DisplayDateEnd = Res.Date.AddDays(3);
        }
    }
}
