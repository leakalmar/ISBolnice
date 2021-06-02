using Controllers;
using Hospital_IS.Controllers;
using Model;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Navigation;
using System.Windows.Threading;

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
        private int selectedRenovationOption = -1;
        private int comboBoxItem;
        private RelayCommand navigateToMEquipmentPageCommand;
        private RelayCommand navigateToMedicinePageCommand;
        private RelayCommand addNewRoom;
        private RelayCommand deleteRoom;
        private RelayCommand updateRoom;
        private RelayCommand navigateToRoomPage;
        private RelayCommand navigateToRoomRenovation;
        private RelayCommand navigateAdvancedRoomRenovation;
        private RelayCommand navigateToEmployeerPage;
        private RelayCommand navigateToBranchPage;
        private NavigationService navService;
        private Room selectedRoom;
        private RelayCommand navigateToManagerProfilePage;



        public RelayCommand NavigateToBranchPage
        {
            get { return navigateToBranchPage; }
            set
            {
                navigateToBranchPage = value;
            }
        }
        public RelayCommand NavigateToEmployeerPage
        {
            get { return navigateToEmployeerPage; }
            set
            {
                navigateToEmployeerPage = value;
            }
        }

        public RelayCommand NavigateAdvancedRoomRenovation
        {
            get { return navigateAdvancedRoomRenovation; }
            set
            {
                navigateAdvancedRoomRenovation = value;
            }
        }
        public RelayCommand NavigateToRoomRenovation
        {
            get { return navigateToRoomRenovation; }
            set
            {
                navigateToRoomRenovation = value;
            }
        }
        public RelayCommand NavigateToManagerProfilePage
        {
            get { return navigateToManagerProfilePage; }
            set
            {
                navigateToManagerProfilePage = value;
            }
        }
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
        
        public int SelectedRenovationOption
        {
            get
            {
                return selectedRenovationOption;
            }
            set
            {
                if (value != selectedRenovationOption)
                {
                    selectedRenovationOption = value;
                    OnPropertyChanged("SelectedRenovationOption");
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

        private void Execute_NavigateToBranchPageCommand(object obj)
        {
            this.NavService.Navigate(
                new Uri("ManagerView1/BranchView.xaml", UriKind.Relative));
        }

        private void Execute_NavigateToEmployeerPageCommand(object obj)
        {
            this.NavService.Navigate(
                new Uri("ManagerView1/EmployeersView.xaml", UriKind.Relative));
        }


        private void Execute_NavigateToRoomPageCommand(object obj)
        {
            this.NavService.Navigate(
                new Uri("ManagerView1/RoomView.xaml", UriKind.Relative));
        }

        private void Execute_NavigateToEquipmentPageCommand(object obj)
        {
            this.NavService.Navigate(
                new Uri("ManagerView1/EquipmentView.xaml", UriKind.Relative));
        }

        private void Execute_NavigateToMedicinePageCommand(object obj)
        {
            MedicineViewModel.Instance.Medicines = new ObservableCollection<Medicine>(MedicineController.Instance.GetAll());
            this.NavService.Navigate(
                new Uri("ManagerView1/MainMedicineView.xaml", UriKind.Relative));
        }
        private void Execute_NavigateToManagerProfilePageCommand(object obj)
        {
            ManagerProfileOptionsVIewModel.Instance.PreviousMainPage = this.NavService.CurrentSource;
            this.NavService.Navigate(
                new Uri("ManagerView1/ManagerProfileOptionsView.xaml", UriKind.Relative));
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
            this.NavigateToManagerProfilePage = new RelayCommand(Execute_NavigateToManagerProfilePageCommand, CanExecute_NavigateCommand);
            this.NavigateToEquipmentPageCommand = new RelayCommand(Execute_NavigateToEquipmentPageCommand, CanExecute_NavigateCommand);
            this.NavigateToRoomRenovation = new RelayCommand(Execute_RenovationRoomCommand, CanExecute_RenovationRoomCommand);
            this.NavigateAdvancedRoomRenovation = new RelayCommand(Execute_AdvancedRenovationRoomCommand);
            this.NavigateToEmployeerPage = new RelayCommand(Execute_NavigateToEmployeerPageCommand);
            this.NavigateToBranchPage = new RelayCommand(Execute_NavigateToBranchPageCommand);
            DispatcherTimer dispatcherTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMinutes(1)
            };
            dispatcherTimer.Tick += timer_Tick;
            dispatcherTimer.Start();
        }


        private void timer_Tick(object sender, EventArgs e)
        {
          
            DateTime time = DateTime.Now;
            foreach (AdvancedRenovation renovation in AdvancedRenovationController.Instance.GetAll())
            {
               
                if (renovation.RenovationEnd <= time && renovation.isMade == false)
                {
                        if(renovation.IsSplit == true)
                        {
                        MessageBox.Show("Uspjesna podijela soba");
                            SplitOneRoomIntoTwo(renovation);
                        AdvancedRenovationController.Instance.RemoveAdvancedRenovation(renovation);
                        break;
                        }else if (renovation.IsMerge == true)
                        {
                        MergeTwoRoomInOne(renovation);
                        MessageBox.Show("Uspjesno spajanje soba");
                        AdvancedRenovationController.Instance.RemoveAdvancedRenovation(renovation);
                        break;
                    }
                                                        
                }
                
               
            }
        }

        private  void MergeTwoRoomInOne(AdvancedRenovation renovation)
        {
            RoomController.Instance.RemoveRoom(renovation.RoomFirst);
            RoomController.Instance.RemoveRoom(renovation.RoomSecond);
            RoomController.Instance.AddRoom(renovation.RenovationResultRoom.RoomNumber, renovation.RenovationResultRoom.RoomFloor, renovation.RenovationResultRoom.SurfaceArea,
                0, (int)renovation.RenovationResultRoom.Type);
           

            LoadRooms();

        }

        private void SplitOneRoomIntoTwo(AdvancedRenovation renovation)
        {
            renovation.RoomFirst.SurfaceArea = renovation.RoomFirst.SurfaceArea / 2;
            RoomController.Instance.UpdateRoom(renovation.RoomFirst.RoomId, renovation.RoomFirst.RoomFloor, renovation.RoomFirst.SurfaceArea / 2,
                renovation.RoomFirst.BedNumber, (int)renovation.RoomFirst.Type);
            RoomController.Instance.AddRoom(renovation.RenovationResultRoom.RoomNumber, renovation.RenovationResultRoom.RoomFloor, renovation.RenovationResultRoom.SurfaceArea,
                0, (int)renovation.RenovationResultRoom.Type);
            

            LoadRooms();
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


        private void Execute_AdvancedRenovationRoomCommand(object obj)
        {
            //RoomRenovationViewModel.Instance.SetAppointmnetForRoom(SelectedRoom.RoomId);
            //RoomRenovationViewModel.Instance.FirstRoom = SelectedRoom;
            AdvancedRoomRenovationViewModel.Instance.NavService = NavService;
            this.NavService.Navigate(
                    new Uri("ManagerView1/AdvancedRoomOptionsView.xaml", UriKind.Relative));
        }
        private void Execute_RenovationRoomCommand(object obj)
        {
            RoomRenovationViewModel.Instance.SetAppointmnetForRoom(SelectedRoom.RoomId);
            RoomRenovationViewModel.Instance.FirstRoom = SelectedRoom;
            this.NavService.Navigate(
                    new Uri("ManagerView1/RoomRenovationView.xaml", UriKind.Relative));
        }


        
        private bool CanExecute_RenovationRoomCommand(object obj)
        {
            return !(SelectedRoom == null);
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
              
                    
                    _roomNumber = value;
                    MessageBox.Show("uslo");
                    
                    OnPropertyChanged("RoomNumber");
              
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
