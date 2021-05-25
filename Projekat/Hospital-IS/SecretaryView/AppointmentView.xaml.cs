using DTOs;
using Enums;
using Model;
using System;
using System.Windows;

namespace Hospital_IS.SecretaryView
{
    /// <summary>
    /// Interaction logic for AppointmentView.xaml
    /// </summary>
    public partial class AppointmentView : Window
    {
        public DoctorAppointmentDTO DocAppointment { get; set; }
        public string Duration { get; set; }
        public AppointmentView(DoctorAppointmentDTO appointment)
        {
            InitializeComponent();
            this.DocAppointment = appointment;
            this.DataContext = this;

            if (appointment.Type == AppointmentType.CheckUp)
                txtDuration.Text = "30 min";
            else if (appointment.Type == AppointmentType.Operation)
            {
                TimeSpan span = appointment.AppointmentEnd - appointment.AppointmentStart;
                double minutes = span.TotalMinutes;
                txtDuration.Text = minutes.ToString() + " min";
            }
        }

        private void Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
