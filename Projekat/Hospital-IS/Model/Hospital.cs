using System;
using System.Collections.Generic;

namespace Model
{
    public class Hospital
    {
        public String Name { get; set; }
        public String City { get; set; }
        public String Country { get; set; }
        public String Address { get; set; }
        public List<Appointment> allAppointments { get; set; }
        public List<Patient> allPatients { get; set; }
        public List<Equipment> allEquipment { get; set; }
        public List<Room> allRooms { get; set; }
        public List<User> allEmployees { get; set; }


        private static Hospital instance = null;

        private Hospital()
        {
        }

        public static Hospital Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Hospital();
                }
                return instance;
            }
        }

        public Hospital(string name, string city, string country, string address)
        {
            Name = name;
            City = city;
            Country = country;
            Address = address;
        }

        public List<Patient> GetAllPatients()
        {
            throw new NotImplementedException();
        }

        public List<Appointment> GellAllAppointmets()
        {
            throw new NotImplementedException();
        }

        public List<Equipment> GetAllEquipments()
        {
            throw new NotImplementedException();
        }

        public List<Room> GetAllRooms()
        {
            throw new NotImplementedException();
        }

        public List<User> GetAllEmployees()
        {
            throw new NotImplementedException();
        }

        public System.Collections.ArrayList room;

        public System.Collections.ArrayList Room
        {
            get
            {
                if (room == null)
                    room = new System.Collections.ArrayList();
                return room;
            }
            set
            {
                RemoveAllRoom();
                if (value != null)
                {
                    foreach (Room oRoom in value)
                        AddRoom(oRoom);
                }
            }
        }

        public void AddRoom(Room newRoom)
        {
           
            if (newRoom == null)
                return;
            if (this.room == null)
                this.room = new System.Collections.ArrayList();
            if (!this.room.Contains(newRoom))
                this.room.Add(newRoom);
        }

        public void RemoveRoom(Room oldRoom)
        {
            if (oldRoom == null)
                return;
            if (this.room != null)
                if (this.room.Contains(oldRoom))
                    this.room.Remove(oldRoom);
        }

        public void RemoveAllRoom()
        {
            if (room != null)
                room.Clear();
        }
        public System.Collections.ArrayList user;

        public System.Collections.ArrayList User
        {
            get
            {
                if (user == null)
                    user = new System.Collections.ArrayList();
                return user;
            }
            set
            {
                RemoveAllUser();
                if (value != null)
                {
                    foreach (User oUser in value)
                        AddUser(oUser);
                }
            }
        }

        public void AddUser(User newUser)
        {
            if (newUser == null)
                return;
            if (this.user == null)
                this.user = new System.Collections.ArrayList();
            if (!this.user.Contains(newUser))
            {
                this.user.Add(newUser);
                newUser.Hospital = this;
            }
        }

        public void RemoveUser(User oldUser)
        {
            if (oldUser == null)
                return;
            if (this.user != null)
                if (this.user.Contains(oldUser))
                {
                    this.user.Remove(oldUser);
                    oldUser.Hospital = null;
                }
        }

        public void RemoveAllUser()
        {
            if (user != null)
            {
                System.Collections.ArrayList tmpUser = new System.Collections.ArrayList();
                foreach (User oldUser in user)
                    tmpUser.Add(oldUser);
                user.Clear();
                foreach (User oldUser in tmpUser)
                    oldUser.Hospital = null;
                tmpUser.Clear();
            }
        }

    }
}