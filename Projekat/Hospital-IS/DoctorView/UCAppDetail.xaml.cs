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
using Hospital_IS.DoctorView;
using System.ComponentModel;

namespace Hospital_IS.DoctorView
{
    public partial class UCAppDetail : UserControl
    {

        public UCAppDetail(DoctorAppointment doctorAppointment)
        {
            InitializeComponent();
            docotrAppointments.DataContext = DoctorHomePage.Instance.DoctorAppointment;
            docotrAppointments.SelectedItem = doctorAppointment;
            info.DataContext = doctorAppointment;
            info2.DataContext = doctorAppointment;
            

            ICollectionView view = new CollectionViewSource { Source = DoctorHomePage.Instance.DoctorAppointment }.View;
            view.Filter = delegate (object item)
            {
                return ((DoctorAppointment)item).AppointmentStart.Date == DateTime.Now.Date;
            };
            view.SortDescriptions.Add(new SortDescription("AppointmentStart", ListSortDirection.Ascending));

            docotrAppointments.DataContext = view;
        }

        private void CartBtnClick(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
            DoctorHomePage.Instance.Home.Children.Add(new UCPatientChart((DoctorAppointment) docotrAppointments.SelectedItem));
        }

        private void StartBtnClick(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
            DoctorHomePage.Instance.Home.Children.Add(new UCPatientChart((DoctorAppointment)docotrAppointments.SelectedItem, true));
        }

        private void docotrAppointments_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DoctorAppointment app = (DoctorAppointment)docotrAppointments.SelectedItem;
            if(app.Report != null)
            {
                start.Visibility = Visibility.Hidden;
            }
            else
            {
                start.Visibility = Visibility.Visible;
            }
            info.DataContext = app;
            info2.DataContext = app;
        }
    }
}
