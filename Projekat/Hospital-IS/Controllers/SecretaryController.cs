using Hospital_IS.DTOs.SecretaryDTOs;
using Hospital_IS.Service;
using System.Collections.Generic;

namespace Hospital_IS.Controllers
{
    class SecretaryController
    {
        private static SecretaryController instance = null;
        public static SecretaryController Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SecretaryController();
                }
                return instance;
            }
        }

        private SecretaryController()
        {

        }

        public void AddPatient(PatientDTO patientDTO)
        {
            SecretaryService.Instance.AddPatient(patientDTO);
        }

        public void UpdatePatient(PatientDTO patientDTO)
        {
            SecretaryService.Instance.UpdatePatient(patientDTO);
        }

        public void ReloadPatients()
        {
            SecretaryService.Instance.ReloadPatients();
        }

        public void DeletePatient(PatientDTO patientDTO)
        {
            SecretaryService.Instance.DeletePatient(patientDTO);
        }

        public List<PatientDTO> GetAllRegisteredPatients()
        {
            return SecretaryService.Instance.GetAllRegisteredPatients();
        }

        public List<PatientDTO> GetAllGuests()
        {
            return SecretaryService.Instance.GetAllGuests();
        }

        public PatientDTO GetPatientByID(int id)
        {
            return SecretaryService.Instance.GetPatientByID(id);
        }

        public List<NotificationDTO> GetAllNotifications()
        {
            return SecretaryService.Instance.AllNotifications;
        }

        public void ReloadNotifications()
        {
            SecretaryService.Instance.ReloadNotifications();
        }

        public void AddNotification(NotificationDTO notificationDTO)
        {
            SecretaryService.Instance.AddNotification(notificationDTO);
        }

        public void UpdateNotification(NotificationDTO notificationDTO)
        {
            SecretaryService.Instance.UpdateNotification(notificationDTO);
        }

        public void DeleteNotification(NotificationDTO notificationDTO)
        {
            SecretaryService.Instance.DeleteNotification(notificationDTO);
        }

        public List<NotificationDTO> GetAllByUser(int userId)
        {
            return SecretaryService.Instance.GetAllByUser(userId);
        }

    }
}
