using Enums;
using Hospital_IS.DTOs;
using Model;
using Service;
using System;
using System.Collections.Generic;

namespace Controllers
{
    class AppointmentController
    {
        private static AppointmentController instance = null;
        public static AppointmentController Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new AppointmentController();
                }
                return instance;
            }
        }

        private AppointmentController()
        {

        }

        public Boolean MakeRenovationAppointmentForRoomMerge(Appointment appointmentFirstRoom,Appointment appointmentSecondRoom)
        {
            return AppointmentService.Instance.MakeRenovationAppointmentForRoomMerge(appointmentFirstRoom, appointmentSecondRoom);
        }

        public List<Appointment> GetAllAppByTwoRooms(int roomIdSource, int roomIdDestination)
        {
            return AppointmentService.Instance.GetAllAppByTwoRooms(roomIdSource, roomIdDestination);
        }    

        public bool MakeRenovationAppointment(Appointment renovationAppointment)
        { 
           return AppointmentService.Instance.MakeRenovationAppointment(renovationAppointment);
        }

        public List<Appointment> GetAppByRoomId(int roomId)
        {
            return AppointmentService.Instance.GetAllApointmentsByRoomId(roomId);
        }

        public List<RenovationReportDTO> FindAllRenovationAppBetweeenDates(RenovationDTO renovationDTO)
        {
            return AppointmentService.Instance.FindAllRenovationAppBetweeenDates(renovationDTO);
        }
    }
}
