using Hospital_IS.Storages;
using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace Storages
{
    public class AppointmentFileStorage : GenericFileStorage<DoctorAppointment>
    {
        public AppointmentFileStorage()
        {
            this.fileLocation = "../../../FileStorage/appointments.json";
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