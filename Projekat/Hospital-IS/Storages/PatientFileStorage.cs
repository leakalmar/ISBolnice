using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

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
            JsonSerializerOptions options = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.All)
            };
            String text = File.ReadAllText(this.fileLocation, Encoding.UTF8);
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

        public Boolean UpdatePatient(Patient patient)
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

        public void SavePatients(List<Patient> patients)
        {
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


        /*       public Boolean Update(Patient p)
               {
                   List<Patient> patients = GetAll();
                   for (int i = 0; i < patients.Count; i++)
                   {
                       if (patients[i].Jmbg.Equals(p.Jmbg))
                       {
                           patients[i].Name = p.Name;
                           patients[i].Surname = p.Surname;
                           patients[i].DateOfBirth = p.DateOfBirth;
                           patients[i].Sex = p.Sex;
                           patients[i].PhoneNumber = p.PhoneNumber;
                           patients[i].Adress = p.Adress;
                           patients[i].IdCard = p.IdCard;
                           patients[i].Email = p.Email;
                           patients[i].EmergencyContact = p.EmergencyContact;
                           patients[i].MedicalRecord = p.MedicalRecord;
                           patients[i].Username = p.Username;
                           patients[i].IsGuest = p.IsGuest;
                           patients[i].Password = p.Password;

                           try
                           {
                               var jsonToFile = JsonConvert.SerializeObject(patients, Formatting.Indented);
                               using (StreamWriter writer = new StreamWriter(this.FileName))
                               {
                                   writer.Write(jsonToFile);
                               }
                           }
                           catch (Exception e)
                           {

                           }
                       }
                   }
                   return false;
               }*/
    }
}
