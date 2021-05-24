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
            newDoctorAppointment.Id = oldDoctorAppointment.Id;
            DoctorAppointmentService.Instance.UpdateAppointment(oldDoctorAppointment, newDoctorAppointment);
        }

        public void EndAppointment(DoctorAppointment doctorAppointment)
        {
            doctorAppointment.IsFinished = true;
            DoctorAppointmentService.Instance.UpdateAppointment(doctorAppointment,doctorAppointment);
        }

        public List<DoctorAppointment> SuggestAppointmentsToPatient(PossibleAppointmentForPatientDTO possibleAppointment)
        {
            return DoctorAppointmentService.Instance.SuggestAppointmentsToPatient(possibleAppointment);
        }

        public List<DoctorAppointment> SuggestAppointmetsToDoctor(List<DateTime> dates, bool isUrgent, Room room, AppointmentType type, TimeSpan duration, Patient patient, Doctor doctor)
        {

            DoctorAppointment tempAppointment = new DoctorAppointment(dates[0], type, false, room.RoomId, doctor, patient);
            tempAppointment.AppointmentEnd = dates[0].Add(duration);
            tempAppointment.IsUrgent = isUrgent;
            return DoctorAppointmentService.Instance.SuggestAppointmetsToDoctor(dates, tempAppointment);
        }

        public List<SuggestedEmergencyAppDTO> SuggestEmergencyAppsToDoctor(List<DateTime> dates, bool isUrgent, Room room, AppointmentType type, TimeSpan duration, Patient patient, Doctor doctor)
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
            return DoctorAppointmentService.Instance.GetAllByPatient(patientId);
        }

        public void ReloadDoctorAppointments()
        {
            DoctorAppointmentService.Instance.ReloadDoctorAppointments();
        }

        internal IEnumerable<SuggestedEmergencyAppDTO> GenerateEmergencyAppointmentsForSecretary(EmergencyAppointmentDTO emerAppointment)
        {
            return DoctorAppointmentService.Instance.GenerateEmergencyAppointmentsForSecretary(emerAppointment);
        }

        public bool VerifyAppointment(DoctorAppointment doctorAppointment)
        {
            return DoctorAppointmentService.Instance.VerifyAppointment(doctorAppointment);
        }

        public List<DoctorAppointment> GetAllByDoctorAndDates(int idDoctor, List<DateTime> dates)
        {
            return DoctorAppointmentService.Instance.GetAllByDoctorAndDates(idDoctor, dates);
        }

        public int GetNumberOfAppointmentsByMonth(int patientId, string month)
        {
            return DoctorAppointmentService.Instance.GetNumberOfAppointmentsByMonth(patientId, month);
        }
    }
}
