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

        public DoctorAppointmentDTO(DoctorAppointment doctorAppointment)
        {
            Reserved = doctorAppointment.Reserved;
            AppointmentCause = doctorAppointment.AppointmentCause;
            AppointmentStart = doctorAppointment.AppointmentStart;
            AppointmentEnd = doctorAppointment.AppointmentEnd;
            Type = doctorAppointment.Type;
            Room = doctorAppointment.Room;
            Id = doctorAppointment.Id;
            NameSurnamePatient = doctorAppointment.NameSurnamePatient;
            AppTypeText = doctorAppointment.AppTypeText;
            IsUrgent = doctorAppointment.IsUrgent;
            Patient = doctorAppointment.Patient;
            Doctor = doctorAppointment.Doctor;
            IsFinished = doctorAppointment.IsFinished;
        }
    }
}
