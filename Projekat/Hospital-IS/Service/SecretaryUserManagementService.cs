using Hospital_IS.DTOs.SecretaryDTOs;
using Model;
using Service;
using System;
using System.Collections.Generic;
namespace Hospital_IS.Service
{
    class SecretaryUserManagementService
    {
        public List<PatientDTO> AllPatients { get; set; } = new List<PatientDTO>();
        public List<NotificationDTO> AllNotifications { get; set; } = new List<NotificationDTO>();

        private static SecretaryUserManagementService instance = null;
        public static SecretaryUserManagementService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SecretaryUserManagementService();
                }
                return instance;
            }
        }

        private SecretaryUserManagementService()
        {
            LoadPatients();
            LoadNotifications();
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
        private void LoadNotifications()
        {
            foreach (Notification notification in NotificationService.Instance.AllNotifications)
                AllNotifications.Add(new NotificationDTO(notification.Id, notification.Title, notification.Text,
                    notification.DatePosted, notification.LastChanged, notification.Recipients));
        }

        public void ReloadNotifications()
        {
            AllNotifications.Clear();
            LoadNotifications();
        }

        public void AddNotification(NotificationDTO notificationDTO)
        {
            NotificationService.Instance.AddNotification(new Notification(notificationDTO.Title, notificationDTO.Text, 
                notificationDTO.DatePosted, notificationDTO.LastChanged, notificationDTO.Recipients, notificationDTO.Id));
            ReloadNotifications();
        }

        public void UpdateNotification(NotificationDTO notificationDTO)
        {
            for (int i = 0; i < AllNotifications.Count; i++)
            {
                if (notificationDTO.Id.Equals(AllNotifications[i].Id))
                {
                    AllNotifications.Remove(AllNotifications[i]);
                    AllNotifications.Insert(i, notificationDTO);

                    NotificationService.Instance.UpdateNotification(new Notification(notificationDTO.Title, notificationDTO.Text,
                notificationDTO.DatePosted, notificationDTO.LastChanged, notificationDTO.Recipients, notificationDTO.Id));
                }
            }

        }

        public void DeleteNotification(NotificationDTO notificationDTO)
        {
            for (int i = 0; i < AllNotifications.Count; i++)
            {
                if (notificationDTO.Id.Equals(AllNotifications[i].Id))
                {
                    AllNotifications.Remove(AllNotifications[i]);

                    NotificationService.Instance.DeleteNotification(new Notification(notificationDTO.Title, notificationDTO.Text,
                notificationDTO.DatePosted, notificationDTO.LastChanged, notificationDTO.Recipients, notificationDTO.Id));
                }
            }
        }

        public List<NotificationDTO> GetAllByUser(int userId)
        {
            List<NotificationDTO> userNotifications = new List<NotificationDTO>();

            foreach (NotificationDTO notif in AllNotifications)
            {
                foreach (int id in notif.Recipients)
                {
                    if (userId == id)
                        userNotifications.Add(notif);
                }
            }

            return userNotifications;
        }
    }
}
