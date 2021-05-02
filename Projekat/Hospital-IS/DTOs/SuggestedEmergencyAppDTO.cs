using System;
using System.Collections.Generic;

namespace Model
{
    public class SuggestedEmergencyAppDTO
    {
        public DoctorAppointment SuggestedAppointment { get; set; }
        public List<DoctorAppointment> ConflictingAppointments { get; set; } 
        public List<RescheduledAppointmentDTO> RescheduledAppointments { get; set; }
        public int TotalReshedulePeriodInHours { get; set; }

        public SuggestedEmergencyAppDTO(DoctorAppointment appointment)
        {
            SuggestedAppointment = appointment;
            ConflictingAppointments = new List<DoctorAppointment>();
            RescheduledAppointments = new List<RescheduledAppointmentDTO>();
        }

        public void CalculateTotalReschedulePeriod()
        {
            TotalReshedulePeriodInHours = 0;
            if (ConflictingAppointments.Count > 0)
                for (int i = 0; i < ConflictingAppointments.Count; i++)
                {
                    TimeSpan period = RescheduledAppointments[i].DocAppointment.AppointmentStart - ConflictingAppointments[i].AppointmentStart;
                    int days = (int)period.TotalHours;
                    TotalReshedulePeriodInHours += days;
                }
        }

    }
}
