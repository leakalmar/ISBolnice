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
            DoctorAppointment first = doctorAppointment;
            foreach (DoctorAppointment docapp in DoctorHomePage.Instance.DoctorAppointment)
            {
                if (docapp.DateAndTime.Date == first.DateAndTime.Date & docapp.DateAndTime.TimeOfDay < first.DateAndTime.TimeOfDay)
                {
                    first = docapp;
                }
            }

            if (!doctorAppointment.Equals(first))
            {
                start.IsEnabled = false;
            }

            ICollectionView view = CollectionViewSource.GetDefaultView(DoctorHomePage.Instance.DoctorAppointment);
            view.Filter = delegate (object item)
            {
                return ((DoctorAppointment)item).DateAndTime.Date == DateTime.Now.Date;
            };
            view.SortDescriptions.Add(new SortDescription("DateAndTime", ListSortDirection.Ascending));

        }

        private void CartBtnClick(object sender, RoutedEventArgs e)
        {
            DoctorHomePage.Instance.Home.Content = new UCPatientChart((DoctorAppointment) docotrAppointments.SelectedItem);
        }

        private void StartBtnClick(object sender, RoutedEventArgs e)
        {
            DoctorHomePage.Instance.Home.Content = new UCPatientChart((DoctorAppointment)docotrAppointments.SelectedItem, true);
        }

        private void docotrAppointments_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DoctorAppointment app = (DoctorAppointment)docotrAppointments.SelectedItem;
            DoctorAppointment first = app;
            foreach (DoctorAppointment docapp in DoctorHomePage.Instance.DoctorAppointment)
            {
                if (docapp.DateAndTime.Date == first.DateAndTime.Date & docapp.DateAndTime.TimeOfDay < first.DateAndTime.TimeOfDay)
                {
                    System.Diagnostics.Debug.WriteLine(docapp.DateAndTime.TimeOfDay);
                    first = docapp;
                }
            }

            if (!app.DateAndTime.TimeOfDay.Equals(first.DateAndTime.TimeOfDay) & app.DateAndTime.Date == first.DateAndTime.Date)
            {
                start.IsEnabled = false;
            }
            else
            {
                start.IsEnabled = true;
            }
            info.DataContext = app;
            info2.DataContext = app;
        }
    }
}
