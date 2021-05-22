using Model;

namespace DTOs
{
    public class AppointmentRowDTO
    {
        public DoctorAppointment Appointment { get; set; }
        public bool Enabled { get; set; }

        public AppointmentRowDTO(DoctorAppointment appointment, bool enabled)
        {
            Appointment = appointment;
            Enabled = enabled;
        }
    }
}
