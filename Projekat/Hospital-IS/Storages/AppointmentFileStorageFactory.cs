using Storages;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital_IS.Storages
{
    public class AppointmentFileStorageFactory : IStorageFactory<AppointmentFileStorage>
    {
        public AppointmentFileStorageFactory()
        {
        }

        public AppointmentFileStorage GetStorage()
        {
            return new AppointmentFileStorage();
        }
    }
}
