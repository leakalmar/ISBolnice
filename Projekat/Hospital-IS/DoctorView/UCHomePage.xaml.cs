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
using Hospital_IS.DoctorViewModel;
using Microsoft.VisualBasic;


namespace Hospital_IS.DoctorView
{
    public partial class UCHomePage : UserControl
    {
        private HomePageViewModel viewModel;
        public HomePageViewModel _ViewModel
        {
            get { return viewModel; }
            set
            {
                viewModel = value;
            }
        }
        public UCHomePage()
        {
            InitializeComponent();
            NavigationService service = NavigationService.GetNavigationService(this);
            this.viewModel = new HomePageViewModel(service);
            this.DataContext = this.viewModel;
        }

        /*private void Patient_Selected(object sender, KeyEventArgs e)
        {
            DoctorAppointment appointment= (DoctorAppointment)docotrAppointments.SelectedItem;
            //DoctorHomePage.Instance.Home.Children.Add(new UCPatientChart(appointment));
        }*/

        private void docotrAppointments_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DoctorAppointmentViewModel selectedAppointment = (DoctorAppointmentViewModel)docotrAppointments.SelectedItem;
            _ViewModel.SelectedAppointment = selectedAppointment;
        }

        private void FrameworkElement_KeyDown(object sender, KeyEventArgs e)
        {
            DoctorAppointmentViewModel selectedAppointment = (DoctorAppointmentViewModel)docotrAppointments.SelectedItem;

            if (e.Key == Key.Enter && selectedAppointment != null)
            {
                NavigationService service = NavigationService.GetNavigationService(this);
                UCAppDetail appDetail = new UCAppDetail(selectedAppointment);
                service.Navigate(appDetail);
            }
        }



        /*public void Patient_DoubleClicked(object sender, MouseButtonEventArgs e)
        {
            DoctorAppointmentViewModel vmDoctorAppointment = (DoctorAppointmentViewModel)docotrAppointments.SelectedItem;
            UCAppDetail appDetail = new UCAppDetail(vmDoctorAppointment);
            
            service.Navigate(appDetail);
            //DoctorHomePage.Instance.Home.Children.Add(new UCAppDetail(ap));


        }*/

        /*private void UserControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            ICollectionView app = CollectionViewSource.GetDefaultView(_ViewModel.DoctorAppointments); 
            app.Filter = delegate (object item)
            {
                return ((DoctorAppointment)item).AppointmentStart.Date == DateTime.Now.Date;
            };
            app.SortDescriptions.Add(new SortDescription("AppointmentStart", ListSortDirection.Ascending));
            docotrAppointments.DataContext = app;
        }*/
    }
}
