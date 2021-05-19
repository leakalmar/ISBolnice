using Model;
using System.Windows;
using System.Windows.Controls;
using Hospital_IS.DoctorViewModel;
using System.ComponentModel;
using System.Windows.Navigation;

namespace Hospital_IS.DoctorView
{
    public partial class UCAppDetail : UserControl
    {
        private AppointmentDetailViewModel viewModel;

        public AppointmentDetailViewModel _ViewModel
        {
            get { return viewModel; }
            set { viewModel = value; }
        }


        public UCAppDetail(NavigationService service)
        {
            InitializeComponent();
            AppointmentDetailViewModel vm = new AppointmentDetailViewModel(service);
            this._ViewModel = vm;
            this.DataContext = vm;
            
        }
        /*

        private void CartBtnClick(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
            DoctorAppointment selectedAppointment = (DoctorAppointment)doctorAppointments.SelectedItem;
           // DoctorHomePage.Instance.Home.Children.Add(new UCPatientChart(selectedAppointment));
        }

        private void StartBtnClick(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
            DoctorAppointment selectedAppointment = (DoctorAppointment)doctorAppointments.SelectedItem;
           // DoctorHomePage.Instance.Home.Children.Add(new UCPatientChart(selectedAppointment, true));
        }

        private void docotrAppointments_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DoctorAppointment app = (DoctorAppointment)doctorAppointments.SelectedItem;
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
        }*/
    }
}
