namespace Model
{
    public class RescheduledAppointmentDTO
    {
        public DoctorAppointment NewDocAppointment { get; set; } = new DoctorAppointment();
        public DoctorAppointment OldDocAppointment { get; set; } = new DoctorAppointment();

        public RescheduledAppointmentDTO(DoctorAppointment doctorAppointment)
        {
            NewDocAppointment.Reserved = doctorAppointment.Reserved;
            NewDocAppointment.AppointmentCause = doctorAppointment.AppointmentCause;
            NewDocAppointment.AppointmentStart = doctorAppointment.AppointmentStart;
            NewDocAppointment.AppointmentEnd = doctorAppointment.AppointmentEnd;
            NewDocAppointment.Type = doctorAppointment.Type;
            NewDocAppointment.Room = doctorAppointment.Room;
            NewDocAppointment.Doctor = doctorAppointment.Doctor;
            NewDocAppointment.Patient = doctorAppointment.Patient;
            NewDocAppointment.AppTypeText = doctorAppointment.AppTypeText;

            OldDocAppointment = new DoctorAppointment(NewDocAppointment);
        }
    }
}
