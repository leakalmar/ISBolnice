using Controllers;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Navigation;

namespace Hospital_IS.ManagerViewModel
{
    public class EquipmentViewModel: ViewModel
    {

        private ObservableCollection<Room> _rooms { get; set; }
        private ObservableCollection<String> roomTypes { get; set; }

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
       



         public Room SelectedRoom
         {
            get { return selectedRoom; }
            set
            {   
                    selectedRoom = value;
                    if(selectedRoom != null)
                {
                    Equipments = new CollectionViewSource { Source = selectedRoom.Equipment }.View;
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

        }

      

       

        private bool CanExecute_NavigateCommand(object obj)
        {
            return true;
        }


        private void LoadRooms()
        {
            
            Rooms = new ObservableCollection<Room>(RoomController.Instance.GetAllRooms());
        }
        private void LoadEquipment()
        {

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
