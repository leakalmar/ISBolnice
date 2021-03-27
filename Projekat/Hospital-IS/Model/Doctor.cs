using System;
using System.Collections.Generic;

namespace Model
{
    public class Doctor : Employee
    {
        public Specialty Specialty { get; set; }
        public List<DoctorAppointment> DoctorAppointment { get; set; }

        public Doctor(int id, string name, string surname, DateTime birthDate, string email, string password, double salary, DateTime employmentDate, List<WorkDay> workDays) : base(id, name, surname, birthDate, email, password, salary, employmentDate, workDays)
        {
            this.DoctorAppointment = new List<DoctorAppointment>();
            this.Patient = new List<Patient>();
        }

        public void ViewPatientDocuments(Patient patient)
        {
            throw new NotImplementedException();
        }

        public Boolean UpadatePatient(Patient patient)
        {
            throw new NotImplementedException();
        }

        public Boolean AddPatietientDocument(Patient patient)
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
                newDoctorAppointment.Doctor = this;
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
                    oldDoctorAppointment.Doctor = null;
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
                    oldDoctorAppointment.Doctor = null;
                tmpDoctorAppointment.Clear();
            }
        }


        public List<Patient> Patient
        {
            get
            {
                if (Patient == null)
                    Patient = new List<Patient>();
                return Patient;
            }
            set
            {
                RemoveAllPatient();
                if (value != null)
                {
                    foreach (Patient oPatient in value)
                        AddPatient(oPatient);
                }
            }
        }

        public void AddPatient(Patient newPatient)
        {
            if (newPatient == null)
                return;
            if (this.Patient == null)
                this.Patient = new List<Patient>();
            if (!this.Patient.Contains(newPatient))
            {
                this.Patient.Add(newPatient);
                newPatient.Doctor = this;
            }
        }

        public void RemovePatient(Patient oldPatient)
        {
            if (oldPatient == null)
                return;
            if (this.Patient != null)
                if (this.Patient.Contains(oldPatient))
                {
                    this.Patient.Remove(oldPatient);
                    oldPatient.Doctor = null;
                }
        }

        public void RemoveAllPatient()
        {
            if (Patient != null)
            {
                System.Collections.ArrayList tmpPatient = new System.Collections.ArrayList();
                foreach (Patient oldPatient in Patient)
                    tmpPatient.Add(oldPatient);
                Patient.Clear();
                foreach (Patient oldPatient in tmpPatient)
                    oldPatient.Doctor = null;
                tmpPatient.Clear();
            }
        }

    }
}