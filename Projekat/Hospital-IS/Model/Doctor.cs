using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Model
{
    public class Doctor : Employee
    {


        public Specialty Specialty { get; set; }

        public Doctor(int id, string name, string surname, DateTime birthDate, string email, string password, string address, double salary, DateTime employmentDate, List<WorkDay> workDays, Specialty spec) : base(id, name, surname, birthDate, email, password, address, salary, employmentDate, workDays)
        {
            this.Specialty = spec;
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



        public ObservableCollection<DoctorAppointment> DoctorAppointment { get; set; }


        public void AddDoctorAppointment(DoctorAppointment newDoctorAppointment)
        {
            if (newDoctorAppointment == null)
                return;
            if (this.DoctorAppointment == null)
                this.DoctorAppointment = new ObservableCollection<DoctorAppointment>();
            if (!this.DoctorAppointment.Contains(newDoctorAppointment))
            {
                this.DoctorAppointment.Add(newDoctorAppointment);
                newDoctorAppointment.Doctor = this;
            }
        }


        public void RemoveDoctorAppointment(DoctorAppointment oldDoctorAppointment)
        {
            if (oldDoctorAppointment == null)
                return;
            if (this.DoctorAppointment != null)
                if (this.DoctorAppointment.Contains(oldDoctorAppointment))
                {
                    this.DoctorAppointment.Remove(oldDoctorAppointment);
                    oldDoctorAppointment.Doctor = null;
                }
        }


        public void RemoveAllDoctorAppointment()
        {
            if (DoctorAppointment != null)
            {
                System.Collections.ArrayList tmpDoctorAppointment = new System.Collections.ArrayList();
                foreach (DoctorAppointment oldDoctorAppointment in DoctorAppointment)
                    tmpDoctorAppointment.Add(oldDoctorAppointment);
                DoctorAppointment.Clear();
                foreach (DoctorAppointment oldDoctorAppointment in tmpDoctorAppointment)
                    oldDoctorAppointment.Doctor = null;
                tmpDoctorAppointment.Clear();
            }
        }
        public System.Collections.ArrayList patient;


        public System.Collections.ArrayList Patient
        {
            get
            {
                if (patient == null)
                    patient = new System.Collections.ArrayList();
                return patient;
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
            if (this.patient == null)
                this.patient = new System.Collections.ArrayList();
            if (!this.patient.Contains(newPatient))
            {
                this.patient.Add(newPatient);
                newPatient.Doctor = this;
            }
        }


        public void RemovePatient(Patient oldPatient)
        {
            if (oldPatient == null)
                return;
            if (this.patient != null)
                if (this.patient.Contains(oldPatient))
                {
                    this.patient.Remove(oldPatient);
                    oldPatient.Doctor = null;
                }
        }

        public void RemoveAllPatient()
        {
            if (patient != null)
            {
                System.Collections.ArrayList tmpPatient = new System.Collections.ArrayList();
                foreach (Patient oldPatient in patient)
                    tmpPatient.Add(oldPatient);
                patient.Clear();
                foreach (Patient oldPatient in tmpPatient)
                    oldPatient.Doctor = null;
                tmpPatient.Clear();
            }
        }

    }
}