using Model;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace Hospital_IS.Storages
{
    public class PatientFileStorage : GenericFileStorage<Patient>
    {
        public PatientFileStorage()
        {
            this.fileLocation = "../../../FileStorage/patients.json";
        }

        public void SavePatients(List<Patient> patients)
        {
            var file = JsonConvert.SerializeObject(patients, Formatting.Indented, new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Serialize,
                PreserveReferencesHandling = PreserveReferencesHandling.Objects
            });
            using (StreamWriter writer = new StreamWriter(this.fileLocation))
            {
                writer.Write(file);
            }
        }
    }
}
