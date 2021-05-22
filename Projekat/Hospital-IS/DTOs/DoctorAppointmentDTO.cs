using Model;
using System;

namespace DTOs
{
    class DoctorAppointmentDTO
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
        public int PatientID { get; set; }
        public int DoctorID { get; set; }
        public bool IsFinished { get; set; }
    }
}
