using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;

namespace Hospital_IS.Storages
{
    public class PatientFileStorage
    {
        private string fileLocation;

        public PatientFileStorage()
        {
            this.fileLocation = "../../../FileStorage/patients.json";
        }

        public List<Patient> GetAll()
        {
            String text = File.ReadAllText(this.fileLocation);
            List<Patient> patients = JsonConvert.DeserializeObject<List<Patient>>(text);

            /*ObservableCollection<String> alergije = new ObservableCollection<String>();
            alergije.Add("Tetanus");
            alergije.Add("Paracetamol");
            Model.Specialty spec = new Model.Specialty("Dermatolog");
            Patient p1 = new Patient(001, "Simona", "Stantic", DateTime.Now, "Petra Drapsina 8", "simona@gmail.com", "123", DateTime.Now, "neki poslodavac", alergije);
            Patient p2 = new Patient(002, "Stefan", "Beckovic", DateTime.Now, "Zelengoska 15", "stefan@gmail.com", "123", DateTime.Now, "neki poslodavac", alergije);
            Patient p3 = new Patient(003, "Uros", "Lekic", DateTime.Now, "Dure Dakovica 10", "uros@gmail.com", "123", DateTime.Now, "neki poslodavac", alergije);
            patients.Add(p1);
            patients.Add(p2);
            patients.Add(p3);*/

            return patients;
        }

        public void SavePatient(Patient patient)
        {
            List<Patient> patients = GetAll();
            patients.Add(patient);

            var file = JsonConvert.SerializeObject(patients, Formatting.Indented, new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Serialize,
                PreserveReferencesHandling = PreserveReferencesHandling.Objects
            });
            using (StreamWriter writer = new StreamWriter(this.fileLocation))
            {
                writer.Write(file);
            }
        }

        public Boolean  UpdatePatient(Patient patient)
        {
            List<Patient> patients = GetAll();

            for (int i = 0; i < patients.Count; i++)
            {
                if (patient.Id.Equals(patients[i].Id))
                {
                    patients.Remove(patients[i]);
                    patients.Insert(i, patient);

                    var file = JsonConvert.SerializeObject(patients, Formatting.Indented);
                    using (StreamWriter writer = new StreamWriter(this.fileLocation))
                    {
                        writer.Write(file);
                    }

                    return true;
                }
            }
            return false;
        }

        public Boolean DeletePatient(Patient patient) 
        {
            List<Patient> patients = GetAll();

            for (int i = 0; i < patients.Count; i++) 
            {
                if (patient.Id.Equals(patients[i].Id))
                {
                    patients.Remove(patients[i]);

                    var file = JsonConvert.SerializeObject(patients, Formatting.Indented);
                    using (StreamWriter writer = new StreamWriter(this.fileLocation))
                    {
                        writer.Write(file);
                    }

                    return true;
                }
            }
            return false;
        }

    }
}
