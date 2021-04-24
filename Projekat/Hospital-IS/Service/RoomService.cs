using Model;
using Storages;
using System;
using System.Collections.Generic;
using System.Text;

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

        }

        public bool CheckQuantity(Room sourceRoom, Equipment equip, int quantity)
        {
            throw new NotImplementedException();
        }

        public void AddRoom(Room room)
        {

        }

        public void RemoveRoom(Room room)
        {

        }

        public void UpdateRoom(Room room)
        {

        }

    }
}
