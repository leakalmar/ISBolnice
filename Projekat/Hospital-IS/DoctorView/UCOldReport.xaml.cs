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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Hospital_IS.DoctorView
{
    //Klasa nije updatovana!
    public partial class UCOldReport : UserControl
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
            }
        }
        public UCOldReport(DoctorAppointment appointment,DoctorAppointment nowAppointment)
        {
            InitializeComponent();
            if(Started)
            {
                reportDetail2.Visibility = Visibility.Collapsed;
                reportDetail.Visibility = Visibility.Visible;
            }

            Appointment = appointment;
            NowAppointment = nowAppointment;
            //reportDetail.Text = Appointment.Report.Anamnesis;
            //medicines.DataContext = Appointment.Patient.MedicalHistory.GetPresciptionByReport(Appointment.AppointmentStart);
            name.Content = Appointment.Doctor.Name;
            surname.Content = Appointment.Doctor.Surname;
            date.Content = Appointment.AppointmentStart;

        }

        private void Grid_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Escape || e.Key == Key.Back)
            {
                if (Started)
                {
                    DoctorAppointmentController.Instance.RemoveAppointment(Appointment);
                    //Appointment.Report.Anamnesis = reportDetail.Text;
                    DoctorAppointmentController.Instance.AddAppointment(Appointment);
                }
                
                DoctorHomePage.Instance.Home.Children.Clear();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Started)
            {
                DoctorAppointmentController.Instance.RemoveAppointment(Appointment);
                //Appointment.Report.Anamnesis = reportDetail.Text;
                DoctorAppointmentController.Instance.AddAppointment(Appointment);
            }

            DoctorHomePage.Instance.Home.Children.Clear();
        }
    }
}
