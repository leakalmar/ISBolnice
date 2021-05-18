using Controllers;
using Hospital_IS.DTOs;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;

namespace Hospital_IS.View.PatientViewModels
{
    public class AppointmentPatientViewModel : BindableBase
    {
        public ObservableCollection<DoctorAppointment> AvailableAppointments { get; set; }
        public ObservableCollection<Doctor> Doctors { get; set; }
        private Doctor doctor;
        private DateTime date;
        private bool timePriority;
        private int timeSlot;
        private DoctorAppointment doctorApp;

        public MyICommand ShowAvailableAppointments { get; set; }
        public MyICommand ReserveAppointment { get; set; }
        public MyICommand RescheduleAppointment { get; set; }

        public AppointmentPatientViewModel()
        {
            AvailableAppointments = new ObservableCollection<DoctorAppointment>();
            Doctors = new ObservableCollection<Doctor>(DoctorController.Instance.GetAll());
            ShowAvailableAppointments = new MyICommand(ShowAvailableApp);
            ReserveAppointment = new MyICommand(ReserveApp);
            RescheduleAppointment = new MyICommand(RescheduleApp);
            Date = DateTime.Today.Date;
            Doctor = Doctors[0];
        }

        public Doctor Doctor
        {
            get { return doctor; }
            set
            {
                if (doctor != value)
                {
                    doctor = value;
                    OnPropertyChanged("Doctors");
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
                    OnPropertyChanged("Calendar");
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
            MessageBox.Show(TimeSlot.ToString());
            PossibleAppointmentForPatientDTO possibleAppointment = new PossibleAppointmentForPatientDTO(TimeSlot.ToString(), Doctor, PatientMainWindowView.Instance.Patient, date, TimePriority);
            List<DoctorAppointment> docApps = DoctorAppointmentController.Instance.SuggestAppointmentsToPatient(possibleAppointment);
            MessageBox.Show(docApps.Count.ToString());
            foreach (DoctorAppointment doctorAppointment in docApps)
            {
                AvailableAppointments.Add(doctorAppointment);
            }
        }

        private void ReserveApp()
        {
            if (DoctorApp == null)
            {
                MessageBox.Show("Izaberite termin!");
            }
            else
            {
                if (!PatientController.Instance.IsPatientTroll(PatientMainWindowView.Instance.Patient, DoctorApp))
                {
                    PatientMainWindowView.Instance.DoctorAppointment.Add(DoctorApp);
                    DoctorAppointmentController.Instance.AddAppointment(DoctorApp);
                    DoctorApp.Reserved = true;
                    AvailableAppointments.Remove(DoctorApp);
                }
                else
                {
                    MessageBox.Show("Zbog učestalog zakazivanja ili izmene termina, ne možete zakazati termin!");
                }
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
                if (!PatientController.Instance.IsPatientTroll(HomePatient.Instance.Patient, DoctorApp))
                {
                    DoctorAppointmentController.Instance.UpdateAppointment(HomePatient.Instance.rescheduledApp, DoctorApp);
                    HomePatient.Instance.DoctorAppointment.Remove(HomePatient.Instance.rescheduledApp);
                    HomePatient.Instance.DoctorAppointment.Add(DoctorApp);
                    DoctorApp.Reserved = true;
                    AvailableAppointments.Remove(DoctorApp);
                }
                else
                {
                    MessageBox.Show("Zbog učestalog zakazivanja ili izmene termina, ne možete zakazati termin!");
                }
            }
        }

        public void SetRescheduleAppointmentView(DoctorAppointment docApp)
        {
            Doctor = docApp.Doctor;
            Date = docApp.AppointmentStart;

            AvailableAppointments.Clear();
            PossibleAppointmentForPatientDTO possibleAppointment = new PossibleAppointmentForPatientDTO(TimeSlot.ToString(), Doctor, HomePatient.Instance.Patient, Date, false);
            List<DoctorAppointment> docApps = DoctorAppointmentController.Instance.SuggestAppointmentsToPatient(possibleAppointment);
            foreach (DoctorAppointment doctorAppointment in docApps)
            {
                AvailableAppointments.Add(doctorAppointment);
            }

        }
    }
}
