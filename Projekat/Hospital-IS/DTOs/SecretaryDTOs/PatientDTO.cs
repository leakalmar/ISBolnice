using Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hospital_IS.DTOs.SecretaryDTOs
{
    public class PatientDTO : ValidationBase
    {
        #region Feilds
        private EducationCategory education;
        private int id;
        private String name;
        private String surname;
        private DateTime birthDate;
        private String phone;
        private String email;
        private String gender;
        private String relationship;
        private String password;
        private String address;
        private String employer;
        private List<String> alergies;
        private DateTime fileDate;
        private Boolean isGuest;
        private Boolean isAdmitted;
        private String bloodType;

        public EducationCategory Education
        {
            get { return education; }
            set {
                education = value;
                OnPropertyChanged("Education");
            }
        }
        public int Id
        {
            get { return id; }
            set
            {
                id = value;
                OnPropertyChanged("Id");
            }
        }
        public String Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }
        public String Surname
        {
            get { return surname; }
            set
            {
                surname = value;
                OnPropertyChanged("Surname");
            }
        }
        public DateTime BirthDate
        {
            get { return birthDate; }
            set
            {
                birthDate = value;
                OnPropertyChanged("BirthDate");
            }
        }
        public String Phone
        {
            get { return phone; }
            set
            {
                phone = value;
                OnPropertyChanged("Phone");
            }
        }
        public String Email
        {
            get { return email; }
            set
            {
                email = value;
                OnPropertyChanged("Email");
            }
        }
        public String Gender
        {
            get { return gender; }
            set
            {
                gender = value;
                OnPropertyChanged("Gender");
            }
        }
      
        public String Relationship
        {
            get { return relationship; }
            set
            {
                relationship = value;
                OnPropertyChanged("Relationship");
            }
        }
        public String Password
        {
            get { return password; }
            set
            {
                password = value;
                OnPropertyChanged("Password");
            }
        }
        public String Address
        {
            get { return address; }
            set
            {
                address = value;
                OnPropertyChanged("Address");
            }
        }
        public String Employer
        {
            get { return employer; }
            set
            {
                employer = value;
                OnPropertyChanged("Employer");
            }
        }
        public List<String> Alergies
        {
            get { return alergies; }
            set
            {
                alergies = value;
                OnPropertyChanged("Alergies");
            }
        }
        public DateTime FileDate
        {
            get { return fileDate; }
            set
            {
                fileDate = value;
                OnPropertyChanged("FileDate");
            }
        }
        public Boolean IsGuest
        {
            get { return isGuest; }
            set
            {
                isGuest = value;
                OnPropertyChanged("IsGuest");
            }
        }
        public Boolean IsAdmitted
        {
            get { return isAdmitted; }
            set
            {
                isAdmitted = value;
                OnPropertyChanged("IsAdmitted");
            }
        }
        public String BloodType
        {
            get { return bloodType; }
            set
            {
                bloodType = value;
                OnPropertyChanged("BloodType");
            }
        }
        #endregion
        public PatientDTO(int id, string name, string surname, string gender, DateTime birthDate, string phone, string email, 
            EducationCategory education, string relationship, string employer, string password, string address, List<string> alergies, 
            Boolean isGuest, Boolean isAdmitted)
        {
            Id = id;
            Name = name;
            Surname = surname;
            Gender = gender;
            BirthDate = birthDate;
            Phone = phone;
            Email = email;
            Education = education;
            Relationship = relationship;
            Employer = employer;
            Password = password;
            Address = address;
            Alergies = alergies;
            IsGuest = isGuest;
            IsAdmitted = isAdmitted;
        }

        public PatientDTO()
        { 
        }
        protected override void ValidateSelf()
        {
            if (string.IsNullOrWhiteSpace(this.Name) || this.Name.Any(char.IsDigit))
            {
                this.ValidationErrors["Name"] = "Polje može sadržati samo slova.";
            }
            if (string.IsNullOrWhiteSpace(this.Surname) || this.Surname.Any(char.IsDigit))
            {
                this.ValidationErrors["Surname"] = "Polje može sadržati samo slova.";
            }
            if (string.IsNullOrWhiteSpace(this.Gender))
            {
                this.ValidationErrors["Gender"] = "Polje mora biti popunjeno.";
            }
            if (string.IsNullOrWhiteSpace(this.BloodType))
            {
                this.ValidationErrors["BloodType"] = "Polje mora biti popunjeno.";
            }
            if(this.Phone != null)
            {
                if (this.Phone.Any(char.IsLetter))
                {
                    this.ValidationErrors["Phone"] = "Polje ne sme sadržati slova.";
                }
            }
            
        }
    }
}
