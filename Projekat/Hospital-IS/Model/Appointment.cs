using System;

namespace Model
{
    public class Appointment
    {
        public DateTime DateAndTime { get; set; }
        public String Date { get; set; }
        public TimeSpan Time { get; set; }
        public AppointmetType Type { get; set; }
        public Boolean Reserved { get; set; }

        public Appointment(DateTime date, AppointmetType type, bool reserved, int room)
        {
            DateAndTime = date;
            Date = date.ToShortDateString();
            Time = date.TimeOfDay;
            Type = type;
            Reserved = reserved;
            Room = room;
        }

        public Boolean StartAppointment()
        {
            throw new NotImplementedException();
        }

        public Boolean EndAppointment()
        {
            throw new NotImplementedException();
        }

        public int room;

        public int Room { get; set; }

    }
}