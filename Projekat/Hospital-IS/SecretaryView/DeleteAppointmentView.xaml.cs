using DTOs;
using Hospital_IS.Controllers;
using System.Windows;

namespace Hospital_IS.SecretaryView
{
    /// <summary>
    /// Interaction logic for DeleteAppointmentView.xaml
    /// </summary>
    public partial class DeleteAppointmentView : Window
    {
        public DoctorAppointmentDTO DoctorAppointment { get; set; }
        public UpdateAppointment uv;
        public DeleteAppointmentView(DoctorAppointmentDTO doctorAppointment, UpdateAppointment uv)
        {
            InitializeComponent();
            this.DoctorAppointment = doctorAppointment;
            this.uv = uv;
        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            NotificationController.Instance.SendAppointmentCancelationNotification(DoctorAppointment);
            DoctorAppointmentManagementController.Instance.RemoveAppointment(DoctorAppointment);
            uv.uca.RefreshGrid();
            this.Close();
            uv.Close();
        }

        private void Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
