using Hospital_IS.ManagerHelp;
using Hospital_IS.ManagerViewModel;
using System.Windows;
using System.Windows.Controls;

namespace Hospital_IS.ManagerView1
{
    
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            StaticTransferHelp staticTransfer = new StaticTransferHelp();
            staticTransfer.ShowDialog();
        }
    }
}
