using DTOs;
using Hospital_IS.Service;
using Model;
using Service;
using System.Collections.Generic;

namespace Hospital_IS.Controllers
{
    class NotificationController
    {
        private static NotificationController instance = null;
        public static NotificationController Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new NotificationController();
                }
                return instance;
            }
        }

        private NotificationController()
        {
        }

        public List<Notification> GetAll()
        {
            return NotificationService.Instance.AllNotifications;
        }

        public List<Notification> GetAllByUser(int userId)
        {
            return NotificationService.Instance.GetAllByUser(userId);
        }

        public void AddNotification(Notification notification)
        {
            NotificationService.Instance.AddNotification(notification);
        }

        public void ReloadNotifications()
        {
            NotificationService.Instance.ReloadNotifications();
        }

        public void UpdateNotification(Notification notification)
        {
            NotificationService.Instance.UpdateNotification(notification);
        }

        public void DeleteNotification(Notification notification)
        {
            NotificationService.Instance.DeleteNotification(notification);
        }

        public void SendAppointmentCancelationNotification(DoctorAppointmentDTO doctorAppointment)
        {
            Patient patient = PatientService.Instance.GetPatientByID(doctorAppointment.Patient.Id);
            Doctor doctor = DoctorService.Instance.GetDoctorByID(doctorAppointment.Doctor.Id);
            DoctorAppointment docAppointment = new DoctorAppointment(new DoctorAppointment(doctorAppointment.Reserved, doctorAppointment.AppointmentCause,
                    doctorAppointment.AppointmentStart, doctorAppointment.AppointmentEnd, doctorAppointment.Type, doctorAppointment.Room,
                    doctorAppointment.Id, doctorAppointment.IsUrgent, patient, doctor, doctorAppointment.IsFinished));
            NotificationService.Instance.SendAppointmentCancelationNotification(docAppointment);
        }


        public List<Notification> GetPrescriptionNotification(int doctorId)
        {
            return NotificationService.Instance.GetPrescriptionNotification(doctorId);
        }
        public void SendNAppointmentRescheduledNotification(DoctorAppointmentDTO oldApp, DoctorAppointmentDTO newApp)
        {
            Patient patient = PatientService.Instance.GetPatientByID(oldApp.Patient.Id);
            Doctor doctor = DoctorService.Instance.GetDoctorByID(oldApp.Doctor.Id);
            DoctorAppointment oldAppointment = new DoctorAppointment(new DoctorAppointment(oldApp.Reserved, oldApp.AppointmentCause,
                    oldApp.AppointmentStart, oldApp.AppointmentEnd, oldApp.Type, oldApp.Room,
                    oldApp.Id, oldApp.IsUrgent, patient, doctor, oldApp.IsFinished));
            DoctorAppointment newAppointment = new DoctorAppointment(new DoctorAppointment(oldApp.Reserved, newApp.AppointmentCause,
                   newApp.AppointmentStart, newApp.AppointmentEnd, oldApp.Type, oldApp.Room,
                   oldApp.Id, oldApp.IsUrgent, patient, doctor, oldApp.IsFinished));

            NotificationService.Instance.SendAppointmentRescheduledNotification(oldAppointment, newAppointment);
        }

    }
}
