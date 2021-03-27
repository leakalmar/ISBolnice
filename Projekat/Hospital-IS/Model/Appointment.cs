using System;

namespace Model
{
    public class Appointment
    {
        public DateTime Date { get; set; }
        public double Time { get; set; }
        public AppointmetType Type { get; set; }
        public Boolean Reserved { get; set; }

        public Appointment(DateTime date, double time, AppointmetType type, bool reserved, Room room)
        {
            this.Date = date;
            this.Time = time;
            this.Type = type;
            this.Reserved = reserved;
            this.Room = room;
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