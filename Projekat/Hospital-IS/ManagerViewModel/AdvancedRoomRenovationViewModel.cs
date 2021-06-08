using Controllers;
using Enums;
using Hospital_IS.Controllers;
using Hospital_IS.ManagerView1;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Navigation;

namespace Hospital_IS.ManagerViewModel
{
    public class AdvancedRoomRenovationViewModel:ViewModel
    {
        private bool isMerge;
        private bool isSplit;
        private bool isComboEnabledOne = false;
        private bool isComboEnabledTwo = false;
        private Room selectedRoomFirst;
        private Room selectedRoomSecond;
        private ObservableCollection<Room> roomsComboFirst;
        private ObservableCollection<Room> roomsComboSecond;
        private ObservableCollection<Appointment> allAppointments;
        private DateTime dateStart = DateTime.Now;
        private DateTime dateEnd = DateTime.Now;
        private string note;
        private int selectedRoomTypeIndex;
        private RelayCommand makeAdvacedRenovation;
        private RelayCommand openRenovationWindow;
        private RelayCommand navigateToPreviousPage;
        private int roomNumber;
        private NavigationService navService;



        public RelayCommand NavigateToPreviousPage
        {
            get { return navigateToPreviousPage; }
            set
            {
                navigateToPreviousPage = value;
            }
        }

        public int RoomNumber
        {
            get
            {
                return roomNumber;
            }
            set
            {
                if (value != roomNumber)
                {

                    roomNumber = value;
                    OnPropertyChanged("RoomNumber");
                }
            }
        }

        public int SelectedRoomTypeIndex
        {
            get
            {
                return selectedRoomTypeIndex;
            }
            set
            {
                if (value != selectedRoomTypeIndex)
                {
                    selectedRoomTypeIndex = value;
                    OnPropertyChanged("SelectedRoomTypeIndex");
                }
            }
        }



        public RelayCommand MakeAdvacedRenovation
        {
            get { return makeAdvacedRenovation; }
            set
            {
                makeAdvacedRenovation = value;
            }
        }

        public RelayCommand OpenMakeNewWindow
        {
            get { return openRenovationWindow; }
            set
            {
                openRenovationWindow = value;
            }
        }

        public ObservableCollection<Appointment> AllAppointments
        {
            get
            {
                return allAppointments;
            }
            set
            {
                if (value != allAppointments)
                {

                    allAppointments = value;
                    OnPropertyChanged("RoomsComboFirst");
                }
            }
        }


        public String Note
        {
            get
            {
                return note;
            }
            set
            {
                if (value != note)
                {

                    note = value;
                    OnPropertyChanged("DateStart");
                }
            }
        }

        public DateTime DateStart
        {
            get
            {
                return dateStart;
            }
            set
            {
                if (value != dateStart)
                {

                    dateStart = value;
                    OnPropertyChanged("DateStart");
                }
            }
        }


        public DateTime DateEnd
        {
            get
            {
                return dateEnd;
            }
            set
            {
                if (value != dateEnd)
                {

                    dateEnd = value;
                    OnPropertyChanged("DateEnd");
                }
            }
        }

        public Room SelectedRoomFirst
        {
            get
            {
                return selectedRoomFirst;
            }
            set
            {
                if (value != selectedRoomFirst)
                {

                    selectedRoomFirst = value;
                    OnPropertyChanged("SelectedRoomFirst");
                }
            }
        }

        public Room SelectedRoomSecond
        {
            get
            {
                return selectedRoomSecond;
            }
            set
            {
                if (value != selectedRoomSecond)
                {

                    selectedRoomSecond = value;
                    OnPropertyChanged("SelectedRoomSecond");
                }
            }
        }

        public Boolean IsMerge
        {
            get
            {
                return isMerge;
            }
            set
            {
                if (value != isMerge)
                {               
                    isMerge = value;
                    if(isMerge == true)
                    {
                        IsSplit = false;
                        IsComboEnabledTwo = true;

                        IsComboEnabledOne = true;
                    }
                    else if (IsMerge = false && IsSplit == false)
                    {
                        IsComboEnabledTwo = false;

                        IsComboEnabledOne = false;
                    }
                   
                    OnPropertyChanged("isMerge");
                  
                }
            }
        }
        public Boolean IsSplit
        {
            get
            {
                return isSplit;
            }
            set
            {
                if (value != isSplit)
                {
                    isSplit = value;


                    IsMerge = false;
                    if (isSplit == true)
                    {
                        IsComboEnabledOne = true;
                        IsComboEnabledTwo = false;
                    }
                    else if (IsMerge = false && IsSplit == false)
                    {
                        IsComboEnabledTwo = false;

                        IsComboEnabledOne = false;
                    }
                    OnPropertyChanged("isSplit");

                }
            }
        }

        public Boolean IsComboEnabledOne
        {
            get
            {
                return isComboEnabledOne;
            }
            set
            {
                if (value != isComboEnabledOne)
                {

                    isComboEnabledOne = value;
                  
                    OnPropertyChanged("IsComboEnabledOne");
                   
                }
            }
        }

