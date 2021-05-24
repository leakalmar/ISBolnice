using Model;
using Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

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

        public List<Report> GetReportsByPatient(Patient patient)
        {
            return ChartService.Instance.GetReportsByPatientId(patient.Id);
        }

        public List<Hospitalization> GetHospitalizationsByPatient(Patient patient)
        {
            return ChartService.Instance.GetHospitalizationsByPatientId(patient.Id);
        }
        
        public List<Prescription> GetPrescriptionsForReport(Patient patient, Report report)
        {
            return ChartService.Instance.GetPrescriptionsForReport(patient.Id, report.ReportId);
        }

        public Hospitalization GetActivHospitalization(Patient patient)
        {
            return ChartService.Instance.GetActivHospitalization(patient.Id);
        }

        public void ReleasePatient(Patient patient)
        {
            ChartService.Instance.ReleasePatient(patient.Id);
        }

        public void UpdateReport(Patient patient, Report report, String newAnamnsis)
        {
            report.Anamnesis = newAnamnsis;
            ChartService.Instance.UpdateReport(patient.Id, report);
        }

        public void AddReport(DoctorAppointment appointment, String anemnesis, int countPresciption, Patient patient)
        {
            Report newReport = new Report(appointment.AppointmentStart,appointment.Doctor.Name,appointment.Doctor.Surname,appointment.Type,appointment.AppointmentCause);
            newReport.Anamnesis = anemnesis;
            if(countPresciption > 0)
            {
                newReport.HaveRecipe = true;
            }
            
            ChartService.Instance.AddReport(newReport, patient.Id);
        }

        public void AddPrescriptions(List<Prescription> prescriptions, Patient patient)
        {
            ChartService.Instance.AddPrescriptions(prescriptions,patient.Id);
        }

        public void AddHospitalization(DateTime AddmissionDate, DateTime ReleaseDate,string Doctor, Room SelectedRoom, string Details, Patient patient)
        {
            Hospitalization newHospitalization = new Hospitalization(AddmissionDate, ReleaseDate, Doctor, SelectedRoom, Details);
            ChartService.Instance.AddHospitalization(newHospitalization, patient.Id);
        }
    }
}
