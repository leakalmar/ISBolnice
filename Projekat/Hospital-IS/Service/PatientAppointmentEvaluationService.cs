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

        private void AddAppointment(PatientAppointmentEvaluation appointmentEvaluation)
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

        public Boolean IsAppointmentEvaluated(int evaluatedAppointmentId)
        {
            foreach (PatientAppointmentEvaluation appointment in AllAppointmentEvaluations)
            {
                if(appointment.DoctorAppointmentId == evaluatedAppointmentId)
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
                DoctorAppointment doctorAppointment = DoctorAppointmentService.Instance.GetAppointmentById(appointment.DoctorAppointmentId);
                if (doctorAppointment.Patient.Id == patientId)
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
