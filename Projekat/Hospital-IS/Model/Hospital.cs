using Storages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

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
                    UserFileStorage ufs = new UserFileStorage();
                    instance.User = ufs.GetAll();
                    ufs.Save(instance.User);
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

        public static ObservableCollection<Room> Room  { get; set; }

       

        public void AddRoom(Room newRoom)
        {
            if (newRoom == null)
            {
                return;
            }

            if (Room == null)
            { 
              Room = new ObservableCollection<Room>();
                
            }

            if (!Room.Contains(newRoom))
            {
                Room.Add(newRoom);
                
            }
        }

        public void RemoveRoom(Room oldRoom)
        {
            foreach (Room r in Room)
            {
                if (r.RoomId == oldRoom.RoomId)
                {
                   
                    Room.Remove(r);
                    
                    break;
                }
            }
        }

        public void RemoveAllRoom()
        {
            if (Room != null)
               Room.Clear();
        }
        public void UpdateRoom(int oldIndex,Room oldRoom)
        {
            
            foreach(Room r in Room)
            {
                if(r.RoomId == oldIndex)
                {
                    int index = Room.IndexOf(r);
                    Room.Remove(r);
                    Room.Insert(index, oldRoom);
                    break;
                }
            }
            
            
        }




        public List<User> User { get; set; }

        public void AddUser(User newUser)
        {
            if (newUser == null)
                return;
            if (this.User == null)
                this.User = new List<User>();
            if (!this.User.Contains(newUser))
            {
                this.User.Add(newUser);
                newUser.Hospital = this;
            }
        }

        public void RemoveUser(User oldUser)
        {
            if (oldUser == null)
                return;
            if (this.User != null)
                if (this.User.Contains(oldUser))
                {
                    this.User.Remove(oldUser);
                    oldUser.Hospital = null;
                }
        }

        public void RemoveAllUser()
        {
            if (User != null)
            {
                List<User> tmpUser = new List<User>();
                foreach (User oldUser in User)
                    tmpUser.Add(oldUser);
                User.Clear();
                foreach (User oldUser in tmpUser)
                    oldUser.Hospital = null;
                tmpUser.Clear();
            }
        }

    }
}