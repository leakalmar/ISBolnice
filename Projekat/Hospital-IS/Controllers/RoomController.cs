using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Controllers
{
    class RoomController
    {
        private static RoomController instance = null;
        public static RoomController Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new RoomController();
                }
                return instance;
            }
        }

        private RoomController()
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
