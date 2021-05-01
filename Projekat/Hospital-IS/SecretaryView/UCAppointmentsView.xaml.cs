using Controllers;
using Model;
using Storages;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace Hospital_IS.SecretaryView
{
    /// <summary>
    /// Interaction logic for UCAppointmentsView.xaml
    /// </summary>
    public partial class UCAppointmentsView : UserControl
    {
        public ObservableCollection<DoctorAppointment> Appointments { get; set; }

        public UCAppointmentsView()
        {
            InitializeComponent();
            RefreshGrid();

            this.DataContext = this;

        }

        public void RefreshGrid()
        {
            if (Appointments != null)
                Appointments.Clear();

            DoctorAppointmentController.Instance.ReloadDoctorAppointments();
            Appointments = new ObservableCollection<DoctorAppointment>(DoctorAppointmentController.Instance.GetAll());

            foreach (DoctorAppointment appointment in Appointments)
            {
                if (string.IsNullOrEmpty(appointment.AppTypeText))
                {
                    if (appointment.Type == AppointmetType.CheckUp)
                        appointment.AppTypeText = "Pregled";
                    else if (appointment.Type == AppointmetType.Operation)
                        appointment.AppTypeText = "Operacija";
                }
            }

            dataGridAppointments.ItemsSource = Appointments;
        }

        private void ShowAppointment(object sender, RoutedEventArgs e)
        {
            if ((DoctorAppointment)dataGridAppointments.SelectedItem != null)
            {
                DoctorAppointment appointment = (DoctorAppointment)dataGridAppointments.SelectedItem;
                AppointmentView av = new AppointmentView(appointment);
                av.Show();
            }
        }

        private void UpdateAppointment(object sender, RoutedEventArgs e)
        {
            if ((DoctorAppointment)dataGridAppointments.SelectedItem != null)
            {
                DoctorAppointment appointment = (DoctorAppointment)dataGridAppointments.SelectedItem;
                UpdateAppointment ua = new UpdateAppointment(appointment, this);
                ua.Show();
            }
        }

        private void DeleteAppointment(object sender, RoutedEventArgs e)
        {
            if ((DoctorAppointment)dataGridAppointments.SelectedItem != null)
            {
                CancelAppointment ca = new CancelAppointment(this);
                ca.Show();
            }
        }

        private void ScheduleAppointment(object sender, RoutedEventArgs e)
        {
            ScheduleAppointment sa = new ScheduleAppointment(this);
            sa.Show();
        }
    }
}
