using System;
using System.Collections.Generic;
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


        public int RoomNumber
        {
            get; set;
        }

        public int RoomId { get; set; }

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

        public Room(int roomFloor, int roomNumber, int roomId, bool isFree, bool isUsable, RoomType type)
        {
            RoomFloor = roomFloor;
            RoomNumber = roomNumber;
            RoomId = roomId;
            IsFree = isFree;
            IsUsable = isUsable;
            Type = type;
        }

        public List<Equipment> Equipment { get; set; } = new List<Equipment>();



        public void AddEquipment(Equipment newEquip)
        {
            if (newEquip == null)
            {
                return;
            }

            if (Equipment == null)
            {
                Equipment = new List<Equipment>();

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
      
    }
}