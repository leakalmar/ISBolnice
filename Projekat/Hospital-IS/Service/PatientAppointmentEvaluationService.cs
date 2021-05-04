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
        private PatientAppointmentEvaluationStorage EvaluationStorage = new PatientAppointmentEvaluationStorage();
        public List<PatientAppointmentEvaluation> AllAppointmentEvaluations { get; set; }

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
            AllAppointmentEvaluations = EvaluationStorage.GetAll();
        }

        public void AddAppointmentEvaluation(PatientAppointmentEvaluation appointmentEvaluation)
        {
            AddAppointment(appointmentEvaluation);
            EvaluationStorage.SaveAppointments(AllAppointmentEvaluations);
        }

        public void AddAppointment(PatientAppointmentEvaluation appointmentEvaluation)
        {
            if (appointmentEvaluation == null)
            {
                return;
            }

            if (AllAppointmentEvaluations == null)
            {
                AllAppointmentEvaluations = new List<PatientAppointmentEvaluation>();

            }

            if (!AllAppointmentEvaluations.Contains(appointmentEvaluation))
            {
                AllAppointmentEvaluations.Add(appointmentEvaluation);

            }
        }

        public Boolean IsAppointmentEvaluated(DoctorAppointment appointmentEvaluation)
        {
            foreach (PatientAppointmentEvaluation appointment in AllAppointmentEvaluations)
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
            foreach (PatientAppointmentEvaluation appointment in AllAppointmentEvaluations)
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
