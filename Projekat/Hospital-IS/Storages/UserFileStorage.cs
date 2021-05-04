using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Storages
{
    public class UserFileStorage
    {
        private string fileLocation;

        public List<User> GetAll()
        {
            List<User> all = new List<User>();

            List<WorkDay> dani = new List<WorkDay>
            {
                new WorkDay(Day.Monday, DateTime.Now, DateTime.Now)
            };

            List<String> alergije = new List<String>();
            alergije.Add("Tetanus");
            alergije.Add("Paracetamol");
            Model.Specialty spec = new Model.Specialty("Dermatolog");
            Doctor doc = new Doctor(111, "Dragana", "Vukmanov Simokov", DateTime.Now, "dragana@gmail.com", "123", "Brace Radica 30", 60000.0, DateTime.Now, dani, spec, 1);
            Patient p1 = new Patient(001, "Simona", "Stantic", DateTime.Now, "Petra Drapsina 8", "simona@gmail.com", "123", DateTime.Now, "neki poslodavac", alergije);
            Patient p2 = new Patient(002, "Stefan", "Beckovic", DateTime.Now, "Zelengoska 15", "stefan@gmail.com", "123", DateTime.Now, "neki poslodavac", alergije);
            Patient p3 = new Patient(003, "Uros", "Lekic", DateTime.Now, "Dure Dakovica 10", "uros@gmail.com", "123", DateTime.Now, "neki poslodavac", alergije);
            all.Add(doc);
            all.Add(p1);
            all.Add(p2);
            all.Add(p3);
            return all;
        }

        public void SaveUser(Model.User user)
        {
            throw new NotImplementedException();
        }

    }
}