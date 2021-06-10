using Controllers;
using DTOs;
using Enums;
using Hospital_IS.Controllers;
using Hospital_IS.DoctorRole.Commands;
using Hospital_IS.DoctorRole.DoctorConverters;
using Hospital_IS.DTOs;
using Hospital_IS.DTOs.SecretaryDTOs;
using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Data;

//MVVM
namespace Hospital_IS.DoctorViewModel
{
    public class NewAppViewModel : BindableBase
    {
        #region Fields

        private List<Specialty> specializations;
        private Specialty selectedSpecialty;

        private List<Doctor> doctors;
        private Doctor selectedDoctor;

        private List<Room> rooms;
        private Room selectedRoom;

        private ICollectionView emergencyAppointments;
        private SuggestedEmergencyAppDTO selectedEmergencyAppointment;

        private ICollectionView appointments;
        private AppointmentRowDTO selectedAppointment;

        public List<string> Types { get; set; }
        private string selectedType;

        private DateTime fromDate;
        private DateTime toDate;

        private TimeSpan duration;
        private bool emergency;

        private bool focused;

        public bool Focused
        {
            get { return focused; }
            set
            {
                focused = value;
                OnPropertyChanged("Focused");
            }
        }

        public List<Specialty> Specializations
        {
            get { return specializations; }
            set
            {
                specializations = value;
                OnPropertyChanged("Specializations");
            }
        }

        public Specialty SelectedSpecialty
        {
            get { return selectedSpecialty; }
            set
            {
                selectedSpecialty = value;
                SetDoctors();
                SetSelectedDoctor();
                OnPropertyChanged("SelectedSpecialty");
                FilterAppointments();
            }
        }

        public List<Doctor> Doctors
        {
            get { return doctors; }
            set
            {
                doctors = value;
                OnPropertyChanged("Doctors");
            }
        }

        public Doctor SelectedDoctor
        {
            get { return selectedDoctor; }
            set
            {
                selectedDoctor = value;
                OnPropertyChanged("SelectedDoctor");
                FilterAppointments();
            }
        }

        public List<Room> Rooms
        {
            get { return rooms; }
            set
            {
                rooms = value;
                OnPropertyChanged("Rooms");
            }
        }

