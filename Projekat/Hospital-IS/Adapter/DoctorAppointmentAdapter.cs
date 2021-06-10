using Controllers;
using DTOs;
using Hospital_IS.Controllers;
using Hospital_IS.DTOs;
using Hospital_IS.DTOs.SecretaryDTOs;
using Model;
using System;
using System.Collections.Generic;
namespace Hospital_IS.Adapter
{
    public class DoctorAppointmentAdapter :IDoctorAppointmentTarget
    {
        public void AddDoctorAppointment(DoctorAppointmentDTO newDoctorAppointmentDTO)
        {
            DoctorAppointment docAppointment = ConvertToModel(newDoctorAppointmentDTO);
            DoctorAppointmentController.Instance.AddAppointment(docAppointment);
        }

        public void DeleteDoctorAppointment(DoctorAppointmentDTO doctorAppointmentDTO)
        {
            DoctorAppointment docAppointment = ConvertToModel(doctorAppointmentDTO);
            DoctorAppointmentController.Instance.RemoveAppointment(docAppointment);
        }

        public List<DoctorAppointmentDTO> GetAll()
        {
            List<DoctorAppointment> docAppointments = DoctorAppointmentController.Instance.GetAll();
            List<DoctorAppointmentDTO> docAppointmentDTOs = new List<DoctorAppointmentDTO>();
            foreach (DoctorAppointment docApp in docAppointments)
                docAppointmentDTOs.Add(ConvertToDTO(docApp));

            return docAppointmentDTOs;
        }

        public void UpdateDoctorAppointment(DoctorAppointmentDTO oldDoctorAppointmentDTO, DoctorAppointmentDTO newDoctorAppointmentDTO)
        {
            DoctorAppointment oldDocAppointment = ConvertToModel(oldDoctorAppointmentDTO);
            DoctorAppointment newDocAppointment = ConvertToModel(newDoctorAppointmentDTO);
            DoctorAppointmentController.Instance.UpdateAppointment(oldDocAppointment, newDocAppointment);
        }

        public DoctorAppointmentDTO GetDoctorAppointmentById(int id)
        {
            return ConvertToDTO(DoctorAppointmentController.Instance.GetAppointmentById(id));
        }

        public List<DoctorAppointmentDTO> GetDoctorAppointmentsByPatientId(int patientId)
        {
            List<DoctorAppointment> docAppointments = DoctorAppointmentController.Instance.GetAllAppointmentsByPatient(patientId);
            List<DoctorAppointmentDTO> docAppointmentDTOs = new List<DoctorAppointmentDTO>();
            foreach (DoctorAppointment docApp in docAppointments)
                docAppointmentDTOs.Add(ConvertToDTO(docApp));

            return docAppointmentDTOs;
        }

        public bool VerifyAppointment(DoctorAppointmentDTO doctorAppointmentDTO)
        {
            DoctorAppointment docAppointment = ConvertToModel(doctorAppointmentDTO);
            return DoctorAppointmentController.Instance.VerifyAppointment(docAppointment);
        }

        public List<DoctorAppointmentDTO> GetAppointmentByDoctorId(int doctorId)
        {
            List<DoctorAppointment> docAppointments = DoctorAppointmentController.Instance.GetAllByDoctor(doctorId);
            List<DoctorAppointmentDTO> docAppointmentDTOs = new List<DoctorAppointmentDTO>();
            foreach (DoctorAppointment docApp in docAppointments)
                docAppointmentDTOs.Add(ConvertToDTO(docApp));

            return docAppointmentDTOs;
        }

        public List<DoctorAppointmentDTO> GetFutureAppointmentsForDoctor(int doctorId)
        {
            List<DoctorAppointment> docAppointments = DoctorAppointmentController.Instance.GetFutureAppointmentsForDoctor(doctorId);
            List<DoctorAppointmentDTO> docAppointmentDTOs = new List<DoctorAppointmentDTO>();
            foreach (DoctorAppointment docApp in docAppointments)
                docAppointmentDTOs.Add(ConvertToDTO(docApp));

            return docAppointmentDTOs;
        }

        public List<DoctorAppointmentDTO> GetPreviousAppointmentsForDoctor(int doctorId)
        {
            List<DoctorAppointment> docAppointments = DoctorAppointmentController.Instance.GetPreviousAppointmentsForDoctor(doctorId);
            List<DoctorAppointmentDTO> docAppointmentDTOs = new List<DoctorAppointmentDTO>();
            foreach (DoctorAppointment docApp in docAppointments)
                docAppointmentDTOs.Add(ConvertToDTO(docApp));

            return docAppointmentDTOs;
        }

        public List<DoctorAppointmentDTO> GetAllDoctorAppointmentsForCurrentWeek(DateTime startOfTheWeek)
        {
            List<DoctorAppointment> docAppointments = DoctorAppointmentController.Instance.GetAllAppointmentsForCurrentWeek(startOfTheWeek);
            List<DoctorAppointmentDTO> docAppointmentDTOs = new List<DoctorAppointmentDTO>();
            foreach (DoctorAppointment docApp in docAppointments)
                docAppointmentDTOs.Add(ConvertToDTO(docApp));

            return docAppointmentDTOs;
        }

        private DoctorAppointment ConvertToModel(DoctorAppointmentDTO docAppointmentDTO)
        {
            Patient patient = PatientController.Instance.GetPatientByID(docAppointmentDTO.Patient.Id);
            Doctor doctor = DoctorController.Instance.GetDoctorById(docAppointmentDTO.Doctor.Id);
            return new DoctorAppointment(docAppointmentDTO.Reserved, docAppointmentDTO.AppointmentCause,
                docAppointmentDTO.AppointmentStart, docAppointmentDTO.AppointmentEnd, docAppointmentDTO.Type, docAppointmentDTO.Room,
                docAppointmentDTO.Id, docAppointmentDTO.IsUrgent, patient, doctor, docAppointmentDTO.IsFinished);
        }
        private DoctorAppointmentDTO ConvertToDTO(DoctorAppointment docAppointment)
        {
            PatientDTO patientDTO = SecretaryManagementController.Instance.GetPatientByID(docAppointment.Patient.Id);
            DoctorDTO doctorDTO = SecretaryManagementController.Instance.GetDoctorById(docAppointment.Doctor.Id);
            return new DoctorAppointmentDTO(docAppointment.Reserved, docAppointment.AppointmentCause, docAppointment.AppointmentStart, docAppointment.AppointmentEnd,
                docAppointment.Type, docAppointment.Room, docAppointment.Id, docAppointment.IsUrgent, patientDTO, doctorDTO, docAppointment.IsFinished);
        }
    }
}
