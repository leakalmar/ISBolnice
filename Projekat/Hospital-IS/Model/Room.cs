using System;
using System.Collections.ObjectModel;

namespace Model
{
    public class Room
    {
        

       


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

        public string RoomIdType
        {
            get { return RoomId + " " + Type; }
        }
        public Room(RoomType type, bool isFree, bool isUsable, int roomFloor, int roomId)
        {
            this.Type = type;
            this.IsFree = isFree;
            this.IsUsable = isUsable;
            this.RoomFloor = roomFloor;
            this.RoomId = roomId;
        }

        public  ObservableCollection<Equipment> Equipment { get; set; }



        public void AddEquipment(Equipment newEquip)
        {
            if (newEquip == null)
            {
                return;
            }

            if (Equipment == null)
            {
                Equipment = new ObservableCollection<Equipment>();

            }

            if (!Equipment.Contains(newEquip))
            {
                Equipment.Add(newEquip);

            }
        }

        public void RemoveEquipment(Equipment oldEquip)
        {
            foreach (Equipment r in Equipment)
            {
                if (r.EquiptId == oldEquip.EquiptId)
                {

                    Equipment.Remove(r);

                    break;
                }
            }
        }

        public void RemoveAllRoom()
        {
            if (Equipment != null)
                Equipment.Clear();
        }
        public void UpdateEquipment(Equipment updateEquip)
        {

            foreach (Equipment r in Equipment)
            {
                if (r.EquiptId == updateEquip.EquiptId)
                {
                    int index = Equipment.IndexOf(r);
                    Equipment.Remove(r);
                    Equipment.Insert(index, updateEquip);
                    break;
                }
            }


        }
        public System.Collections.ArrayList appointment;

        public System.Collections.ArrayList Appointment { get; set; }
     /*   {
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
                newAppointment.Room = this.RoomId;
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
     */
    }
}