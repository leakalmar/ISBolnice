using Controllers;
using Model;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Navigation;

namespace Hospital_IS.ManagerViewModel
{
    class EquipmentTransferStaticViewModel:ViewModel
    {
        private ObservableCollection<Equipment> equipmentsFirstRoom;
        private ObservableCollection<Equipment> equipmentSecondRoom;
        private Equipment selectedEquipmentFirst;
        private ObservableCollection<Room> roomsStaticTransferFirstBox;
        private ObservableCollection<Room> roomsStaticTransferSecondBox;
        private RelayCommand transferStaticEquipmentCommand;
        private RelayCommand navigateToPreviousPage;
        private Room selectedRoomFirst;
        private Room selectedRoomSecond;
        private string transferAmount;
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



        public RelayCommand TransferStaticEquipmentCommand
        {
            get { return transferStaticEquipmentCommand; }
            set
            {
                transferStaticEquipmentCommand = value;
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
                    if (SelectedRoomFirst != null)
                    {
                        if (SelectedRoomFirst.Id == SelectedRoomSecond.Id)
                        {
                            MessageBox.Show("Izabrali ste istu sobu");

                            EquipmentSecondRoom = new ObservableCollection<Equipment>();
                            return;
                         
                        }
                        else
                        {

                            EquipmentSecondRoom = new ObservableCollection<Equipment>(SelectedRoomSecond.Equipment);
                           
                        }
                    }
                    else
                    {
                        
                        if (SelectedRoomSecond == null)
                        {
                          
                            EquipmentSecondRoom = new ObservableCollection<Equipment>();
                        }
                        else
                        {
                            EquipmentSecondRoom = new ObservableCollection<Equipment>(SelectedRoomSecond.Equipment);
                        }
                        
                    }
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

                    if(SelectedRoomSecond != null && SelectedRoomFirst != null)
                    {
                        if (SelectedRoomFirst.Id == SelectedRoomSecond.Id)
                        {
                            MessageBox.Show("Izabrali ste istu sobu");
                            EquipmentsFirstRoom = new ObservableCollection<Equipment>();
                            SelectedRoomFirst = null;
                            OnPropertyChanged("SelectedRoomFirst");
                        }
                        else
                        {

                            EquipmentsFirstRoom = new ObservableCollection<Equipment>(SelectedRoomFirst.Equipment);
                            OnPropertyChanged("SelectedRoomFirst");
                        }
                    }
                    else
                    {
                        if (SelectedRoomFirst == null)
                        {

                            EquipmentsFirstRoom = new ObservableCollection<Equipment>();
                        }
                        else
                        {
                            EquipmentsFirstRoom = new ObservableCollection<Equipment>(SelectedRoomFirst.Equipment);
                        }
                       
                        
                    }
                  
                   
                   

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

        private static EquipmentTransferStaticViewModel instance = null;
        public static EquipmentTransferStaticViewModel Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new EquipmentTransferStaticViewModel();
                }
                return instance;
            }
        }
        private EquipmentTransferStaticViewModel()
        {
            RoomsStaticTransferFirstBox = new ObservableCollection<Room>(RoomController.Instance.GetAllRooms());
            RoomsStaticTransferSecondBox = new ObservableCollection<Room>(RoomController.Instance.GetAllRooms());
            this.TransferStaticEquipmentCommand = new RelayCommand(Execute_TransferStaticEquipment, CanExecute_NavigateToChooseAppViewCommand);
            this.NavigateToPreviousPage = new RelayCommand(Execute_NavigateToPreviousPage);
        }


        private void Execute_NavigateToPreviousPage(object obj)
        {
            this.NavService.GoBack();
            SelectedRoomFirst = null;
            SelectedRoomSecond = null;
            SelectedEquipmentFirst = null;
            TransferAmount = null;
            EquipmentSecondRoom = new ObservableCollection<Equipment>();
            EquipmentsFirstRoom = new ObservableCollection<Equipment>();
        }





        private void Execute_TransferStaticEquipment(object obj)
        {

            if (RoomController.Instance.CheckQuantity(SelectedRoomFirst, SelectedEquipmentFirst, Convert.ToInt32(TransferAmount))) 
            {
                ScheduleStaticTransferViewModel.Instance.SetAppointmentsForRoom(SelectedRoomFirst.Id, SelectedRoomSecond.Id);
                ScheduleStaticTransferViewModel.Instance.SourceRoom = SelectedRoomFirst;
                ScheduleStaticTransferViewModel.Instance.DestinationRoom = SelectedRoomSecond;
                ScheduleStaticTransferViewModel.Instance.Quantity = Convert.ToInt32(TransferAmount);
                ScheduleStaticTransferViewModel.Instance.NavService = NavService;
                ScheduleStaticTransferViewModel.Instance.Equipment = SelectedEquipmentFirst;
                this.NavService.Navigate(
                   new Uri("ManagerView1/ScheduleStaticAppTransfer.xaml", UriKind.Relative));

               
            }else
            {
                MessageBox.Show("Nedovoljna kolicina opreme");
            }
        }

        private bool CanExecute_NavigateToChooseAppViewCommand(object obj)
        {

            return isNumberIsGraterThanZero() && SelectedEquipmentFirst != null && SelectedRoomSecond!= null && (SelectedRoomSecond.Id != SelectedRoomFirst.Id);
        }

        private bool isNumberIsGraterThanZero()
        {

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

