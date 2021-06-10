﻿
using DTOs;
using Enums;
using Hospital_IS.DTOs.SecretaryDTOs;
using Hospital_IS.Service;
using System;
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
            docAppointmentDTO.Reserved = true;
            DoctorAppointmentManagementService.Instance.AddAppointment(docAppointmentDTO);
        }

        public void RemoveAppointment(DoctorAppointmentDTO doctorAppointmentDTO)
        {
            DoctorAppointmentManagementService.Instance.RemoveAppointment(doctorAppointmentDTO);
        }

        public void UpdateAppointment(DoctorAppointmentDTO oldDoctorAppointmentDTO, DoctorAppointmentDTO newDoctorAppointmentDTO)
        {
            newDoctorAppointmentDTO.Id = oldDoctorAppointmentDTO.Id;
            DoctorAppointmentManagementService.Instance.UpdateAppointment(oldDoctorAppointmentDTO, newDoctorAppointmentDTO);
        }

        public void ReloadAppointments()
        {
            DoctorAppointmentManagementService.Instance.ReloadAppointments();
        }

        public List<DoctorAppointmentDTO> GetAppointmentsByPatientId(int patientId)
        {
            return DoctorAppointmentManagementService.Instance.GetAppointmentsByPatientId(patientId);
        }

        public bool VerifyAppointment(DoctorAppointmentDTO doctorAppointmentDTO)
        {
            return DoctorAppointmentManagementService.Instance.VerifyAppointment(doctorAppointmentDTO);
        }

        public List<RoomDTO> GetAllRooms()
        {
            return DoctorAppointmentManagementService.Instance.AllRooms;
        }
        public List<RoomDTO> GetRoomByType(RoomType type)
        {
            return DoctorAppointmentManagementService.Instance.GetRoomByType(type);
        }

        public RoomDTO GetRoomById(int id)
        {
            return DoctorAppointmentManagementService.Instance.GetRoomById(id);
        }

        public List<DoctorAppointmentDTO> GetFutureAppointmentsForDoctor(int doctorId)
        {
            return DoctorAppointmentManagementService.Instance.GetFutureAppointmentsForDoctor(doctorId);
        }

        public List<DoctorAppointmentDTO> GetPreviousAppointmentsForDoctor(int doctorId)
        {
            return DoctorAppointmentManagementService.Instance.GetPreviousAppointmentsForDoctor(doctorId);
        }

        public List<DoctorAppointmentDTO> GetAllAppointmentsForCurrentWeek(DateTime startOfTheWeek)
        {
            return DoctorAppointmentManagementService.Instance.GetAllAppointmentsForCurrentWeek(startOfTheWeek);
        }
    }
}
