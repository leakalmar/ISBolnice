using Model;
using Storages;
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
            AppointmentFileStorage afs = new AppointmentFileStorage();
            Appointments = afs.GetAll();

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
            this.DataContext = this;

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

        }

        private void ScheduleAppointment(object sender, RoutedEventArgs e)
        {
            ScheduleAppointment sa = new ScheduleAppointment(this);
            sa.Show();
        }
    }
}
