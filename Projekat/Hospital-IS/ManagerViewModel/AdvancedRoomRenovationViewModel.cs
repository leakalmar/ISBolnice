using Controllers;
using Enums;
using Hospital_IS.Controllers;
using Hospital_IS.DTOs;
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
        private Room selectedValue;
        private RoomNumberValidation roomValidation = new RoomNumberValidation();
        private RenovationWindow renovationWindow = new RenovationWindow();
        private NavigationService navService;



        public RelayCommand NavigateToPreviousPage
        {
            get { return navigateToPreviousPage; }
            set
            {
                navigateToPreviousPage = value;
            }
        }

        public RoomNumberValidation RoomValidation
        {
            get
            {
                return roomValidation;
            }
            set
            {
                if (value != roomValidation)
                {

                    roomValidation = value;
                    OnPropertyChanged("RoomValidation");
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
                    OnPropertyChanged("AllAppointments");
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

        public Room SelectedValue
        {
            get
            {
                return selectedValue;
            }
            set
            {
                if (value != selectedValue)
                {

                    selectedValue = value;
                    OnPropertyChanged("SelectedValue");
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
                   
                    
                    if (selectedRoomFirst != null)
                    {
                        if (SelectedRoomSecond != null && SelectedRoomFirst != null)
                        {
                            if (SelectedRoomSecond.Id == SelectedRoomFirst.Id)
                            {
                                MessageBox.Show("Izabrali ste istu sobu");
                                return;
                            }
                        }
                        if (isMerge == true && SelectedRoomSecond != null)
                        {
                            AllAppointments = new ObservableCollection<Appointment>(AppointmentController.Instance.GetAllAppByTwoRooms(selectedRoomFirst.Id, SelectedRoomSecond.Id));
                        }
                        else
                        {
                            AllAppointments = new ObservableCollection<Appointment>(AppointmentController.Instance.GetAppByRoomId(selectedRoomFirst.Id));

                        }
                    }
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
                    if (selectedRoomSecond != null)
                    {
                       
                        if (SelectedRoomSecond != null && SelectedRoomFirst != null)
                        {
                            if (SelectedRoomSecond.Id == SelectedRoomFirst.Id)
                            {
                                MessageBox.Show("Izabrali ste istu sobu");
                                ChangeValue();
                                return;
                                
                               
                            }
                        }


                        if (SelectedRoomFirst != null)
                        {
                            AllAppointments = new ObservableCollection<Appointment>(AppointmentController.Instance.GetAllAppByTwoRooms(SelectedRoomFirst.Id, selectedRoomSecond.Id));
                           
                        }
                        else
                        {
                            AllAppointments = new ObservableCollection<Appointment>(AppointmentController.Instance.GetAppByRoomId(selectedRoomSecond.Id));
                        }
                    }
                 
                      
                    
                    OnPropertyChanged("SelectedRoomSecond");
                }
            }
        }

        public void ChangeValue() {
            SelectedRoomSecond = null;
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
                        SelectedRoomFirst = null;
                        SelectedRoomSecond = null;
                        AllAppointments = new ObservableCollection<Appointment>();
                        OnPropertyChanged("isMerge");
                    }
                    else if (IsMerge == false && IsSplit == false)
                    {
                        IsComboEnabledTwo = false;

                        IsComboEnabledOne = false;
                        SelectedRoomFirst = null;
                        SelectedRoomSecond = null;
                        AllAppointments = new ObservableCollection<Appointment>();
                        OnPropertyChanged("isMerge");
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


                    if (isSplit == true)
                    {
                        IsMerge = false;
                        IsComboEnabledOne = true;
                        IsComboEnabledTwo = false;
                        SelectedRoomFirst = null;
                        SelectedRoomSecond = null;
                        AllAppointments = new ObservableCollection<Appointment>();
                        OnPropertyChanged("isSplit");
                    }
                    else if (IsMerge == false && IsSplit == false)
                    {
                        IsComboEnabledTwo = false;

                        IsComboEnabledOne = false;
                        SelectedRoomFirst = null;
                        SelectedRoomSecond = null;
                        AllAppointments = new ObservableCollection<Appointment>();
                        OnPropertyChanged("isSplit");
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
            RoomValidation.Validate();
        
            if (RoomValidation.IsValid)
            {
                
                if (IsSplit == true)
                {
                    Appointment renovationAppointment = new Appointment(true, Note, DateStart, DateEnd, AppointmentType.Renovation, SelectedRoomFirst.Id);
                    bool isSuccess = AppointmentController.Instance.MakeRenovationAppointment(renovationAppointment);
                    if (isSuccess)
                    {
                        MessageBox.Show("Uspjesno zakazivanje razdvajanja");
                        RoomType roomType = (RoomType)SelectedRoomTypeIndex;
                    
                        Room room = new Room(SelectedRoomFirst.RoomFloor, Convert.ToInt32(RoomValidation.RoomNumber), SelectedRoomFirst.SurfaceArea / 2, roomType, new List<Equipment>());
                        AdvancedRenovation advancedRenovation = new AdvancedRenovation(SelectedRoomFirst, null, room, AdvancedRenovationType.SPLIT,false, DateEnd);
                        advancedRenovation.RenovationStart = DateStart;
                        
                        AdvancedRenovationController.Instance.MakeAdvancedRenovation(advancedRenovation);
                        MessageBox.Show("Uspjesno zakazivanje razdvajanja");


                        AllAppointments = new ObservableCollection<Appointment>(AppointmentController.Instance.GetAppByRoomId(SelectedRoomFirst.Id));
                        renovationWindow.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Izaberite drugi termin");
                    }
                }
                else
                {
                    if (IsMerge == true)
                    {
                        MessageBox.Show(SelectedRoomFirst.RoomFloor.ToString() + " " + SelectedRoomSecond.RoomFloor.ToString());
                        if (SelectedRoomFirst.RoomFloor == SelectedRoomSecond.RoomFloor)
                        {
                            Appointment appointmentFirstRoom = new Appointment(DateStart, DateEnd, AppointmentType.Renovation, SelectedRoomFirst.Id);
                            Appointment appointmentSecondRoom = new Appointment(DateStart, DateEnd, AppointmentType.Renovation, SelectedRoomSecond.Id);
                            bool isSucces = AppointmentController.Instance.MakeRenovationAppointmentForRoomMerge(appointmentFirstRoom, appointmentSecondRoom);
                            if (isSucces)
                            {
                                MessageBox.Show("Uspjesno zakazivanje spajanja");
                                RoomType roomType = (RoomType)SelectedRoomTypeIndex;
                                int surfaceArea = SelectedRoomFirst.SurfaceArea + SelectedRoomSecond.SurfaceArea;
                                Room room = new Room(Convert.ToInt32(RoomValidation.RoomNumber),SelectedRoomFirst.RoomFloor, surfaceArea, roomType, new List<Equipment>());
                                AdvancedRenovation advancedRenovation = new AdvancedRenovation(SelectedRoomFirst, SelectedRoomSecond, room, AdvancedRenovationType.MERGE, false, DateEnd);
                                advancedRenovation.RenovationStart = DateStart;
                                AdvancedRenovationController.Instance.MakeAdvancedRenovation(advancedRenovation);                            
                                AllAppointments = new ObservableCollection<Appointment>(AppointmentController.Instance.GetAllAppByTwoRooms(SelectedRoomFirst.Id, SelectedRoomSecond.Id));
                                renovationWindow.Hide();

                            }
                            else
                            {
                                MessageBox.Show("Izaberite drugi termin");
                            }

                        }
                        else
                        {
                            MessageBox.Show("Sobe treba da budu na istom spratu");
                        }
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
          
        }

        public void OpenWindow()
        {
            renovationWindow.DataContext = this;
            renovationWindow.Show();

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
                else if (SelectedRoomFirst.Id == SelectedRoomSecond.Id || CheckIfDateIsValid())
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
