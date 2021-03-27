using System;
using System.Collections.Generic;

namespace Model
{
    public class Patient : User
    {
        public int chartId { get; set; }
        public String maritalStatus { get; set; }
        public DateTime fileDate { get; set; }
        public String employer { get; set; }
        public Boolean admitted { get; set; }
        public List<String> alergies { get; set; }

        public Boolean isGuest = false;

        public Patient(int id, string name, string surname, DateTime birthDate, string adress, string email, string password, int chartId, ) : base(id, name, surname, birthDate, address, email, password)
        {

        }

        public List<DoctorAppointment> doctorAppointment { get; set; }
        public MedicalHistory medicalHistory { get; set; }

        public Doctor doctor { get; set; }



        public Boolean IsAdmitted()
        {
            throw new NotImplementedException();
        }

        public Boolean ReserveAppointment()
        {
            throw new NotImplementedException();
        }

        public Boolean RemoveAppointment(DoctorAppointment appointment)
        {
            throw new NotImplementedException();
        }

        public Boolean UpdateAppointment(Appointment appointment)
        {
            throw new NotImplementedException();
        }


        public List<DoctorAppointment> DoctorAppointment
        {
            get
            {
                if (doctorAppointment == null)
                    doctorAppointment = new List<DoctorAppointment>();
                return doctorAppointment;
            }
            set
            {
                RemoveAllDoctorAppointment();
                if (value != null)
                {
                    foreach (DoctorAppointment oDoctorAppointment in value)
                        AddDoctorAppointment(oDoctorAppointment);
                }
            }
        }

        public void AddDoctorAppointment(DoctorAppointment newDoctorAppointment)
        {
            if (newDoctorAppointment == null)
                return;
            if (this.doctorAppointment == null)
                this.doctorAppointment = new List<DoctorAppointment>();
            if (!this.doctorAppointment.Contains(newDoctorAppointment))
            {
                this.doctorAppointment.Add(newDoctorAppointment);
                newDoctorAppointment.Patient = this;
            }
        }

        public void RemoveDoctorAppointment(DoctorAppointment oldDoctorAppointment)
        {
            if (oldDoctorAppointment == null)
                return;
            if (this.doctorAppointment != null)
                if (this.doctorAppointment.Contains(oldDoctorAppointment))
                {
                    this.doctorAppointment.Remove(oldDoctorAppointment);
                    oldDoctorAppointment.Patient = null;
                }
        }

        public void RemoveAllDoctorAppointment()
        {
            if (doctorAppointment != null)
            {
                System.Collections.ArrayList tmpDoctorAppointment = new System.Collections.ArrayList();
                foreach (DoctorAppointment oldDoctorAppointment in doctorAppointment)
                    tmpDoctorAppointment.Add(oldDoctorAppointment);
                doctorAppointment.Clear();
                foreach (DoctorAppointment oldDoctorAppointment in tmpDoctorAppointment)
                    oldDoctorAppointment.Patient = null;
                tmpDoctorAppointment.Clear();
            }
        }

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
                        oldDoctor.RemovePatient(this);
                    }
                    if (value != null)
                    {
                        this.doctor = value;
                        this.doctor.AddPatient(this);
                    }
                }
            }
        }

    }
}