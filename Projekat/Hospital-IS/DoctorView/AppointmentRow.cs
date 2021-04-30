using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital_IS.DoctorView
{
    public class AppointmentRow
    {
        public DoctorAppointment Appointment { get; set; }
        public bool Enabled { get; set; }

        public AppointmentRow(DoctorAppointment appointment, bool enabled)
        {
            Appointment = appointment;
            Enabled = enabled;
        }
    }
}
