using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;

namespace Storages
{
    public class RoomStorage
    {
        private String fileLocation ;


        public RoomStorage()
        {
            this.fileLocation = "../../../FileStorage/rooms.json";
        }

        public ObservableCollection<Room>  GetAll()
        {
            /*Room r1 = new Room(RoomType.ConsultingRoom, false, true, 2, 25);
            Room r2 = new Room(RoomType.ConsultingRoom, false, true, 1, 11);

            List<Room> all = new List<Room>();
            all.Add(r2);
            all.Add(r1);
            return all;*/

            String text = File.ReadAllText(this.fileLocation);
            ObservableCollection<Room>  allRooms  = JsonConvert.DeserializeObject<ObservableCollection<Room>>(text);
            return allRooms;

        }

        public void SaveRooms(ObservableCollection<Room> allRooms)
        {
            

            var file = JsonConvert.SerializeObject(allRooms, Formatting.Indented);
            using (StreamWriter writer = new StreamWriter(this.fileLocation))
            {
                writer.Write(file);
            }
        }

    }
}