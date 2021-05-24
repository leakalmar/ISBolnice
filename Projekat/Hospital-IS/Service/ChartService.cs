using Model;
using Storages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Service
{
    public class ChartService
    {
        private ChartFileStorage cfs = new ChartFileStorage();
        public List<MedicalHistory> AllCharts { get; set; }

        private static ChartService instance = null;
        public static ChartService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ChartService();
                }
                return instance;
            }
        }

        private ChartService()
        {
            AllCharts = cfs.GetAll();
        }

        public List<Therapy> GetTherapiesByPatientId(int id)
        {
            MedicalHistory medicalHistory = GetChartById(id);
            return medicalHistory.Therapies;
        }

        public MedicalHistory GetChartById(int id)
        {
            foreach(MedicalHistory medicalHistory in AllCharts)
            {
                if (medicalHistory.Id.Equals(id))
                {
                    return medicalHistory;
                }
            }
            return null;
        }

        public List<Prescription> GetPrescriptionsByPatientId(int id)
        {
            MedicalHistory medicalHistory = GetChartById(id);
            return medicalHistory.Prescription;
        }

        public List<Prescription> GetPrescriptionsForReport(int id, DateTime reportId)
        {
            List<Prescription> allPrescriptions = GetPrescriptionsByPatientId(id);
            List<Prescription> reportPrescriptions = new List<Prescription>();
            foreach (Prescription prescription in allPrescriptions)
            {
                if (prescription.DatePrescribed.Equals(reportId))
                {
                    reportPrescriptions.Add(prescription);
                }
            }
            return reportPrescriptions;
        }

        public Hospitalization GetActivHospitalization(int id)
        {
            Hospitalization ret = null;
            List<Hospitalization> allHospitalizations = GetHospitalizationsByPatientId(id);
            foreach (Hospitalization hospitalization in allHospitalizations)
            {
                if (hospitalization.Released == false)
                {
                    ret = hospitalization;
                }
            }
            return ret;
        }

        public void ReleasePatient(int id)
        {
            MedicalHistory medicalHistory = GetChartById(id);
            for (int i = 0; i < medicalHistory.Hospitalization.Count; i++)
            {
                if (medicalHistory.Hospitalization[i].Released == false)
                {
                    medicalHistory.Hospitalization[i].Released = true;
                    cfs.SaveCharts(AllCharts);
                    return;
                }
            }
        }

        public void AddPrescriptions(List<Prescription> prescriptions, int id)
        {
            MedicalHistory medicalHistory = GetChartById(id);
            medicalHistory.Prescription.AddRange(prescriptions);
            cfs.SaveCharts(AllCharts);
        }

        public void AddReport(Report newReport, int id)
        {
            MedicalHistory medicalHistory = GetChartById(id);
            medicalHistory.Reports.Add(newReport);
            cfs.SaveCharts(AllCharts);
        }

        public void AddHospitalization(Hospitalization newHospitalization, int id)
        {
            MedicalHistory medicalHistory = GetChartById(id);
            medicalHistory.Hospitalization.Add(newHospitalization);
            cfs.SaveCharts(AllCharts);
        }

        public void UpdateReport(int id, Report report)
        {
            MedicalHistory medicalHistory = GetChartById(id);
            for (int i = 0; i < medicalHistory.Reports.Count; i++)
            {
                if (medicalHistory.Reports[i].ReportId.Equals(report.ReportId))
                {
                    medicalHistory.Reports.RemoveAt(i);
                    medicalHistory.Reports.Insert(i, report);
                    cfs.SaveCharts(AllCharts);
                    return;
                }
            }
        }

        public List<Report> GetReportsByPatientId(int id)
        {
            MedicalHistory medicalHistory = GetChartById(id);
            return medicalHistory.Reports;
        }

        public List<Hospitalization> GetHospitalizationsByPatientId(int id)
        {
            MedicalHistory medicalHistory = GetChartById(id);
            return medicalHistory.Hospitalization;
        }

        public void SaveChart(MedicalHistory medicalHistory)
        {
            AllCharts.Add(medicalHistory);
            cfs.SaveCharts(AllCharts);
        }

        public void DeleteChart(int idChart)
        {
            foreach(MedicalHistory medicalHistory in AllCharts)
            {
                if (medicalHistory.Id.Equals(idChart))
                {
                    AllCharts.Remove(medicalHistory);
                    cfs.SaveCharts(AllCharts);
                    return;
                }
            }
            return;
        }
    }
}
