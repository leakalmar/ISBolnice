using Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Model
{
    public class User : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        public EducationCategory Education { get; set; }
        public int Id { get; set; }
        public String Name { get; set; }
        public String Surname { get; set; }
        public DateTime BirthDate { get; set; }
        public String Phone { get; set; }
        public String Email { get; set; }
        public String Gender { get; set; }
        public String Relationship { get; set; }
        public String Password { get; set; }
        public String Address { get; set; }

        public User(int id, string name, string surname, DateTime birthDate, string address, string email, string password)
        {
            this.Id = id;
            this.Name = name;
            this.Surname = surname;
            this.BirthDate = birthDate;
            this.Email = email;
            this.Password = password;
            this.Address = address;
        }

        public User(int id, string name, string surname, DateTime birthDate, string email, string password)
        {
            Id = id;
            Name = name;
            Surname = surname;
            BirthDate = birthDate;
            Email = email;
            Password = password;
        }

        public User()
        {
        }

        public Hospital hospital;

        public Hospital Hospital
        {
            get
            {
                return hospital;
            }
            set
            {
                if (this.hospital == null || !this.hospital.Equals(value))
                {
                    if (this.hospital != null)
                    {
                        Hospital oldHospital = this.hospital;
                        this.hospital = null;
                        oldHospital.RemoveUser(this);
                    }
                    if (value != null)
                    {
                        this.hospital = value;
                        this.hospital.AddUser(this);
                    }
                }
            }
        }

    }
}