using System;
using System.Collections.ObjectModel;
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
        public int ChartId { get; set; }
        public DateTime FileDate { get; set; }
        public String Employer { get; set; }
        public Boolean Admitted { get; set; }
        public ObservableCollection<String> Alergies { get; set; }
        

        public Boolean IsGuest { get; set; } = false;

        public Patient(int id, string name, string surname, DateTime birthDate, string address, string email, string password, DateTime filedate, String employer, ObservableCollection<String> alergies) : base(id, name, surname, birthDate, address, email, password)
        {
            this.FileDate = filedate;
            this.Employer = employer;
            this.Alergies = alergies;
            this.MedicalHistory = new MedicalHistory();
        }

        public Patient()
        {
        }

        public MedicalHistory MedicalHistory { get; set; } = new MedicalHistory();


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
