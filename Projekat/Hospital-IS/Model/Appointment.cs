using Newtonsoft.Json;
using System;

namespace Model
{
    public class Appointment
    {
        
        public DateTime DateAndTime { get; set; }
        public String Date {get; set; }
        public TimeSpan Time { get; set; }
       
        public Boolean Reserved { get; set; }

        [JsonProperty]
        public  DateTime AppointmentStart { get; set; }
        [JsonProperty]
        public DateTime AppointmentEnd { get; set; }
        [JsonProperty]
        public AppointmetType Type { get; set; }
        [JsonProperty]
        public int Room { get; set; }


        public Appointment(DateTime date, AppointmetType type, bool reserved, int room)
        {
            DateAndTime = date;
            Date = date.ToShortDateString();
            Time = date.TimeOfDay;
            Type = type;
            Reserved = reserved;
            Room = room;
        }




        [JsonConstructor]
        public Appointment(DateTime appointmentstart, DateTime appointmentEnd, AppointmetType type, int room)
        {
            AppointmentStart = appointmentstart;
            AppointmentEnd = appointmentEnd;
            Type = type;
            Room = room;
        }

        

    }
}