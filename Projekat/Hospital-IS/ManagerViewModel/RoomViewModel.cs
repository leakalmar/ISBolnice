using Controllers;
using Model;
using Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Hospital_IS.ManagerViewModel
{
    public class RoomViewModel : ViewModel
    {
        private  ObservableCollection<Room> _rooms { get; set; }
        private Injector injector;
        private String _roomNumber = "Unesite broj sobe";
        private String _roomFloor = "Unesite sprat sobe";
        private String _surfaceArea = "Unesite površinu sobe";
        private String _bedNumber = "Unesite broj kreveta";
        private int comboBoxItem;
        private RelayCommand navigateToMEquipmentPageCommand;
        private RelayCommand navigateToMedicinePageCommand;
        private RelayCommand addNewRoom;
        private RelayCommand deleteRoom;
        private RelayCommand updateRoom;
        private RelayCommand navigateToRoomPage;
        private NavigationService navService;
        private Room selectedRoom;

        public RelayCommand AddNewRoom
        {
            get { return addNewRoom; }
            set
            {
                addNewRoom = value;
            }
        }
        public RelayCommand DeleteRoom
        {
            get { return deleteRoom; }
            set
            {
                deleteRoom = value;
            }
        }
        public RelayCommand UpdateRoom
        {
            get { return updateRoom; }
            set
            {
                updateRoom = value;
            }
        }
        
        public RelayCommand NavigateToMedicinePageCommand
        {
            get { return navigateToMedicinePageCommand; }
            set
            {
                navigateToMedicinePageCommand = value;
            }
        }
        public RelayCommand NavigateToRoomPage
        {
            get { return navigateToRoomPage; }
            set
            {
                navigateToRoomPage = value;
            }
        }
        public RelayCommand NavigateToEquipmentPageCommand
        {
            get { return navigateToMEquipmentPageCommand; }
            set
            {
                navigateToMEquipmentPageCommand = value;
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

        public Injector Injector
        {
            get { return injector; }
            set
            {
                injector = value;
            }
        }
        public ObservableCollection<Room> Rooms
        {
            get
            {
                return _rooms;
            }
            set
            {
                if (value != _rooms)
                {

                    _rooms = value;
                    OnPropertyChanged("Rooms");
                }
            }
        }

        public int ComboBoxItem
        {
            get
            {
                return comboBoxItem;
            }
            set
            {
                if (value != comboBoxItem)
                {

                    comboBoxItem = value;
                    OnPropertyChanged("ComboBoxItem");
                }
            }
        }
        public Room SelectedRoom
        {
            get { return selectedRoom; }
            set
            {
                selectedRoom = value;

                OnPropertyChanged("SelectedRoom");

            }
        }

        private void Execute_NavigateToRoomPageCommand(object obj)
        {
            this.NavService.Navigate(
                new Uri("ManagerView1/RoomView.xaml", UriKind.Relative));
        }

        private void Execute_NavigateToMedicinePageCommand(object obj)
        {
            this.NavService.Navigate(
                new Uri("ManagerView1/MainMedicineView.xaml", UriKind.Relative));
        }

        private void Execute_AddRoomCommand(object obj)
        {
            if (Valiadate())
            {
                RoomController.Instance.AddRoom(Convert.ToInt32(RoomNumber), Convert.ToInt32(RoomFloor), Convert.ToInt32(SurfaceArea),
                    Convert.ToInt32(BedNumber), ComboBoxItem);
                LoadRooms();

                this.NavService.Navigate(
                    new Uri("ManagerView1/RoomView.xaml", UriKind.Relative));
            }
        }

        private bool Valiadate()
        {
            bool firstCheck = false;

            if (int.TryParse(RoomNumber, out int number) && int.TryParse(RoomFloor, out int floor) && int.TryParse(SurfaceArea, out int area)
               && int.TryParse(BedNumber, out int bed) && !string.IsNullOrEmpty(RoomNumber) && !string.IsNullOrEmpty(RoomFloor) && !string.IsNullOrEmpty(SurfaceArea) &&
               !string.IsNullOrEmpty(BedNumber))
            {
               
                if ((number > 0 && number < 1000) && (floor >= 0 && floor <= 10) && (area > 0 && area <= 10000) && bed >= 0)
                {
                    firstCheck = true;
                }
            }

            return firstCheck;
        }

        private void Execute_DeleteRoomCommand(object obj)
        {
            RoomController.Instance.RemoveRoom(SelectedRoom);
            LoadRooms();
        }

        private void Execute_UpdateRoomCommand(object obj)
        {
            if (Valiadate())
            {
                RoomController.Instance.UpdateRoom(Convert.ToInt32(RoomNumber), Convert.ToInt32(RoomFloor), Convert.ToInt32(SurfaceArea),
                    Convert.ToInt32(BedNumber), ComboBoxItem);
                LoadRooms();
            }
              
            
        }
        private bool CanExecute_DeleteRoomCommand(object obj)
        {
            return !(SelectedRoom == null);
        }


        private bool CanExecute_NavigateCommand(object obj)
        {
            return true;
        }

        public RoomViewModel(NavigationService navigationService)
        {
            this.navService = navigationService;
            LoadRooms();
            this.AddNewRoom = new RelayCommand(Execute_AddRoomCommand);
            this.NavigateToRoomPage = new RelayCommand(Execute_NavigateToRoomPageCommand, CanExecute_NavigateCommand);
            this.DeleteRoom = new RelayCommand(Execute_DeleteRoomCommand, CanExecute_DeleteRoomCommand);
            this.UpdateRoom = new RelayCommand(Execute_UpdateRoomCommand);
            this.NavigateToMedicinePageCommand = new RelayCommand(Execute_NavigateToMedicinePageCommand, CanExecute_NavigateCommand);
           
        }

        public void SetFields(object selectedRoom)
        {
            Room room = (Room)selectedRoom;

            RoomNumber = room.RoomNumber.ToString();
            RoomFloor = room.RoomFloor.ToString();
            SurfaceArea = room.SurfaceArea.ToString();
            BedNumber = room.BedNumber.ToString();
            ComboBoxItem = (int)room.Type;
            


        }

        private void LoadRooms()
        { 
           
            
            Rooms = new ObservableCollection<Room>(RoomController.Instance.GetAllRooms());
        }

        public String RoomNumber
        {
            get
            {
                return _roomNumber;
            }
            set
            {
                if (value != _roomNumber)
                {
                    
                    _roomNumber = value;
                    OnPropertyChanged("RoomNumber");
                }
            }
        }

     


        public String RoomFloor
        {
            get
            {
                return _roomFloor;
            }
            set
            {
                if (value != _roomFloor)
                {
                    _roomFloor = value;
                    OnPropertyChanged("RoomFloor");
                }

            }
        }


        public String SurfaceArea
        {
            get
            {
                return _surfaceArea;
            }
            set
            {
                if (value != _surfaceArea)
                {
                    _surfaceArea = value;
                    OnPropertyChanged("SurfaceArea");
                }

            }
        }

        public String BedNumber
        {
            get
            {
                return _bedNumber;
            }
            set
            {
                if (value != _bedNumber)
                {
                    _bedNumber = value;
                    OnPropertyChanged("BedNumber");

                }

            }
        }
    }
}
