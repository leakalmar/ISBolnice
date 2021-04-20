using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;

namespace Hospital_IS.Storages
{
    public class FSDoctor
    {
        private string fileLocation;

        public FSDoctor()
        {
            this.fileLocation = "../../../FileStorage/doctors.json";
        }

        public ObservableCollection<Doctor> GetAll()
        {
            String text = File.ReadAllText(this.fileLocation);
            ObservableCollection<Doctor> doctors = JsonConvert.DeserializeObject<ObservableCollection<Doctor>>(text);


            /*
             *List<Doctor> doctors = new List<Doctor>();
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
            Doctor doc = new Doctor(101, "Ana", "Draganic", new DateTime(1980, 05, 12), "doktor@gmail.com", "doktor", "Brace Radica 30", 70000.0, DateTime.Now, dani, spec, r);
            doctors.Add(doc);
            */

            return doctors;
        }

        public void Save(Doctor doctor)
        {
            ObservableCollection<Doctor> doctors = GetAll();
            doctors.Add(doctor);

            var file = JsonConvert.SerializeObject(doctors, Formatting.Indented, new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            using (StreamWriter writer = new StreamWriter(this.fileLocation))
            {
                writer.Write(file);
            }
        }
        public Boolean UpdateDoctor(Doctor doctor)
        {
            ObservableCollection<Doctor> doctors = GetAll();

            for (int i = 0; i < doctors.Count; i++)
            {
                if (doctor.Id.Equals(doctors[i].Id))
                {
                    doctors.Remove(doctors[i]);
                    doctors.Insert(i, doctor);

                    var file = JsonConvert.SerializeObject(doctors, Formatting.Indented);
                    using (StreamWriter writer = new StreamWriter(this.fileLocation))
                    {
                        writer.Write(file);
                    }

                    return true;
                }
            }
            return false;
        }

        public Model.Doctor GetByEmail(String email)
        {
            throw new NotImplementedException();
        }
    }
}
