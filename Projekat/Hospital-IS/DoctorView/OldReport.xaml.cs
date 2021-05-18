using Controllers;
using Model;
using Storages;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Hospital_IS.DoctorView
{
    public partial class OldReport : Window
    {
        private DoctorAppointment NowAppointment;
        private Report Report;
        private bool _started;

        public bool Started
        {
            get { return _started; }
            set
            {
                _started = value;
                if (_started)
                {
                    reportDetail2.Visibility = Visibility.Collapsed;
                    group.Visibility = Visibility.Visible;
                }
            }
        }
        public OldReport(Report report, DoctorAppointment nowAppointment)
        {
            InitializeComponent();


            Report = report;
            NowAppointment = nowAppointment;
            reportDetail2.Text = Report.Anamnesis;
            reportDetail.Text = Report.Anamnesis;
            medicines.DataContext = ChartController.Instance.GetPrescriptionsForReport(NowAppointment.Patient, Report);
            name.Content = Report.DoctorName;
            surname.Content = Report.DoctorSurname;
            date.Content = Report.ReportId;

        }

        private void Grid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape || e.Key == Key.Back)
            {
                if (Started)
                {
                    //ovde bi isao update reporta
                    //DoctorAppointmentController.Instance.RemoveAppointment(Appointment);
                    //Report.Anamnesis = reportDetail.Text;
                    //DoctorAppointmentController.Instance.AddAppointment(Appointment);
                    ChartController.Instance.UpdateReport(NowAppointment.Patient, Report, reportDetail.Text, medicines.Items.Count);
                }

                //DoctorHomePage.Instance.Home.Children.Clear();
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            if (Started)
            {
                //ovde bi isao update reporta
                //DoctorAppointmentController.Instance.RemoveAppointment(Appointment);
                //Appointment.Report.Anamnesis = reportDetail.Text;
                //DoctorAppointmentController.Instance.AddAppointment(Appointment);
                ChartController.Instance.UpdateReport(NowAppointment.Patient, Report, reportDetail.Text, medicines.Items.Count);
            }

            this.Close();
        }
    }
}
