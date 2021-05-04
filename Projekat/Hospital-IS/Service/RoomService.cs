using Model;
using Storages;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace Service
{
    class RoomService
    {
        private RoomStorage rfs = new RoomStorage();
        public List<Room> allRooms { get; set; }

        private static RoomService instance = null;
        public static RoomService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new RoomService();
                }
                return instance;
            }
        }

        private RoomService()
        {
            allRooms = rfs.GetAll();
           
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

        public Room getRoomById(int roomId)
        {
            foreach (Room r in allRooms)
            {
                if (r.RoomId == roomId)
                {
                    return r;
                }
            }
            return null;
        }

        public void AddRoom(Room newRoom)
        {
         
         allRooms.Add(newRoom);
       
         rfs.SaveRooms(allRooms);
        }

        public void RemoveRoom(Room room)
        {
            foreach (Room r in allRooms)
            {
                if (r.RoomId == room.RoomId)
                {

                    allRooms.Remove(r);

                    break;
                }
            }

            rfs.SaveRooms(allRooms);
        }

        public void UpdateRoom(Room oldRoom)
        {
            foreach (Room r in allRooms)
            {
                if (r.RoomId == oldRoom.RoomId)
                {
                    int index = allRooms.IndexOf(r);
                    allRooms.Remove(r);
                    allRooms.Insert(index, oldRoom);
                    break;
                }
            }
            rfs.SaveRooms(allRooms);
        }

        public List<Room> getRoomByType(RoomType type)
        {
            List<Room> allRoomByType = new List<Room>();

            foreach (Room room in allRooms)
            {
                if (room.Type == type)
                {
                    allRoomByType.Add(room);
                }
            }
            return allRoomByType;
        }

        public List<Room> getAllRooms()
        {
            return allRooms;
        }

        public void SaveChange()
        {
            rfs.SaveRooms(allRooms);
        }

        public void AddEquipment(Room room, Equipment newEquip)
        {
            room.AddEquipment(newEquip);
            rfs.SaveRooms(allRooms);
        }

        public void RemoveEquipment(Room room, Equipment oldEquip)
        {
            room.RemoveEquipment(oldEquip);
            rfs.SaveRooms(allRooms);
        }

      
        public void UpdateEquipment(Room room, Equipment updateEquip)
        {
            room.UpdateEquipment(updateEquip);
            rfs.SaveRooms(allRooms);
        }

    }
}
