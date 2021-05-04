﻿using DoctorView;
using Hospital_IS.DoctorView;
using Hospital_IS.Storages;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Service
{
    class PatientService
    {
        private PatientFileStorage pfs = new PatientFileStorage();
        public List<Patient> AllPatients { get; set; }

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
            AllPatients = pfs.GetAll();
            UpdatePatientTrollMechanism();
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
            AllPatients.Add(patient);

            pfs.SavePatients(AllPatients);
        }

        public void UpdatePatient(Patient patient)
        {
            for (int i = 0; i < AllPatients.Count; i++)
            {
                if (patient.Id.Equals(AllPatients[i].Id))
                {
                    AllPatients.Remove(AllPatients[i]);
                    AllPatients.Insert(i, patient);
                }
            }


            pfs.SavePatients(AllPatients);
        }

        public void DeletePatient(Patient patient)
        {
            for (int i = 0; i < AllPatients.Count; i++)
            {
                if (patient.Id.Equals(AllPatients[i].Id))
                {
                    AllPatients.Remove(AllPatients[i]);
                }
            }

            pfs.SavePatients(AllPatients);
        }

        public bool CheckIfAllergicToComponent(List<MedicineComponent> composition, ObservableCollection<string> alergies)
        {
            List<MedicineComponent> components = composition;
            foreach (MedicineComponent c in components)
            {
                foreach (String allergie in alergies)
                {
                    Medicine med = MedicineService.Instance.FindMedicine(allergie);
                    if (med != null)
                    {
                        List<MedicineComponent> allergieComponents = med.Composition;
                        foreach (MedicineComponent allergieComp in allergieComponents)
                        {
                            if (c.Component.ToLower().Equals(allergieComp.Component.ToLower()))
                            {
                                return true;
                            }
                        }
                    }

                }
            }

            return false;
        }

        public bool CheckIfAllergic(ObservableCollection<string> alergies, string name)
        {
            foreach (String allergie in alergies)
            {
                if (name.ToLower().Contains(allergie.ToLower()))
                {
                    return true;
                }
            }
            return false;
        }

        public List<int> GetPatientIDs()
        {
            List<int> allPatientIDs = new List<int>();
            foreach (Patient patient in AllPatients)
            {
                allPatientIDs.Add(patient.Id);
            }
            return allPatientIDs;
        }

        public bool IsPatientTroll(Patient patient, DoctorAppointment doctorAppointment)
        {
            int timeRange = 14;
            int maxAppointmentsInTimeRange = 3;
            if (doctorAppointment.AppointmentStart < patient.TrollMechanism.TrollCheckStartDate.AddDays(timeRange))
            {
                patient.TrollMechanism.AppointmentCounterInTimeRange++;
                if (patient.TrollMechanism.AppointmentCounterInTimeRange >= maxAppointmentsInTimeRange)
                {
                    patient.TrollMechanism.IsTroll = true;
                    patient.TrollMechanism.TrollCheckStartDate = doctorAppointment.AppointmentStart.Date;
                    return patient.TrollMechanism.IsTroll;
                }
            }
            else if (doctorAppointment.AppointmentStart >= patient.TrollMechanism.TrollCheckStartDate.AddDays(timeRange) && patient.TrollMechanism.IsTroll)
            {
                patient.TrollMechanism.IsTroll = false;
                patient.TrollMechanism.TrollCheckStartDate = doctorAppointment.AppointmentStart.Date;
                patient.TrollMechanism.AppointmentCounterInTimeRange = 1;
            }
            return patient.TrollMechanism.IsTroll;
        }

        private void UpdatePatientTrollMechanism()
        {
            int timeRange = 14;
            int maxAppointmentsInTimeRange = 3;
            foreach (Patient patient in AllPatients)
            {
                if (patient.TrollMechanism.TrollCheckStartDate.AddDays(timeRange) == DateTime.Today)
                {
                    patient.TrollMechanism.TrollCheckStartDate = DateTime.Today;
                    if (DoctorAppointmentService.Instance.GetNumberOfAppointmentsInTimeRange(patient.Id, DateTime.Today, DateTime.Today.AddDays(timeRange)) > maxAppointmentsInTimeRange)
                    {
                        patient.TrollMechanism.IsTroll = true;
                    }
                    else
                    {
                        patient.TrollMechanism.AppointmentCounterInTimeRange = DoctorAppointmentService.Instance.GetNumberOfAppointmentsInTimeRange(patient.Id, DateTime.Today, DateTime.Today.AddDays(timeRange));
                    }                  
                }
            }
        }
        public List<Patient> GetAllRegisteredPatients()
        {
            List<Patient> registeredPatients = new List<Patient>();
            foreach (Patient patient in AllPatients)
            {
                if (!patient.IsGuest)
                    registeredPatients.Add(patient);
            }
            return registeredPatients;
        }

        public List<Patient> GetAllGuests()
        {
            List<Patient> guests = new List<Patient>();
            foreach (Patient patient in AllPatients)
            {
                if (patient.IsGuest)
                    guests.Add(patient);
            }
            return guests;
        }

        public void ReloadPatients()
        {
            AllPatients = pfs.GetAll();
        }

    }
}
