using Enums;
using Hospital_IS.DTOs.SecretaryDTOs;
using Model;
using System;
using System.Collections.Generic;

namespace Hospital_IS.DTOs
{
    public class EmergencyAppointmentDTO
    {
        public AppointmentType AppointmetType { get; set; }
        public string Specialty { get; set; }
        public PatientDTO Patient { get; set; }
        public RoomDTO Room { get; set; }
        public int DurationInMinutes { get; set; }
        public List<DateTime> RequestedDates { get; set; }

        public EmergencyAppointmentDTO(AppointmentType appointmetType, string specialty, PatientDTO patient, RoomDTO room, int durationInMinutes)
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
