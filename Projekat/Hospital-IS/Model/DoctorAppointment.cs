using Newtonsoft.Json;
using System;

namespace Model
{
    public class DoctorAppointment : Appointment
    {
        public String NameSurnamePatient { get; set; }
        public String AppTypeText { get; set; }


        [JsonConstructor]
        public DoctorAppointment(DateTime date, AppointmetType type, bool reserved, int room, Doctor doc, Patient patient) : base(date, type, reserved, room)
        {
            if (patient != null)
                this.NameSurnamePatient = patient.Name + " " + patient.Surname;
            this.Doctor = doc;
            this.Patient = patient;
        }
        
        public DoctorAppointment()
        {
        }
        
        public DoctorAppointment(DateTime dateStart, DateTime dateEnd, AppointmetType type, int room, Doctor doc, Patient patient) : base(dateStart, dateEnd, type, room)
        {
            this.NameSurnamePatient = patient.Name + " " + patient.Surname;
            this.Doctor = doc;
            this.Patient = patient;
        }

        public DoctorAppointment(DoctorAppointment docApp) 
        {
            this.Reserved = docApp.Reserved;
            this.AppointmentCause = docApp.AppointmentCause;
            this.AppointmentStart = docApp.AppointmentStart;
            this.AppointmentEnd = docApp.AppointmentEnd;
            this.Type = docApp.Type;
            this.Room = docApp.Room;
            this.Doctor = docApp.Doctor;
            this.Patient = docApp.Patient;
            this.AppTypeText = docApp.AppTypeText;
        }

        public void SetAdmitted(Patient patient)
        {
            throw new NotImplementedException();
        }
        public Patient Patient { get; set; }


        public Doctor Doctor { get; set; }

        public bool IsFinished { get; set; }

    }
}
