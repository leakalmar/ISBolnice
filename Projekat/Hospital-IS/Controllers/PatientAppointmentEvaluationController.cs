using DTOs;
using Model;
using Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace Controllers
{
    public class PatientAppointmentEvaluationController
    {
        private static PatientAppointmentEvaluationController instance = null;
        public static PatientAppointmentEvaluationController Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new PatientAppointmentEvaluationController();
                }
                return instance;
            }
        }

        private PatientAppointmentEvaluationController()
        {

        }

        public void AddAppointmentEvaluation(PatientAppointmentEvaluation appointmentEvaluation)
        {
            PatientAppointmentEvaluationService.Instance.AddAppointmentEvaluation(appointmentEvaluation);
        }

        public Boolean IsAppointmentEvaluated(DoctorAppointment appointmentEvaluation)
        {
            return PatientAppointmentEvaluationService.Instance.IsAppointmentEvaluated(appointmentEvaluation);
        }

        public Boolean ShowHospitalEvaluation(int patientId)
        {
            return PatientAppointmentEvaluationService.Instance.ShowHospitalEvaluation(patientId);
        }
    }
}
