using Controllers;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;

namespace Hospital_IS.View.PatientViewModels
{
    public class AllAppointmentsViewModel : BindableBase
    {
        public ObservableCollection<DoctorAppointment> AllAppointments { get; set; }
        //private ICollectionView allApps;
        public KeyValuePair<string, int>[] ChartData { get; set; }

        private DoctorAppointment selectedDoctorAppointment;
        private bool shouldShowEvaluate = false;
        private bool shouldShowNote = false;
        private bool chooseItem = true;
        private string date;
        private string doctorName;
        private string appointmentType;
        private int roomId;
        private string details;


        public MyICommand ShowEvaluationWindow { get; set; }
        public MyICommand ShowNote { get; set; }

        public AllAppointmentsViewModel()
        {
            /*
            ObservableCollection<DoctorAppointment>  allAppointments = new ObservableCollection<DoctorAppointment>(DoctorAppointmentController.Instance.GetAllAppointmentsByPatient(PatientMainWindowViewModel.Patient.Id));
            AllAppointments = new CollectionViewSource { Source = allAppointments }.View;*/
            AllAppointments = new ObservableCollection<DoctorAppointment>(DoctorAppointmentController.Instance.GetAllAppointmentsByPatient(PatientMainWindowViewModel.Patient.Id));
            ShowEvaluationWindow = new MyICommand(ShowEvaluation);
            ShowNote = new MyICommand(ShowAppNote);
            LoadAppointmentChartData();
        }
        /*
        public ICollectionView AllAppointments
        {
            get { return allApps; }
            set
            {
                allApps = value;
                OnPropertyChanged("Patients");
            }
        }*/

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
                    OnPropertyChanged("DoctorName");
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

        public bool ChooseItem
        {
            get { return chooseItem; }
            set
            {
                if (chooseItem != value)
                {
                    chooseItem = value;
                    OnPropertyChanged("ChooseItem");
                }
            }
        }

        private void SetAppointmentInfo()
        {
            Date = SelectedDoctorAppointment.AppointmentStart.ToString("dd.MM.yyyy.");
            DoctorName = SelectedDoctorAppointment.Doctor.Name;
            AppointmentType = SelectedDoctorAppointment.AppTypeText;
            RoomId = SelectedDoctorAppointment.Room;
            Details = SelectedDoctorAppointment.AppointmentCause;
            ChooseItem = false;
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
            PatientNoteView appointmentNote = new PatientNoteView(SelectedDoctorAppointment.Id);
            appointmentNote.AppointmentNoteViewModel.OnRequestClose += (s, e) => appointmentNote.Close();
            appointmentNote.Show();
        }

        private void LoadAppointmentChartData()
        {
            ChartData =  new KeyValuePair<string, int>[]{
                new KeyValuePair<string,int>("Jan", DoctorAppointmentController.Instance.GetNumberOfAppointmentsByMonth(PatientMainWindowViewModel.Patient.Id, "Jan")),
                new KeyValuePair<string,int>("Feb", DoctorAppointmentController.Instance.GetNumberOfAppointmentsByMonth(PatientMainWindowViewModel.Patient.Id, "Feb")),
                new KeyValuePair<string,int>("Mar", DoctorAppointmentController.Instance.GetNumberOfAppointmentsByMonth(PatientMainWindowViewModel.Patient.Id, "Mar")),
                new KeyValuePair<string,int>("Apr", DoctorAppointmentController.Instance.GetNumberOfAppointmentsByMonth(PatientMainWindowViewModel.Patient.Id, "Apr")),
                new KeyValuePair<string,int>("Maj", DoctorAppointmentController.Instance.GetNumberOfAppointmentsByMonth(PatientMainWindowViewModel.Patient.Id, "May")),
                new KeyValuePair<string,int>("Jun", DoctorAppointmentController.Instance.GetNumberOfAppointmentsByMonth(PatientMainWindowViewModel.Patient.Id, "June"))
            };
        }
    }
}
