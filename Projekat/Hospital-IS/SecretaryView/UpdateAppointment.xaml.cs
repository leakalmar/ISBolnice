using Controllers;
using Hospital_IS.Controllers;
using Model;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Media;

namespace Hospital_IS.SecretaryView
{
    /// <summary>
    /// Interaction logic for UpdateAppointment.xaml
    /// </summary>
    public partial class UpdateAppointment : Window
    {
        public DoctorAppointment OldDocAppointment { get; set; }

        public DoctorAppointment NewDocAppointment { get; set; }

        UCAppointmentsView uca;

        public UpdateAppointment(DoctorAppointment appointment, UCAppointmentsView uca)
        {
            InitializeComponent();
            OldDocAppointment = appointment;
            this.uca = uca;

            this.DataContext = this;

            if (OldDocAppointment.Type == AppointmetType.CheckUp)
            {
                txtAppType.Text = "Pregled";
                txtEndOfApp.IsEnabled = false;
            }
            else if (OldDocAppointment.Type == AppointmetType.Operation)
            {
                txtAppType.Text = "Operacija";
            }

            txtAppDate.Text = OldDocAppointment.AppointmentStart.ToString("dd.MM.yyyy.");
            txtStartOfApp.Text = OldDocAppointment.AppointmentStart.ToString("HH:mm");
            txtEndOfApp.Text = OldDocAppointment.AppointmentEnd.ToString("HH:mm");

            NewDocAppointment = new DoctorAppointment(OldDocAppointment);
        }

        private void ChangeAppointment(object sender, RoutedEventArgs e)
        {
            SendNotification(OldDocAppointment, NewDocAppointment);

            DoctorAppointmentController.Instance.UpdateAppointment(OldDocAppointment, NewDocAppointment);
            uca.RefreshGrid();
            this.Close();
        }

        private void SendNotification(DoctorAppointment oldApp, DoctorAppointment appointment)
        {
            string title = "Pomeren pregled";

            string text = "Pregled koji ste imali " + oldApp.AppointmentStart.ToString("dd.MM.yyyy.") + " u "
                + oldApp.AppointmentStart.ToString("HH:mm") + "h je pomeren za "
                + appointment.AppointmentStart.ToString("dd.MM.yyyy.") + " u " + appointment.AppointmentStart.ToString("HH:mm") + "h.";

            Notification notification = new Notification(title, text, DateTime.Now);
            notification.Recipients.Add(appointment.Patient.Id);
            notification.Recipients.Add(appointment.Doctor.Id);

            NotificationController.Instance.AddNotification(notification);
        }

        private void Close(object sender, RoutedEventArgs e)
        {
            uca.RefreshGrid();
            this.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            uca.RefreshGrid();
        }


        private void txtEndOfApp_LostFocus(object sender, RoutedEventArgs e)
        {
            VerifyAppointment();
        }

        private void txtStartOfApp_LostFocus(object sender, RoutedEventArgs e)
        {
            if (OldDocAppointment.Type == AppointmetType.CheckUp && !string.IsNullOrEmpty(txtStartOfApp.Text))
            {
                DateTime appStart = DateTime.ParseExact(txtStartOfApp.Text, "HH:mm", CultureInfo.InvariantCulture);
                DateTime appEnd = appStart.AddMinutes(30);
                txtEndOfApp.Text = appEnd.ToString("t", DateTimeFormatInfo.InvariantInfo);

                VerifyAppointment();
            }
        }

        private void VerifyAppointment()
        {
            // datum, vreme i trajanje pregleda
            try
            {
                DateTime appDate = DateTime.ParseExact(txtAppDate.Text, "dd.MM.yyyy.", CultureInfo.InvariantCulture);
                NewDocAppointment.AppointmentStart = appDate;

                DateTime appStart = DateTime.ParseExact(txtStartOfApp.Text, "HH:mm", CultureInfo.InvariantCulture);
                NewDocAppointment.AppointmentStart = appDate.Date.Add(appStart.TimeOfDay);

                if (NewDocAppointment.Type == AppointmetType.CheckUp)
                {
                    NewDocAppointment.AppointmentEnd = NewDocAppointment.AppointmentStart.AddMinutes(30);
                }
                else if (NewDocAppointment.Type == AppointmetType.Operation)
                {
                    DateTime appEnd = DateTime.ParseExact(txtEndOfApp.Text, "HH:mm", CultureInfo.InvariantCulture);
                    NewDocAppointment.AppointmentEnd = appDate.Date.Add(appEnd.TimeOfDay);
                }

            }
            catch (Exception ex)
            {
            }

            EnableAppointmentConfirmation(DoctorAppointmentController.Instance.VerifyAppointment(NewDocAppointment, null));
        }

        private void EnableAppointmentConfirmation(bool isValid)
        {
            if (isValid)
            {
                btnConfirm.IsEnabled = true;
                txtStartOfApp.Background = new SolidColorBrush(Colors.White);
                txtEndOfApp.Background = new SolidColorBrush(Colors.White);
            }
            else
            {
                txtStartOfApp.Background = new SolidColorBrush(Colors.Red);
                txtEndOfApp.Background = new SolidColorBrush(Colors.Red);
                btnConfirm.IsEnabled = false;
            }
        }

    }
}
