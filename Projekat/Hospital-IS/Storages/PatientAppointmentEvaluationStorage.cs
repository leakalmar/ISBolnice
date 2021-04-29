using DTOs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Storages
{
    public class PatientAppointmentEvaluationStorage
    {
        private string fileLocation;

        public PatientAppointmentEvaluationStorage()
        {
            this.fileLocation = "../../../FileStorage/appointmentEvaluations.json";
        }

        public List<PatientAppointmentEvaluationDTO> GetAll()
        {
            String text = File.ReadAllText(this.fileLocation);
            List<PatientAppointmentEvaluationDTO> allAppointmentEvaluations = JsonConvert.DeserializeObject<List<PatientAppointmentEvaluationDTO>>(text);
            return allAppointmentEvaluations;
        }

        public void SaveAppointment(List<PatientAppointmentEvaluationDTO> allAppointmentEvaluations)
        {
            var file = JsonConvert.SerializeObject(allAppointmentEvaluations, Formatting.Indented, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
            using (StreamWriter writer = new StreamWriter(this.fileLocation))
            {
                writer.Write(file);
            }
        }
    }
}
