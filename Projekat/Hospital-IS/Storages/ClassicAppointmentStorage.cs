using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;

namespace Storages
{
    public class ClassicAppointmentStorage
    {

        private string fileLocation;

        public ClassicAppointmentStorage()
        {
            this.fileLocation = "../../../FileStorage/classicAppointment.json";
        }

        public ObservableCollection<Appointment> GetAll()
        {
            String text = File.ReadAllText(this.fileLocation);
            ObservableCollection<Appointment> allAppointments = JsonConvert.DeserializeObject<ObservableCollection<Appointment>>(text);
            return allAppointments;
        }

        public ObservableCollection<Appointment> GetAllByRoomId(int roomId )
        {
            ObservableCollection<Appointment> appointments = GetAll();
            ObservableCollection<Appointment> roomAppointment = new ObservableCollection<Appointment>();

            foreach (Appointment appointment in appointments)
            {
                if (appointment.Room == roomId)
                {
                    roomAppointment.Add(appointment);
                }
            }

            return roomAppointment;

        }

        public void SaveAppointment(ObservableCollection<Appointment> allAppointments)
        {
            var file = JsonConvert.SerializeObject(allAppointments, Formatting.Indented, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
            using (StreamWriter writer = new StreamWriter(this.fileLocation))
            {
                writer.Write(file);
            }
        }
    }
}
