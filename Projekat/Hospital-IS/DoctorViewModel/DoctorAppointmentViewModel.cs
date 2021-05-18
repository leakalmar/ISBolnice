using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital_IS.DoctorViewModel
{
    public class DoctorAppointmentViewModel : DoctorViewModelClass
    {
        private DoctorAppointment doctorAppointment;

        public DoctorAppointment DoctorAppointment
        {
            get { return doctorAppointment; }
            set
            {
                doctorAppointment = value;
            }
        }

    }
}
