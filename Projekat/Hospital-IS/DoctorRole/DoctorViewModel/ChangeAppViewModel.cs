using Controllers;
using Enums;
using Hospital_IS.DoctorRole.Commands;
using Hospital_IS.DoctorRole.DoctorView;
using Model;
using System;
using System.Collections.Generic;

//MVVM
namespace Hospital_IS.DoctorViewModel
{
    public class ChangeAppViewModel : BindableBase
    {
        #region Fields
        private List<Room> rooms;
        private Room selectedRoom;

        private List<DoctorAppointment> appointments;
        private DoctorAppointment selectedAppointment;

        private List<Doctor> doctors;
        private Doctor selectedDoctor;

        private DateTime selectedDate;
        private DoctorAppointment oldAppointment;

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

        public List<DoctorAppointment> Appointments
        {
            get { return appointments; }
            set
            {
                appointments = value;
                OnPropertyChanged("Appointments");
            }
        }

        public DoctorAppointment SelectedAppointment
        {
            get { return selectedAppointment; }
            set
            {
                selectedAppointment = value;
                OnPropertyChanged("SelectedAppointments");
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

        public DateTime SelectedDate
        {
            get { return selectedDate; }
            set
            {
                selectedDate = value;
                OnPropertyChanged("SelectedDate");
                FilterAppointments();
            }
        }

        public DoctorAppointment OldAppointment
        {
            get { return oldAppointment; }
            set
            {
                oldAppointment = value;
                OnPropertyChanged("OldAppointment");
                SetRooms();
                FilterAppointments();
            }
        }

        #endregion

        #region Commands
        private RelayCommand changeAppointmentCommand;
        private RelayCommand backCommand;

        public RelayCommand ChangeAppointmentCommand
        {
            get { return changeAppointmentCommand; }
            set { changeAppointmentCommand = value; }
        }
        public RelayCommand BackCommand
        {
            get { return backCommand; }
            set { backCommand = value; }
        }
        #endregion

        #region Actions
        private void Execute_ChangeAppointmentCommand(object obj)
        {
            if (SelectedAppointment != null)
            {

                DoctorAppointment newDoctorAppointment = (DoctorAppointment)SelectedAppointment;
                newDoctorAppointment.Reserved = true;
                DoctorAppointmentController.Instance.UpdateAppointment(OldAppointment, newDoctorAppointment);
                AppointmentsViewModel.Instance.ShowChangePanel = false;
                new ExitMess("Termin je uspešno odložen.", "info").ShowDialog();
            }
            else
            {
                new ExitMess("Morate odabrati termin koji želite da pomerite.", "info").ShowDialog();
            }

        }

        private void Execute_BackCommand(object obj)
        {
            AppointmentsViewModel.Instance.ShowChangePanel = false;
        }

        private bool CanExecute_Command(object obj)
        {
            return true;
        }
        #endregion

        #region Methods

        private void FilterAppointments()
        {
            if (SelectedDate != null && SelectedDoctor != null && SelectedRoom != null && OldAppointment != null)
            {
                AppointmentsViewModel.Instance.SelectedAppointment = OldAppointment;
                TimeSpan duration = OldAppointment.AppointmentEnd - OldAppointment.AppointmentStart;
                List<DateTime> dates = new List<DateTime>();
                dates.Add(SelectedDate);
                DoctorAppointment tempAppointment = new DoctorAppointment(dates[0], OldAppointment.Type, false, SelectedRoom.Id, SelectedDoctor, PatientController.Instance.GetPatientByID(OldAppointment.Patient.Id));
                tempAppointment.AppointmentEnd = dates[0].Add(duration);
                tempAppointment.IsUrgent = OldAppointment.IsUrgent;
                List<DoctorAppointment> list = DoctorAppointmentController.Instance.SuggestAppointmetsToDoctor(dates, tempAppointment);
                Appointments = list;
            }
        }

        private void InitializeFilters()
        {
            SelectedDate = DateTime.Now;
            this.Doctors = DoctorController.Instance.GetDoctorsBySpecilty(DoctorMainWindowModel.Instance.Doctor.Specialty);
            foreach (Doctor doctor in Doctors)
            {
                if (doctor.Id.Equals(DoctorMainWindowModel.Instance.Doctor.Id))
                {
                    this.SelectedDoctor = doctor;
                }
            }
        }

        private void SetRooms()
        {
            if(OldAppointment != null)
            {
                if (OldAppointment.Type == AppointmentType.CheckUp)
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
            
        }
        #endregion

        #region Constructor
        public ChangeAppViewModel()
        {
            InitializeFilters();
            this.ChangeAppointmentCommand = new RelayCommand(Execute_ChangeAppointmentCommand, CanExecute_Command);
            this.BackCommand = new RelayCommand(Execute_BackCommand, CanExecute_Command);
        }
        #endregion
    }
}
