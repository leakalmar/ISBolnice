using Hospital_IS.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Model
{
    public class Patient : User, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        public DateTime FileDate { get; set; }
        public String Employer { get; set; }
        public Boolean Admitted { get; set; }
        public List<String> Alergies { get; set; }
        public AntiTroll TrollMechanism { get; set; } = new AntiTroll();
        public List<PatientNote> PatientNotes { get; set; } = new List<PatientNote>();
        
        public Boolean IsGuest { get; set; } = false;

        public Patient(int id, string name, string surname, DateTime birthDate, string address, string email, string password, DateTime filedate, String employer, List<String> alergies) : base(id, name, surname, birthDate, address, email, password)
        {
            this.FileDate = filedate;
            this.Employer = employer;
            this.Alergies = alergies;
        }

        public Patient(int id, string name, string surname, DateTime birthDate, string address, string email, string password, DateTime filedate, String employer, List<String> alergies, Boolean isGuest) : base(id, name, surname, birthDate, address, email, password)
        {
            this.FileDate = filedate;
            this.Employer = employer;
            this.Alergies = alergies;
            this.IsGuest = isGuest;
        }

        public Patient()
        {
        }

        public Doctor doctor { get; set; }

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
