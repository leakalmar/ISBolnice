using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;

namespace Storages
{
    public class AppointmentFileStorage
    {
        private string fileLocation;

        public AppointmentFileStorage()
        {
            this.fileLocation = "../../../FileStorage/appointments.json";
        }

        public ObservableCollection<DoctorAppointment> GetAll()
        {
            String text = File.ReadAllText(this.fileLocation);
            ObservableCollection<DoctorAppointment> allAppointments = JsonConvert.DeserializeObject<ObservableCollection<DoctorAppointment>>(text);
            return allAppointments;
        }

        public ObservableCollection<DoctorAppointment> GetAllByPatient(int patient)
        {
            /*
            Room r = new Room(RoomType.ConsultingRoom, false, true, 2, 25);
            List<Model.WorkDay> dani = new List<Model.WorkDay>
            {
                new Model.WorkDay(Day.Monday, DateTime.Now, DateTime.Now)
            };
            Model.Specialty spec = new Model.Specialty("Dermatolog");
            Model.Doctor doc = new Model.Doctor(111, "Dragana", "Vukmanov Simokov", DateTime.Now, "dragana@gmail.com", "123", "Brace Radica 30", 60000.0, DateTime.Now, dani,spec, r);
            Appointment a1 = new DoctorAppointment(DateTime.Now, AppointmetType.CheckUp, true, r,doc,patient);
            Appointment a2 = new DoctorAppointment(DateTime.Now, AppointmetType.Operation, true, r, doc,patient);
            Appointment a3 = new DoctorAppointment(DateTime.Now, AppointmetType.CheckUp, true, r, doc,patient);

            List<Appointment> all = new List<Appointment>();
            all.Add(a1);
            all.Add(a2);
            all.Add(a3);

            return all;
            */
            String text = File.ReadAllText(this.fileLocation);
            ObservableCollection<DoctorAppointment> allAppointments = JsonConvert.DeserializeObject<ObservableCollection<DoctorAppointment>>(text);
            ObservableCollection<DoctorAppointment> patientAppointments = new ObservableCollection<DoctorAppointment>();
            foreach (DoctorAppointment docApp in allAppointments)
            {
                if(docApp.Patient.Id == patient)
                {
                    patientAppointments.Add(docApp);
                }
            }
            return patientAppointments;

        }

        public List<Appointment> GetAllByDoctor(Doctor doctor)
        {
            throw new NotImplementedException();
        }

        public void SaveAppointment(ObservableCollection<DoctorAppointment> allAppointments)
        {
            var file = JsonConvert.SerializeObject(allAppointments, Formatting.Indented, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore});
            using (StreamWriter writer = new StreamWriter(this.fileLocation))
            {
                writer.Write(file);
            }
        }

        public List<Appointment> GetAllByRoom(Model.Room room)
        {
            throw new NotImplementedException();
        }

    }
}