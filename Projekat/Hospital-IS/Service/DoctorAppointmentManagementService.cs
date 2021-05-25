using DTOs;
using Hospital_IS.DTOs;
using Hospital_IS.DTOs.SecretaryDTOs;
using Model;
using Service;
using System.Collections.Generic;

namespace Hospital_IS.Service
{
    class DoctorAppointmentManagementService
    {
        public List<DoctorAppointmentDTO> AllAppointments { get; set; } = new List<DoctorAppointmentDTO>();

        private static DoctorAppointmentManagementService instance = null;
        public static DoctorAppointmentManagementService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DoctorAppointmentManagementService();
                }
                return instance;
            }
        }

        private DoctorAppointmentManagementService()
        {
            LoadAppointments();
        }

        private void LoadAppointments()
        {
            foreach (DoctorAppointment docAppointment in DoctorAppointmentService.Instance.allAppointments)
            {
                PatientDTO patientDTO = SecretaryUserManagementService.Instance.GetPatientByID(docAppointment.Patient.Id);
                DoctorDTO doctorDTO = SecretaryUserManagementService.Instance.GetDoctorByID(docAppointment.Doctor.Id);
                AllAppointments.Add(new DoctorAppointmentDTO(docAppointment.Reserved, docAppointment.AppointmentCause, docAppointment.AppointmentStart, docAppointment.AppointmentEnd,
                    docAppointment.Type, docAppointment.Room, docAppointment.Id, docAppointment.IsUrgent, patientDTO, doctorDTO, docAppointment.IsFinished));
            }
        }
        public void ReloadAppointments()
        {
            AllAppointments.Clear();
            LoadAppointments();

        }

        public void AddAppointment(DoctorAppointmentDTO docAppointmentDTO)
        {
            if (docAppointmentDTO == null)
            {
                return;
            }

            if (AllAppointments == null)
            {
                AllAppointments = new List<DoctorAppointmentDTO>();

            }

            if (!AllAppointments.Contains(docAppointmentDTO))
            {
                Patient patient = PatientService.Instance.GetPatientByID(docAppointmentDTO.Patient.Id);
                Doctor doctor = DoctorService.Instance.GetDoctorByID(docAppointmentDTO.Doctor.Id);
                DoctorAppointmentService.Instance.AddAppointment(new DoctorAppointment(docAppointmentDTO.Reserved, docAppointmentDTO.AppointmentCause, 
                    docAppointmentDTO.AppointmentStart, docAppointmentDTO.AppointmentEnd, docAppointmentDTO.Type, docAppointmentDTO.Room, 
                    docAppointmentDTO.Id, docAppointmentDTO.IsUrgent, patient, doctor, docAppointmentDTO.IsFinished));
                ReloadAppointments();    
            }
        }

        public void RemoveAppointment(DoctorAppointmentDTO doctorAppointmentDTO)
        {
            if (doctorAppointmentDTO == null)
            {
                return;
            }

            if (AllAppointments != null)
            {
                foreach (DoctorAppointmentDTO doctorApp in AllAppointments)
                {
                    if (doctorAppointmentDTO.AppointmentStart.Equals(doctorApp.AppointmentStart) && doctorAppointmentDTO.Doctor.Id.Equals(doctorApp.Doctor.Id))
                    {
                        AllAppointments.Remove(doctorApp);
                        Patient patient = PatientService.Instance.GetPatientByID(doctorApp.Patient.Id);
                        Doctor doctor = DoctorService.Instance.GetDoctorByID(doctorApp.Doctor.Id);
                        DoctorAppointmentService.Instance.RemoveAppointment(new DoctorAppointment(doctorApp.Reserved, doctorApp.AppointmentCause,
                            doctorApp.AppointmentStart, doctorApp.AppointmentEnd, doctorApp.Type, doctorApp.Room,
                            doctorApp.Id, doctorApp.IsUrgent, patient, doctor, doctorApp.IsFinished));
                        break;
                    }
                }
            }

        }

        public void UpdateAppointment(DoctorAppointmentDTO oldDoctorAppointmentDTO, DoctorAppointmentDTO newDoctorAppointmentDTO)
        {
            for (int i = 0; i < AllAppointments.Count; i++)
            {
                if (newDoctorAppointmentDTO.Id == AllAppointments[i].Id)
                {
                    AllAppointments.Remove(AllAppointments[i]);
                    AllAppointments.Insert(i, newDoctorAppointmentDTO);

                    Patient patient = PatientService.Instance.GetPatientByID(newDoctorAppointmentDTO.Patient.Id);
                    Doctor doctor = DoctorService.Instance.GetDoctorByID(newDoctorAppointmentDTO.Doctor.Id);
                    DoctorAppointmentService.Instance.RemoveAppointment(new DoctorAppointment(newDoctorAppointmentDTO.Reserved, newDoctorAppointmentDTO.AppointmentCause,
                        newDoctorAppointmentDTO.AppointmentStart, newDoctorAppointmentDTO.AppointmentEnd, newDoctorAppointmentDTO.Type, newDoctorAppointmentDTO.Room,
                        newDoctorAppointmentDTO.Id, newDoctorAppointmentDTO.IsUrgent, patient, doctor, newDoctorAppointmentDTO.IsFinished));

                    return;
                }
            }
        }
    }
}
