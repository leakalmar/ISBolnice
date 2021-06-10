using DTOs;
using Enums;
using Hospital_IS.DTOs;
using Hospital_IS.Service;
using Model;
using Service;
using System;
using System.Collections.Generic;

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
            return DoctorAppointmentService.Instance.AllAppointments;
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

        public void EndAppointment(DoctorAppointmentDTO doctorAppointment)
        {
            DoctorAppointment endingAppoitnment = new DoctorAppointment(doctorAppointment);
            endingAppoitnment.IsFinished = true;
            DoctorAppointmentService.Instance.UpdateAppointment(endingAppoitnment, endingAppoitnment);
        }

        public List<DoctorAppointment> SuggestAppointmentsToPatient(PossibleAppointmentForPatientDTO possibleAppointment)
        {
            return SuggestedAppointmentService.Instance.SuggestAppointmentsToPatient(possibleAppointment);
        }

        public List<DoctorAppointment> SuggestAppointmetsToDoctor(List<DateTime> dates, DoctorAppointment tempAppointment)
        {
            return SuggestedAppointmentService.Instance.SuggestAppointmetsToDoctor(dates, tempAppointment);
        }

        public List<SuggestedEmergencyAppDTO> SuggestEmergencyAppsToDoctor(EmergencyAppointmentDTO emergencyAppointmentDTO)
        { 
            return EmergencyAppointmentsByDoctor.Instance.GenerateEmergencyAppointments(emergencyAppointmentDTO);
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
            return EmergencyAppointmentsBySecretary.Instance.GenerateEmergencyAppointments(emerAppointment);
        }

        public bool VerifyAppointment(DoctorAppointment doctorAppointment)
        {
            return VerifyAppointmentService.Instance.VerifyAppointment(doctorAppointment);
        }

        public List<DoctorAppointment> GetAllByDoctorAndDates(int idDoctor, List<DateTime> dates)
        {
            return DoctorAppointmentService.Instance.GetAllByDoctorAndDates(idDoctor, dates);
        }

        public int GetNumberOfAppointmentsByMonth(int patientId, string month)
        {
            return DoctorAppointmentService.Instance.GetNumberOfAppointmentsByMonth(patientId, month);
        }

        public DoctorAppointment GetAppointmentById(int appointmentId)
        {
            return DoctorAppointmentService.Instance.GetAppointmentById(appointmentId);
        }

        public List<DoctorAppointment> GetAllAppointmentsForCurrentWeek(DateTime startOfTheWeek)
        {
            return DoctorAppointmentService.Instance.GetAllAppointmentsForCurrentWeek(startOfTheWeek);
        }

        public List<DoctorAppointment> GetFutureAppointmentsForDoctor(int doctorId)
        {
            return DoctorAppointmentService.Instance.GetFutureAppointmentsForDoctor(doctorId);
        }

        public List<DoctorAppointment> GetPreviousAppointmentsForDoctor(int doctorId)
        {
            return DoctorAppointmentService.Instance.GetPreviousAppointmentsForDoctor(doctorId);
        }

    }
}
