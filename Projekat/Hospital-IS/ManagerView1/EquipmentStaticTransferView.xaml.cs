using Hospital_IS.ManagerHelp;
using Hospital_IS.ManagerViewModel;
using System.Windows.Controls;

namespace Hospital_IS.ManagerView1
{
    /// <summary>
    /// Interaction logic for EquipmentStaticTransferView.xaml
    /// </summary>
    public partial class EquipmentStaticTransferView : Page
    {
        public EquipmentStaticTransferView()
        {
            InitializeComponent();
            this.DataContext = EquipmentTransferStaticViewModel.Instance;
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            ScheduleAppointmentHelp scheduleAppointmentHelp = new ScheduleAppointmentHelp();
            scheduleAppointmentHelp.ShowDialog();
        }
    }
}
