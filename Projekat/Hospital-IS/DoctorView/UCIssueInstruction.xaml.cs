using Controllers;
using Hospital_IS.Controllers;
using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    public partial class UCIssueInstruction : UserControl, INotifyPropertyChanged
    {
        public UCNewApp SchedulingAppointment { get; set; }
        public DoctorAppointment SelectedAppointment { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        private String _documentMessage;

        public String DocumentMessage
        {
            get { return _documentMessage; }
            set
            {
                if (value != _documentMessage)
                {
                    _documentMessage = value;

                }

                if (!DoctorHomePage.Instance.Doctor.Id.Equals(SelectedAppointment.Doctor.Id))
                {
                    if (value == "")
                    {
                        save.IsEnabled = false;
                    }
                    else
                    {
                        save.IsEnabled = true;
                    }
                }
                else
                {
                    save.IsEnabled = true;
                }
                OnPropertyChanged("DocumentMessage");
            }
        }
        public UCIssueInstruction(UCNewApp schedulingAppointment)
        {
            InitializeComponent();
            cause.BorderBrush = Brushes.PaleVioletRed;
            cause.DataContext = this;

            SchedulingAppointment = schedulingAppointment;

            SelectedAppointment = (DoctorAppointment)((AppointmentRow)SchedulingAppointment.appointments.SelectedItem).Appointment;
            date.Content = SelectedAppointment.AppointmentStart.Date;
            time.Content = SelectedAppointment.AppointmentStart.TimeOfDay;
            documentSpecialty.Content = SelectedAppointment.Doctor.Specialty.Name;

            documentDoctor.Content = SelectedAppointment.Doctor.Name.ToString() + " " + ShortSurname(SelectedAppointment.Doctor);
            thisDoctor.Content = DoctorHomePage.Instance.Doctor.Name.ToString() + " " + ShortSurname(DoctorHomePage.Instance.Doctor);
            today.Content = DateTime.Now.Date;

            if(SelectedAppointment.Doctor.Id != DoctorHomePage.Instance.Doctor.Id)
            {
                save.IsEnabled = false;
            }

            cause.Focus();
        }

        private string ShortSurname(Doctor doctor)
        {
            String newSurname = doctor.Surname;
            if (doctor.Surname.Length > 12)
            {
                String[] surnames = doctor.Surname.Split(" ");
                newSurname = surnames[0].ToCharArray()[0].ToString() + ". " + surnames[1];
            }
            return newSurname;
        }
        private void save_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedAppointment.Reserved == true)
            {
                return;
            }
            SelectedAppointment.AppointmentCause = cause.Text;

            sendNotification(SelectedAppointment);
            DoctorAppointmentController.Instance.AddAppointment(SelectedAppointment);
            DoctorHomePage.Instance.DoctorAppointment.Add(SelectedAppointment);

            DoctorHomePage.Instance.Home.Children.Remove(this);
            DoctorHomePage.Instance.Home.Children.Add(new UCPatientChart(SchedulingAppointment.Appointment, true));
        }

        private void sendNotification(DoctorAppointment appointment)
        {
            string title = "Zakazan pregled";

            string text = "Novi pregled Vam je zakazan " + appointment.AppointmentStart.ToString("dd.MM.yyyy.") + " u "
                + appointment.AppointmentStart.ToString("HH:mm") + ".";

            Notification notification = new Notification(title, text, DateTime.Now);
            notification.Recipients.Add(appointment.Patient.Id);

            NotificationController.Instance.AddNotification(notification);
        }


        private void cancle_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
            SchedulingAppointment.Visibility = Visibility.Visible;
        }

    }
}
