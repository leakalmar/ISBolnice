
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

    }
}
