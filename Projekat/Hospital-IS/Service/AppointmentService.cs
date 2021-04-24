using Model;
using Storages;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service
{
    class AppointmentService
    {
        private ClassicAppointmentStorage afs = new ClassicAppointmentStorage();
        public List<Appointment> allAppointments { get; set; }

        private static AppointmentService instance = null;
        public static AppointmentService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new AppointmentService();
                }
                return instance;
            }
        }

        private AppointmentService()
        {

        }

        public List<Appointment> getAllAppByTwoRooms(int roomIdSource, int roomIdDestination)
        {
            throw new NotImplementedException();
        }

        public bool CheckAppointment(List<Appointment> roomAppointment, DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }

        public bool IsBetweenDates(DateTime end, DateTime start, Appointment appointment)
        {
            throw new NotImplementedException();
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
