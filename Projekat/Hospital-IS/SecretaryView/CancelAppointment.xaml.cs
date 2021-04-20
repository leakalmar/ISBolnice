using Hospital_IS.Storages;
using Model;
using Storages;
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
            DoctorAppointment appointment = (DoctorAppointment)uca.dataGridAppointments.SelectedItem;

            sendNotification(appointment);

            uca.Appointments.Remove(appointment);
            Hospital.Instance.RemoveAppointment(appointment);
            AppointmentFileStorage afs = new AppointmentFileStorage();
            afs.SaveAppointment(Hospital.Instance.allAppointments);
            this.Close();
        }

        private void sendNotification(DoctorAppointment appointment)
        {
            string title = "Otkazan pregled";

            string text = "Pregled koji ste imali " + appointment.AppointmentStart.ToString("dd.MM.yyyy.") + " u " 
                + appointment.AppointmentStart.ToString("HH:mm") + "h je otkazan.";

            Notification notification = new Notification(title, text, DateTime.Now);

            NotificationFileStorage nfs = new NotificationFileStorage();
            nfs.SaveNotification(notification);

            appointment.Patient.addNotification(notification.Id);
            PatientFileStorage pfs = new PatientFileStorage();
            pfs.UpdatePatient(appointment.Patient);

            appointment.Doctor.addNotification(notification.Id);
            FSDoctor fsd = new FSDoctor();
            fsd.UpdateDoctor(appointment.Doctor);
        }

        private void Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
