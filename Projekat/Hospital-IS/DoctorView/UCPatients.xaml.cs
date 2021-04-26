using Controllers;
using Hospital_IS.Storages;
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
    /// Interaction logic for UCPatients.xaml
    /// </summary>
    public partial class UCPatients : UserControl
    {
        PatientFileStorage pfs = new PatientFileStorage();
        public UCPatients()
        {
            InitializeComponent();
            ICollectionView app = new CollectionViewSource { Source = PatientController.Instance.GetAll()}.View;

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
            AppointmentFileStorage afs = new AppointmentFileStorage();
            List<DoctorAppointment> appointments = afs.GetAllByPatient(patient.Id);
            DoctorHomePage.Instance.Home.Children.Add(new UCPatientChart(appointments[0]));
            this.Visibility = Visibility.Collapsed;
        }

        private void UserControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            ICollectionView app = CollectionViewSource.GetDefaultView(pfs.GetAll());
           // app.Filter = delegate (object item)
            //{
           //     return ((DoctorAppointment)item).AppointmentStart.Date == DateTime.Now.Date;
            //};
            //app.SortDescriptions.Add(new SortDescription("AppointmentStart", ListSortDirection.Ascending));
            patients.DataContext = app;
        }
    }
}
