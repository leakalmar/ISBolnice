using Model;
using Storages;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service
{
    public class AppointmentService
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
            allAppointments = afs.GetAll();
        }

        public List<Appointment> getAllAppByTwoRooms(int roomIdSource, int roomIdDestination)
        {
            List<Appointment> allApointemnts = new List<Appointment>();


           /* foreach (Appointment ap in getDocAppByRoom(roomIdSource))
            {
                allApointemnts.Add(ap);
            }

            foreach (Appointment ap in getDocAppByRoom(roomIdDestination))
            {
                allApointemnts.Add(ap);
            }*/

            foreach (Appointment ap in getAppByRoom(roomIdSource))
            {
                allApointemnts.Add(ap);
            }


            foreach (Appointment ap in getAppByRoom(roomIdDestination))
            {
                allApointemnts.Add(ap);
            }


            return allApointemnts;
        }


        public List<Appointment> getAppByRoom(int roomId)
        {
 
            List<Appointment> roomAppointment = new List<Appointment>();

            foreach (Appointment appointment in allAppointments)
            {

                if (appointment.Room == roomId)
                {
                    roomAppointment.Add(appointment);

                }
            }

            return roomAppointment;
        }

        public bool CheckAppointment(List<Appointment> roomAppointment, DateTime start, DateTime end)
        {
            bool isFree = true;

            foreach (Appointment appointment in roomAppointment)
            {

                bool between = IsBetweenDates(start, end, appointment);
                if (between || (start <= appointment.AppointmentStart && end >= appointment.AppointmentEnd))
                {

                    isFree = false;
                    break;
                }

            }
            return isFree;
        }

        public bool IsBetweenDates(DateTime end, DateTime start, Appointment appointment)
        {
            return (start > appointment.AppointmentStart && start < appointment.AppointmentEnd) || (end > appointment.AppointmentStart && end < appointment.AppointmentEnd);
        }

        public void AddAppointment(Appointment appointment)
        {
           
            if (appointment == null)
            {
                return;
            }

            if (allAppointments == null)
            {
                allAppointments = new List<Appointment>();

            }

            if (!allAppointments.Contains(appointment))
            {
                allAppointments.Add(appointment);

              
            }
        }

        public void RemoveAppointment(Appointment appointment)
        {

        }

        public void UpdateAppointment(Appointment appointment)
        {

        }


    }
}
