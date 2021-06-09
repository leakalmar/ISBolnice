using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace Hospital_IS.Storages
{
    public class FSDoctor : GenericFileStorage<Doctor>
    {
        public FSDoctor()
        {
            this.fileLocation = "../../../FileStorage/doctors.json";
        }

        public void SaveDoctors(List<Doctor> doctors)
        {
            var file = JsonConvert.SerializeObject(doctors, Formatting.Indented, new JsonSerializerSettings()
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
