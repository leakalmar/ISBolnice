using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Controllers
{
    class PatientController
    {
        private static PatientController instance = null;
        public static PatientController Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new PatientController();
                }
                return instance;
            }
        }

        private PatientController()
        {

        }

        public void GetPatientChart(Patient patient)
        {

        }

        public bool UpdatePatient(Patient patient)
        {
            throw new NotImplementedException();
        }

        public void AddPrescription(Patient patient, String datePrescribed, Medicine medicine)
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
