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
        public List<Room> AllRooms { get; set; }

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
            AllRooms = rfs.GetAll();
           
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

        public Room GetRoomById(int roomId)
        {
            foreach (Room r in AllRooms)
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
         
         AllRooms.Add(newRoom);
       
         rfs.SaveRooms(AllRooms);
        }

        public void RemoveRoom(Room room)
        {
            foreach (Room r in AllRooms)
            {
                if (r.RoomId == room.RoomId)
                {

                    AllRooms.Remove(r);

                    break;
                }
            }

            rfs.SaveRooms(AllRooms);
        }

        public void UpdateRoom(Room oldRoom)
        {
            foreach (Room r in AllRooms)
            {
                if (r.RoomId == oldRoom.RoomId)
                {
                    int index = AllRooms.IndexOf(r);
                    AllRooms.Remove(r);
                    AllRooms.Insert(index, oldRoom);
                    break;
                }
            }
            rfs.SaveRooms(AllRooms);
        }

        public List<Room> GetRoomByType(RoomType type)
        {
            List<Room> allRoomByType = new List<Room>();

            foreach (Room room in AllRooms)
            {
                if (room.Type == type)
                {
                    allRoomByType.Add(room);
                }
            }
            return allRoomByType;
        }

        public List<Room> GetAllRooms()
        {
            return AllRooms;
        }

        public void SaveChange()
        {
            rfs.SaveRooms(AllRooms);
        }

        public void AddEquipment(Room room, Equipment newEquip)
        {
            room.AddEquipment(newEquip);
            rfs.SaveRooms(AllRooms);
        }

        public void RemoveEquipment(Room room, Equipment oldEquip)
        {
            room.RemoveEquipment(oldEquip);
            rfs.SaveRooms(AllRooms);
        }

      
        public Boolean UpdateEquipment(Room room, Equipment updateEquip)
        {
            bool isSucces = room.UpdateEquipment(updateEquip);
            rfs.SaveRooms(AllRooms);
            return isSucces;
        }

    }
}
