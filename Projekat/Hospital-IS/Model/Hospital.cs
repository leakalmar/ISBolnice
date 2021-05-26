using Hospital_IS.Storages;
using Storages;
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
        public List<User> allUsers { get; set; }

        public RoomStorage roomStorage = new RoomStorage();

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
                    FSDoctor dfs = new FSDoctor();
                    PatientFileStorage pfs = new PatientFileStorage();
                    instance.Doctors = dfs.GetAll();
                }
                return instance;
            }
        }

        public List<Doctor> Doctors { get; set; }
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

        public bool CheckQuantity(Room sourceRoom, Equipment equip, int quantity)
        {
            foreach (Equipment eq in sourceRoom.Equipment)
            {
                if (eq.EquiptId == equip.EquiptId)
                {
                    if (equip.Quantity - quantity >= 0)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

       

       

       
    }
}
