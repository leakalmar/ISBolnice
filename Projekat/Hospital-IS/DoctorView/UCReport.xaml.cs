using Controllers;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
    public partial class UCReport : UserControl
    {
        public UCPatientChart PatientChart { get; set; }
        public ObservableCollection<Prescription> Prescriptions { get; set; }
        public Patient Patient { get; set; }
        public UCReport(UCPatientChart patientChart)
        {
            InitializeComponent();
            Prescriptions = new ObservableCollection<Prescription>();
            PatientChart = patientChart;
            Patient = patientChart.Patient;
            medicines.DataContext = Prescriptions;

        }

        private void Perscription_Click(object sender, RoutedEventArgs e)
        {
            PatientChart.Visibility = Visibility.Collapsed;
            DoctorHomePage.Instance.Home.Children.Add(new UCSearchMedicine(PatientChart));
        }

        private void delete_(object sender, KeyEventArgs e)
        {
            Prescriptions.Remove((Prescription)medicines.SelectedItem);
            medicines.Items.Refresh();
        }

        private void delete_prescription(object sender, KeyEventArgs e)
        {
            Prescriptions.Remove((Prescription)medicines.SelectedItem);
            medicines.Items.Refresh();
        }
    }
}
