using Enums;
using Model;
using System;

namespace DTOs
{
    public class DoctorAppointmentDTO
    {
        public Boolean Reserved { get; set; }
        public String AppointmentCause { get; set; }
        public DateTime AppointmentStart { get; set; }
        public DateTime AppointmentEnd { get; set; }
        public AppointmentType Type { get; set; }
        public int Room { get; set; }
        public int Id { get; set; }
        public String NameSurnamePatient { get; set; }
        public String AppTypeText { get; set; }
        public bool IsUrgent { get; set; } = false;
        public Patient Patient { get; set; }
        public Doctor Doctor { get; set; }
        public bool IsFinished { get; set; } = false;

        public DoctorAppointmentDTO(bool reserved, string appointmentCause, DateTime appointmentStart, DateTime appointmentEnd, AppointmentType type, int room, int id, string nameSurnamePatient, string appTypeText, bool isUrgent, Patient patient, Doctor doctor, bool isFinished)
        {
            Reserved = reserved;
            AppointmentCause = appointmentCause;
            AppointmentStart = appointmentStart;
            AppointmentEnd = appointmentEnd;
            Type = type;
            Room = room;
            Id = id;
            NameSurnamePatient = nameSurnamePatient;
            AppTypeText = appTypeText;
            IsUrgent = isUrgent;
            Patient = patient;
            Doctor = doctor;
            IsFinished = isFinished;
        }
    }
}
