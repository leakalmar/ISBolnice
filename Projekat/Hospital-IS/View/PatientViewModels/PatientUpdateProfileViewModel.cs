using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital_IS.View.PatientViewModels
{
    public class PatientUpdateProfileViewModel : BindableBase
    {
        private Patient patient;
        private string name;
        private string surname;
        private string birthDate;
        private string address;
        private string fileDate;
        private string employer;
        private string email;
        public MyICommand UpdateProfile { get; set; }
        public MyICommand ShowFeedback { get; set; }
        private readonly MyWindowFactory windowFactory;

        public PatientUpdateProfileViewModel()
        {
            patient = PatientMainWindowViewModel.Patient;
            Name = patient.Name;
            Surname = patient.Surname;
            BirthDate = patient.BirthDate.ToString("dd.MM.yyyy.");
            Address = patient.Address;
            FileDate = patient.FileDate.ToString("dd.MM.yyyy.");
            Employer = patient.Employer;
            Email = patient.Email;

            UpdateProfile = new MyICommand(SaveChange);
            ShowFeedback = new MyICommand(Feedback);
            windowFactory = new WindowProductionFactory();
        }

        public string Name
        {
            get { return name; }
            set
            {
                if (name != value)
                {
                    name = value;
                    OnPropertyChanged("Name");
                }
            }
        }

        public string Surname
        {
            get { return surname; }
            set
            {
                if (surname != value)
                {
                    surname = value;
                    OnPropertyChanged("Surname");
                }
            }
        }

        public string BirthDate
        {
            get { return birthDate; }
            set
            {
                if (birthDate != value)
                {
                    birthDate = value;
                    OnPropertyChanged("Birthdate");
                }
            }
        }

        public string Address
        {
            get { return address; }
            set
            {
                if (address != value)
                {
                    address = value;
                    OnPropertyChanged("Address");
                }
            }
        }

        public string FileDate
        {
            get { return fileDate; }
            set
            {
                if (fileDate != value)
                {
                    fileDate = value;
                    OnPropertyChanged("FileDate");
                }
            }
        }

        public string Employer
        {
            get { return employer; }
            set
            {
                if (employer != value)
                {
                    employer = value;
                    OnPropertyChanged("Employer");
                }
            }
        }

        public string Email
        {
            get { return email; }
            set
            {
                if (email != value)
                {
                    email = value;
                    OnPropertyChanged("Email");
                }
            }
        }

        private void SaveChange()
        {

        }

        private void Feedback()
        {
            windowFactory.CreateFeedback();
        }
    }
}
