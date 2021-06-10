﻿using Enums;
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
            bool isEnough = false;
            foreach (Equipment eq in sourceRoom.Equipment)
            {
                if (eq.EquiptId == equip.EquiptId)
                {
                    if (equip.Quantity - quantity >= 0)
                    {
                        isEnough = true;
                    }
                }
            }
            return isEnough;
        }

        public Room GetRoomById(int roomId)
        {
            Room room = null;
            foreach (Room r in AllRooms)
            {
                if (r.RoomId == roomId)
                {
                    room = r;
                }
            }
            return room;
        }

        public List<int> GetAllFloors()
        {
            List<int> floors = new List<int>();
            foreach (Room r in AllRooms)
            {
                if (!floors.Contains(r.RoomFloor))
                {
                    floors.Add(r.RoomFloor);
                }
            }
            return floors;
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

        public List<Room> GetRoomByNumber(string roomNumber)
        {
            List<Room> allRoomByNumber = new List<Room>();
            foreach (Room room in AllRooms)
            {
                if (Int32.Parse(roomNumber) > room.RoomNumber )
                {
                    allRoomByNumber.Add(room);
                }
            }
            return allRoomByNumber;
        }

        public bool CheckIfRoomNumberIsUnique(int roomNumber)
        {
            bool isUnique = true;
           foreach(Room room in AllRooms)
           {
                if(room.RoomNumber == roomNumber)
                {
                    isUnique = false;
                }
           }

           foreach(AdvancedRenovation renovation in AdvancedRenovationService.Instance.AllAdvancedRenovations)
            {
                if(renovation.RenovationResultRoom.RoomNumber == roomNumber)
                {
                    isUnique = false;
                }
            }
            return isUnique;
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
            bool isUnique = true;
            foreach (Room room in AllRooms)
            {
                if (room.Equipment != null)
                {
                    foreach (Equipment eq in room.Equipment)
                    {
                        if (eq.EquiptId == id)
                        {
                            isUnique = false;
                        }
                    }
                }
            }
            return isUnique;
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
            int roomNum = 0;
            foreach (Room r in AllRooms)
            {
                if(r.RoomId == roomId)
                {
                    roomNum = r.RoomNumber;
                }
            }
            return roomNum;
        }

    }
}
