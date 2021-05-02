using System;

namespace Model
{
    public class RescheduledAppointmentDTO
    {
        public DoctorAppointment DocAppointment { get; set; } = new DoctorAppointment();
        public DateTime NewAppointmentStart { get; set; }
        public DateTime NewAppointmentEnd { get; set; }

        public RescheduledAppointmentDTO(DoctorAppointment doctorAppointment)
        {
            DocAppointment.Reserved = doctorAppointment.Reserved;
            DocAppointment.AppointmentCause = doctorAppointment.AppointmentCause;
            DocAppointment.AppointmentStart = doctorAppointment.AppointmentStart;
            DocAppointment.AppointmentEnd = doctorAppointment.AppointmentEnd;
            DocAppointment.Type = doctorAppointment.Type;
            DocAppointment.Room = doctorAppointment.Room;
            DocAppointment.Doctor = doctorAppointment.Doctor;
            DocAppointment.Patient = doctorAppointment.Patient;
            DocAppointment.AppTypeText = doctorAppointment.AppTypeText;
        }
    }
}
