using Model;
using Service;
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
           return RoomService.Instance.CheckQuantity(sourceRoom, equip, quantity);
        }

        public void AddRoom(Room room)
        {
            RoomService.Instance.AddRoom(room);
        }

        public void RemoveRoom(Room room)
        {
            RoomService.Instance.RemoveRoom(room);
        }

        public void UpdateRoom(Room room)
        {
            RoomService.Instance.UpdateRoom(room);
        }

        public List<Room> GetAllRooms()
        {
            return RoomService.Instance.GetAllRooms();
        }

        public List<Room> GetRoomByType(RoomType type)
        {
            return RoomService.Instance.GetRoomByType(type);
        }

        public void AddEquipment(Room room, Equipment newEquip)
        {
            RoomService.Instance.AddEquipment(room,newEquip);
        }

        public void RemoveEquipment(Room room, Equipment oldEquip)
        {
            RoomService.Instance.RemoveEquipment(room,oldEquip);
        }


        public Boolean UpdateEquipment(Room room, Equipment updateEquip)
        {
            return RoomService.Instance.UpdateEquipment(room,updateEquip);
        }
    }
}
