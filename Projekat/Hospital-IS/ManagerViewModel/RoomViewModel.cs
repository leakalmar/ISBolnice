using Controllers;
using Enums;
using Hospital_IS.Controllers;
using Hospital_IS.ManagerHelp;
using Hospital_IS.ManagerView1;
using Model;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using System.Windows.Navigation;
using System.Windows.Threading;

namespace Hospital_IS.ManagerViewModel
{
    public class RoomViewModel : ViewModel
    {
        private ICollectionView _rooms;
        private Injector injector;
        private String _roomNumber = "Unesite broj sobe";
        private String _roomFloor = "Unesite sprat sobe";
        private String _surfaceArea = "Unesite površinu sobe";
        private String _bedNumber = "Unesite broj kreveta";
        private String searchBox = "Unesite broj sobe";
        private int selectedRenovationOption = -1;
        private int comboBoxItem;
        private int searchSelectedIndex = 4;
        private RelayCommand navigateToMEquipmentPageCommand;
        private RelayCommand navigateToMedicinePageCommand;
        private RelayCommand addNewRoom;
        private RelayCommand deleteRoom;
        private RelayCommand naviagteToUpdateRoom;
        private RelayCommand navigateToRoomPage;
        private RelayCommand navigateToRoomRenovation;
        private RelayCommand navigateAdvancedRoomRenovation;
        private RelayCommand navigateToEmployeerPage;
        private RelayCommand navigateToBranchPage;
        private RelayCommand navigateToRenovationReport;
        private RelayCommand navigateToAddRoom;
        private RelayCommand showHelpDialog;
        private RelayCommand deleteRoomCoomand;
        private RelayCommand closeWindowCoomand;
        private RelayCommand openHelpWindow;
        private NavigationService navService;
        private Room selectedRoom = null;
        private RelayCommand navigateToManagerProfilePage;
        private DeleteRoomWindow deleteRoomWindow = new DeleteRoomWindow();

        public RelayCommand OpenHelpWindow
        {
            get { return openHelpWindow; }
            set
            {
                openHelpWindow = value;
            }
        }

        public RelayCommand NavigateToAddRoom
        {
            get { return navigateToAddRoom; }
            set
            {
                navigateToAddRoom = value;
            }
        }

