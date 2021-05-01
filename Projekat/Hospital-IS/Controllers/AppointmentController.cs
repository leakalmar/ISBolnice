using Model;
using Service;
using System;
using System.Collections.Generic;
using System.Text;

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

        public AppointmentController()
        {

        }

        public List<Appointment> getAllAppByTwoRooms(int roomIdSource, int roomIdDestination)
        {
            return AppointmentService.Instance.getAllAppByTwoRooms(roomIdSource, roomIdDestination);
        }


        public List<Appointment> GetAppointments(int roomID)
        {
            return AppointmentService.Instance.GetAllApointmentsByRoomId(roomID);
        }

    

        public bool CheckAppointment(List<Appointment> appointments, DateTime start, DateTime end)
        {
            return AppointmentService.Instance.CheckAppointment(appointments, start, end);
        }


        public bool MakeRenovationAppointment(DateTime start, DateTime end, String description,int roomId)
        {
            Appointment renovationAppointment = new Appointment(start, end, AppointmetType.Renovation, roomId);
            renovationAppointment.AppointmentCause = description;
           return AppointmentService.Instance.MakeRenovationAppointment(renovationAppointment);

        }

        public bool IsBetweenDates(DateTime end, DateTime start, Appointment appointment)
        {
            throw new NotImplementedException();
        }

        public List<Appointment> getAppByRoomId(int RoomId)
        {
            return AppointmentService.Instance.getAppByRoom(RoomId);
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
