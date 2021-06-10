using Controllers;
using Enums;
using Hospital_IS.DoctorRole.Commands;
using Hospital_IS.DoctorRole.DoctorView;
using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Navigation;

namespace Hospital_IS.DoctorRole.DoctorViewModel
{
    public class RoomsViewModel : BindableBase
    {
        #region Fields
        private List<int> floors;
        private int selectedFloor;

        private string enteredNumber;
        private Room selectedRoom;
        private ICollectionView filterdRooms;

        public List<string> Types { get; set; }
        private string selectedType;

        private bool showPanel;
        private NavigationService insideNavigation;
        private RoomInfo roomInfoView;

        public List<int> Floors
        {
            get { return floors; }
            set
            {
                floors = value;
                OnPropertyChanged("Floors");
            }
        }


        public string EnteredNumber
        {
            get { return enteredNumber; }
            set
            {
                enteredNumber = value;
                OnPropertyChanged("EnteredNumber");
                FilterRooms();
            }
        }

        public Room SelectedRoom
        {
            get { return selectedRoom; }
            set
            {
                selectedRoom = value;
                OnPropertyChanged("SelectedRoom");
                RoomInfoView._ViewModel.SelectedRoom = SelectedRoom;
            }
        }

        public ICollectionView FilterdRooms
        {
            get { return filterdRooms; }
            set
            {
                filterdRooms = value;
                OnPropertyChanged("FilterdRooms");
            }
        }

        public int SelectedFloor
        {
            get { return selectedFloor; }
            set
            {
                selectedFloor = value;
                OnPropertyChanged("SelectedFloor");
                FilterRooms();
            }
        }

        public string SelectedType
        {
            get { return selectedType; }
            set
            {
                selectedType = value;
                OnPropertyChanged("SelectedType");
                FilterRooms();
            }
        }

        public bool ShowPanel
        {
            get { return showPanel; }
            set
            {
                showPanel = value;
                OnPropertyChanged("ShowPanel");
            }
        }

        public NavigationService InsideNavigation
        {
            get { return insideNavigation; }
            set { insideNavigation = value; }
        }

        public RoomInfo RoomInfoView
        {
            get { return roomInfoView; }
            set { roomInfoView = value; }
        }
        #endregion

        #region Commands
        private RelayCommand chooseRoomCommand;

        public RelayCommand ChooseRoomCommand
        {
            get { return chooseRoomCommand; }
            set { chooseRoomCommand = value; }
        }
        #endregion

        #region Actions

        private void Execute_ChooseRoomCommand(object obj)
        {
            RoomInfoView._ViewModel.SelectedRoom = SelectedRoom;
            InsideNavigation.Navigate(RoomInfoView);
            ShowPanel = true;
        }


        private bool CanExecute_Command(object obj)
        {
            return true;
        }
        #endregion

        #region Methods
        private RoomType FindType()
        {
            RoomType type;
            if (SelectedType.Equals("Soba za oporavak"))
            {
                type = RoomType.RecoveryRoom;
            }
            else if (SelectedType.Equals("Soba za konsultacije"))
            {
                type = RoomType.ConsultingRoom;
            }
            else if (SelectedType.Equals("Operaciona sala"))
            {
                type = RoomType.OperationRoom;
            }
            else
            {
                type = RoomType.StorageRoom;
            }

            return type;
        }
        private void FilterRooms()
        {
            ShowPanel = false;
            List<Room> rooms = RoomController.Instance.GetAllRooms();
            ICollectionView view = new CollectionViewSource { Source = rooms }.View;
            if ((EnteredNumber == null || EnteredNumber == "") && SelectedType == null && SelectedFloor == 0)
            {
                view.SortDescriptions.Add(new SortDescription("RoomNumber", ListSortDirection.Ascending));
                FilterdRooms = view;
            }
            else
            {
                view.Filter = delegate (object item)
                {
                    return CheckRoom((Room)item);
                };
                view.SortDescriptions.Add(new SortDescription("RoomNumber", ListSortDirection.Ascending));
                FilterdRooms = view;
            }

        }

        public bool CheckRoom(Room room)
        {
            int i;
            try
            {
                i = int.Parse(EnteredNumber.Trim());

            }
            catch (Exception e) { return true; }

            if (SelectedType == null && SelectedFloor == 0)
            {
                return room.RoomNumber > i;
            }
            else if (SelectedType != null && SelectedFloor == 0)
            {
                return room.Type.Equals(FindType());
            }
            else if (SelectedType == null && SelectedFloor != 0)
            {
                return room.RoomFloor == SelectedFloor;
            }
            else if (SelectedType != null && SelectedFloor != 0)
            {
                return room.Type.Equals(FindType()) && room.RoomFloor == SelectedFloor;
            }
            else if (SelectedType == null && SelectedFloor != 0)
            {
                return room.RoomNumber > i && room.RoomFloor == SelectedFloor;
            }
            else if (SelectedType != null && SelectedFloor == 0)
            {
                return room.RoomNumber > i && room.Type.Equals(FindType());
            }
            else
            {
                return room.RoomNumber > i && room.Type.Equals(FindType()) && room.RoomFloor == SelectedFloor;
            }
        }

        #endregion

        #region Constructor
        public RoomsViewModel()
        {
            RoomInfoView = new RoomInfo();
            Types = new List<string>();
            Types.Add("Soba za oporavak");
            Types.Add("Soba za konsultacije");
            Types.Add("Operaciona sala");
            Types.Add("Magacin");
            this.ChooseRoomCommand = new RelayCommand(Execute_ChooseRoomCommand, CanExecute_Command);
            this.Floors = RoomController.Instance.GetAllFloors();
            FilterRooms();
        }
        #endregion
    }
}
