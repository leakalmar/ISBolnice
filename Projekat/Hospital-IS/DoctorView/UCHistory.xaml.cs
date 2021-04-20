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
    /// <summary>
    /// Interaction logic for UCHistory.xaml
    /// </summary>
    public partial class UCHistory : UserControl
    {
        private AppointmentFileStorage afs { get; } = new AppointmentFileStorage();
        public UCHistory(Patient patient)
        {
            InitializeComponent(); ICollectionView view = new CollectionViewSource { Source = afs.GetAllByPatient(patient.Id) }.View; view.Filter = null;
            view.Filter = delegate (object item)
            {
                return ((DoctorAppointment)item).Report != null;
            };
            dataGrid.DataContext = view;
        }

        public void Report_DoubleClicked(object sender, MouseButtonEventArgs e)
        {
            Model.Report report = (Model.Report)dataGrid.SelectedItem;
            //Window window = new Report(report);
            //window.Show();
        }
    }
}
