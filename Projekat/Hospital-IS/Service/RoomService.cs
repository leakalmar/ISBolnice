using Enums;
using Model;
using Storages;
using System;
using System.Collections.Generic;
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
         newRoom.RoomId = FindMax() + 1;
         AllRooms.Add(newRoom);
       
         rfs.SaveRooms(AllRooms);
        }

        private int FindMax()
        {
            int maxId = -1;
            foreach(Room  r in AllRooms)
            {
                if(maxId < r.RoomId)
                {
                    maxId = r.RoomId;
                }
            }
            return maxId;
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

        public bool CheckIfRoomNumberIsUnique(int roomNumber)
        {
           foreach(Room room in AllRooms)
           {
                if(room.RoomNumber == roomNumber)
                {
                  
                    return false;
                }
           }

            return true;
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
            newEquip.EquiptId = genereteId(room.Equipment);
            room.AddEquipment(newEquip);
            rfs.SaveRooms(AllRooms);
        }

        private int genereteId(List<Equipment> equipList)
        {
            if (equipList != null)
            {

                int id = 0;
                foreach (Equipment eq in equipList)
                {
                    if (eq.EquiptId > id)
                    {
                        id = eq.EquiptId;
                    }

                }


                bool isUnique = CheckIfUnique(id);

                while (!isUnique)
                {
                    ++id;
                    isUnique = CheckIfUnique(id);
                }

                return id;
            }
            else
            {
                return 1;
            }

        }

        private bool CheckIfUnique(int id)
        {
            foreach (Room room in AllRooms)
            {
                if (room.Equipment != null)
                {
                    foreach (Equipment eq in room.Equipment)
                    {
                        if (eq.EquiptId == id)
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        public void RemoveEquipment(Room room, Equipment oldEquip)
        {
            room.RemoveEquipment(oldEquip);
            rfs.SaveRooms(AllRooms);
        }

      
        public Boolean UpdateEquipment(Room room, Equipment updateEquip)
        {
            MessageBox.Show("uslo u kontroler");
            bool isSucces = room.UpdateEquipment(updateEquip);
            rfs.SaveRooms(AllRooms);
            return isSucces;
        }

        public int GetRoomNumber(int roomId)
        {
            foreach (Room r in AllRooms)
            {
                if(r.RoomId == roomId)
                {
                    return r.RoomNumber;
                }
            }
            return 0;
        }

    }
}
