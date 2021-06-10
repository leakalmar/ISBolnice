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

      
    }
}