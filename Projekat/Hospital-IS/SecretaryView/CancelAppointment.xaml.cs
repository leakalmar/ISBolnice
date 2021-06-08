using Controllers;
using DTOs;
using Hospital_IS.Controllers;
using Model;
using System;
using System.Windows;

namespace Hospital_IS.SecretaryView
{
    /// <summary>
    /// Interaction logic for CancelAppointment.xaml
    /// </summary>
    public partial class CancelAppointment : Window
    {
        UCAppointmentsView uca;
        public CancelAppointment(UCAppointmentsView uca)
        {
            InitializeComponent();
            this.uca = uca;
        }

        private void DeleteAppointment(object sender, RoutedEventArgs e)
        {
            DoctorAppointmentDTO appointment = (DoctorAppointmentDTO)uca.dataGridAppointments.SelectedItem;

            sendNotification(appointment);

            DoctorAppointmentManagementController.Instance.RemoveAppointment(appointment);
            uca.RefreshGrid();

            this.Close();
        }

        private void sendNotification(DoctorAppointmentDTO appointment)
        {
            string title = "Otkazan pregled";

            string text = "Pregled koji ste imali " + appointment.AppointmentStart.ToString("dd.MM.yyyy.") + " u " 
                + appointment.AppointmentStart.ToString("HH:mm") + "h je otkazan.";

            Notification notification = new Notification(title, text, DateTime.Now);
            notification.Recipients.Add(appointment.Patient.Id);
            notification.Recipients.Add(appointment.Doctor.Id);

            NotificationController.Instance.AddNotification(notification);
        }

        private void Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
