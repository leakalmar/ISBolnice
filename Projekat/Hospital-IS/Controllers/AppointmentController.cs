﻿using Model;
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