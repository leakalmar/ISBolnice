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

        }

        public void GetPatientChart(Patient patient)
        {

        }

        public bool UpdatePatient(Patient patient)
        {
            throw new NotImplementedException();
        }

        public void AddPrescription(Patient patient,String datePrescribed, Medicine medicine)
        {

        }

        public void RemovePrescription(Patient patient, string datePrescribed, Medicine medicine)
        {

        }

        public void AddPatient(Patient patient)
        {

        }

    }
}
