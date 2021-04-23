using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Windows;

namespace Storages
{
    public class ClassicAppointmentStorage
    {

        private string fileLocation;

        public ClassicAppointmentStorage()
        {
            this.fileLocation = "../../../FileStorage/classicAppointment.json";
        }

        public List<Appointment> GetAll()
        {
            String text = File.ReadAllText(this.fileLocation);
            List<Appointment> allAppointments = JsonConvert.DeserializeObject<List<Appointment>>(text);
            return allAppointments;
        }

        public List<Appointment> GetAllByRoomId(int roomId )
        {
            List<Appointment> appointments = GetAll();

           

            List<Appointment> roomAppointment = new List<Appointment>();

            foreach (Appointment appointment in appointments)
            {
              
                if (appointment.Room == roomId)
                {
                    roomAppointment.Add(appointment);
                   
                }
            }

            return roomAppointment;

        }

        public void SaveAppointment(List<Appointment> allAppointments)
        {
            var file = JsonConvert.SerializeObject(allAppointments, Formatting.Indented, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
            using (StreamWriter writer = new StreamWriter(this.fileLocation))
            {
                writer.Write(file);
            }
        }


        public List<DoctorAppointment> GetAllDocAppointmentsById(int roomId)
        {
            String text = File.ReadAllText(this.fileLocation);
            List<DoctorAppointment> appointments = JsonConvert.DeserializeObject<List<DoctorAppointment>>(text);

            List<DoctorAppointment> roomAppointment = new List<DoctorAppointment>();

            foreach (DoctorAppointment appointment in appointments)
            {

                if (appointment.Room == roomId)
                {
                    roomAppointment.Add(appointment);
                }
            }
            return roomAppointment;
        }
    }
}
