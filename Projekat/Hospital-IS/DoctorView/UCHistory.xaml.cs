using Model;
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

namespace Hospital_IS.DoctorView
{
    /// <summary>
    /// Interaction logic for UCHistory.xaml
    /// </summary>
    public partial class UCHistory : UserControl
    {
        public UCHistory(Patient patient)
        {
            InitializeComponent();
            dataGrid.DataContext = patient.MedicalHistory.Reports;
        }

        public void Report_DoubleClicked(object sender, MouseButtonEventArgs e)
        {
            Model.Report report = (Model.Report)dataGrid.SelectedItem;
            //Window window = new Report(report);
            //window.Show();
        }
    }
}
