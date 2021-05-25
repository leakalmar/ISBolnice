using DTOs;
using Model;
using Service;
using System;
using System.Collections.Generic;

namespace Controllers
{
    public class ChartController
    {
        private static ChartController instance = null;
        public static ChartController Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ChartController();
                }
                return instance;
            }
        }

        public ChartController()
        {

        }

        public List<Therapy> GetTherapiesByPatient(Patient patient)
        {
            return ChartService.Instance.GetTherapiesByPatientId(patient.Id);
        }

        public List<ReportDTO> GetReportsByPatient(Patient patient)
        {
            return ConvertToReportDTO(ChartService.Instance.GetReportsByPatientId(patient.Id));
        }

        private List<ReportDTO> ConvertToReportDTO(List<Report> reports)
        {
            List<ReportDTO> reportDTOs = new List<ReportDTO>();
            foreach (Report report in reports)
            {
                reportDTOs.Add(new ReportDTO(report));
            }
            return reportDTOs;
        }

        public List<Hospitalization> GetHospitalizationsByPatient(Patient patient)
        {
            return ChartService.Instance.GetHospitalizationsByPatientId(patient.Id);
        }

        public List<Prescription> GetPrescriptionsForReport(Patient patient, DateTime reportDate)
        {
            return ChartService.Instance.GetPrescriptionsForReport(patient.Id, reportDate);
        }

        public Hospitalization GetActivHospitalization(Patient patient)
        {
            return ChartService.Instance.GetActivHospitalization(patient.Id);
        }

        public void ReleasePatient(Patient patient)
        {
            ChartService.Instance.ReleasePatient(patient.Id);
        }

        public void UpdateReport(Patient patient, ReportDTO report)
        {
            ChartService.Instance.UpdateReport(patient.Id, new Report(report.AppointmentStart,report.DoctorName,report.DoctorSurname,report.Type,report.AppointmentCause));
        }

        public void AddReport(ReportDTO reportDTO)
        {
            Report newReport = new Report(reportDTO.AppointmentStart, reportDTO.DoctorName, reportDTO.DoctorSurname, reportDTO.Type, reportDTO.AppointmentCause);
            newReport.Anamnesis = reportDTO.Anemnesis;
            if(reportDTO.CountPresciption > 0)
            {
                newReport.HaveRecipe = true;
            }

            ChartService.Instance.AddReport(newReport, reportDTO.PatientID);
        }

        public void AddPrescriptions(List<Prescription> prescriptions, Patient patient)
        {
            ChartService.Instance.AddPrescriptions(prescriptions,patient.Id);
        }


        public int GetNumberOfTherapiesByMonth(int patientId, string month)
        {
            return ChartService.Instance.GetNumberOfTherapiesByMonth(patientId, month);
        }

        public void AddHospitalization(HospitalizationDTO hospitalizationDTO, Patient patient)
        {
            Room room = RoomController.Instance.GetRoomById(hospitalizationDTO.RoomID);
            Bed bed = BedController.Instance.GetBedById(hospitalizationDTO.BedID);
            Hospitalization newHospitalization = new Hospitalization(hospitalizationDTO.AdmissionDate,hospitalizationDTO.ReleaseDate,hospitalizationDTO.Detail,hospitalizationDTO.Doctor,hospitalizationDTO.IsReleased, room, bed);
            ChartService.Instance.AddHospitalization(newHospitalization, patient.Id);
        }
    }
}
