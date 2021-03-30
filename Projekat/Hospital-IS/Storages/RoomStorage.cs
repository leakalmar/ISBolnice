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
            /*Room r1 = new Room(RoomType.ConsultingRoom, false, true, 2, 25);
            Room r2 = new Room(RoomType.ConsultingRoom, false, true, 1, 11);

            List<Room> all = new List<Room>();
            all.Add(r2);
            all.Add(r1);
            return all;*/

            /*String text = File.ReadAllText(this.fileLocation);
            ObservableCollection<Room>  allRooms  = JsonConvert.DeserializeObject<ObservableCollection<Room>>(text);
            return allRooms*/


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

        public void SaveRooms(ObservableCollection<Room> allRooms)
        {


            XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<Room>));
            TextWriter filestream = new StreamWriter("../../../FileStorage/rooms.xml");
            serializer.Serialize(filestream, allRooms);
            filestream.Close();
        }

    }
}