using Controllers;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;

namespace Hospital_IS.View.PatientViewModels
{
    public class AllAppointmentsViewModel : BindableBase
    {
        public ObservableCollection<DoctorAppointment> AllAppointments { get; set; }

        private DoctorAppointment selectedDoctorAppointment;
        private bool shouldShowEvaluate = false;
        private bool shouldShowNote = false;
        private string date;
        private string doctorName;
        private string appointmentType;
        private int roomId;
        private string details;

        public MyICommand ShowEvaluationWindow { get; set; }
        public MyICommand ShowNote { get; set; }

        public AllAppointmentsViewModel()
        {
            AllAppointments = new ObservableCollection<DoctorAppointment>(DoctorAppointmentController.Instance.GetAllAppointmentsByPatient(PatientMainWindowViewModel.Patient.Id));
            ShowEvaluationWindow = new MyICommand(ShowEvaluation);
            ShowNote = new MyICommand(ShowAppNote);
        }

        public DoctorAppointment SelectedDoctorAppointment
        {
            get { return selectedDoctorAppointment; }
            set
            {
                if (selectedDoctorAppointment != value)
                {
                    selectedDoctorAppointment = value;
                    OnPropertyChanged("SelectedDoctorAppointment");
                    SetAppointmentInfo();
                }
            }
        }

        public string Date
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

        public string DoctorName
        {
            get { return doctorName; }
            set
            {
                if (doctorName != value)
                {
                    doctorName = value;
                    OnPropertyChanged("Doctor");
                }
            }
        }

        public string AppointmentType
        {
            get { return appointmentType; }
            set
            {
                if (appointmentType != value)
                {
                    appointmentType = value;
                    OnPropertyChanged("AppointmentType");
                }
            }
        }

        public int RoomId
        {
            get { return roomId; }
            set
            {
                if (roomId != value)
                {
                    roomId = value;
                    OnPropertyChanged("RoomId");
                }
            }
        }

        public string Details
        {
            get { return details; }
            set
            {
                if (details != value)
                {
                    details = value;
                    OnPropertyChanged("Details");
                }
            }
        }

        public bool ShouldShowEvaluate
        {
            get { return shouldShowEvaluate; }
            set
            {
                if (shouldShowEvaluate != value)
                {
                    shouldShowEvaluate = value;
                    OnPropertyChanged("ShouldShowEvaluate");
                }
            }
        }

        public bool ShouldShowNote
        {
            get { return shouldShowNote; }
            set
            {
                if (shouldShowNote != value)
                {
                    shouldShowNote = value;
                    OnPropertyChanged("ShouldShowNote");
                }
            }
        }

        private void SetAppointmentInfo()
        {
            Date = SelectedDoctorAppointment.AppointmentStart.ToString("dd.MM.yyyy.");
            DoctorName = SelectedDoctorAppointment.Doctor.Name;
            MessageBox.Show(SelectedDoctorAppointment.Doctor.Name);
            AppointmentType = SelectedDoctorAppointment.AppTypeText;
            RoomId = SelectedDoctorAppointment.Room;
            MessageBox.Show(SelectedDoctorAppointment.Room.ToString());
            Details = SelectedDoctorAppointment.AppointmentCause;
            ShouldShowNote = true;
            if (SelectedDoctorAppointment.AppointmentStart <= DateTime.Today && !PatientAppointmentEvaluationController.Instance.IsAppointmentEvaluated(SelectedDoctorAppointment.Id))
            {
                ShouldShowEvaluate = true;
            }
            else
            {
                ShouldShowEvaluate = false;
            }
        }

        private void ShowEvaluation()
        {
            PatientAppointmentEvaluationWindow appointmentEvaluation = new PatientAppointmentEvaluationWindow(SelectedDoctorAppointment.Id);
            appointmentEvaluation.AppointmentEvaluation.OnRequestClose += (s, e) => appointmentEvaluation.Close();
            appointmentEvaluation.Show();
        }

        private void ShowAppNote()
        {
            PatientNoteView appointmentNote = new PatientNoteView();
            appointmentNote.AppointmentNoteViewModel.OnRequestClose += (s, e) => appointmentNote.Close();
            appointmentNote.Show();
        }
    }
}
