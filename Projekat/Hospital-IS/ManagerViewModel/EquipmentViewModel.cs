using Controllers;
using Enums;
using Model;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Navigation;

namespace Hospital_IS.ManagerViewModel
{
    public class EquipmentViewModel: ViewModel
    {

        private ObservableCollection<Room> _rooms { get; set; }
        private ObservableCollection<String> roomTypes { get; set; }
        private int selectedTransferOption = -1;
        private int selectedTypeIndex = -1;
        private ICollectionView  _equipments { get; set; }
        private string _name= "Unesite ime opreme";
        private string _quantity = "Unesite kolicinu";
        private string _producerName = "Unesite ime proizvodjaca";
        private String selectedRoomType;
        private NavigationService navService;
        private RelayCommand navigateToRoomPage;
        private RelayCommand changeEquipmentGrid;
        private RelayCommand addNewEquipment;
        private RelayCommand deleteEquipment;
        private RelayCommand updateEquipment;
        private RelayCommand navigateToUpdateEquipment;
        private int comboBoxItem;
        private Equipment selectedEquipment;
        private Room selectedRoom;
        private RelayCommand navigateToManagerProfilePage;
        private RelayCommand navigateToTransferOptions;
        private RelayCommand searchCommnd;
        private RelayCommand navigateToMedicinePage;
        private RelayCommand navigateToEmployeePage;
        private RelayCommand navigateToBranchPage;
        private string searchBox;
        private int selectedCondition =0;




        public RelayCommand NavigateToBranchPage
        {
            get { return navigateToBranchPage; }
            set
            {
                navigateToBranchPage = value;
            }
        }
        public RelayCommand NavigateToMedicinePage
        {
            get { return navigateToMedicinePage; }
            set
            {
                navigateToMedicinePage = value;
            }
        }

        public RelayCommand NavigateToEmployeePage
        {
            get { return navigateToEmployeePage; }
            set
            {
                navigateToEmployeePage = value;
            }
        }

        public int SelectedCondition
        {
            get { return selectedCondition; }
            set
            {
                if (value != selectedCondition)
                {

                    selectedCondition = value;
                    OnPropertyChanged("SelectedCondition");
                }
            }
        }

        public String SearchBox
        {
            get { return searchBox; }
            set
            {
                searchBox = value;
               
                OnPropertyChanged("SelectedRoom");
            }
        }


        public int SelectedTypeIndex
        {
            get { return selectedTypeIndex; }
            set
            {
                if (value != selectedTypeIndex)
                {
                    
                    selectedTypeIndex = value;
                    OnPropertyChanged("SelectedTypeIndex");

                    if (selectedTypeIndex != 2)
                    {
                        ICollectionView view = new CollectionViewSource { Source = SelectedRoom.Equipment }.View;
                        view.Filter = null;
                        view.Filter = delegate (object item)
                        {
                            EquiptType type = ((Equipment)item).EquipType;
                            EquiptType equiptType = EquiptType.Dynamic;
                            if (selectedTypeIndex == 0)
                            {
                                equiptType = EquiptType.Stationary;
                            }
                            else if (selectedTypeIndex == 1)
                            {
                                equiptType = EquiptType.Dynamic;
                            }

                            return type == equiptType;
                        };
                        
                        Equipments = view;
                    }
                    else
                    {
                        Equipments = new CollectionViewSource { Source = SelectedRoom.Equipment }.View;
                    }
                }
            }
        }

        public RelayCommand SearchCommnd
        {
            get { return searchCommnd; }
            set
            {
                searchCommnd = value;
            }
        }

