using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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

        public List<DoctorAppointment> GetAll()
        {
            String text = File.ReadAllText(this.fileLocation);
            List<DoctorAppointment> allAppointments = JsonConvert.DeserializeObject<List<DoctorAppointment>>(text);
            return allAppointments;
        }

        public void SaveAppointment(List<DoctorAppointment> allAppointments)
        {
            var file = JsonConvert.SerializeObject(allAppointments, Formatting.Indented, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
            using (StreamWriter writer = new StreamWriter(this.fileLocation))
            {
                writer.Write(file);
            }
        }
    }
}