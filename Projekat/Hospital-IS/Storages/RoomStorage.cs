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
            this.fileLocation = "../../../FileStorage/rooms.xml";
        }

        public List<Room>  GetAll()
        {
            


             if (File.ReadAllText("../../../FileStorage/rooms.xml").Trim().Equals(""))
            {
                return allRooms;
            }
            else
            {
                FileStream filestream = File.OpenRead("../../../FileStorage/rooms.xml");
                XmlSerializer serializer = new XmlSerializer(typeof(List<Room>));
                allRooms = (List<Room>)serializer.Deserialize(filestream);
                filestream.Close();
                return allRooms;
            }
            

        }


        public List<Room> GetRoomsByType(RoomType type)
        {
            List<Room> allRoomByType = new List<Room>();

            foreach(Room room in GetAll())
            {
                if(room.Type == RoomType.StorageRoom)
                {
                    allRoomByType.Add(room);
                }
            }
            return allRoomByType;
        }

        public void SaveRooms(List<Room> allRooms)
        {


            XmlSerializer serializer = new XmlSerializer(typeof(List<Room>));
            TextWriter filestream = new StreamWriter("../../../FileStorage/rooms.xml");
            serializer.Serialize(filestream, allRooms);
            filestream.Close();
        }

    }
}