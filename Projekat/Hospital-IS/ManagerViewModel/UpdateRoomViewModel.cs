using Controllers;
using Enums;
using Hospital_IS.DTOs;
using Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Navigation;

namespace Hospital_IS.ManagerViewModel
{
    public class UpdateRoomViewModel:ViewModel
    {
        private RoomVallidationDTO roomVallidationDTO = new RoomVallidationDTO();
        private NavigationService navService;
        private RelayCommand updateRoomCommand;
        private RelayCommand navigateToPreviousPage;
        private Room room;
        private int selectedEnumIndex = 0;



        public RelayCommand NavigateToPreviousPage
        {
            get { return navigateToPreviousPage; }
            set
            {
                navigateToPreviousPage = value;
            }
        }

        public int SelectedEnumIndex
        {
            get
            {
                return selectedEnumIndex;
            }
            set
            {
                if (value != selectedEnumIndex)
                {
                    selectedEnumIndex = value;
                    OnPropertyChanged("RoomVallidationDTO");
                }

            }
        }

        public RoomVallidationDTO RoomVallidationDTO
        {
            get
            {
                return roomVallidationDTO;
            }
            set
            {
                if (value != roomVallidationDTO)
                {
                    roomVallidationDTO = value;
                    OnPropertyChanged("RoomVallidationDTO");
                }

            }
        }

        public RelayCommand UpdateRoomCommand
        {
            get { return updateRoomCommand; }
            set
            {
                updateRoomCommand = value;
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

        private static UpdateRoomViewModel instance = null;
        public static UpdateRoomViewModel Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new UpdateRoomViewModel();
                }
                return instance;
            }
        }
        private UpdateRoomViewModel()
        {
            this.UpdateRoomCommand = new RelayCommand(Execute_UpdateRoomCommand);
            this.NavigateToPreviousPage = new RelayCommand(Execute_NavigateToPreviousPage);

        }

        public void SetRoom(Room room)
        {
            RoomVallidationDTO.RoomNumber = room.RoomNumber.ToString();
            RoomVallidationDTO.RoomFloor = room.RoomFloor.ToString();
            RoomVallidationDTO.BedNumber = room.BedNumber.ToString();
            RoomVallidationDTO.SurfaceArea = room.SurfaceArea.ToString();
            SelectedEnumIndex = (int)room.Type;
            this.room = room;
        }


        private void Execute_UpdateRoomCommand(object obj)
        {
            RoomVallidationDTO.OldNumber = this.room.RoomNumber;
            RoomVallidationDTO.Validate();
            if (RoomVallidationDTO.IsValid)
            {

                RoomType type = (RoomType)SelectedEnumIndex;
                int roomNumber = Convert.ToInt32(RoomVallidationDTO.RoomNumber);
                int roomFloor = Convert.ToInt32(RoomVallidationDTO.RoomFloor);
                int bedNumber = Convert.ToInt32(RoomVallidationDTO.BedNumber);
                int surfaceArea = Convert.ToInt32(RoomVallidationDTO.SurfaceArea);
           
                Room updateRoom = new Room(roomFloor, roomNumber, surfaceArea, bedNumber, this.room.Id, type);
                RoomController.Instance.UpdateRoom(updateRoom);
                this.NavService.GoBack();
                this.RoomVallidationDTO = new RoomVallidationDTO();
            }
        }

        private void Execute_NavigateToPreviousPage(object obj)
        {
            this.NavService.GoBack();
        }

    }
}