        public RelayCommand NavigateToRenovationReport
        {
            get { return navigateToRenovationReport; }
            set
            {
                navigateToRenovationReport = value;
            }
        }

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
        public RelayCommand NaviagteToUpdateRoom
        {
            get { return naviagteToUpdateRoom; }
            set
            {
                naviagteToUpdateRoom = value;
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

        public RelayCommand ShowHelpDialogCommand
        {
            get { return showHelpDialog; }
            set
            {
                showHelpDialog = value;
            }
        }

        public RelayCommand DeleteRoomCoomand
        {
            get { return deleteRoomCoomand; }
            set
            {
                deleteRoomCoomand = value;
            }
        }

        public RelayCommand CloseWindowCoomand
        {
            get { return closeWindowCoomand; }
            set
            {
                closeWindowCoomand = value;
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
        public ICollectionView Rooms
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

        public int SearchSelectedIndex
        {
            get { return searchSelectedIndex; }
            set
            {
                searchSelectedIndex = value;
                ICollectionView view = new CollectionViewSource { Source = RoomController.Instance.GetAllRooms() }.View;
                if (searchSelectedIndex != 4) {
                    view.Filter = null;
                    view.Filter = delegate (object item)
                    {
                        String broj = ((Room)item).RoomNumber.ToString();
                        RoomType type = (RoomType)searchSelectedIndex;
                        RoomType roomType = ((Room)item).Type;

                        return broj.Contains(SearchBox) && type == roomType;
                    };
                }
                else
                {
                    view.Filter = null;
                    view.Filter = delegate (object item)
                    {
                        String broj = ((Room)item).RoomNumber.ToString();

                        return broj.Contains(SearchBox);
                    };

                }

                Rooms = view;


                OnPropertyChanged("SearchSelectedIndex");

            }
        }




        public String SearchBox
        {
            get { return searchBox; }
            set
            {
                searchBox = value;
                ICollectionView view = new CollectionViewSource { Source = RoomController.Instance.GetAllRooms()}.View;
                view.Filter = null;

                if (searchSelectedIndex != 4)
                {
                    view.Filter = null;
                    view.Filter = delegate (object item)
                    {
                        String broj = ((Room)item).RoomNumber.ToString();
                        RoomType type = (RoomType)searchSelectedIndex;
                        RoomType roomType = ((Room)item).Type;

                        return broj.Contains(SearchBox) && type == roomType;
                    };
                }
                else
                {
                    view.Filter = null;
                    view.Filter = delegate (object item)
                    {
                        String broj = ((Room)item).RoomNumber.ToString();

                        return broj.Contains(SearchBox);
                    };

                }
                Rooms = view;

                OnPropertyChanged("SearchBox");

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

        private void Execute_OpenHelpWindowCommand(object obj)
        {
            RoomHelpWindow roomHelpWindow = new RoomHelpWindow();
            roomHelpWindow.ShowDialog();
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

            this.NavigateToRoomPage = new RelayCommand(Execute_NavigateToRoomPageCommand, CanExecute_NavigateCommand);
            this.DeleteRoom = new RelayCommand(Execute_DeleteRoomCommand, CanExecute_DeleteRoomCommand);
            this.NavigateToMedicinePageCommand = new RelayCommand(Execute_NavigateToMedicinePageCommand, CanExecute_NavigateCommand);
            this.NavigateToManagerProfilePage = new RelayCommand(Execute_NavigateToManagerProfilePageCommand, CanExecute_NavigateCommand);
            this.NavigateToEquipmentPageCommand = new RelayCommand(Execute_NavigateToEquipmentPageCommand, CanExecute_NavigateCommand);
            this.NavigateToRoomRenovation = new RelayCommand(Execute_RenovationRoomCommand, CanExecute_RenovationRoomCommand);
            this.NavigateAdvancedRoomRenovation = new RelayCommand(Execute_AdvancedRenovationRoomCommand);
            this.NavigateToEmployeerPage = new RelayCommand(Execute_NavigateToEmployeerPageCommand);
            this.NavigateToBranchPage = new RelayCommand(Execute_NavigateToBranchPageCommand);
            this.NavigateToRenovationReport = new RelayCommand(Execute_NavigateToRoomRenovationReport);
            this.NavigateToAddRoom = new RelayCommand(Execute_NavigateToAddRoomPage);
            this.NaviagteToUpdateRoom = new RelayCommand(Execute_NavigateToUpdateRoomPage, CanExecute_DeleteRoomCommand);
            this.ShowHelpDialogCommand = new RelayCommand(Execute_ShowHelpMessage);
            this.DeleteRoomCoomand = new RelayCommand(Execute_DeleteRoomByWindowCommand);
            this.CloseWindowCoomand = new RelayCommand(Execute_CloseWindowInCommand);
            this.OpenHelpWindow = new RelayCommand(Execute_OpenHelpWindowCommand);

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
                if (DateTime.Compare(renovation.RenovationEnd, time) < 0)
                {
                    AdvancedRenovationController.Instance.ExecuteAdvancedRoomRenovation(renovation);
                    AdvancedRenovationController.Instance.RemoveAdvancedRenovation(renovation);
                    LoadRooms();
                    if(renovation.Type == AdvancedRenovationType.MERGE)
                    {
                        MessageBox.Show("Uspjesno izvrseno spajanje");
                    }
                    else
                    {
                        MessageBox.Show("Uspjenso izvrsena podijela");
                    }
                    break;
                }
            }
        }


        private void Execute_ShowHelpMessage(object obj)
        {

          

        }

        private void Execute_DeleteRoomCommand(object obj)
        {
           
            this.deleteRoomWindow.DataContext = this;
            this.deleteRoomWindow.ShowDialog();
         
        }

        private void Execute_DeleteRoomByWindowCommand(object obj)
        {
            RoomController.Instance.RemoveRoom(SelectedRoom);
            LoadRooms();
            this.deleteRoomWindow.Hide();
            SelectedRoom = null;
            MessageBox.Show("Uspjesno brisanje");
        }

        private void Execute_CloseWindowInCommand(object obj)
        {
            this.deleteRoomWindow.Hide();
            SelectedRoom = null;
        }

        private void Execute_NavigateToUpdateRoomPage(object obj)
        {

            UpdateRoomViewModel.Instance.SetRoom(SelectedRoom);
            UpdateRoomViewModel.Instance.NavService = this.NavService;
            this.NavService.Navigate(
                    new Uri("ManagerView1/UpdateRoomVIew.xaml", UriKind.Relative));

        }

        private void Execute_NavigateToAddRoomPage(object obj)
        {

            AddRoomViewModel.Instance.NavService = this.NavService;
            this.NavService.Navigate(
                    new Uri("ManagerView1/AddRoom.xaml", UriKind.Relative));
        }

        private void Execute_NavigateToRoomRenovationReport(object obj)
        {
            RenovationReportVIewModel.Instance.NavService = this.NavService;
            ReportRenovationWindow reportRenovation = new ReportRenovationWindow();
            reportRenovation.ShowDialog();
        }


       

        private  void MergeTwoRoomInOne(AdvancedRenovation renovation)
        {
            RoomController.Instance.RemoveRoom(renovation.RoomFirst);
            RoomController.Instance.RemoveRoom(renovation.RoomSecond);
            Room room = new Room(renovation.RenovationResultRoom.RoomNumber, renovation.RenovationResultRoom.RoomFloor, renovation.RenovationResultRoom.SurfaceArea,
                0, renovation.RenovationResultRoom.Type);
            RoomController.Instance.AddRoom(room);           
            LoadRooms();
        }

        private void SplitOneRoomIntoTwo(AdvancedRenovation renovation)
        {
            renovation.RoomFirst.SurfaceArea = renovation.RoomFirst.SurfaceArea / 2;
            Room updateRoom = new Room(renovation.RoomFirst.Id, renovation.RoomFirst.RoomFloor, renovation.RoomFirst.SurfaceArea / 2,
                renovation.RoomFirst.BedNumber, renovation.RoomFirst.Type);
            RoomController.Instance.UpdateRoom(updateRoom);
            Room room = new Room(renovation.RenovationResultRoom.RoomNumber, renovation.RenovationResultRoom.RoomFloor, renovation.RenovationResultRoom.SurfaceArea,
                 0, renovation.RenovationResultRoom.Type);
            RoomController.Instance.AddRoom(room);
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
            AdvancedRoomRenovationViewModel.Instance.NavService = NavService;
            this.NavService.Navigate(
                    new Uri("ManagerView1/AdvancedRoomOptionsView.xaml", UriKind.Relative));
        }
        private void Execute_RenovationRoomCommand(object obj)
        {
            RoomRenovationViewModel.Instance.SetAppointmnetForRoom(SelectedRoom.Id);
            RoomRenovationViewModel.Instance.FirstRoom = SelectedRoom;
            RoomRenovationViewModel.Instance.NavService = this.NavService;
            this.NavService.Navigate(
                    new Uri("ManagerView1/RoomRenovationView.xaml", UriKind.Relative));
        }


        
        private bool CanExecute_RenovationRoomCommand(object obj)
        {
            return !(SelectedRoom == null);
        }



        private void LoadRooms()
        {


            Rooms = new CollectionViewSource { Source = RoomController.Instance.GetAllRooms() }.View;
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
