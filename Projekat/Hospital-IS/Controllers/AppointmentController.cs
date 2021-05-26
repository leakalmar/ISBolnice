using Enums;
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

        public bool MakeRenovationAppointment(DateTime start, DateTime end, String description,int roomId)
        {
            Appointment renovationAppointment = new Appointment(start, end, AppointmentType.Renovation, roomId);
            renovationAppointment.AppointmentCause = description;
           return AppointmentService.Instance.MakeRenovationAppointment(renovationAppointment);

        }

        public List<Appointment> GetAppByRoomId(int roomId)
        {
            return AppointmentService.Instance.GetAppByRoom(roomId);
        }

        public void AddAppointment(Appointment appointment)
        {

        }

        public void RemoveAppointment(Appointment appointment)
        {

        }

        public void UpdateAppointment(Appointment appointment)
        {

        }
    }
}
