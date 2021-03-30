using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;

namespace Storages
{
    public class AppointmentFileStorage
    {
        private string fileLocation;

        public AppointmentFileStorage()
        {
            this.fileLocation = "../../../FileStorage/appointments.json";
        }

        public ObservableCollection<DoctorAppointment> GetAll()
        {
            String text = File.ReadAllText(this.fileLocation);
            ObservableCollection<DoctorAppointment> allAppointments = JsonConvert.DeserializeObject<ObservableCollection<DoctorAppointment>>(text);
            return allAppointments;
        }

        public ObservableCollection<DoctorAppointment> GetAllByPatient(int patient)
        {
            String text = File.ReadAllText(this.fileLocation);
            ObservableCollection<DoctorAppointment> allAppointments = JsonConvert.DeserializeObject<ObservableCollection<DoctorAppointment>>(text);
            ObservableCollection<DoctorAppointment> patientAppointments = new ObservableCollection<DoctorAppointment>();
            foreach (DoctorAppointment docApp in allAppointments)
            {
                if(docApp.Patient.Id == patient)
                {
                    patientAppointments.Add(docApp);
                }
            }
            return patientAppointments;

        }

        public List<Appointment> GetAllByDoctor(Doctor doctor)
        {
            throw new NotImplementedException();
        }

        public void SaveAppointment(ObservableCollection<DoctorAppointment> allAppointments)
        {
            var file = JsonConvert.SerializeObject(allAppointments, Formatting.Indented, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore});
            using (StreamWriter writer = new StreamWriter(this.fileLocation))
            {
                writer.Write(file);
            }
        }

        public List<Appointment> GetAllByRoom(Model.Room room)
        {
            throw new NotImplementedException();
        }

    }
}