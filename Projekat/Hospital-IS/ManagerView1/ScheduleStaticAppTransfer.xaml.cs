using Hospital_IS.ManagerViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
            MessageBox.Show("uslo");
        }
    }
}
