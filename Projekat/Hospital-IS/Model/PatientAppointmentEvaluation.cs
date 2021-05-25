using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class PatientAppointmentEvaluation
    {

        public int Grade { get; set; }
        public String Comment { get; set; }
        public int DoctorAppointmentId { get; set; }

        public PatientAppointmentEvaluation(int doctorAppointmentId)
        {
            this.DoctorAppointmentId = doctorAppointmentId;
        }
    }
}
