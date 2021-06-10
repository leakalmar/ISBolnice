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
using System.Windows.Shapes;

namespace Hospital_IS.ManagerView1
{
    /// <summary>
    /// Interaction logic for ReportRenovationWindow.xaml
    /// </summary>
    public partial class ReportRenovationWindow : Window
    {
        public ReportRenovationWindow()
        {
            InitializeComponent();
            this.DataContext = RenovationReportVIewModel.Instance;
        }

        private void SendReNotification_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
