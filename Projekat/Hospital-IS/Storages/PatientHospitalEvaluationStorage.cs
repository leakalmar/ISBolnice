using Hospital_IS.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace Hospital_IS.Storages
{
    public class PatientHospitalEvaluationStorage
    {
        private string fileLocation;

        public PatientHospitalEvaluationStorage()
        {
            this.fileLocation = "../../../FileStorage/hospitalEvaluations.json";
        }

        public List<PatientHospitalEvaluation> GetAll()
        {
            String text = File.ReadAllText(this.fileLocation);
            List<PatientHospitalEvaluation> allHospitalEvaluations = JsonConvert.DeserializeObject<List<PatientHospitalEvaluation>>(text);
            return allHospitalEvaluations;
        }

        public void SaveAppointments(List<PatientHospitalEvaluation> allHospitalEvaluations)
        {
            var file = JsonConvert.SerializeObject(allHospitalEvaluations, Formatting.Indented, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
            using (StreamWriter writer = new StreamWriter(this.fileLocation))
            {
                writer.Write(file);
            }
        }
    }
}
