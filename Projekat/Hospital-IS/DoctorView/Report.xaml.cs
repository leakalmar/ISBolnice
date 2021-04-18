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
using Hospital_IS.DoctorView;

namespace Hospital_IS.DoctorView
{
    /// <summary>
    /// Interaction logic for Report.xaml
    /// </summary>
    public partial class Report : Window
    {
        public Report(Model.Report report)
        {
            InitializeComponent();
            reportData.DataContext = report;
        }
        public void ExitBtnClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public void MinimizeBtnClick(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        public void MaximizeBtnClick(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Maximized;
        }

    }
}
