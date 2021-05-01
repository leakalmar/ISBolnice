using Model;
using Storages;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

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
            List<Appointment> allApointments = new List<Appointment>();

            allAppointments = GetAllApointmentsByRoomId(roomIdSource);

            allAppointments.AddRange(GetAllApointmentsByRoomId(roomIdDestination));

            return allApointments;

        }

        public List<Appointment> GetAllApointmentsByRoomId(int roomID)
        {
            List<Appointment> allAppointments = new List<Appointment>();
            GetAllDoctorsAppointment(roomID, allAppointments);

            GetAllClassicAppointments(roomID, allAppointments);

            return allAppointments;

        }

        private void GetAllClassicAppointments(int roomIdSource, List<Appointment> allApointments)
        {
            foreach (Appointment ap in getAppByRoom(roomIdSource))
            {
                allApointments.Add(ap);
            }
        }

        private void GetAllDoctorsAppointment(int roomIdSource, List<Appointment> allApointments)
        {
            foreach (Appointment ap in DoctorAppointmentService.Instance.GetAllAppointmentsByRoomId(roomIdSource))
            {
                allApointments.Add(ap);
            }
        }


        public bool MakeRenovationAppointment(Appointment appointment)
        {
            List<Appointment> appointments = new List<Appointment>();

            appointments = GetAllApointmentsByRoomId(appointment.Room);

            bool isPossible = CheckAppointment(appointments, appointment.AppointmentStart, appointment.AppointmentEnd);

            if (isPossible)
            {

                appointment.Reserved = true;
                AddAppointment(appointment);
            }

            return isPossible;
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

        public bool CheckAppointment(List<Appointment> appointments, DateTime start, DateTime end)
        {
            bool isFree = true;
            foreach (Appointment appointment in appointments)
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

        public bool IsBetweenDates(DateTime start, DateTime end, Appointment appointment)
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
                afs.SaveAppointment(allAppointments);
              
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
