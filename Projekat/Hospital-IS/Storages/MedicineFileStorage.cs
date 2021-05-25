using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace Hospital_IS.Storages
{
    public class MedicineFileStorage
    {
        private string fileLocation;

        public MedicineFileStorage()
        {
            this.fileLocation = "../../../FileStorage/medicines.json";
        }

        public List<Medicine> GetAll()
        {
            String text = File.ReadAllText(this.fileLocation);
            List<Medicine> medicines = JsonConvert.DeserializeObject<List<Medicine>>(text);
            return medicines;
        }

        public void Save(List<Medicine> medicines)
        {
            var file = JsonConvert.SerializeObject(medicines, Formatting.Indented, new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            using (StreamWriter writer = new StreamWriter(this.fileLocation))
            {
                writer.Write(file);
            }
        }
    }
}
