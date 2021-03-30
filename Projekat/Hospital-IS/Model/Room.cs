using System;

namespace Model
{
    public class Room
    {
        

        public System.Collections.ArrayList equipment;


        public Room()
        {

        }
        public int RoomFloor
        {
            get; set;
        }


        public int RoomId
        {
            get; set;
        }

        public Boolean IsFree
        {
            get; set;
        }

        public Boolean IsUsable
        {
            get; set;
        }

        public RoomType Type
        {
            get; set;
        }

        public Room(RoomType type, bool isFree, bool isUsable, int roomFloor, int roomId)
        {
            this.Type = type;
            this.IsFree = isFree;
            this.IsUsable = isUsable;
            this.RoomFloor = roomFloor;
            this.RoomId = roomId;
        }

        public System.Collections.ArrayList Equipment
        {
            get
            {
                if (equipment == null)
                    equipment = new System.Collections.ArrayList();
                return equipment;
            }
            set
            {
                RemoveAllEquipment();
                if (value != null)
                {
                    foreach (Equipment oEquipment in value)
                        AddEquipment(oEquipment);
                }
            }
        }

        public void AddEquipment(Equipment newEquipment)
        {
            if (newEquipment == null)
                return;
            if (this.equipment == null)
                this.equipment = new System.Collections.ArrayList();
            if (!this.equipment.Contains(newEquipment))
            {
                this.equipment.Add(newEquipment);
                newEquipment.AddRoom(this);
            }
        }

        public void RemoveEquipment(Equipment oldEquipment)
        {
            if (oldEquipment == null)
                return;
            if (this.equipment != null)
                if (this.equipment.Contains(oldEquipment))
                {
                    this.equipment.Remove(oldEquipment);
                    oldEquipment.RemoveRoom(this);
                }
        }

        public void RemoveAllEquipment()
        {
            if (equipment != null)
            {
                System.Collections.ArrayList tmpEquipment = new System.Collections.ArrayList();
                foreach (Equipment oldEquipment in equipment)
                    tmpEquipment.Add(oldEquipment);
                equipment.Clear();
                foreach (Equipment oldEquipment in tmpEquipment)
                    oldEquipment.RemoveRoom(this);
                tmpEquipment.Clear();
            }
        }
        public System.Collections.ArrayList appointment;

        public System.Collections.ArrayList Appointment
        {
            get
            {
                if (appointment == null)
                    appointment = new System.Collections.ArrayList();
                return appointment;
            }
            set
            {
                RemoveAllAppointment();
                if (value != null)
                {
                    foreach (Appointment oAppointment in value)
                        AddAppointment(oAppointment);
                }
            }
        }

        public void AddAppointment(Appointment newAppointment)
        {
            if (newAppointment == null)
                return;
            if (this.appointment == null)
                this.appointment = new System.Collections.ArrayList();
            if (!this.appointment.Contains(newAppointment))
            {
                this.appointment.Add(newAppointment);
                newAppointment.Room = this;
            }
        }


        public void RemoveAppointment(Appointment oldAppointment)
        {
            if (oldAppointment == null)
                return;
            if (this.appointment != null)
                if (this.appointment.Contains(oldAppointment))
                {
                    this.appointment.Remove(oldAppointment);
                    oldAppointment.Room = null;
                }
        }

        public void RemoveAllAppointment()
        {
            if (appointment != null)
            {
                System.Collections.ArrayList tmpAppointment = new System.Collections.ArrayList();
                foreach (Appointment oldAppointment in appointment)
                    tmpAppointment.Add(oldAppointment);
                appointment.Clear();
                foreach (Appointment oldAppointment in tmpAppointment)
                    oldAppointment.Room = null;
                tmpAppointment.Clear();
            }
        }

    }
}