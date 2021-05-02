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

        internal List<Prescription> GetPrescriptionsForReport(int id, DateTime reportId)
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

        internal void DeletePrescription(int id, Prescription selectedPrescription)
        {
            foreach(MedicalHistory medicalHistory in AllCharts)
            {
                if (medicalHistory.Id.Equals(id))
                {
                    medicalHistory.Prescription.Remove(selectedPrescription);
                    cfs.SaveChart(AllCharts);
                }
            }
        }

        internal void AddPrescriptions(List<Prescription> prescriptions, int id)
        {
            foreach (MedicalHistory medicalHistory in AllCharts)
            {
                if (medicalHistory.Id.Equals(id))
                {
                    medicalHistory.Prescription.AddRange(prescriptions);
                    cfs.SaveChart(AllCharts);
                }
            }
        }

        public void AddReport(Report newReport, int id)
        {
            foreach(MedicalHistory medicalHistory in AllCharts)
            {
                if (medicalHistory.Id.Equals(id))
                {
                    medicalHistory.Reports.Add(newReport);
                    cfs.SaveChart(AllCharts);
                    return;
                }
            }
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
                    cfs.SaveChart(AllCharts);
                    return;
                }
            }
        }

        public List<Report> GetReportsByPatientId(int id)
        {
            MedicalHistory medicalHistory = GetChartById(id);
            return medicalHistory.Reports;
        }
    }
}
