using Model;
using Service;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

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

     

        public void RemoveRoom(Room room)
        {
            RoomService.Instance.RemoveRoom(room);
        }

       

        public List<Room> GetAllRooms()
        {
            return RoomService.Instance.GetAllRooms();
        }

        public List<Room> GetRoomByType(RoomType type)
        {
            return RoomService.Instance.GetRoomByType(type);
        }

        public Room GetRoomById(int roomId)
        {
            return RoomService.Instance.GetRoomById(roomId);
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
            MessageBox.Show("uslo u kontroler");
            return RoomService.Instance.UpdateEquipment(room,updateEquip);
        }

        public void UpdateRoom(int roomNumber, int roomFloor, int surfaceArea, int bedNumber, int roomTypeIndex)
        {
            RoomType roomType = new RoomType();
            roomType = CheckRoomType(roomTypeIndex, roomType);
            Room room = new Room(roomFloor, roomNumber, surfaceArea, bedNumber, roomType);
            RoomService.Instance.UpdateRoom(room);
        }



        internal void AddRoom(int roomNumber, int roomFloor, int surfaceArea, int bedNumber, int roomTypeIndex)
        {
            RoomType roomType = new RoomType();
            roomType = CheckRoomType(roomTypeIndex, roomType);
            Room room = new Room(roomFloor, roomNumber, surfaceArea, bedNumber, roomType);
            RoomService.Instance.AddRoom(room);
        }

      
        private static RoomType CheckRoomType(int roomTypeIndex, RoomType roomType)
        {
            if (roomTypeIndex == 0)
            {
               
                roomType = RoomType.RecoveryRoom;
            }
            else if (roomTypeIndex == 1)
            {
              
                roomType = RoomType.ConsultingRoom;
            }
            else if (roomTypeIndex == 2)
            {
                roomType = RoomType.OperationRoom;
            }
            else if (roomTypeIndex == 3)
            {
                roomType = RoomType.StorageRoom;
            }

            return roomType;
        }

      
    }
}
