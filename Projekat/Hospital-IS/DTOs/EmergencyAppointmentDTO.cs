using Enums;
using Model;

namespace Hospital_IS.DTOs
{
    public class EmergencyAppointmentDTO
    {
        public AppointmentType AppointmetType { get; set; }
        public Specialty Specialty { get; set; }
        public Patient Patient { get; set; }
        public Room Room { get; set; }
        public int DurationInMinutes { get; set; }

        public EmergencyAppointmentDTO(AppointmentType appointmetType, Specialty specialty, Patient patient, Room room, int durationInMinutes)
        {
            AppointmetType = appointmetType;
            Specialty = specialty;
            Patient = patient;
            Room = room;
            DurationInMinutes = durationInMinutes;
        }

        public EmergencyAppointmentDTO()
        {
        }
    }
}
