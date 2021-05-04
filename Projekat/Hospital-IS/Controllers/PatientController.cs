﻿using DoctorView;
using Model;
using Service;
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

        public List<Patient> GetAll()
        {
            return PatientService.Instance.AllPatients;
        }

        public void AddPatient(Patient patient)
        {
            PatientService.Instance.AddPatient(patient);
        }

        public void UpdatePatient(Patient patient)
        {
            PatientService.Instance.UpdatePatient(patient);
        }

        public void DeletePatient(Patient patient)
        {
            PatientService.Instance.DeletePatient(patient);
        }
        public List<Patient> GetAllRegisteredPatients()
        {
            return PatientService.Instance.GetAllRegisteredPatients();
        }
        public List<Patient> GetAllGuests()
        {
            return PatientService.Instance.GetAllGuests();
        }

        public void ReloadPatients()
        {
            PatientService.Instance.ReloadPatients();
        }

        public bool CheckIfAllergicToMedicine(Patient patient, Medicine medicine)
        {
            return PatientService.Instance.CheckIfAllergicToMedicine(patient.Alergies, medicine.Name);
        }

        public bool CheckIfAllergicToComponent(Patient patient,Medicine medicine)
        {
            return PatientService.Instance.CheckIfAllergicToComponent(medicine.Composition, patient.Alergies);
        }

        public bool IsPatientTroll(Patient patient, DoctorAppointment doctorAppointment)
        {
            return PatientService.Instance.IsPatientTroll(patient, doctorAppointment);
        }
    }
}
