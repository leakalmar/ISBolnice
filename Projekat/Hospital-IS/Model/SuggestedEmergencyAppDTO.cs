using System;
using System.Collections.Generic;
using System.Windows;

namespace Model
{
    public class SuggestedEmergencyAppDTO
    {
        public DoctorAppointment SuggestedAppointment { get; set; }
        public List<DoctorAppointment> ConflictingAppointments { get; set; } 
        public List<DoctorAppointment> RescheduledAppointments { get; set; }
        public int TotalReshedulePeriodInDays { get; set; }

        public SuggestedEmergencyAppDTO(DoctorAppointment appointment)
        {
            SuggestedAppointment = appointment;
            ConflictingAppointments = new List<DoctorAppointment>();
            RescheduledAppointments = new List<DoctorAppointment>();
        }

        public void CalculateTotalReschedulePeriod()
        {
            TotalReshedulePeriodInDays = 0;
            if (ConflictingAppointments.Count > 0)
                for (int i = 0; i < ConflictingAppointments.Count; i++)
                {
                    TimeSpan period = RescheduledAppointments[i].AppointmentStart - ConflictingAppointments[i].AppointmentStart;
                    int days = (int)period.TotalDays;
                    TotalReshedulePeriodInDays += days;
                }
        }

    }
}
