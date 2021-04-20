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
        private ObservableCollection<Room> allRooms = new ObservableCollection<Room>();

        public RoomStorage()
        {
            this.fileLocation = "../../../FileStorage/rooms.xml";
        }

        public ObservableCollection<Room>  GetAll()
        {
            


             if (File.ReadAllText("../../../FileStorage/rooms.xml").Trim().Equals(""))
            {
                return allRooms;
            }
            else
            {
                FileStream filestream = File.OpenRead("../../../FileStorage/rooms.xml");
                XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<Room>));
                allRooms = (ObservableCollection<Room>)serializer.Deserialize(filestream);
                filestream.Close();
                return allRooms;
            }
            

        }


        public ObservableCollection<Room> GetRoomsByType(RoomType type)
        {
            ObservableCollection<Room> allRoomByType = new ObservableCollection<Room>();

            foreach(Room room in GetAll())
            {
                if(room.Type == RoomType.StorageRoom)
                {
                    allRoomByType.Add(room);
                }
            }
            return allRoomByType;
        }

        public void SaveRooms(ObservableCollection<Room> allRooms)
        {


            XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<Room>));
            TextWriter filestream = new StreamWriter("../../../FileStorage/rooms.xml");
            serializer.Serialize(filestream, allRooms);
            filestream.Close();
        }

    }
}