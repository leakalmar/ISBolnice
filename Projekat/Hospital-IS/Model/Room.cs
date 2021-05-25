using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

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

        public int SurfaceArea { get; set; }
        public int BedNumber { get; set; }

        public int RoomId { get; set; }

        public RoomType Type
        {
            get; set;
        }

        public string RoomIdType
        {
            get { return RoomId + " " + Type; }
        }

        public List<Equipment> Equipment { get; set; } = new List<Equipment>();

        public Boolean isUsable { get; set; }

        public Room(int roomFloor, int roomNumber, int surfaceArea, int bedNumber,RoomType type)
        {
            RoomFloor = roomFloor;
            RoomNumber = roomNumber;
            SurfaceArea = surfaceArea;
            BedNumber = bedNumber;
            Type = type;
        }

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
        public Boolean UpdateEquipment(Equipment updateEquip)
        {

            foreach (Equipment r in Equipment)
            {
                MessageBox.Show(r.EquiptId.ToString() + " " + updateEquip.EquiptId.ToString());
                if (r.EquiptId == updateEquip.EquiptId)
                {
                    
                    int index = Equipment.IndexOf(r);
                    Equipment.Remove(r);
                    Equipment.Insert(index, updateEquip);
                    return true;
                }
            }

            return false;
        }
      
    }
}