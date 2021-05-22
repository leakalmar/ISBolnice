using Controllers;
using Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace Hospital_IS.DoctorView
{
    public partial class Patients : UserControl
    {
        public Patients()
        {
            InitializeComponent();
            ObservableCollection<Patient> allPatients = new ObservableCollection<Patient>(PatientController.Instance.GetAll());
            ICollectionView app = new CollectionViewSource { Source = allPatients }.View;

            //app.Filter = delegate (object item)
            //{
            //    return ((DoctorAppointment)item).AppointmentStart.Date == DateTime.Now.Date;
            //};
            //app.SortDescriptions.Add(new SortDescription("AppointmentStart", ListSortDirection.Ascending));
            patients.DataContext = app;
        }

        private void DataGridRow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Patient patient = (Patient)patients.SelectedItem;
            List<DoctorAppointment> appointments = DoctorAppointmentController.Instance.GetAllAppointmentsByPatient(patient.Id);
           // DoctorHomePage.Instance.Home.Children.Add(new UCPatientChart(appointments[0]));
            this.Visibility = Visibility.Collapsed;
        }

        private void UserControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            ObservableCollection<Patient> allPatients = new ObservableCollection<Patient> (PatientController.Instance.GetAll());
            ICollectionView app = CollectionViewSource.GetDefaultView(allPatients);
           // app.Filter = delegate (object item)
            //{
           //     return ((DoctorAppointment)item).AppointmentStart.Date == DateTime.Now.Date;
            //};
            //app.SortDescriptions.Add(new SortDescription("AppointmentStart", ListSortDirection.Ascending));
            patients.DataContext = app;
        }
    }
}
