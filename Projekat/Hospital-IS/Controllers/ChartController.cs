using DTOs;
using Hospital_IS.DTOs;
using Hospital_IS.DTOs.SecretaryDTOs;
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

        public List<Therapy> GetTherapiesByPatient(PatientDTO patient)
        {
            return ChartService.Instance.GetTherapiesByPatientId(patient.Id);
        }

        public List<Test> GetTestsByPatient(PatientDTO patient)
        {
            return ChartService.Instance.GetTestsByPatientId(patient.Id);
        }

        public List<ReportDTO> GetReportsByPatient(int patientId)
        {
            return ConvertToReportDTO(ChartService.Instance.GetReportsByPatientId(patientId));
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

        public List<PrescriptionDTO> GetPrescriptionsForReport(int patientId, DateTime reportDate)
        {
            return ConvertPrescriptionToDTO(ChartService.Instance.GetPrescriptionsForReport(patientId, reportDate));
        }

        public List<Test> GetTestsForReport(int patientId, DateTime reportDate)
        {
            return ChartService.Instance.GetTestsForReport(patientId, reportDate);
        }

        public Hospitalization GetActivHospitalization(Patient patient)
        {
            return ChartService.Instance.GetActivHospitalization(patient.Id);
        }

        public void ReleasePatient(Patient patient)
        {
            ChartService.Instance.ReleasePatient(patient.Id);
        }

        public void UpdateReport(int patientId, ReportDTO report)
        {
            ChartService.Instance.UpdateReport(patientId, new Report(report.AppointmentStart,report.DoctorName,report.DoctorSurname,report.Type,report.AppointmentCause));
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

        public void AddPrescriptions(List<PrescriptionDTO> prescriptions, PatientDTO patient)
        {
            ChartService.Instance.AddPrescriptions(ConvertDTOToPrescription(prescriptions) ,patient.Id);
        }

        private List<Prescription> ConvertDTOToPrescription(List<PrescriptionDTO> prescriptionDTOs)
        {
            List<Prescription> prescriptions = new List<Prescription>();
            foreach (PrescriptionDTO prescriptionDTO in prescriptionDTOs)
            {
                Medicine medicine = MedicineController.Instance.ConvertDTOToMedicine(prescriptionDTO.Medicine);
                prescriptions.Add(new Prescription(medicine,prescriptionDTO.DatePrescribed));
            }
            return prescriptions;
        }

        private List<PrescriptionDTO> ConvertPrescriptionToDTO(List<Prescription> prescriptions)
        {
            List<PrescriptionDTO> prescriptionDTOs = new List<PrescriptionDTO>();
            foreach (Prescription prescription in prescriptions)
            {
                MedicineDTO medicine = MedicineController.Instance.ConvertMedicineToDTO(prescription.Medicine);
                prescriptionDTOs.Add(new PrescriptionDTO(medicine, prescription.DatePrescribed));
            }
            return prescriptionDTOs;
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

        public List<Therapy> FindTherapiesInTimeRange(TherapyReportDTO therapyReportDTO)
        {
            return ChartService.Instance.FindTherapiesInTimeRange(therapyReportDTO);
        }
        public void AddTherapy(Therapy newTherapy, PatientDTO patient)
        {
            ChartService.Instance.AddTherapy(newTherapy, patient.Id);
        }

        public void AddTest(Test newTest, PatientDTO patient)
        {
            ChartService.Instance.AddTest(newTest, patient.Id);
        }
    }
}
