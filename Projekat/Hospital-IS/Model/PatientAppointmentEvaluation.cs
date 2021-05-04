using System;
using System.Collections.Generic;
using System.Text;

namespace DTOs
{
    public class PatientAppointmentEvaluation
    {
        public int Grade { get; set; }
        public String Comment { get; set; }
        public DateTime DoctorAppointmentDate { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }

        public PatientAppointmentEvaluation(DateTime doctorAppointmentDate, int patientId, int doctorId)
        {
            this.DoctorAppointmentDate = doctorAppointmentDate;
            this.PatientId = patientId;
            this.DoctorId = doctorId;
        }
    }
}