        public Boolean IsComboEnabledTwo
        {
            get
            {
                return isComboEnabledTwo;
            }
            set
            {
                if (value != isComboEnabledTwo)
                {

                    isComboEnabledTwo = value;
                    OnPropertyChanged("IsComboEnabledTwo");
                }
            }
        }




        public ObservableCollection<Room> RoomsComboFirst
        {
            get
            {
                return roomsComboFirst;
            }
            set
            {
                if (value != roomsComboFirst)
                {

                    roomsComboFirst = value;
                    OnPropertyChanged("RoomsComboFirst");
                }
            }
        }

        public ObservableCollection<Room> RoomsComboSecond
        {
            get
            {
                return roomsComboSecond;
            }
            set
            {
                if (value != roomsComboSecond)
                {

                    roomsComboSecond = value;
                    OnPropertyChanged("RoomsComboSecond");
                }
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

        private static AdvancedRoomRenovationViewModel instance = null;
        public static AdvancedRoomRenovationViewModel Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new AdvancedRoomRenovationViewModel();
                }
                return instance;
            }
        }
        private AdvancedRoomRenovationViewModel()
        {
            this.RoomsComboFirst = new ObservableCollection<Room>(RoomController.Instance.GetAllRooms());
            this.RoomsComboSecond = new ObservableCollection<Room>(RoomController.Instance.GetAllRooms());
            this.MakeAdvacedRenovation = new RelayCommand(Execute_AdvancedRenovationCommand, CanExecute_AdancedRenovationCommand);
            this.OpenMakeNewWindow = new RelayCommand(Execute_OpenWinodowCommand, CanExecute_OpenWindowCommand);
            this.NavigateToPreviousPage = new RelayCommand(Execute_NavigateToPreviousPage);
        }

        private void Execute_AdvancedRenovationCommand(object obj)
        {
            if (IsSplit == true)
            {
                Appointment renovationAppointment = new Appointment(true, Note, DateStart, DateEnd, AppointmentType.Renovation, SelectedRoomFirst.RoomId);
                bool isSuccess = AppointmentController.Instance.MakeRenovationAppointment(renovationAppointment);              
                if (isSuccess)
                {
                    MessageBox.Show("Dodavanje uspjesno");
                    RoomType roomType = (RoomType)SelectedRoomTypeIndex;
                    Room room = new Room(SelectedRoomFirst.RoomFloor, RoomNumber, SelectedRoomFirst.SurfaceArea / 2, roomType, new List<Equipment>());
                    AdvancedRenovation advancedRenovation = new AdvancedRenovation(SelectedRoomFirst, null, room, IsSplit, IsMerge, DateEnd);
                    advancedRenovation.isMade = false;
                    AdvancedRenovationController.Instance.MakeAdvancedRenovation(advancedRenovation);
                }
            }
            else
            {
                if(IsMerge == true)
                {
                    if (SelectedRoomFirst.RoomFloor != SelectedRoomSecond.RoomFloor)
                    {
                        Appointment appointmentFirstRoom = new Appointment(DateStart, DateEnd, AppointmentType.Renovation, SelectedRoomFirst.RoomId);
                        Appointment appointmentSecondRoom = new Appointment(DateStart, DateEnd, AppointmentType.Renovation, SelectedRoomSecond.RoomId);
                        bool isSucces = AppointmentController.Instance.MakeRenovationAppointmentForRoomMerge(appointmentFirstRoom, appointmentSecondRoom);
                        if (isSucces)
                        {
                            RoomType roomType = (RoomType)SelectedRoomTypeIndex;
                            int surfaceArea = SelectedRoomFirst.SurfaceArea + SelectedRoomSecond.SurfaceArea;
                            Room room = new Room(SelectedRoomFirst.RoomFloor, RoomNumber, surfaceArea, roomType, new List<Equipment>());
                            AdvancedRenovation advancedRenovation = new AdvancedRenovation(SelectedRoomFirst, SelectedRoomSecond, room, IsSplit, IsMerge, DateEnd);
                            AdvancedRenovationController.Instance.MakeAdvancedRenovation(advancedRenovation);

                        }
                        MessageBox.Show("Sobe treba da budu na istom spratu");
                    }
                }
            }

        }


        private void Execute_NavigateToPreviousPage(object obj)
        {
            this.NavService.GoBack();
        }
        private void Execute_OpenWinodowCommand(object obj)
        {
            RenovationWindow renovation = new RenovationWindow();
            renovation.Show();
        }

        private bool CanExecute_AdancedRenovationCommand(object obj)
        {
            return true;
        }


        private bool CanExecute_OpenWindowCommand(object obj)
        {
            
            if(IsMerge == true)
            {
                if (SelectedRoomFirst == null || SelectedRoomSecond == null)
                {
                    return false;
                }
                else if (SelectedRoomFirst.RoomId == SelectedRoomSecond.RoomId || CheckIfDateIsValid())
                {

                    return false;
                }
                else
                {
                    return true;
                }
            }
            else if(IsSplit == true)
            {
                if (SelectedRoomFirst == null)
                {
                    return false;
                }
                else if (CheckIfDateIsValid())
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }

        }

        private bool CheckIfDateIsValid()
        {
           
            return  DateStart < DateTime.Now || DateEnd < DateTime.Now || DateStart >= DateEnd;
        }



    }
}
