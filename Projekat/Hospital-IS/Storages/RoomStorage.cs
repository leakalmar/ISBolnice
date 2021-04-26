using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Xml.Serialization;

namespace Storages
{
    public class RoomStorage
    {
        private String fileLocation ;
        private List<Room> allRooms = new List<Room>();

        public RoomStorage()
        {
            this.fileLocation = "../../../FileStorage/rooms.json";
        }

        public List<Room>  GetAll()
        {
            String text = File.ReadAllText(this.fileLocation);
            List<Room> allRooms = JsonConvert.DeserializeObject<List<Room>>(text);
            return allRooms;
         
        }


      
        public void SaveRooms(List<Room> allRooms)
        {
            var file = JsonConvert.SerializeObject(allRooms, Formatting.Indented, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
            using (StreamWriter writer = new StreamWriter(this.fileLocation))
            {
                writer.Write(file);
            }
        }

    }
}