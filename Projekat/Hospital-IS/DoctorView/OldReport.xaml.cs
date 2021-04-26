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
    /// <summary>
    /// Interaction logic for OldReport.xaml
    /// </summary>
    public partial class OldReport : Window
    {
        private DoctorAppointment NowAppointment;
        private DoctorAppointment Appointment;
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
        public OldReport(DoctorAppointment appointment, DoctorAppointment nowAppointment)
        {
            InitializeComponent();
            

            Appointment = appointment;
            NowAppointment = nowAppointment;
            reportDetail2.Text = Appointment.Report.Amnesis;
            reportDetail.Text = Appointment.Report.Amnesis;
            medicines.DataContext = Appointment.Patient.MedicalHistory.GetPresciptionByReport(Appointment.AppointmentStart);
            name.Content = Appointment.Doctor.Name;
            surname.Content = Appointment.Doctor.Surname;
            date.Content = Appointment.AppointmentStart;

        }

        private void Grid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape || e.Key == Key.Back)
            {
                if (Started)
                {
                    //ovde bi isao update reporta
                    DoctorAppointmentController.Instance.RemoveAppointment(Appointment);
                    Appointment.Report.Amnesis = reportDetail.Text;
                    DoctorAppointmentController.Instance.AddAppointment(Appointment);
                }

                DoctorHomePage.Instance.Home.Children.Clear();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Started)
            {
                //ovde bi isao update reporta
                DoctorAppointmentController.Instance.RemoveAppointment(Appointment);
                Appointment.Report.Amnesis = reportDetail.Text;
                DoctorAppointmentController.Instance.AddAppointment(Appointment);
            }

            this.Close();
        }
    }
}
