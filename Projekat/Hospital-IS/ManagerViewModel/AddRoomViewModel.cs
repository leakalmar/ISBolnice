using Controllers;
using Enums;
using Hospital_IS.DTOs;
using Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Navigation;

namespace Hospital_IS.ManagerViewModel
{
    public class AddRoomViewModel:ViewModel
    {
        private RoomVallidationDTO roomVallidationDTO = new RoomVallidationDTO();
        private NavigationService navService;
        private RelayCommand addRoomCommand;
        private RelayCommand navigateToPreviousPage;
        private int selectedEnumIndex =0;


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

        public RelayCommand AddRoomCommand
        {
            get { return addRoomCommand; }
            set
            {
                addRoomCommand = value;
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

        private static AddRoomViewModel instance = null;
        public static AddRoomViewModel Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new AddRoomViewModel();
                }
                return instance;
            }
        }
        private AddRoomViewModel()
        {
            this.AddRoomCommand = new RelayCommand(Execute_AddRoomCommand);
            this.NavigateToPreviousPage = new RelayCommand(Execute_NavigateToPreviousPage);

        }

        private void Execute_AddRoomCommand(object obj)
        {
            RoomVallidationDTO.Validate();
            if (RoomVallidationDTO.IsValid)
            {
              
                RoomType type = (RoomType)SelectedEnumIndex;
                int roomNumber = Convert.ToInt32(RoomVallidationDTO.RoomNumber);
                int roomFloor = Convert.ToInt32(RoomVallidationDTO.RoomFloor);
                int bedNumber = Convert.ToInt32(RoomVallidationDTO.BedNumber);
                int surfaceArea = Convert.ToInt32(RoomVallidationDTO.SurfaceArea);
                RoomController.Instance.AddRoom(new Room(roomFloor,roomNumber,surfaceArea,bedNumber,type));
               
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
