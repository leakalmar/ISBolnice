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

namespace Hospital_IS.View
{
    /// <summary>
    /// Interaction logic for UCAppDetail.xaml
    /// </summary>
    public partial class UCAppDetail : UserControl
    {
        public UCAppDetail(DoctorAppointment doctorAppointment)
        {
            InitializeComponent();
            docotrAppointments.DataContext = DoctorHomePage.Instance.DoctorAppointment;
            docotrAppointments.SelectedItem = doctorAppointment;
            info.DataContext = doctorAppointment;
            info2.DataContext = doctorAppointment;
        }

        private void CartBtnClick(object sender, RoutedEventArgs e)
        {
            DoctorHomePage.Instance.Home.Content = new UCPatientChart((DoctorAppointment) docotrAppointments.SelectedItem);
        }

        private void StartBtnClick(object sender, RoutedEventArgs e)
        {
            DoctorHomePage.Instance.Home.Content = new UCPatientChart((DoctorAppointment)docotrAppointments.SelectedItem, true);
        }
    }
}
