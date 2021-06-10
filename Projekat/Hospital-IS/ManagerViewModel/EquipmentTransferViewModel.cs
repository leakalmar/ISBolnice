using Controllers;
using Enums;
using Model;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Navigation;

namespace Hospital_IS.ManagerViewModel
{
    public class EquipmentTransferViewModel:ViewModel
    {
        private ObservableCollection<Equipment> equipmentsFirstRoom;
        private Equipment selectedEquipmentFirst;
        private ObservableCollection<Room> roomsDynamicTransfer;
        private RelayCommand transferDynamicEquipmentCommand;
        private RelayCommand navigateToPreviousPage;
        private Room selectedRoomFirst;
        private string transferAmount ;
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

        public RelayCommand NavigateToPreviousPage
        {
            get { return navigateToPreviousPage; }
            set
            {
                navigateToPreviousPage = value;
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
                    if (SelectedRoomFirst != null)
                    {
                        EquipmentsFirstRoom = new ObservableCollection<Equipment>(SelectedRoomFirst.Equipment);
                    }
                    else
                    {
                        EquipmentsFirstRoom = new ObservableCollection<Equipment>();
                    }

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
           

            this.TransferDynamicEquipmentCommand = new RelayCommand(Execute_TransferDynamicEquipment, CanExecute_NavigateToTransferViewCommand);
            this.NavigateToPreviousPage = new RelayCommand(Execute_NavigateToPreviousPage);
          

        }

        private void Execute_NavigateToPreviousPage(object obj)
        {
            this.NavService.GoBack();
            SelectedRoomFirst = null;
            TransferAmount = "";
          
        }



        private void Execute_TransferDynamicEquipment(object obj)
        {

            if(RoomController.Instance.CheckQuantity(SelectedRoomFirst, SelectedEquipmentFirst, Convert.ToInt32(TransferAmount)))
            {
                TransferController.Instance.ReduceEquipmentQuantity(SelectedRoomFirst, SelectedEquipmentFirst, Convert.ToInt32(TransferAmount));
                EquipmentsFirstRoom = new ObservableCollection<Equipment>(SelectedRoomFirst.Equipment);
            }
            else
            {
                MessageBox.Show("Nedovoljna kolicina opreme");
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
