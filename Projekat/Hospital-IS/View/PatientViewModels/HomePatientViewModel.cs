using Controllers;
using Model;
using System;
using System.Collections.ObjectModel;
using System.Windows;

namespace Hospital_IS.View.PatientViewModels
{
    public class HomePatientViewModel : BindableBase 
    {
        private DoctorAppointment selectedApp;
        private Patient patient;
        private ObservableCollection<DoctorAppointment> futureAppointments;
        public ObservableCollection<DoctorAppointment> FutureAppointments 
        {
            get { return futureAppointments; }
            set
            {
                if (futureAppointments != value)
                {
                    futureAppointments = value;
                    OnPropertyChanged("FutureAppointments");
                }
            }
        }

        private string name;
        private string surname;
        private string birthDate;
        private string address;
        private string fileDate;
        private string employer;
        private string email;

        public MyICommand CancelAppointment { get; set; }
        public MyICommand RescheduleAppointment { get; set; }
        public MyICommand ShowCalendar { get; set; }
        public static DoctorAppointment SelectedAppointment { get; set; }
        public HomePatientViewModel()
        {
            patient = PatientMainWindowViewModel.Patient;
            FutureAppointments = new ObservableCollection<DoctorAppointment>(DoctorAppointmentController.Instance.GetFutureAppointmentsByPatient(PatientMainWindowViewModel.Patient.Id));
            Name = patient.Name;
            Surname = patient.Surname;
            BirthDate = patient.BirthDate.ToString("dd.MM.yyyy.");
            Address = patient.Address;
            FileDate = patient.FileDate.ToString("dd.MM.yyyy.");
            Employer = patient.Employer;
            Email = patient.Email;

            CancelAppointment = new MyICommand(CancelApp);
            RescheduleAppointment = new MyICommand(RescheduleApp);
            ShowCalendar = new MyICommand(ShowCal);
        }

        public DoctorAppointment SelectedApp
        {
            get { return selectedApp; }
            set
            {
                if(selectedApp != value)
                {
                    selectedApp = value;
                    OnPropertyChanged("SelectedApp");
                    SelectedAppointment = selectedApp;
                }
            }
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

        private void CancelApp()
        {
            DateTime today = DateTime.Today;
            int dayLimiter = 3;
            if (SelectedApp == null)
            {
                MessageBox.Show("Izaberite termin!");
            }
            else if (SelectedApp.AppointmentStart.Date < today.AddDays(dayLimiter))
            {
                MessageBox.Show("Ne možete otkazati termin na manje od 3 dana do termina!");
            }
            else
            {
                SelectedApp.Reserved = false;
                DoctorAppointmentController.Instance.RemoveAppointment(SelectedApp);
                FutureAppointments.Remove(SelectedApp);
            }
        }

        private void RescheduleApp()
        {
            if (SelectedApp == null)
            {
                MessageBox.Show("Izaberite termin!");
            }
            else
            {
                RescheduleAppointmentViewModel rescheduleAppointmentViewModel = new RescheduleAppointmentViewModel();
                PatientMainWindowView.Instance.PatientMainView.CurrentViewModel = rescheduleAppointmentViewModel;
            }
        }

        private void ShowCal()
        {
            CalendarAppointmentViewModel calendarViewModel = new CalendarAppointmentViewModel();
            PatientMainWindowView.Instance.PatientMainView.CurrentViewModel = calendarViewModel;
        }
    }
}
