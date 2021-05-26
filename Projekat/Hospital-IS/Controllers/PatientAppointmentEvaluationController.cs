using Model;
using Service;
using System;

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

        public Boolean IsAppointmentEvaluated(int evaluatedAppointmentId)
        {
            return PatientAppointmentEvaluationService.Instance.IsAppointmentEvaluated(evaluatedAppointmentId);
        }

        public Boolean ShowHospitalEvaluation(int patientId)
        {
            return PatientAppointmentEvaluationService.Instance.ShowHospitalEvaluation(patientId);
        }
    }
}
