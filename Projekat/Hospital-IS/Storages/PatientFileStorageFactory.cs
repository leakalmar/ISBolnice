using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital_IS.Storages
{
    public class PatientFileStorageFactory : IStorageFactory<PatientFileStorage>
    {
        public PatientFileStorageFactory()
        {
        }

        public PatientFileStorage GetStorage()
        {
            return new PatientFileStorage(); 
        }
    }
}
