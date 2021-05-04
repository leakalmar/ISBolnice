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
        public UCHistory(UCPatientChart patientChart)
        {
            InitializeComponent();
            ObservableCollection<Report> reports = new ObservableCollection<Report>(ChartController.Instance.GetReportsByPatient(patientChart.Patient));
            dataGrid.DataContext = reports;
            Appointment = patientChart.Appointment;
        }

        public void Report_DoubleClicked(object sender, MouseButtonEventArgs e)
        {
            Report report = (Report)dataGrid.SelectedItem;
            if(report.ReportId.Equals(Appointment.AppointmentStart))
            {
                OldReport r = new OldReport(report, Appointment);
                r.Started = true;
                r.Show();
            }
            else
            {
                OldReport r = new OldReport(report, Appointment);
                r.Started = false;
                r.Show();
            }
            
        }
    }
}
