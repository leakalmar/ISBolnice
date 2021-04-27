using Controllers;
using Model;
using Storages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// <summary>
    /// Interaction logic for UCHistory.xaml
    /// </summary>
    public partial class UCHistory : UserControl
    {
        private DoctorAppointment Appointment;
        private bool _started;

        public bool Started
        {
            get { return _started; }
            set
            {
                _started = value;
            }
        }
        public UCHistory(DoctorAppointment appointment)
        {
            InitializeComponent();
            ObservableCollection<DoctorAppointment> appointments = new ObservableCollection<DoctorAppointment>(DoctorAppointmentController.Instance.GetAllAppointmentsByPatient(appointment.Patient.Id));
            ICollectionView view = new CollectionViewSource { Source = appointments }.View; view.Filter = null;
            view.Filter = delegate (object item)
            {
                return ((DoctorAppointment)item).Report != null;
            };
            dataGrid.DataContext = view;
            Appointment = appointment;
        }

        public void Report_DoubleClicked(object sender, MouseButtonEventArgs e)
        {
            DoctorAppointment appointment = (DoctorAppointment)dataGrid.SelectedItem;
            if(appointment.AppointmentStart == Appointment.AppointmentStart)
            {
                OldReport r = new OldReport(appointment, Appointment);
                r.Started = true;
                r.Show();
            }
            else
            {
                OldReport r = new OldReport(appointment, Appointment);
                r.Started = false;
                r.Show();
            }
            
        }
    }
}
