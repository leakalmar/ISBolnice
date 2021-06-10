using Controllers;
using DTOs;
using Enums;
using Hospital_IS.DoctorRole.Commands;
using Hospital_IS.DTOs;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

//MVVM
namespace Hospital_IS.DoctorViewModel
{
    public class HospitalizationsViewModel : BindableBase
    {
        #region Fields
        private bool newHospitalization;
        private bool hospitalized;
        private Patient patient;

        private DateTime addmissionDate;
        private DateTime releaseDate;

        private List<Room> rooms;
        private Room selectedRoom;

        private List<Bed> beds;
        private Bed selectedBed;

        private string doctor;
        private string details;

        private ObservableCollection<Hospitalization> hospitalizations;
        private Hospitalization selectedHospitalization;
        private bool started;

        public bool Started
        {
            get { return started; }
            set
            {
                started = value;
                OnPropertyChanged("Started");
            }
        }

        public bool NewHospitalization
        {
            get { return newHospitalization; }
            set
            {
                newHospitalization = value;
                OnPropertyChanged("NewHospitalization");
            }
        }

        public bool Hospitalized
        {
            get { return hospitalized; }
            set
            {
                hospitalized = value;
                SetFeilds();
                OnPropertyChanged("Hospitalized");
            }
        }

        public Patient Patient
        {
            get { return patient; }
            set
            {
                patient = value;
                this.Hospitalizations = new ObservableCollection<Hospitalization>(ChartController.Instance.GetHospitalizationsByPatient(Patient));
                this.Hospitalized = patient.Admitted;
                OnPropertyChanged("Patient");
            }
        }

        public ObservableCollection<Hospitalization> Hospitalizations
        {
            get { return hospitalizations; }
            set
            {
                hospitalizations = value;
                OnPropertyChanged("Hospitalizations");
            }
        }

        public Hospitalization SelectedHospitalization
        {
            get { return selectedHospitalization; }
            set
            {
                selectedHospitalization = value;
                OnPropertyChanged("SelectedHospitalization");
            }
        }

        public DateTime AddmissionDate
        {
            get { return addmissionDate; }
            set
            {
                addmissionDate = value;
                OnPropertyChanged("AddmissionDate");
            }
        }

        public DateTime ReleaseDate
        {
            get { return releaseDate; }
            set
            {
                if(value <= DateTime.Now)
                {
                    releaseDate = DateTime.Now.AddDays(1);
                }
                else
                {
                    releaseDate = value;
                }
                OnPropertyChanged("ReleaseDate");
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
                Beds = BedController.Instance.GetBedsByRoomId(selectedRoom.Id);
                OnPropertyChanged("SelectedRoom");
            }
        }

        public List<Bed> Beds
        {
            get { return beds; }
            set
            {
                if (!NewHospitalization)
                {
                    beds = BedController.Instance.GetBedsByRoomId(SelectedRoom.Id);
                }
                else
                {
                    beds = CheckIfTaken(value);

                    if (beds.Count != 0)
                    {
                        SelectedBed = beds[0];
                    }
                }
                
                OnPropertyChanged("Beds");
            }
        }

        public Bed SelectedBed
        {
            get { return selectedBed; }
            set
            {
                selectedBed = value;

                OnPropertyChanged("SelectedBed");
            }
        }

        public string Doctor
        {
            get { return doctor; }
            set
            {
                doctor = value;
                OnPropertyChanged("Doctor");
            }
        }

        public string Details
        {
            get { return details; }
            set
            {
                details = value;
                OnPropertyChanged("Details");
            }
        }

        #endregion

        #region Commands
        private RelayCommand hospitalizePatientCommand;
        private RelayCommand endCreatingHospitalizationCommand;
        private RelayCommand releasePatientCommand;

