using System;

namespace Model
{
    public class DoctorAppointment : Appointment
    {
        public String Cause { get; set; }
        public String Detail { get; set; }


        public DoctorAppointment(DateTime date, double time, AppointmetType type, bool reserved, Room room) : base(date, time, type, reserved, room)
        {
        }

        public void SetAdmitted(Patient patient)
        {
            throw new NotImplementedException();
        }

        public Patient patient;

        public Patient Patient
        {
            get
            {
                return patient;
            }
            set
            {
                if (this.patient == null || !this.patient.Equals(value))
                {
                    if (this.patient != null)
                    {
                        Patient oldPatient = this.patient;
                        this.patient = null;
                        oldPatient.RemoveDoctorAppointment(this);
                    }
                    if (value != null)
                    {
                        this.patient = value;
                        this.patient.AddDoctorAppointment(this);
                    }
                }
            }
        }
        public Doctor doctor;


        public Doctor Doctor
        {
            get
            {
                return doctor;
            }
            set
            {
                if (this.doctor == null || !this.doctor.Equals(value))
                {
                    if (this.doctor != null)
                    {
                        Doctor oldDoctor = this.doctor;
                        this.doctor = null;
                        oldDoctor.RemoveDoctorAppointment(this);
                    }
                    if (value != null)
                    {
                        this.doctor = value;
                        this.doctor.AddDoctorAppointment(this);
                    }
                }
            }
        }

    }
}