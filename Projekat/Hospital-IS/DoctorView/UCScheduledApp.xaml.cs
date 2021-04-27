using Controllers;
using Model;
using Storages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    public partial class UCScheduledApp : UserControl
    {
        private bool _started;

        public bool Started
        {
            get { return _started; }
            set
            {
                _started = value;
                if (!Started)
                {
                    add.Visibility = Visibility.Collapsed;
                }
                else
                {
                    add.Visibility = Visibility.Visible;
                }
            }
        }
        public DoctorAppointment Appointment { get; }
        public UCScheduledApp(DoctorAppointment appointment)
        {
            InitializeComponent();

            ICollectionView view = new CollectionViewSource{ Source = DoctorAppointmentController.Instance.GetFutureAppointmentsByPatient(appointment.Patient.Id)}.View; 
            view.Filter = null;
            view.Filter = delegate (object item)
            {
                return ((DoctorAppointment)item).Report == null && ((DoctorAppointment)item).AppointmentStart > DateTime.Now;
            };
            dataGrid.DataContext = view;
            Appointment = appointment;

            if (!Started)
            {
                add.Visibility = Visibility.Collapsed;
            }
            else
            {
                add.Visibility = Visibility.Visible;
            }
        }

        private void NewApp_Click(object sender, RoutedEventArgs e)
        {
            (this.Parent as Grid).Visibility = Visibility.Collapsed;
            DoctorHomePage.Instance.Home.Children.Add(new UCNewApp(Appointment));
        }
    }
}
