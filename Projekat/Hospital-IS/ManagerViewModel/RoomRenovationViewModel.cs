using Controllers;
using Enums;
using Microsoft.Windows.Controls;
using Model;
using System;
using System.Collections.ObjectModel;
using System.Windows.Navigation;

namespace Hospital_IS.ManagerViewModel
{
    public class RoomRenovationViewModel:ViewModel
    {

        private DateTime dateStart = DateTime.Now;
        private DateTime dateEnd = DateTime.Now;
        private Equipment selectedEquipment;
        private string note;
        private NavigationService navService;
        private ObservableCollection<Appointment> appointments;
        private RelayCommand transferStaticEquipmentCommand;
        private RelayCommand navigateToPreviousPage;
        private Room firstRoom;
        private Equipment equipment;
        public int Quantity { get; set; }



        public Equipment Equipment
        {
            get
            {
                return equipment;
            }
            set
            {
                if (value != equipment)
                {

                    equipment = value;
                    OnPropertyChanged("Equipment");

                }
            }
        }



        public Room FirstRoom
        {
            get
            {
                return firstRoom;
            }
            set
            {
                if (value != firstRoom)
                {

                    firstRoom = value;
                    OnPropertyChanged("FirstRoom");

                }
            }
        }

        public RelayCommand NavigateToPreviousPagel
        {
            get { return navigateToPreviousPage; }
            set
            {
                navigateToPreviousPage = value;
            }
        }

        public RelayCommand TransferStaticEquipmentCommand
        {
            get { return transferStaticEquipmentCommand; }
            set
            {
                transferStaticEquipmentCommand = value;
            }
        }
        public ObservableCollection<Appointment> Appointments
        {
            get
            {
                return appointments;
            }
            set
            {
                if (value != appointments)
                {

                    appointments = value;
                    OnPropertyChanged("Appointments");

                }
            }
        }

        public DateTime DateStart
        {
            get { return dateStart; }
            set
            {

                dateStart = value;

                OnPropertyChanged("DateStart");
            }
        }

        public DateTime DateEnd
        {
            get { return dateEnd; }
            set
            {
                dateEnd = value;

                OnPropertyChanged("DateEnd");
            }
        }

        public String Note
        {
            get { return note; }
            set
            {
                note = value;

                OnPropertyChanged("Note");
            }
        }

        public Equipment SelectedEquipment
        {
            get { return selectedEquipment; }
            set
            {
                selectedEquipment = value;

                OnPropertyChanged("Note");
            }
        }

        public NavigationService NavService
        {
            get { return navService; }
            set
            {
                navService = value;
            }
        }

        private static RoomRenovationViewModel instance = null;
        public static RoomRenovationViewModel Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new RoomRenovationViewModel();
                }
                return instance;
            }
        }
        private RoomRenovationViewModel()
        {
            this.TransferStaticEquipmentCommand = new RelayCommand(Execute_RoomRenovation, CanExecute_IfEveryhingIsCorecct);
            this.NavigateToPreviousPagel = new RelayCommand(Execute_NavigateToPreviousPage);
        }

        private void Execute_NavigateToPreviousPage(object obj)
        {
            this.NavService.GoBack();
        }

        public void SetAppointmnetForRoom(int roomIdSource)
        {
            Appointments = new ObservableCollection<Appointment>(AppointmentController.Instance.GetAppByRoomId(roomIdSource));
        }

        private bool CanExecute_IfEveryhingIsCorecct(object obj)
        {
            DateTimePicker dateTimePicker = (DateTimePicker)obj;
            if (DateStart < DateTime.Now || DateEnd < DateTime.Now || DateStart >= DateEnd || dateTimePicker.Text.Length == 0)
            {
                return false;
            }
            return true;
        }

        private void Execute_RoomRenovation(object obj)
        {
           

        }

        public void DoRoomRenovation()
        {
            Appointment renovationAppointment = new Appointment(true, Note, DateStart, DateEnd, AppointmentType.Renovation, FirstRoom.Id);
            bool isSuccces = AppointmentController.Instance.MakeRenovationAppointment(renovationAppointment);
            Appointments = new ObservableCollection<Appointment>(AppointmentController.Instance.GetAppByRoomId(FirstRoom.Id));
            if (!isSuccces)
            {
                System.Windows.MessageBox.Show("Niste uspjeli da zakazete termin");
            }
            else
            {
                System.Windows.MessageBox.Show("Uspjesno zakazivanje termina");
            }
        }

    }
}