        public RelayCommand NavigateToTransferOptions
        {
            get { return navigateToTransferOptions; }
            set
            {
                navigateToTransferOptions = value;
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

        

        public int SelectedTransferOption
        {
            get { return selectedTransferOption; }
            set
            {
                if (value != selectedTransferOption)
                {
                   
                    selectedTransferOption = value;
                    OnPropertyChanged("SelectedTransferOption");
                }
            }
        }

        public Room SelectedRoom
         {
            get { return selectedRoom; }
            set
            {   
                    selectedRoom = value;
                    if(selectedRoom != null)
                {
                    Equipments = new CollectionViewSource { Source = selectedRoom.Equipment }.View;
                    SelectedTypeIndex = 2;
                    SelectedEquipment = null;
                }
                   
                    OnPropertyChanged("SelectedRoom");
            }
        }

        public Equipment SelectedEquipment
        {
            get { return selectedEquipment; }
            set
            {
                if (value != SelectedEquipment)
                {
                    selectedEquipment = value;
                    OnPropertyChanged("SelectedEquipment");
                }
            }
        }

        public String SelectedRoomType
        {
            get { return selectedRoomType; }
            set
            {
                selectedRoomType = value;
                
                OnPropertyChanged("SelectedRoom");

            }
        }

        public RelayCommand ChangeEquipmentGrid
        {
            get { return changeEquipmentGrid; }
            set
            {
                changeEquipmentGrid = value;
            }
        }

        public RelayCommand DeleteEquipment
        {
            get { return deleteEquipment; }
            set
            {
                deleteEquipment = value;
            }
        }
        public RelayCommand UpdateEquipment
        {
            get { return updateEquipment; }
            set
            {
                updateEquipment = value;
            }
        }
        public RelayCommand AddNewEquipment
        {
            get { return addNewEquipment; }
            set
            {
                addNewEquipment = value;
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

        public RelayCommand NavigateToUpdateEquipment
        {
            get { return navigateToUpdateEquipment; }
            set
            {
                navigateToUpdateEquipment = value;
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
        public ObservableCollection<string> RoomTypes
        {
            get
            {
                return roomTypes;
            }
            set
            {
                if (value != roomTypes)
                {

                    roomTypes = value;
                    OnPropertyChanged("RoomTypes");

                }
            }
        }

        public ICollectionView Equipments
        {
            get
            {
                return _equipments;
            }
            set
            {
                if (value != _equipments)
                {

                    _equipments = value;
                    OnPropertyChanged("Equipments");
                }
            }
        }

        public String Quantity
        {
            get
            {
                return _quantity;
            }
            set
            {
                if (value != _quantity)
                {

                    _quantity = value;
                    OnPropertyChanged("Quantity");
                }
            }
        }

        public String ProducerName
        {
            get
            {
                return _producerName;
            }
            set
            {
                if (value != _producerName)
                {

                    _producerName = value;
                    OnPropertyChanged("ProducerName");
                }
            }
        }

        public String Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (value != _name)
                {

                    _name = value;
                    OnPropertyChanged("Name");
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

        private static EquipmentViewModel instance = null;
        public static EquipmentViewModel Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new EquipmentViewModel();
                }
                return instance;
            }
        }
        private EquipmentViewModel()
        {

            LoadRooms();
            RoomTypes = new ObservableCollection<String>();
            RoomTypes.Add("Staticka");
            RoomTypes.Add("Dinamicka");
            RoomTypes.Add("Sva");
            this.NavigateToRoomPage = new RelayCommand(Execute_NavigateToRoomPageCommand, CanExecute_NavigateCommand);
            this.AddNewEquipment = new RelayCommand(Execute_AddNewEquipmentCommand);
            this.DeleteEquipment = new RelayCommand(Execute_DeleteEquipmentComand, CanExecute_IfEquipmentIsSelected);
            this.NavigateToUpdateEquipment = new RelayCommand(Execute_NavigateToUpdateEquipmentPageCommand, CanExecute_IfEquipmentIsSelected);
            this.UpdateEquipment = new RelayCommand(Execute_UpdateEquipmentCommand);
            this.NavigateToManagerProfilePage = new RelayCommand(Execute_NavigateToManagerProfilePageCommand, CanExecute_NavigateCommand);
            this.NavigateToTransferOptions = new RelayCommand(Execute_NavigateToTransferEquipmentPageCommand, CanExecute_NavigateToTransferViewCommand);
            this.SearchCommnd = new RelayCommand(Execute_SearchCommand);
            this.NavigateToMedicinePage = new RelayCommand(Execute_NavigateToMedicinePageCommand);
            this.NavigateToEmployeePage = new RelayCommand(Execute_NavigateToEmployeePageCommand);
            this.NavigateToBranchPage = new RelayCommand(Execute_NavigateToBranchPageCommand);

        }


        private void Execute_NavigateToBranchPageCommand(object obj)
        {
            MedicineViewModel.Instance.NavService = this.NavService;
            this.NavService.Navigate(
                new Uri("ManagerView1/BranchView.xaml", UriKind.Relative));
        }

        private void Execute_NavigateToMedicinePageCommand(object obj)
        {
            MedicineViewModel.Instance.NavService = this.NavService;
            this.NavService.Navigate(
                new Uri("ManagerView1/MainMedicineView.xaml", UriKind.Relative));
        }

        private void Execute_NavigateToEmployeePageCommand(object obj)
        {
            this.NavService.Navigate(
                new Uri("ManagerView1/EmployeersView.xaml", UriKind.Relative));
        }

        private void Execute_SearchCommand(object obj)
        {

            if (SelectedTypeIndex != 2)
            {
                ICollectionView view = new CollectionViewSource { Source = SelectedRoom.Equipment }.View;
                view.Filter = null;
                view.Filter = delegate (object item)
                {
                    EquiptType type = ((Equipment)item).EquipType;
                    EquiptType equiptType = EquiptType.Dynamic;
                    if (SelectedTypeIndex == 0)
                    {
                        equiptType = EquiptType.Stationary;
                    }
                    else if (SelectedTypeIndex == 1)
                    {
                        equiptType = EquiptType.Dynamic;
                    }



                    String equipName = ((Equipment)item).Name;
                    int amaount = ((Equipment)item).Quantity;
                    String producerName = ((Equipment)item).ProducerName;
                    if (int.TryParse(SearchBox, out int number)){

                        if(SelectedCondition == 0)
                        {
                            return type == equiptType && number == amaount;
                        }else if(SelectedCondition == 1){
                            return type == equiptType && number < amaount;
                        }else if(SelectedCondition == 2)
                        {
                            return type == equiptType && number > amaount;
                        }
                       
                    }else
                    {
                        String[] searchItems = SearchBox.Split(",");
                        if(searchItems.Length == 1)
                        {
                            return type == equiptType && equipName.Contains(searchItems[0]);
                        }
                        else
                        {
                            return type == equiptType && equipName.Contains(searchItems[0]) && producerName.Contains(searchItems[1]);
                        }
                        
                    }

                    return type == equiptType;
                   
                };

                Equipments = view;
            }
            else
            {
                ICollectionView view = new CollectionViewSource { Source = SelectedRoom.Equipment }.View;
                view.Filter = null;
                view.Filter = delegate (object item)
                {
                    String equipName = ((Equipment)item).Name;
                    int amaount = ((Equipment)item).Quantity;
                    String producerName = ((Equipment)item).ProducerName;
                    if (int.TryParse(SearchBox, out int number))
                    {

                        if (SelectedCondition == 0)
                        {
                            return number == amaount;
                        }
                        else if (SelectedCondition == 1)
                        {
                            return number < amaount;
                        }
                        else if (SelectedCondition == 2)
                        {
                            return number > amaount;
                        }

                    }
                    else
                    {
                        String[] searchItems = SearchBox.Split(",");
                        if (searchItems.Length == 1)
                        {
                            return equipName.Contains(searchItems[0]);
                        }
                        else
                        {
                            return equipName.Contains(searchItems[0]) && producerName.Contains(searchItems[1]);
                        }

                    }
                    return true;
                };

                Equipments = view;
            }
        }


        private void Execute_NavigateToTransferEquipmentPageCommand(object obj)
        {
            
            if (SelectedTransferOption == 0)
            {
                EquipmentTransferViewModel.Instance.NavService = this.NavService;
                this.NavService.Navigate(
               new Uri("ManagerView1/EquipmentTransferDynamicView.xaml", UriKind.Relative));
            }else if(SelectedTransferOption == 1)
            {
                EquipmentTransferStaticViewModel.Instance.NavService = this.NavService;
                this.NavService.Navigate(
              new Uri("ManagerView1/EquipmentStaticTransferView.xaml", UriKind.Relative));
            }
        }

        private bool CanExecute_NavigateToTransferViewCommand(object obj)
        {
            return !(SelectedTransferOption == -1);
        }

        private void Execute_NavigateToManagerProfilePageCommand(object obj)
        {
            ManagerProfileOptionsVIewModel.Instance.PreviousMainPage = this.NavService.CurrentSource;
            this.NavService.Navigate(
                new Uri("ManagerView1/ManagerProfileOptionsView.xaml", UriKind.Relative));
        }

        private bool CanExecute_NavigateCommand(object obj)
        {
            return true;
        }


        private void LoadRooms()
        {
            
            Rooms = new ObservableCollection<Room>(RoomController.Instance.GetAllRooms());
        }
       

        private void Execute_NavigateToRoomPageCommand(object obj)
        {
            this.NavService.GoBack();
            SelectedEquipment = null;

        }

        private void Execute_DeleteEquipmentComand(object obj)
        {

            RoomController.Instance.RemoveEquipment(SelectedRoom,SelectedEquipment);
            Equipments = new CollectionViewSource { Source = SelectedRoom.Equipment }.View;


        }

        private void Execute_NavigateToUpdateEquipmentPageCommand(object obj)
        {
            this.NavService.Navigate(
                    new Uri("ManagerView1/UpdateEquipmentView.xaml", UriKind.Relative));
            Name = SelectedEquipment.Name;
            ProducerName = SelectedEquipment.Name;
            Quantity = SelectedEquipment.Quantity.ToString();
            ComboBoxItem = (int)SelectedEquipment.EquipType;

        }


        private bool CanExecute_IfEquipmentIsSelected(object obj)
        {
            return !(SelectedEquipment == null);
        }


        private void Execute_AddNewEquipmentCommand(object obj)
        {
            if(!string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(ProducerName) && int.TryParse(Quantity,out int quantity))
            {
                if(quantity > 0)
                {
                    EquiptType equiptType = (EquiptType)ComboBoxItem;
                    Equipment equipment = new Equipment(equiptType, Name, quantity, ProducerName);
                    
                    RoomController.Instance.AddEquipment(SelectedRoom, equipment);
                    Name = "Unesite ime opreme";
                    Quantity = "Unesite kolicinu";
                    ProducerName = "Unesite ime proizvodjaca";

                    this.NavService.Navigate(
                    new Uri("ManagerView1/EquipmentView.xaml", UriKind.Relative));

                }

            } 
        }

        private void Execute_UpdateEquipmentCommand(object obj)
        {
           
            if (!string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(ProducerName) && int.TryParse(Quantity, out int quantity))
            {
                if (quantity > 0)
                {
                    EquiptType equiptType = (EquiptType)ComboBoxItem;
                    Equipment equipment = new Equipment(equiptType, Name, quantity, ProducerName);
                    equipment.EquiptId = SelectedEquipment.EquiptId;
                    RoomController.Instance.UpdateEquipment(SelectedRoom,equipment);
                   
                    SelectedEquipment = null;
                    this.NavService.Navigate(
                    new Uri("ManagerView1/EquipmentView.xaml", UriKind.Relative));

                }

            }
        }

        public void SetSelectedRoom(object selectedItem)
        {
            SelectedRoom = (Room)selectedItem;
        }



    }
}
