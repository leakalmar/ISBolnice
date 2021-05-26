using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace Storages
{
    public class MedcineNotificationStorage
    {
        private string fileLocation;

        public MedcineNotificationStorage()
        {
            this.fileLocation = "../../../FileStorage/medicineNotification.json";
        }

        public List<MedicineNotification> GetAll()
        {
            String text = File.ReadAllText(this.fileLocation);
            List<MedicineNotification> allAppointments = JsonConvert.DeserializeObject<List<MedicineNotification>>(text);
            if(allAppointments == null)
            {
                allAppointments = new List<MedicineNotification>();
            }
            return allAppointments;
        }



        public void Save(List<MedicineNotification> allNotification)
        {
            var file = JsonConvert.SerializeObject(allNotification, Formatting.Indented, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
            using (StreamWriter writer = new StreamWriter(this.fileLocation))
            {
                writer.Write(file);
            }
        }

    }
}
