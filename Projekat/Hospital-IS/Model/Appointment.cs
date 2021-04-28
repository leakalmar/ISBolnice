using Newtonsoft.Json;
using System;

namespace Model
{
    public class Appointment
    {

        public Boolean Reserved { get; set; }

        public String AppointmentCause { get; set; }

        public  DateTime AppointmentStart { get; set; }
       
        public DateTime AppointmentEnd { get; set; }
        
        public AppointmetType Type { get; set; }
        
        public int Room { get; set; }


        public Appointment(DateTime date, AppointmetType type, bool reserved, int room)
        {
            AppointmentStart = date;
            if (type.Equals(AppointmetType.CheckUp))
            {
                AppointmentEnd = AppointmentStart.AddMinutes(30);
            }
            Type = type;
            Reserved = reserved;
            Room = room;
        }
        
        public Appointment()
        {
        }

        public Appointment(DateTime appointmentstart, DateTime appointmentEnd, AppointmetType type, int room)
        {
            AppointmentStart = appointmentstart;
            AppointmentEnd = appointmentEnd;
            Type = type;
            Room = room;
        }



    }
}
