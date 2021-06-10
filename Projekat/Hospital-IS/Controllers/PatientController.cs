using Hospital_IS.Model;
using Model;
using Service;
using System.Collections.Generic;

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

        public Patient GetPatientByID(int id)
        {
            return PatientService.Instance.GetPatientByID(id);
        }

        public void ReloadPatients()
        {
            PatientService.Instance.ReloadPatients();
        }

        public bool CheckIfAllergicToMedicine(Patient patient, Medicine medicine)
        {
            return PatientService.Instance.CheckIfAllergicToMedicine(patient.Alergies, medicine.Name);
        }

        public bool CheckIfAllergicToComponent(List<string> allergies, string medicineName)
        {
            return PatientService.Instance.CheckIfAllergicToComponent(medicineName, allergies);
        }

        public bool IsPatientTroll(Patient patient, DoctorAppointment doctorAppointment)
        {
            return PatientService.Instance.IsPatientTroll(patient, doctorAppointment);
        }

        public PatientNote GetNoteForPatientByAppointmentId(int patientId, int appointmentId)
        {
            return PatientService.Instance.GetNoteForPatientByAppointmentId(patientId, appointmentId);
        }

        public void AddAppointmentNote(int patientId, PatientNote patientNote)
        {
            PatientService.Instance.AddAppointmentNote(patientId, patientNote);
        }

       

        public List<PatientNote> GetNotesByPatient(int patientId)
        {
            return PatientService.Instance.GetNotesByPatient(patientId);
        }
    }
}
