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
using Model;
using Hospital_IS.DoctorView;
using System.ComponentModel;

namespace Hospital_IS.DoctorView
{
    /// <summary>
    /// Interaction logic for UserControlHomePage.xaml
    /// </summary>
    public partial class UserControlHomePage : UserControl
    {
        public UserControlHomePage()
        {
            InitializeComponent();
            docotrAppointments.DataContext = DoctorHomePage.Instance.DoctorAppointment;

            ICollectionView view = CollectionViewSource.GetDefaultView(DoctorHomePage.Instance.DoctorAppointment);
            view.Filter = delegate (object item)
            {
                return ((DoctorAppointment)item).DateAndTime.Date == DateTime.Now.Date;
            };
            view.SortDescriptions.Add(new SortDescription("DateAndTime", ListSortDirection.Ascending));
        }

        private void Patient_Selected(object sender, KeyEventArgs e)
        {
            DoctorAppointment appointment= (DoctorAppointment)docotrAppointments.SelectedItem;
            DoctorHomePage.Instance.Home.Content = new UCPatientChart(appointment);
        }

        public void Patient_DoubleClicked(object sender, MouseButtonEventArgs e)
        {
            DoctorAppointment ap = (DoctorAppointment)docotrAppointments.SelectedItem;
            DoctorHomePage.Instance.Home.Content = new UCAppDetail(ap);


        }
    }
}
