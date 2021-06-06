using Hospital_IS.ManagerViewModel;
using System.Windows;
using System.Windows.Controls;

namespace Hospital_IS.ManagerView1
{
    /// <summary>
    /// Interaction logic for ScheduleStaticAppTransfer.xaml
    /// </summary>
    public partial class ScheduleStaticAppTransfer : Page
    {

        public ScheduleStaticAppTransfer()
        {
            InitializeComponent();
            this.DataContext = ScheduleStaticTransferViewModel.Instance;
           
        }

        private void TransferStart_Error(object sender, ValidationErrorEventArgs e)
        {
            AffirmTransfer.IsEnabled = false;
          
        }

        private void AffirmTransfer_Click(object sender, RoutedEventArgs e)
        {
            if(TransferEnd.Text.Length == 0 || TransferStart.Text.Length == 0)
            {
                MessageBox.Show("Unesite datume u validnom obliku");
            }
            else{
                ScheduleStaticTransferViewModel.Instance.TransferStaticExecute();
            }
        }
    }
}