        public Room SelectedRoom
        {
            get { return selectedRoom; }
            set
            {
                selectedRoom = value;
                OnPropertyChanged("SelectedRoom");
                FilterAppointments();
            }
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

        public AppointmentRowDTO SelectedAppointment
        {
            get { return selectedAppointment; }
            set
            {
                selectedAppointment = value;
                OnPropertyChanged("SelectedAppointments");
            }
        }

        public ICollectionView EmergencyAppointments
        {
            get { return emergencyAppointments; }
            set
            {
                emergencyAppointments = value;
                OnPropertyChanged("EmergencyAppointments");
            }
        }

        public SuggestedEmergencyAppDTO SelectedEmergencyAppointment
        {
            get { return selectedEmergencyAppointment; }
            set
            {
                selectedEmergencyAppointment = value;
                OnPropertyChanged("SelectedEmergencyAppointment");
            }
        }

        public string SelectedType
        {
            get { return selectedType; }
            set
            {
                selectedType = value;
                OnPropertyChanged("SelectedType");
                FilterAppointments();
            }
        }

        public DateTime FromDate
        {
            get { return fromDate; }
            set
            {
                if(value < DateTime.Now)
                {
                    fromDate = DateTime.Now;
                }
                else
                {
                    fromDate = value;
                }
                ToDate = value;
                OnPropertyChanged("FromDate");
                FilterAppointments();
            }
        }

        public DateTime ToDate
        {
            get { return toDate; }
            set
            {
                toDate = value;
                toDate = FromDate.AddDays(1);
                OnPropertyChanged("ToDate");
                FilterAppointments();
            }
        }

        public TimeSpan Duration
        {
            get { return duration; }
            set
            {
                duration = value;
                OnPropertyChanged("ToDate");
                FilterAppointments();
            }
        }

        public bool Emergency
        {
            get { return emergency; }
            set
            {
                emergency = value;
                OnPropertyChanged("Emergency");
                FilterAppointments();
            }
        }


        #endregion

        #region Commands
        private RelayCommand chooseAppointmentCommand;
        private RelayCommand changeEmergencyCommand;

        public RelayCommand ChooseAppointmentCommand
        {
            get { return chooseAppointmentCommand; }
            set { chooseAppointmentCommand = value; }
        }

        public RelayCommand ChangeEmergencyCommand
        {
            get { return changeEmergencyCommand; }
            set { changeEmergencyCommand = value; }
        }
        #endregion

        #region Actions
        private void Execute_ChooseAppointmentCommand(object obj)
        {
            DoctorNavigationController.Instance.NavigateToInstructionCommand(this);
        }

        private void Execute_ChangeEmergencyCommand(object obj)
        {
            this.Emergency = !this.Emergency;
        }


        private bool CanExecute_Command(object obj)
        {
            return true;
        }
        #endregion

        #region Methods
        private void SetSelectedDoctor()
        {
            if (SelectedSpecialty.Equals(DoctorMainWindowModel.Instance.Doctor.Specialty))
            {
                foreach (Doctor doctor in Doctors)
                {
                    if (doctor.Id.Equals(DoctorMainWindowModel.Instance.Doctor.Id))
                    {
                        SelectedDoctor = doctor;
                    }
                }
            }
            else
            {
                if (Doctors.Count != 0)
                {
                    SelectedDoctor = Doctors[0];
                }
                SelectedType = "Pregled";
            }
        }

        private void SetDoctors()
        {
            if (Emergency)
            {
                List<Doctor> doctorList = new List<Doctor>();
                doctorList.Add(new Doctor(-1, "Svi", "doktori", DateTime.Now, null, null, null, 0, DateTime.Now, null, selectedSpecialty, 0));
                doctorList.AddRange(DoctorController.Instance.GetDoctorsBySpecilty(SelectedSpecialty.Name));
                Doctors = doctorList;
            }
            else
            {
                Doctors = DoctorController.Instance.GetDoctorsBySpecilty(SelectedSpecialty.Name);
            }
        }

        private AppointmentType FindType()
        {
            AppointmentType type;
            if (SelectedType.Equals("Pregled"))
            {
                type = AppointmentType.CheckUp;
            }
            else
            {
                type = AppointmentType.Operation;
            }

            return type;
        }
        private void FilterAppointments()
        {
            if (SelectedType != null && SelectedSpecialty != null && SelectedRoom != null && SelectedDoctor != null)
            {
                List<DateTime> dates = GenerateDates();
                PatientDTO patient = PatientChartViewModel.Instance.Patient;

                if (Emergency)
                {
                    GetEmergencyAppointments(dates, SelectedDoctor, patient.Id);
                }
                else
                {
                    GetAppointments(dates, SelectedDoctor, patient.Id);
                }
            }
        }

        private void GetAppointments(List<DateTime> dates, Doctor doctor, int patientId)
        {

            DoctorAppointment tempAppointment = new DoctorAppointment(dates[0], FindType(), false, SelectedRoom.Id, doctor, PatientController.Instance.GetPatientByID(patientId));
            tempAppointment.AppointmentEnd = dates[0].Add(Duration);
            tempAppointment.IsUrgent = Emergency;
            List<DoctorAppointment> list = DoctorAppointmentController.Instance.SuggestAppointmetsToDoctor(dates, tempAppointment);
            ICollectionView view = new CollectionViewSource { Source = new DoctorAppointmentConverter().ConvertExistingAppointmentsToDTO(DoctorAppointmentController.Instance.SuggestAppointmetsToDoctor(dates, tempAppointment)) }.View;
            view.SortDescriptions.Clear();
            view.SortDescriptions.Add(new SortDescription("Appointment.AppointmentStart", ListSortDirection.Ascending));
            Appointments = view;
        }

        private void GetEmergencyAppointments(List<DateTime> dates, Doctor doctor, int patientId)
        {
            PatientDTO patientDTO = SecretaryManagementController.Instance.GetPatientByID(patientId);
            RoomDTO roomDTO = DoctorAppointmentManagementController.Instance.GetRoomById(SelectedRoom.Id);
            EmergencyAppointmentDTO emergencyAppointmentDTO = new EmergencyAppointmentDTO(FindType(), doctor.Specialty.Name, patientDTO, roomDTO, duration.Minutes);
            emergencyAppointmentDTO.RequestedDates = dates;
            List<SuggestedEmergencyAppDTO> allEmergencyAppointments = DoctorAppointmentController.Instance.SuggestEmergencyAppsToDoctor(emergencyAppointmentDTO);
            ICollectionView view = new CollectionViewSource { Source = allEmergencyAppointments }.View;
            if (doctor.Id != -1)
            {
                view.Filter = null;
                view.Filter = delegate (object item)
                {
                    return ((SuggestedEmergencyAppDTO)item).SuggestedAppointment.Doctor.Id == doctor.Id;
                };
            }
            view.SortDescriptions.Add(new SortDescription("SuggestedAppointment.AppointmentStart", ListSortDirection.Ascending));
            view.GroupDescriptions.Add((new PropertyGroupDescription("SuggestedAppointment.Doctor.Surname")));
            
            EmergencyAppointments = view;
        }

        private List<DateTime> GenerateDates()
        {
            List<DateTime> dates = new List<DateTime>();
            dates.Add(FromDate);
            int i = -1;
            do
            {
                i++;
                dates.Add(FromDate.AddDays(i));

            } while (FromDate.AddDays(i).Date != ToDate.Date);
            return dates;
        }

        private void InitializeFilters()
        {
            Types = new List<string>();
            Types.Add("Pregled");
            Types.Add("Operacija");
            SelectedType = "Pregled";
            FromDate = DateTime.Now;
            ToDate = DateTime.Now.AddDays(1);
            Duration = new TimeSpan(0, 15, 0);
            Emergency = false;
            this.Specializations = SpecializationController.Instance.GetAll();
            foreach (Specialty specialty in Specializations)
            {
                if (specialty.Name.Equals(DoctorMainWindowModel.Instance.Doctor.Specialty))
                {
                    this.SelectedSpecialty = specialty;
                }
            }

            this.Doctors = DoctorController.Instance.GetDoctorsBySpecilty(selectedSpecialty.Name);
            foreach (Doctor doctor in Doctors)
            {
                if (doctor.Id.Equals(DoctorMainWindowModel.Instance.Doctor.Id))
                {
                    this.SelectedDoctor = doctor;
                }
            }

            if (FindType() == AppointmentType.CheckUp)
            {
                Rooms = RoomController.Instance.GetRoomByType(RoomType.ConsultingRoom);
            }
            else
            {
                Rooms = RoomController.Instance.GetRoomByType(RoomType.OperationRoom);
            }
            foreach (Room room in Rooms)
            {
                if (room.Id.Equals(DoctorMainWindowModel.Instance.Doctor.PrimaryRoom))
                {
                    this.SelectedRoom = room;
                }
            }
        }

        #endregion

        #region Constructor
        public NewAppViewModel()
        {
            InitializeFilters();
            this.Focused = true;
            this.ChooseAppointmentCommand = new RelayCommand(Execute_ChooseAppointmentCommand, CanExecute_Command);
            this.ChangeEmergencyCommand = new RelayCommand(Execute_ChangeEmergencyCommand, CanExecute_Command);
        }


        #endregion
    }
}
