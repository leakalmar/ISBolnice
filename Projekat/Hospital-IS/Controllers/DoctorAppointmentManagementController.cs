﻿
using DTOs;
using Hospital_IS.Service;
using System.Collections.Generic;

namespace Hospital_IS.Controllers
{
    class DoctorAppointmentManagementController
    {
        private static DoctorAppointmentManagementController instance = null;
        public static DoctorAppointmentManagementController Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DoctorAppointmentManagementController();
                }
                return instance;
            }
        }

        private DoctorAppointmentManagementController()
        {

        }

        public List<DoctorAppointmentDTO> GetAll()
        {
            return DoctorAppointmentManagementService.Instance.AllAppointments;
        }

        public void AddAppointment(DoctorAppointmentDTO docAppointmentDTO)
        {
            DoctorAppointmentManagementService.Instance.AddAppointment(docAppointmentDTO);
        }

        public void RemoveAppointment(DoctorAppointmentDTO doctorAppointmentDTO)
        {
            DoctorAppointmentManagementService.Instance.RemoveAppointment(doctorAppointmentDTO);
        }

        public void UpdateAppointment(DoctorAppointmentDTO oldDoctorAppointmentDTO, DoctorAppointmentDTO newDoctorAppointmentDTO)
        {
            DoctorAppointmentManagementService.Instance.UpdateAppointment(oldDoctorAppointmentDTO, newDoctorAppointmentDTO);
        }

        public void ReloadAppointments()
        {
            DoctorAppointmentManagementService.Instance.ReloadAppointments();
        }
        public bool VerifyAppointment(DoctorAppointmentDTO doctorAppointmentDTO)
        {
            return DoctorAppointmentManagementService.Instance.VerifyAppointment(doctorAppointmentDTO);
        }
    }
}
