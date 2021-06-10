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

     
    }
}
