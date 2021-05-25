using Controllers;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;

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
        private RelayCommand makeAdvacedRenovation;

        public RelayCommand MakeAdvacedRenovation
        {
            get { return makeAdvacedRenovation; }
            set
            {
                makeAdvacedRenovation = value;
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
                   
                    IsComboEnabledTwo = true;
                  
                    IsComboEnabledOne = true;
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

                    IsComboEnabledOne = true;
                    IsComboEnabledTwo = false;
                    isSplit = value;
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
        }







    }
}
