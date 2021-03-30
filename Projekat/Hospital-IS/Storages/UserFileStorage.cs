using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;

namespace Storages
{
    public class UserFileStorage
    {
        private string fileLocation;

        public UserFileStorage()
        {
            this.fileLocation = "../../../FileStorage/users.json";
        }

        public List<User> GetAll()
        {
            String text = File.ReadAllText(this.fileLocation);
            List<User> users = JsonConvert.DeserializeObject<List<User>>(text);

            List<WorkDay> dani = new List<WorkDay>();
            dani.Add(new WorkDay(Day.Monday, new DateTime(2020, 02, 02, 8, 0, 0), new DateTime(2020, 02, 02, 20, 0, 0)));
            dani.Add(new WorkDay(Day.Tuesday, new DateTime(2020, 02, 02, 8, 0, 0), new DateTime(2020, 02, 02, 20, 0, 0)));
            dani.Add(new WorkDay(Day.Wednesday, new DateTime(2020, 02, 02, 8, 0, 0), new DateTime(2020, 02, 02, 20, 0, 0)));
            dani.Add(new WorkDay(Day.Thursday, new DateTime(2020, 02, 02, 8, 0, 0), new DateTime(2020, 02, 02, 20, 0, 0)));
            dani.Add(new WorkDay(Day.Friday, new DateTime(2020, 02, 02, 8, 0, 0), new DateTime(2020, 02, 02, 20, 0, 0)));
            dani.Add(new WorkDay(Day.Saturday, new DateTime(2020, 02, 02, 8, 0, 0), new DateTime(2020, 02, 02, 20, 0, 0)));
            dani.Add(new WorkDay(Day.Sunday, new DateTime(2020, 02, 02, 8, 0, 0), new DateTime(2020, 02, 02, 20, 0, 0)));
            Specialty spec = new Specialty("Dermatolog");
            Room r = new Room(RoomType.ConsultingRoom, false, true, 2, 25);
            Doctor doc = new Doctor(101, "Dragana", "Vukmanov Simokov", new DateTime(1980, 05, 12), "doktor@gmail.com", "doktor", "Brace Radica 30", 70000.0, DateTime.Now, dani, spec, r);
            users.Add(doc);
            return users;
        }

        public void Save(List<User> users)
        {
            var file = JsonConvert.SerializeObject(users, Formatting.Indented);
            using (StreamWriter writer = new StreamWriter(this.fileLocation))
            {
                writer.Write(file);
            }
        }

        public Model.User GetByEmail(String email)
        {
            throw new NotImplementedException();
        }

    }
}