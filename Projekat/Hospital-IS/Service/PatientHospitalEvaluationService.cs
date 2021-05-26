using Hospital_IS.Model;
using Hospital_IS.Storages;
using System.Collections.Generic;

namespace Hospital_IS.Service
{
    public class PatientHospitalEvaluationService
    {
        private PatientHospitalEvaluationStorage EvaluationStorage = new PatientHospitalEvaluationStorage();
        public List<PatientHospitalEvaluation> AllHospitalEvaluations { get; set; }

        private static PatientHospitalEvaluationService instance = null;
        public static PatientHospitalEvaluationService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new PatientHospitalEvaluationService();
                }
                return instance;
            }
        }

        private PatientHospitalEvaluationService()
        {
            AllHospitalEvaluations = EvaluationStorage.GetAll();
        }

        public void AddHospitalEvaluation(PatientHospitalEvaluation hospitalEvaluation)
        {
            AddEvaluation(hospitalEvaluation);
            EvaluationStorage.SaveAppointments(AllHospitalEvaluations);
        }

        private void AddEvaluation(PatientHospitalEvaluation hospitalEvaluation)
        {
            if (hospitalEvaluation == null)
            {
                return;
            }

            if (AllHospitalEvaluations == null)
            {
                AllHospitalEvaluations = new List<PatientHospitalEvaluation>();

            }

            if (!AllHospitalEvaluations.Contains(hospitalEvaluation))
            {
                AllHospitalEvaluations.Add(hospitalEvaluation);

            }
        }
    }
}
