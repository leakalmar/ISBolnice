using System;
using System.Collections.Generic;

namespace Model
{
    public class Manager : Employee
    {

        public Manager(int id, string name, string surname, DateTime birthDate, string email, string password, string address, double salary, DateTime employmentDate, List<WorkDay> workDays) : base(id, name, surname, birthDate, email, password, address, salary, employmentDate, workDays)
        {
        }

        private static Manager instance = null;

        private Manager()
        {
        }

        public static Manager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Manager();
                }
                return instance;
            }
        }


        public Boolean AddRoom(Room newroom)
        {
            
            Hospital.Instance.AddRoom(newroom);
           
            return true;
        }

        public Boolean RemoveRoom(Room room)
        {
            Hospital.RemoveRoom(room);
            return true;
        }

        public void UpdateRoom(Room room)
        {
            var rooms = new System.Collections.ArrayList();
            rooms = Hospital.room;
            foreach (Room r in rooms)
            {
                if (r == room)
                {
                    r.RoomFloor = room.RoomFloor;
                    r.RoomId = room.RoomId;
                    r.IsFree = room.IsFree;
                    r.Type = room.Type;
                }
            }

        }

        public Boolean MakeAppointment(Room room)
        {
            throw new NotImplementedException();
        }

        public Boolean RemoveAppointment(Appointment appointment)
        {
            throw new NotImplementedException();
        }

    }
}