using Hospital_IS.DTOs;
using Model;
using Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Controls;

namespace Controllers
{
    class DoctorAppointmentController
    {
        private static DoctorAppointmentController instance = null;
        public static DoctorAppointmentController Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DoctorAppointmentController();
                }
                return instance;
            }
        }

        private DoctorAppointmentController()
        {

        }

        public List<DoctorAppointment> GetAll()
        {
            return DoctorAppointmentService.Instance.allAppointments;
        }

        public List<DoctorAppointment> GetAllByDoctor(int doctorId)
        {
            return DoctorAppointmentService.Instance.GetAllByDoctor(doctorId);
        }

        public void AddAppointment(DoctorAppointment doctorAppointment)
        {
            doctorAppointment.Reserved = true;
            DoctorAppointmentService.Instance.AddAppointment(doctorAppointment);
        }

        public void RemoveAppointment(DoctorAppointment doctorAppointment)
        {
            DoctorAppointmentService.Instance.RemoveAppointment(doctorAppointment);
        }

        public void UpdateAppointment(DoctorAppointment oldDoctorAppointment, DoctorAppointment newDoctorAppointment)
        {
            DoctorAppointmentService.Instance.UpdateAppointment(oldDoctorAppointment, newDoctorAppointment);
        }

        public void EndAppointment(DoctorAppointment doctorAppointment)
        {
            doctorAppointment.IsFinished = true;
            DoctorAppointmentService.Instance.UpdateAppointment(doctorAppointment,doctorAppointment);
        }

        public List<DoctorAppointment> SuggestAppointmentsToPatient(String timeSlot, Doctor doctor, Patient patient, DateTime date, Boolean priority)
        {
            return DoctorAppointmentService.Instance.SuggestAppointmentsToPatient(timeSlot, doctor, patient, date, priority);
        }

        public List<DoctorAppointment> GetSuggestedAppointmentsByDoctor(List<DateTime> dates, bool isUrgent, Room room, AppointmetType type, TimeSpan duration, Patient patient, Doctor doctor)
        {

            DoctorAppointment tempAppointment = new DoctorAppointment(dates[0], type, false, room.RoomId, doctor, patient);
            tempAppointment.AppointmentEnd = dates[0].Add(duration);
            tempAppointment.IsUrgent = isUrgent;
            return DoctorAppointmentService.Instance.SuggestAppointmetsToDoctor(dates, tempAppointment);
        }

        public List<SuggestedEmergencyAppDTO> GetSuggestedEmergencyAppsForDoctor(List<DateTime> dates, bool isUrgent, Room room, AppointmetType type, TimeSpan duration, Patient patient, Doctor doctor)
        {
            DoctorAppointment tempAppointment = new DoctorAppointment(dates[0], type, false, room.RoomId, doctor, patient);
            tempAppointment.AppointmentEnd = dates[0].Add(duration);
            tempAppointment.IsUrgent = isUrgent;
            return DoctorAppointmentService.Instance.GenerateEmergencyAppointmentsForDoctor(dates, tempAppointment);
        }

        public List<DoctorAppointment> GetFutureAppointmentsByPatient(int patientId)
        {
            return DoctorAppointmentService.Instance.GetFutureAppointmentsByPatient(patientId);
        }

        public List<DoctorAppointment> GetAllAppointmentsByPatient(int patientId)
        {
            return DoctorAppointmentService.Instance.GetAllAppointmentsByPatient(patientId);
        }

        public void ReloadDoctorAppointments()
        {
            DoctorAppointmentService.Instance.ReloadDoctorAppointments();
        }

        internal IEnumerable<SuggestedEmergencyAppDTO> GenerateEmergencyAppointmentsForSecretary(EmergencyAppointmentDTO emerAppointment)
        {
            return DoctorAppointmentService.Instance.GenerateEmergencyAppointmentsForSecretary(emerAppointment);
        }

        public bool VerifyAppointment(DoctorAppointment doctorAppointment, List<Appointment> roomAppointments)
        {
            return DoctorAppointmentService.Instance.VerifyAppointment(doctorAppointment, roomAppointments);
        }

        public List<DoctorAppointment> GetAllByDoctorAndDates(int idDoctor, List<DateTime> dates)
        {
            return DoctorAppointmentService.Instance.GetAllByDoctorAndDates(idDoctor, dates);
        }

    }
}
