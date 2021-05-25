namespace DTOs
{
    public class AppointmentRowDTO
    {
        public DoctorAppointmentDTO Appointment { get; set; }
        public bool IsEnabled { get; set; }
        public bool IsStarted { get; set; } =  false;

        public AppointmentRowDTO(DoctorAppointmentDTO appointment, bool enabled)
        {
            Appointment = appointment;
            IsEnabled = enabled;
        }
    }
}
