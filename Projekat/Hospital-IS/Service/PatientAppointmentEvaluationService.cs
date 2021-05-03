using DTOs;
using Model;
using Storages;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service
{
    public class PatientAppointmentEvaluationService
    {
        private PatientAppointmentEvaluationStorage evaluationStorage = new PatientAppointmentEvaluationStorage();
        public List<PatientAppointmentEvaluationDTO> AllAppointmentEvaluations { get; set; }

        private static PatientAppointmentEvaluationService instance = null;
        public static PatientAppointmentEvaluationService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new PatientAppointmentEvaluationService();
                }
                return instance;
            }
        }

        private PatientAppointmentEvaluationService()
        {
            AllAppointmentEvaluations = evaluationStorage.GetAll();
        }

        public void AddAppointmentEvaluation(PatientAppointmentEvaluationDTO appointmentEvaluation)
        {
            AddAppointment(appointmentEvaluation);
            evaluationStorage.SaveAppointment(AllAppointmentEvaluations);
        }

        public void AddAppointment(PatientAppointmentEvaluationDTO appointmentEvaluation)
        {
            if (appointmentEvaluation == null)
            {
                return;
            }

            if (AllAppointmentEvaluations == null)
            {
                AllAppointmentEvaluations = new List<PatientAppointmentEvaluationDTO>();

            }

            if (!AllAppointmentEvaluations.Contains(appointmentEvaluation))
            {
                AllAppointmentEvaluations.Add(appointmentEvaluation);

            }
        }

        public Boolean IsAppointmentEvaluated(DoctorAppointment appointmentEvaluation)
        {
            foreach (PatientAppointmentEvaluationDTO appointment in AllAppointmentEvaluations)
            {
                if(appointment.DoctorAppointmentDate == appointmentEvaluation.AppointmentStart.Date && appointment.DoctorId == appointmentEvaluation.Doctor.Id && appointment.PatientId == appointmentEvaluation.Patient.Id)
                {
                    return true;
                }
            }
            return false;
        }

        public Boolean ShowHospitalEvaluation(int patientId)
        {
            int appointmentCounter = 0;
            int hospitalEvaluation = 3;
            foreach (PatientAppointmentEvaluationDTO appointment in AllAppointmentEvaluations)
            {
                if (appointment.PatientId == patientId)
                {
                    appointmentCounter++;
                }
            }

            if (appointmentCounter % hospitalEvaluation == 0)
            {
                return true;
            }
            return false;
        }
    }
}
