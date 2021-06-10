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
        //public ObservableCollection<DoctorAppointment> AllAppointments { get; set; }
        private ICollectionView appointments;
        public KeyValuePair<string, int>[] ChartData { get; set; }

        private DoctorAppointment selectedDoctorAppointment;
        private bool shouldShowEvaluate = false;
        private bool shouldShowNote = false;
        private bool chooseItem = false;
        private string date;
        private string doctorName;
        private string appointmentType;
        private int roomId;
        private string details;
        private string searchText;
        public MyICommand ShowEvaluationWindow { get; set; }
        public MyICommand ShowNote { get; set; }
        public MyICommand DoSearch { get; set; }
        private readonly MyWindowFactory windowFactory;

        public AllAppointmentsViewModel()
        {
            ObservableCollection<DoctorAppointment> AllAppointments = new ObservableCollection<DoctorAppointment>(DoctorAppointmentController.Instance.GetAllAppointmentsByPatient(PatientMainWindowViewModel.Patient.Id));
            Appointments = new CollectionViewSource { Source = AllAppointments }.View;
            windowFactory = new WindowProductionFactory();
            ShowEvaluationWindow = new MyICommand(ShowEvaluation);
            ShowNote = new MyICommand(ShowAppNote);
            DoSearch = new MyICommand(Search);
            LoadAppointmentChartData();
        }

        public ICollectionView Appointments
        {
            get { return appointments; }
            set
            {
                appointments = value;
                OnPropertyChanged("Appointments");
            }
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
                    OnPropertyChanged("Id");
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

        public string SearchText
        {
            get { return searchText; }
            set
            {
                if (searchText != value)
                {
                    searchText = value;
                    OnPropertyChanged("SearchText");
                    Search();
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
            windowFactory.CreateAppointmentEvaluation(SelectedDoctorAppointment.Id);
        }

        private void ShowAppNote()
        {
            windowFactory.CreateNote(SelectedDoctorAppointment.Id);
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

        private void Search()
        {
            Appointments.Filter = delegate (object item)
            {
                DoctorAppointment appointment = item as DoctorAppointment;
                return CheckIfAppointmentMeetsSearchCriteria(appointment);
            };
        }

        private bool CheckIfAppointmentMeetsSearchCriteria(DoctorAppointment appointment)
        {
            string[] search = SearchText.ToLower().Split(" ");
            if (SearchText.Equals("Pretraži..."))
                search[0] = string.Empty;

            if (search.Length <= 1)
                return appointment.Doctor.Name.ToLower().Contains(search[0]) | appointment.Doctor.Surname.ToLower().Contains(search[0]) | appointment.AppointmentStart.Date.ToString("dd.MM.yyyy.").Contains(search[0]);
            else
            {
                bool FirstName = true;
                bool LastName = true;
                bool AppointmentDate = true;
                int cnt = 0;

                for (int i = 0; i < search.Length; i++)
                {
                    if (appointment.Doctor.Name.ToLower().Contains(search[i]) && FirstName)
                    {
                        FirstName = false;
                        cnt++;
                        continue;
                    }
                    if (appointment.Doctor.Surname.ToLower().Contains(search[i]) && LastName)
                    {
                        LastName = false;
                        cnt++;
                        continue;
                    }
                    if (appointment.AppointmentStart.Date.ToString("dd.MM.yyyy.").Contains(search[i]) && AppointmentDate)
                    {
                        AppointmentDate = false;
                        cnt++;
                        continue;
                    }
                }

                return cnt == search.Length;
            }

        }
    }
}
