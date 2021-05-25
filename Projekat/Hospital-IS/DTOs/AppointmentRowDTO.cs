namespace DTOs
{
    public class AppointmentRowDTO
    {
        public DoctorAppointmentDTO Appointment { get; set; }
        public bool Enabled { get; set; }
        public bool Started { get; set; } =  false;

        public AppointmentRowDTO(DoctorAppointmentDTO appointment, bool enabled)
        {
            Appointment = appointment;
            Enabled = enabled;
        }
    }
}
