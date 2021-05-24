using Model;

namespace Hospital_IS.DoctorViewModel
{
    public class StartAppointmentDTO
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
