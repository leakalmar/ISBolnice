using Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace Hospital_IS.DoctorViewModel
{
    public class DoctorAppointmentViewModel : DoctorViewModelClass
    {
        private DoctorAppointment doctorAppointment;
        private Visibility started = Visibility.Collapsed;

        public DoctorAppointment DoctorAppointment
        {
            get { return doctorAppointment; }
            set
            {
                doctorAppointment = value;
            }
        }

        public Visibility Started
        {
            get { return started; }
            set
            {
                started = value;
            }
        }

    }
}
