using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;

namespace Hospital_IS.Storages
{
    public class MedicineFileStorage
    {
        private string fileLocation;

        public MedicineFileStorage()
        {
            this.fileLocation = "../../../FileStorage/medicines.json";
        }

        public ObservableCollection<Medicine> GetAll()
        {
            String text = File.ReadAllText(this.fileLocation);
            ObservableCollection<Medicine> medicines = JsonConvert.DeserializeObject<ObservableCollection<Medicine>>(text);
            return medicines;
        }

        public void Save(Medicine medicine)
        {
            ObservableCollection<Medicine> medicines = GetAll();
            medicines.Add(medicine);

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
