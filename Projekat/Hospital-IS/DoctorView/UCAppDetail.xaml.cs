using Model;
using System.Windows;
using System.Windows.Controls;
using Hospital_IS.DoctorViewModel;

namespace Hospital_IS.DoctorView
{
    public partial class UCAppDetail : UserControl
    {

        public UCAppDetail(DoctorAppointmentViewModel doctorAppointment = null)
        {
            InitializeComponent();
            //docotrAppointments.DataContext = DoctorMainWindow.Instance._ViewModel.DoctorAppointments;
            docotrAppointments.SelectedItem = doctorAppointment;
            info.DataContext = doctorAppointment;
            info2.DataContext = doctorAppointment;


            /*ICollectionView view = new CollectionViewSource { Source = DoctorMainWindow.Instance._ViewModel.DoctorAppointments }.View;
            view.Filter = delegate (object item)
            {
                return ((DoctorAppointment)item).AppointmentStart.Date == DateTime.Now.Date;
            };
            view.SortDescriptions.Add(new SortDescription("AppointmentStart", ListSortDirection.Ascending));

            docotrAppointments.DataContext = view;*/
        }

        private void CartBtnClick(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
            DoctorAppointment selectedAppointment = (DoctorAppointment)docotrAppointments.SelectedItem;
           // DoctorHomePage.Instance.Home.Children.Add(new UCPatientChart(selectedAppointment));
        }

        private void StartBtnClick(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
            DoctorAppointment selectedAppointment = (DoctorAppointment)docotrAppointments.SelectedItem;
           // DoctorHomePage.Instance.Home.Children.Add(new UCPatientChart(selectedAppointment, true));
        }

        private void docotrAppointments_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DoctorAppointment app = (DoctorAppointment)docotrAppointments.SelectedItem;
            if (app.IsFinished)
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
