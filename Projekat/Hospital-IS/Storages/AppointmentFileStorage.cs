using Model;
using System;
using System.Collections.Generic;

namespace Storages
{
    public class AppointmentFileStorage
    {
        private string fileLocation;

        public List<Appointment> GetAllByPatient(Patient patient)
        {
            Room r = new Room(RoomType.ConsultingRoom, false, true, 2, 25);
            List<Model.WorkDay> dani = new List<Model.WorkDay>
            {
                new Model.WorkDay("Pon", DateTime.Now, DateTime.Now)
            };
            Model.Specialty spec = new Model.Specialty("Dermatolog");
            Model.Doctor doc = new Model.Doctor(111, "Dragana", "Vukmanov Simokov", DateTime.Now, "dragana@gmail.com", "123", "Brace Radica 30", 60000.0, DateTime.Now, dani,spec);
            Appointment a1 = new DoctorAppointment(DateTime.Now, AppointmetType.CheckUp, true, r,doc,patient);
            Appointment a2 = new DoctorAppointment(DateTime.Now, AppointmetType.Operation, true, r, doc,patient);
            Appointment a3 = new DoctorAppointment(DateTime.Now, AppointmetType.CheckUp, true, r, doc,patient);

            List<Appointment> all = new List<Appointment>();
            all.Add(a1);
            all.Add(a2);
            all.Add(a3);

            return all;

        }

        public List<Appointment> GetAllByDoctor(Doctor doctor)
        {
            throw new NotImplementedException();
        }

        public void SaveAppointment(Appointment appointment)
        {
            throw new NotImplementedException();
        }

        public List<Appointment> GetAllByRoom(Model.Room room)
        {
            throw new NotImplementedException();
        }

    }
}