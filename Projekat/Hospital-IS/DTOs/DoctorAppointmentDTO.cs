using Enums;
using Hospital_IS.DTOs;
using Hospital_IS.DTOs.SecretaryDTOs;
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
        public PatientDTO Patient { get; set; }
        public DoctorDTO Doctor { get; set; }
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
            //Patient = doctorAppointment.Patient;
            //Doctor = doctorAppointment.Doctor;
            IsFinished = doctorAppointment.IsFinished;
        }

        public DoctorAppointmentDTO(bool reserved, string appointmentCause, DateTime appointmentStart, 
            DateTime appointmentEnd, AppointmentType type, int room, int id, bool isUrgent, PatientDTO patient, DoctorDTO doctor, bool isFinished)
        {
            Reserved = reserved;
            AppointmentCause = appointmentCause;
            AppointmentStart = appointmentStart;
            AppointmentEnd = appointmentEnd;
            Type = type;
            Room = room;
            Id = id;
            IsUrgent = isUrgent;
            Patient = patient;
            Doctor = doctor;
            IsFinished = isFinished;

            NameSurnamePatient = Patient.Name + " " + Patient.Surname;
            if (Type.Equals(AppointmentType.CheckUp))
                AppTypeText = "Pregled";
            else if (Type.Equals(AppointmentType.Operation))
                AppTypeText = "Operacija";
        }
    }
}
