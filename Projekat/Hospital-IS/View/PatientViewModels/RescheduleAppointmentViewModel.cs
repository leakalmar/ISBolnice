using Controllers;
using Hospital_IS.DTOs;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace Hospital_IS.View.PatientViewModels
{
    public class RescheduleAppointmentViewModel : BindableBase
    {
        public ObservableCollection<DoctorAppointment> AvailableAppointments { get; set; }
        public ObservableCollection<Doctor> Doctors { get; set; }
        private Doctor doctor;
        private DateTime date;
        private bool timePriority;
        private int timeSlot;
        private DoctorAppointment doctorApp;
        private DoctorAppointment rescheduledApp;

        public MyICommand ShowAvailableAppointments { get; set; }
        public MyICommand ReserveAppointment { get; set; }
        public MyICommand RescheduleAppointment { get; set; }

        public RescheduleAppointmentViewModel()
        {
            AvailableAppointments = new ObservableCollection<DoctorAppointment>();
            Doctors = new ObservableCollection<Doctor>(DoctorController.Instance.GetAll());
            ShowAvailableAppointments = new MyICommand(ShowAvailableApp);
            RescheduleAppointment = new MyICommand(RescheduleApp);
            RescheduledApp = HomePatientViewModel.SelectedAppointment;
            Date = RescheduledApp.AppointmentStart.Date;
            Doctor = RescheduledApp.Doctor;
            if (RescheduledApp.AppointmentStart.Hour < 11)
            {
                TimeSlot = 0;
            }
            else if (RescheduledApp.AppointmentStart.Hour < 14 && RescheduledApp.AppointmentStart.Hour >= 11)
            {
                TimeSlot = 1;
            }
            else if (RescheduledApp.AppointmentStart.Hour < 17 && RescheduledApp.AppointmentStart.Hour >= 14)
            {
                TimeSlot = 2;
            }
            else if (RescheduledApp.AppointmentStart.Hour < 20 && RescheduledApp.AppointmentStart.Hour >= 17)
            {
                TimeSlot = 3;
            }
            PossibleAppointmentForPatientDTO possibleAppointment = new PossibleAppointmentForPatientDTO(TimeSlot.ToString(), Doctor, PatientMainWindowViewModel.Patient, Date, false);
            List<DoctorAppointment> docApps = DoctorAppointmentController.Instance.SuggestAppointmentsToPatient(possibleAppointment);
            foreach (DoctorAppointment doctorApp in docApps)
            {
                AvailableAppointments.Add(doctorApp);
            }
        }

        public DoctorAppointment RescheduledApp
        {
            get { return rescheduledApp; }
            set
            {
                if(rescheduledApp != value)
                {
                    rescheduledApp = value;
                }
            }
        }

        public Doctor Doctor
        {
            get { return doctor; }
            set
            {
                if (doctor != value)
                {
                    doctor = value;
                    OnPropertyChanged("Doctor");
                }
            }
        }

        public DateTime Date
        {
            get { return date; }
            set
            {
                if (date != value)
                {
                    date = value;
                    OnPropertyChanged("Date");
                }
            }
        }

        public bool TimePriority
        {
            get { return timePriority; }
            set
            {
                if (timePriority != value)
                {
                    timePriority = value;
                    OnPropertyChanged("TimePriority");
                }
            }
        }

        public int TimeSlot
        {
            get { return timeSlot; }
            set
            {
                if (timeSlot != value)
                {
                    timeSlot = value;
                    OnPropertyChanged("TimeSlot");
                }
            }
        }

        public DoctorAppointment DoctorApp
        {
            get { return doctorApp; }
            set
            {
                if (doctorApp != value)
                {
                    doctorApp = value;
                    OnPropertyChanged("listOfAppointments");
                }
            }
        }

        private void ShowAvailableApp()
        {
            AvailableAppointments.Clear();
            PossibleAppointmentForPatientDTO possibleAppointment = new PossibleAppointmentForPatientDTO(TimeSlot.ToString(), Doctor, PatientMainWindowViewModel.Patient, date, TimePriority);
            List<DoctorAppointment> docApps = DoctorAppointmentController.Instance.SuggestAppointmentsToPatient(possibleAppointment);
            foreach (DoctorAppointment doctorAppointment in docApps)
            {
                AvailableAppointments.Add(doctorAppointment);
            }
        }

        private void RescheduleApp()
        {
            if (DoctorApp == null)
            {
                MessageBox.Show("Izaberite termin!");
            }
            else
            {
                if (!PatientController.Instance.IsPatientTroll(PatientMainWindowViewModel.Patient, DoctorApp))
                {
                    DoctorAppointmentController.Instance.UpdateAppointment(rescheduledApp, DoctorApp);
                    DoctorApp.Reserved = true;
                    AvailableAppointments.Remove(DoctorApp);
                }
                else
                {
                    MessageBox.Show("Zbog učestalog zakazivanja ili izmene termina, ne možete zakazati termin!");
                }
            }
        }
    }
}
