using Model;
using System;
using System.Collections.Generic;


namespace Storages
{
    public class RoomStorage
    {
        private String fileLocation;

        public List<Room> GetAll()
        {
            Room r1 = new Room(RoomType.ConsultingRoom, false, true, 2, 25);
            Room r2 = new Room(RoomType.ConsultingRoom, false, true, 1, 11);

            List<Room> all = new List<Room>();
            all.Add(r2);
            all.Add(r1);
            return all;
        }

        public void SaveRoom(Model.Room newRoom)
        {
            throw new NotImplementedException();
        }

    }
}