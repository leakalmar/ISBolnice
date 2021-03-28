using System;

namespace Model
{
    public class Appointment
    {
        public DateTime DateAndTime { get; set; }
        public String Date {get; set; }
        public TimeSpan Time { get; set; }
        public AppointmetType Type { get; set; }
        public Boolean Reserved { get; set; }

        public Appointment(DateTime date, AppointmetType type, bool reserved, Room room)
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

        public Room room;

        public Room Room
        {
            get
            {
                return room;
            }
            set
            {
                if (this.room == null || !this.room.Equals(value))
                {
                    if (this.room != null)
                    {
                        Room oldRoom = this.room;
                        this.room = null;
                        oldRoom.RemoveAppointment(this);
                    }
                    if (value != null)
                    {
                        this.room = value;
                        this.room.AddAppointment(this);
                    }
                }
            }
        }

    }
}