        public RelayCommand HospitalizePatientCommand
        {
            get { return hospitalizePatientCommand; }
            set { hospitalizePatientCommand = value; }
        }
        public RelayCommand EndCreatingHospitalizationCommand
        {
            get { return endCreatingHospitalizationCommand; }
            set { endCreatingHospitalizationCommand = value; }
        }
        public RelayCommand ReleasePatientCommand
        {
            get { return releasePatientCommand; }
            set { releasePatientCommand = value; }
        }
        #endregion

        #region Actions
        private void Execute_HospitalizePatientCommand(object obj)
        {
            NewHospitalization = true;
            Hospitalized = true;
            SelectedRoom = Rooms[0];
            AddmissionDate = DateTime.Now;
            ReleaseDate = DateTime.Now.AddDays(3);
            Doctor = DoctorMainWindowModel.Instance.Doctor.Name + " " + ShortSurname(DoctorMainWindowModel.Instance.Doctor);
        }

        private void Execute_EndCreatingHospitalizationCommand(object obj)
        {
            Hospitalized = true;
            NewHospitalization = false;
            Patient.Admitted = true;
            PatientController.Instance.UpdatePatient(Patient);
            SelectedBed.Taken = true;
            BedController.Instance.UpdateBed(SelectedBed);
            HospitalizationDTO hospitalizationDTO = new HospitalizationDTO(AddmissionDate, ReleaseDate, Details, SelectedRoom.Id, SelectedBed.BedId, Doctor, false);
            ChartController.Instance.AddHospitalization(hospitalizationDTO, Patient);
            this.Hospitalizations = new ObservableCollection<Hospitalization>(ChartController.Instance.GetHospitalizationsByPatient(Patient));
        }

        private void Execute_ReleasePatientCommand(object obj)
        {
            Hospitalized = false;
            Patient.Admitted = false;
            PatientController.Instance.UpdatePatient(Patient);
            SelectedBed.Taken = false;
            BedController.Instance.UpdateBed(SelectedBed);
            ChartController.Instance.ReleasePatient(Patient);
        }

        private bool CanExecute_Command(object obj)
        {
            return true;
        }
        #endregion

        #region Methods
        private string ShortSurname(DoctorDTO doctor)
        {
            String newSurname = doctor.Surname;
            if (doctor.Surname.Length > 12)
            {
                String[] surnames = doctor.Surname.Split(" ");
                newSurname = surnames[0].ToCharArray()[0].ToString() + ". " + surnames[1];
            }
            return newSurname;
        }

        private static List<Bed> CheckIfTaken(List<Bed> value)
        {
            List<Bed> listBeds = new List<Bed>();
            foreach (Bed bed in value)
            {
                if (bed.Taken.Equals(false))
                {
                    listBeds.Add(bed);
                }
            }

            return listBeds;
        }


        private void SetFeilds()
        {
            if (hospitalized == true && NewHospitalization == false)
            {
                Hospitalization hospitalization = ChartController.Instance.GetActivHospitalization(Patient);
                AddmissionDate = hospitalization.AdmissionDate;
                ReleaseDate = hospitalization.ReleaseDate;
                SelectedRoom = RoomController.Instance.GetRoomById(hospitalization.Room.Id);
                SelectedBed = BedController.Instance.GetBedById(hospitalization.Bed.BedId);
                Doctor = hospitalization.Doctor;
            }
        }
        #endregion

        #region Constructor
        public HospitalizationsViewModel()
        {
            this.HospitalizePatientCommand = new RelayCommand(Execute_HospitalizePatientCommand, CanExecute_Command);
            this.EndCreatingHospitalizationCommand = new RelayCommand(Execute_EndCreatingHospitalizationCommand, CanExecute_Command);
            this.ReleasePatientCommand = new RelayCommand(Execute_ReleasePatientCommand, CanExecute_Command);
            this.Rooms = RoomController.Instance.GetRoomByType(RoomType.RecoveryRoom);
            this.Patient = PatientController.Instance.GetPatientByID(PatientChartViewModel.Instance.Patient.Id);
            this.NewHospitalization = false;
        }
        #endregion
    }
}
