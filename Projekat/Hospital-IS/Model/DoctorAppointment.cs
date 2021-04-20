using System;

namespace Model
{
    public class DoctorAppointment : Appointment
    {
        public String Cause { get; set; }
        public String Detail { get; set; }

        public String NameSurnamePatient { get; set; }

        public DoctorAppointment(DateTime date, AppointmetType type, bool reserved, int room, Doctor doc, Patient patient) : base(date, type, reserved, room)
        {
            this.NameSurnamePatient = patient.Name + " " + patient.Surname;
            this.Doctor = doc;
            this.Patient = patient;
        }

        public void SetAdmitted(Patient patient)
        {
            throw new NotImplementedException();
        }
        public Patient Patient { get; set; }


        public Doctor Doctor { get; set; }

        public Model.Report Report { get; set; }

    }
}