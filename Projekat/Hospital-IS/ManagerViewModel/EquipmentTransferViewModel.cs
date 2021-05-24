using Controllers;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Navigation;

namespace Hospital_IS.ManagerViewModel
{
    public class EquipmentTransferViewModel:ViewModel
    {
        private ObservableCollection<Equipment> equipmentsFirstRoom;
        private ObservableCollection<Equipment> equipmentSecondRoom;
        private Equipment selectedEquipmentFirst;
        private ObservableCollection<Room> roomsDynamicTransfer;
        private ObservableCollection<Room> roomsStaticTransferFirstBox;
        private ObservableCollection<Room> roomsStaticTransferSecondBox;
        private RelayCommand transferDynamicEquipmentCommand;
        private Room selectedRoomFirst;
        private Room selectedRoomSecond;
        private string transferAmount = "haloo";
        private NavigationService navService;



        public string TransferAmount
        {
            get
            {
                return transferAmount;
            }
            set
            {
                if (value != transferAmount)
                {
                   
                    transferAmount = value;
                    OnPropertyChanged("TransferAmount");

                }
            }
        }

        public RelayCommand TransferDynamicEquipmentCommand
        {
            get { return transferDynamicEquipmentCommand; }
            set
            {
                transferDynamicEquipmentCommand = value;
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
                    EquipmentSecondRoom = new ObservableCollection<Equipment>(SelectedRoomSecond.Equipment);
                    OnPropertyChanged("SelectedRoomSecond");

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
                    EquipmentsFirstRoom = new ObservableCollection<Equipment>(SelectedRoomFirst.Equipment);
                    OnPropertyChanged("SelectedRoomFirst");

                }
            }
        }

        public Equipment SelectedEquipmentFirst
        {
            get
            {
                return selectedEquipmentFirst;
            }
            set
            {
                if (value != selectedEquipmentFirst)
                {

                    selectedEquipmentFirst = value;
                    OnPropertyChanged("SelectedEquipmentFirst");

                }
            }
        }

        

        public ObservableCollection<Room> RoomsDynamicTransfer
        {
            get
            {
                return roomsDynamicTransfer;
            }
            set
            {
                if (value != roomsDynamicTransfer)
                {

                    roomsDynamicTransfer = value;
                    OnPropertyChanged("RoomsDynamicTransfer");

                }
            }
        }

        public ObservableCollection<Room> RoomsStaticTransferFirstBox
        {
            get
            {
                return roomsStaticTransferFirstBox;
            }
            set
            {
                if (value != roomsStaticTransferFirstBox)
                {

                    roomsStaticTransferFirstBox = value;
                    OnPropertyChanged("RoomsStaticTransferFirstBox");

                }
            }
        }

        public ObservableCollection<Room> RoomsStaticTransferSecondBox
        {
            get
            {
                return roomsStaticTransferSecondBox;
            }
            set
            {
                if (value != roomsStaticTransferSecondBox)
                {

                    roomsStaticTransferSecondBox = value;
                    OnPropertyChanged("RoomsStaticTransferSecondBox");

                }
            }
        }

        public ObservableCollection<Equipment> EquipmentsFirstRoom
        {
            get
            {
                return equipmentsFirstRoom;
            }
            set
            {
                if (value != equipmentsFirstRoom)
                {

                    equipmentsFirstRoom = value;
                    OnPropertyChanged("EquipmentsFirstRoom");

                }
            }
        }

        public ObservableCollection<Equipment> EquipmentSecondRoom
        {
            get
            {
                return equipmentSecondRoom;
            }
            set
            {
                if (value != equipmentSecondRoom)
                {

                    equipmentSecondRoom = value;
                    OnPropertyChanged("EquipmentSecondRoom");

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

        private static EquipmentTransferViewModel instance = null;
        public static EquipmentTransferViewModel Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new EquipmentTransferViewModel();
                }
                return instance;
            }
        }
        private EquipmentTransferViewModel()
        {

            RoomsDynamicTransfer = new ObservableCollection<Room>(RoomController.Instance.GetRoomByType(RoomType.StorageRoom));
            RoomsStaticTransferFirstBox = new ObservableCollection<Room>(RoomController.Instance.GetAllRooms());
            RoomsStaticTransferSecondBox = new ObservableCollection<Room>(RoomController.Instance.GetAllRooms());
            this.TransferDynamicEquipmentCommand = new RelayCommand(Execute_TransferDynamicEquipment, CanExecute_NavigateToTransferViewCommand);
          

        }

        private void Execute_TransferDynamicEquipment(object obj)
        {

            if(RoomController.Instance.CheckQuantity(SelectedRoomFirst, SelectedEquipmentFirst, Convert.ToInt32(TransferAmount)));
            {
                TransferController.Instance.ReduceEquipmentQuantity(SelectedRoomFirst, SelectedEquipmentFirst, Convert.ToInt32(TransferAmount));
                EquipmentsFirstRoom = new ObservableCollection<Equipment>(SelectedRoomFirst.Equipment);
            }
        }

        private bool CanExecute_NavigateToTransferViewCommand(object obj)
        {

            return isNumberIsGraterThanZero() && SelectedEquipmentFirst != null;
          }

        private bool isNumberIsGraterThanZero() {

            if (TransferAmount != null)
            {
                bool isEmpty = false;
                if (TransferAmount.Equals(""))
                {
                    isEmpty = true;
                }

                bool isIntString = TransferAmount.All(char.IsDigit);

                bool isNumberOverZero = true;
                if (!isIntString || isEmpty == true)
                {

                    isNumberOverZero = false;
                }
                return isNumberOverZero;
            }
            else
            {
                return false;
            }
        }
    }
}
