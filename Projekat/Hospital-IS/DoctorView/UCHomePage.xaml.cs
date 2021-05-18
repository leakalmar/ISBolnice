using Model;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Hospital_IS.DoctorView;
using System.ComponentModel;
using System.Windows.Data;

namespace Hospital_IS.DoctorView
{
    /// <summary>
    /// Interaction logic for UserControlHomePage.xaml
    /// </summary>
    public partial class UCHomePage : UserControl
    {
        public UCHomePage()
        {
            InitializeComponent();

            ICollectionView app = new CollectionViewSource { Source = DoctorMainWindow.Instance.DoctorAppointment }.View;
            
            app.Filter = delegate (object item)
            {
                return ((DoctorAppointment)item).AppointmentStart.Date == DateTime.Now.Date;
            };
            app.SortDescriptions.Add(new SortDescription("AppointmentStart", ListSortDirection.Ascending));
            docotrAppointments.DataContext = app;

        }

        private void Patient_Selected(object sender, KeyEventArgs e)
        {
            DoctorAppointment appointment= (DoctorAppointment)docotrAppointments.SelectedItem;
            //DoctorHomePage.Instance.Home.Children.Add(new UCPatientChart(appointment));
        }

        public void Patient_DoubleClicked(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
            DoctorAppointment ap = (DoctorAppointment)docotrAppointments.SelectedItem;
            //DoctorHomePage.Instance.Home.Children.Add(new UCAppDetail(ap));


        }

        private void UserControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            ICollectionView app = CollectionViewSource.GetDefaultView(DoctorMainWindow.Instance.DoctorAppointment); 
            app.Filter = delegate (object item)
            {
                return ((DoctorAppointment)item).AppointmentStart.Date == DateTime.Now.Date;
            };
            app.SortDescriptions.Add(new SortDescription("AppointmentStart", ListSortDirection.Ascending));
            docotrAppointments.DataContext = app;
        }
    }
}
