using Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace Hospital_IS.DoctorViewModel
{
    public class StartAppointmentDTO : DoctorViewModelClass
    {
        private DoctorAppointment doctorAppointment;
        private bool started = false;

        public DoctorAppointment DoctorAppointment
        {
            get { return doctorAppointment; }
            set
            {
                doctorAppointment = value;
            }
        }

        public bool Started
        {
            get { return started; }
            set
            {
                started = value;
            }
        }

    }
}
