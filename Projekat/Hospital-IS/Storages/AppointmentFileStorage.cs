using Model;
using System;
using System.Collections.Generic;

namespace Storages
{
    public class AppointmentFileStorage
    {
        private string fileLocation;

        public List<Appointment> GetAllByPatient(Patient patient)
        {
            throw new NotImplementedException();
        }

        public List<Appointment> GetAllByDoctor(Doctor doctor)
        {
            throw new NotImplementedException();
        }

        public void SaveAppointment(Appointment appointment)
        {
            throw new NotImplementedException();
        }

        public List<Appointment> GetAllByRoom(Model.Room room)
        {
            throw new NotImplementedException();
        }

    }
}