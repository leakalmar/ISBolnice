using Controllers;
using Enums;
using Hospital_IS.Controllers;
using Hospital_IS.DoctorRole.Commands;
using Hospital_IS.DoctorRole.DoctorView;
using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Navigation;

namespace Hospital_IS.DoctorViewModel
{
    public class AppointmentsViewModel : BindableBase
    {
        #region Fields
        private List<Room> rooms;
        private Room selectedRoom;

        private ICollectionView appointments;
        private DoctorAppointment selectedAppointment;

        public List<string> Types { get; set; }
        private string selectedType;

        private DateTime fromDate;
        private DateTime toDate;

        private bool showChangePanel;
        private NavigationService insideNavigation;
        private ChangeApp changeAppView;

        private static AppointmentsViewModel instance;

        public static AppointmentsViewModel Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new AppointmentsViewModel();
                }
                return instance;
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

        public DoctorAppointment SelectedAppointment
        {
            get { return selectedAppointment; }
            set
            {
                selectedAppointment = value;
                OnPropertyChanged("SelectedAppointments");
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
                fromDate = value;
                ToDate = fromDate.AddDays(7);
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
                OnPropertyChanged("ToDate");
                FilterAppointments();
            }
        }

        public bool ShowChangePanel
        {
            get { return showChangePanel; }
            set
            {
                if (ShowChangePanel == true && value == false)
                {
                    FromDate = FromDate;
                    SelectedRoom = SelectedRoom;
                    SelectedType = SelectedType;
                    DoctorMainWindowModel.Instance.NavigateToAppointmentsCommand.Execute(null);
                }
                showChangePanel = value;
                OnPropertyChanged("ShowChangePanel");
                FilterAppointments();
            }
        }

        public NavigationService InsideNavigation
        {
            get { return insideNavigation; }
            set { insideNavigation = value; }
        }

        public ChangeApp ChangeAppView
        {
            get { return changeAppView; }
            set { changeAppView = value; }
        }
        #endregion

        #region Commands
        private RelayCommand chooseAppointmentCommand;
        private RelayCommand deleteAppointmentCommand;

        public RelayCommand ChooseAppointmentCommand
        {
            get { return chooseAppointmentCommand; }
            set { chooseAppointmentCommand = value; }
        }

        public RelayCommand DeleteAppointmentCommand
        {
            get { return deleteAppointmentCommand; }
            set { deleteAppointmentCommand = value; }
        }
        #endregion

        #region Actions

        private void Execute_ChooseAppointmentCommand(object obj)
        {
            if(SelectedAppointment != null)
            {
                if (SelectedAppointment.AppointmentStart > DateTime.Now.AddDays(3))
                {
                    DoctorInsideNavigationController.Instance.NavigateToChangeAppointment(this);
                }
                else if (SelectedAppointment.AppointmentStart < DateTime.Now)
                {
                    new ExitMess("Odabrali ste termin koji je završen.", "info").ShowDialog();
                }
                else
                {
                    new ExitMess("Termin nije moguće odložiti tri dana pre održavanja.", "info").ShowDialog();
                }
            }
            else
            {
                new ExitMess("Morate odabrati termin koji želite da otkažete.", "info").ShowDialog();
            }
            
        }

        private void Execute_DeleteAppointmentCommand(object obj)
        {
            if (SelectedAppointment == null)
            {
                bool dialog = (bool)new ExitMess("Morate odabrati termin koji želite da otkažete.", "info").ShowDialog();
            }
            else
            {
                if (SelectedAppointment.AppointmentStart > DateTime.Now.AddDays(3))
                {
                    bool dialog = (bool)new ExitMess("Da li želite da otkažete termin?", "yesNo").ShowDialog();
                    if (dialog)
                    {
                        SendCanceledNotification(SelectedAppointment);
                        DoctorAppointmentController.Instance.RemoveAppointment(SelectedAppointment);
                        //mislim da nece trebati
                        // DoctorMainWindowModel.Instance.DoctorAppointments.Remove(app);
                    }
                }
                else if(SelectedAppointment.AppointmentStart < DateTime.Now)
                {
                    new ExitMess("Odabrali ste termin koji je završen.", "info").ShowDialog();
                }
                else
                {
                    new ExitMess("Termin nije moguće otkazati tri dana pre održavanja.", "info").ShowDialog();
                }
            }
        }

        private bool CanExecute_Command(object obj)
        {
            return true;
        }
        #endregion

        #region Methods
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
            if (SelectedType != null && SelectedRoom != null && FromDate != null && ToDate != null && ShowChangePanel == false)
            {
                List<DoctorAppointment> app = DoctorAppointmentController.Instance.GetAllByDoctor(DoctorMainWindowModel.Instance.Doctor.Id);
                ICollectionView view = new CollectionViewSource { Source = app }.View;
                view.Filter = null;
                view.Filter = delegate (object item)
                {
                    return ((DoctorAppointment)item).Type.Equals(FindType()) & ((DoctorAppointment)item).Room == SelectedRoom.Id & ((DoctorAppointment)item).AppointmentStart.Date <= ToDate.Date & ((DoctorAppointment)item).AppointmentStart.Date >= FromDate.Date;
                };

                Appointments = view;
            }
        }

        private void InitializeFilters()
        {
            ShowChangePanel = false;
            Types = new List<string>();
            Types.Add("Pregled");
            Types.Add("Operacija");
            SelectedType = "Pregled";
            FromDate = DateTime.Now;
            ToDate = DateTime.Now.AddDays(7);
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

        private void SendCanceledNotification(DoctorAppointment appointment)
        {
            string title = "Otkazan pregled";

            string text = "Pregled koji Vam je bio zakazan " + appointment.AppointmentStart.ToString("dd.MM.yyyy.") + " u "
                + appointment.AppointmentStart.ToString("HH:mm") + "h je otkazan usled nepredviđenih okolnosti.";
            ;

            Notification notification = new Notification(title, text, DateTime.Now);
            notification.Recipients.Add(appointment.Patient.Id);
            notification.Recipients.Add(appointment.Doctor.Id);

            NotificationController.Instance.AddNotification(notification);
        }
        #endregion

        #region Constructor
        private AppointmentsViewModel()
        {
            InitializeFilters();
            ChangeAppView = new ChangeApp();
            this.ChooseAppointmentCommand = new RelayCommand(Execute_ChooseAppointmentCommand, CanExecute_Command);
            this.DeleteAppointmentCommand = new RelayCommand(Execute_DeleteAppointmentCommand, CanExecute_Command);

        }
        #endregion
    }
}
