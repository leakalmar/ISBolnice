using Hospital_IS.DTOs.SecretaryDTOs;
using Model;
using Service;
using System.Collections.Generic;
namespace Hospital_IS.Service
{
    class SecretaryService
    {
        public List<PatientDTO> AllPatients { get; set; } = new List<PatientDTO>();

        private static SecretaryService instance = null;
        public static SecretaryService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SecretaryService();
                }
                return instance;
            }
        }

        private SecretaryService()
        {
            LoadPatients();
        }

        private void LoadPatients()
        {
            foreach (Patient patient in PatientService.Instance.AllPatients)
                AllPatients.Add(new PatientDTO(patient.Id, patient.Name, patient.Surname, patient.Gender, patient.BirthDate, patient.Phone, patient.Email,
                    patient.Education, patient.Relationship, patient.Employer, patient.Password, patient.Address, patient.Alergies, patient.IsGuest));
        }

        public void ReloadPatients()
        {
            AllPatients.Clear();
            LoadPatients();

        }

        public void AddPatient(PatientDTO patientDTO)
        {
            AllPatients.Add(patientDTO);
            PatientService.Instance.AddPatient(new Patient(patientDTO.Id, patientDTO.Name, patientDTO.Surname, patientDTO.BirthDate, patientDTO.Address, patientDTO.Email,
                patientDTO.Password, patientDTO.FileDate, patientDTO.Employer, patientDTO.Alergies, patientDTO.IsGuest));
        }

        public void UpdatePatient(PatientDTO patientDTO)
        {
            for (int i = 0; i < AllPatients.Count; i++)
            {
                if (patientDTO.Id.Equals(AllPatients[i].Id))
                {
                    AllPatients.Remove(AllPatients[i]);
                    AllPatients.Insert(i, patientDTO);

                    PatientService.Instance.UpdatePatient(new Patient(patientDTO.Id, patientDTO.Name, patientDTO.Surname, patientDTO.BirthDate, patientDTO.Address, patientDTO.Email,
                patientDTO.Password, patientDTO.FileDate, patientDTO.Employer, patientDTO.Alergies, patientDTO.IsGuest));

                }
            }
        }

        public void DeletePatient(PatientDTO patientDTO)
        {
            for (int i = 0; i < AllPatients.Count; i++)
            {
                if (patientDTO.Id.Equals(AllPatients[i].Id))
                {
                    AllPatients.Remove(AllPatients[i]);
                    ChartService.Instance.DeleteChart(patientDTO.Id);
                    PatientService.Instance.DeletePatient(new Patient(patientDTO.Id, patientDTO.Name, patientDTO.Surname, patientDTO.BirthDate, patientDTO.Address, patientDTO.Email,
                patientDTO.Password, patientDTO.FileDate, patientDTO.Employer, patientDTO.Alergies, patientDTO.IsGuest));
                }
            }

        }

        public List<PatientDTO> GetAllRegisteredPatients()
        {
            List<PatientDTO> registeredPatients = new List<PatientDTO>();
            foreach (PatientDTO patientDTO in AllPatients)
            {
                if (!patientDTO.IsGuest)
                    registeredPatients.Add(patientDTO);
            }
            return registeredPatients;
        }

        public List<PatientDTO> GetAllGuests()
        {
            List<PatientDTO> guests = new List<PatientDTO>();
            foreach (PatientDTO patientDTO in AllPatients)
            {
                if (patientDTO.IsGuest)
                    guests.Add(patientDTO);
            }
            return guests;
        }

        public PatientDTO GetPatientByID(int id)
        {
            foreach (PatientDTO patientDTO in AllPatients)
            {
                if (patientDTO.Id.Equals(id))
                    return patientDTO;
            }
            return null;
        }
    }
}
