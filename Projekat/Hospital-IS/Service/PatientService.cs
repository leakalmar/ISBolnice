using Hospital_IS.Storages;
using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service
{
    class PatientService
    {
        private PatientFileStorage pfs = new PatientFileStorage();
        public List<Patient> allPatients { get; set; }

        private static PatientService instance = null;
        public static PatientService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new PatientService();
                }
                return instance;
            }
        }

        private PatientService()
        {
            allPatients = pfs.GetAll();
        }

        public void GetPatientChart(Patient patient)
        {

        }

        public void AddPrescription(Patient patient,String datePrescribed, Medicine medicine)
        {

        }

        public void RemovePrescription(Patient patient, string datePrescribed, Medicine medicine)
        {

        }

        public void AddPatient(Patient patient)
        {
            allPatients.Add(patient);

            pfs.SavePatients(allPatients);
        }

        public void UpdatePatient(Patient patient)
        {
            for (int i = 0; i < allPatients.Count; i++)
            {
                if (patient.Id.Equals(allPatients[i].Id))
                {
                    allPatients.Remove(allPatients[i]);
                    allPatients.Insert(i, patient);
                }
            }


            pfs.SavePatients(allPatients);
        }

        public void DeletePatient(Patient patient)
        {
            for (int i = 0; i < allPatients.Count; i++)
            {
                if (patient.Id.Equals(allPatients[i].Id))
                {
                    allPatients.Remove(allPatients[i]);
                }
            }

            pfs.SavePatients(allPatients);
        }

    }
}
