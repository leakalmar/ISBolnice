using DTOs;
using System;
using System.Collections.Generic;


namespace Hospital_IS.Adapter
{
    public interface IDoctorAppointmentTarget
    {
        public void AddDoctorAppointment(DoctorAppointmentDTO newDoctorAppointmentDTO);
        public void DeleteDoctorAppointment(DoctorAppointmentDTO doctorAppointmentDTO);
        public void UpdateDoctorAppointment(DoctorAppointmentDTO oldDoctorAppointmentDTO, DoctorAppointmentDTO newDoctorAppointmentDTO);
        public List<DoctorAppointmentDTO> GetAll();
        public DoctorAppointmentDTO GetDoctorAppointmentById(int id);
        public List<DoctorAppointmentDTO> GetDoctorAppointmentsByPatientId(int patientId);
        public bool VerifyAppointment(DoctorAppointmentDTO doctorAppointmentDTO);
        public List<DoctorAppointmentDTO> GetAppointmentByDoctorId(int doctorId);
        public List<DoctorAppointmentDTO> GetFutureAppointmentsForDoctor(int doctorId);
        public List<DoctorAppointmentDTO> GetPreviousAppointmentsForDoctor(int doctorId);
        public List<DoctorAppointmentDTO> GetAllDoctorAppointmentsForCurrentWeek(DateTime startOfTheWeek);
    }
}
