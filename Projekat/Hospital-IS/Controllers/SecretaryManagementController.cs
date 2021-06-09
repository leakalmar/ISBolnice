using Hospital_IS.DTOs;
using Hospital_IS.DTOs.SecretaryDTOs;
using Hospital_IS.Service;
using System.Collections.Generic;

namespace Hospital_IS.Controllers
{
    class SecretaryManagementController
    {
        private static SecretaryManagementController instance = null;
        public static SecretaryManagementController Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SecretaryManagementController();
                }
                return instance;
            }
        }

        private SecretaryManagementController()
        {

        }

        public void AddPatient(PatientDTO patientDTO)
        {
            SecretaryUserManagementService.Instance.AddPatient(patientDTO);
        }

        public void UpdatePatient(PatientDTO patientDTO)
        {
            SecretaryUserManagementService.Instance.UpdatePatient(patientDTO);
        }

        public void ReloadPatients()
        {
            SecretaryUserManagementService.Instance.ReloadPatients();
        }

        public void DeletePatient(PatientDTO patientDTO)
        {
            SecretaryUserManagementService.Instance.DeletePatient(patientDTO);
        }

        public List<PatientDTO> GetAllRegisteredPatients()
        {
            return SecretaryUserManagementService.Instance.GetAllRegisteredPatients();
        }

        public List<PatientDTO> GetAllGuests()
        {
            return SecretaryUserManagementService.Instance.GetAllGuests();
        }

        public PatientDTO GetPatientByID(int id)
        {
            return SecretaryUserManagementService.Instance.GetPatientByID(id);
        }

        public List<NotificationDTO> GetAllNotifications()
        {
            return SecretaryUserManagementService.Instance.AllNotifications;
        }

        public void ReloadNotifications()
        {
            SecretaryUserManagementService.Instance.ReloadNotifications();
        }

        public void AddNotification(NotificationDTO notificationDTO)
        {
            SecretaryUserManagementService.Instance.AddNotification(notificationDTO);
        }

        public void UpdateNotification(NotificationDTO notificationDTO)
        {
            SecretaryUserManagementService.Instance.UpdateNotification(notificationDTO);
        }

        public void DeleteNotification(NotificationDTO notificationDTO)
        {
            SecretaryUserManagementService.Instance.DeleteNotification(notificationDTO);
        }

        public List<NotificationDTO> GetAllByUser(int userId)
        {
            return SecretaryUserManagementService.Instance.GetAllByUser(userId);
        }
        public void ReloadDoctors()
        {
            SecretaryUserManagementService.Instance.ReloadDoctors();
        }
        public List<DoctorDTO> GetAllDoctors()
        {
            return SecretaryUserManagementService.Instance.AllDoctors;
        }

        public DoctorDTO GetDoctorById(int id)
        {
            return SecretaryUserManagementService.Instance.GetDoctorByID(id);
        }

    }
}
