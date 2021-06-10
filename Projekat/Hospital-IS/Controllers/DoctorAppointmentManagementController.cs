
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
        public void AddAppointment(DoctorAppointmentDTO docAppointmentDTO)
        {
            docAppointmentDTO.Reserved = true;
            DoctorAppointmentManagementService.Instance.AddAppointment(docAppointmentDTO);
        }

        public void UpdateAppointment(DoctorAppointmentDTO oldDoctorAppointmentDTO, DoctorAppointmentDTO newDoctorAppointmentDTO)
        {
            newDoctorAppointmentDTO.Id = oldDoctorAppointmentDTO.Id;
            DoctorAppointmentManagementService.Instance.UpdateAppointment(oldDoctorAppointmentDTO, newDoctorAppointmentDTO);
        }

        public DoctorAppointmentDTO GetAppointmentById(int id)
        {
            return DoctorAppointmentManagementService.Instance.GetAppointmentById(id);
        }

        public void EndAppointment(DoctorAppointmentDTO docAppointmentDTO)
        {
            docAppointmentDTO.IsFinished = true;
            DoctorAppointmentManagementService.Instance.UpdateAppointment(docAppointmentDTO, docAppointmentDTO);
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

        public List<DoctorAppointmentDTO> GetAppointmentByDoctorId(int doctorId)
        {
            return DoctorAppointmentManagementService.Instance.GetAppointmentByDoctorId(doctorId);
        }

    }
}
