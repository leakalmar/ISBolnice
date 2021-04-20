using Newtonsoft.Json;
using System;

namespace Model
{
    public class Appointment
    {

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
            AppointmentStart = date;
            if (type == AppointmetType.CheckUp)
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
