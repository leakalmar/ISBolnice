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
    public class AddEquipmentViewModel:ViewModel
    {

        private EquipmentValidationDTO equipmentValidationDTO = new EquipmentValidationDTO();
        private int selectedEnum = 0;
        private NavigationService navService;
        private RelayCommand addNewEquipmentToRoom;
        private RelayCommand navigateToPreviousPage;
        public Room SelectedRoom { get; set; }

        public int SelectedEnum
        {
            get { return selectedEnum; }
            set
            {
                if (value != selectedEnum)
                {

                    selectedEnum = value;
                    OnPropertyChanged("SelectedEnum");
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

        public RelayCommand AddNewEquipmentToRoom
        {
            get { return addNewEquipmentToRoom; }
            set
            {
                addNewEquipmentToRoom = value;
            }
        }


        public EquipmentValidationDTO EquipmentValidationDTO
        {
            get { return equipmentValidationDTO; }
            set
            {
                if (value != equipmentValidationDTO)
                {

                    equipmentValidationDTO = value;
                    OnPropertyChanged("EquipmentValidationDTO");
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

        private static AddEquipmentViewModel instance = null;
        public static AddEquipmentViewModel Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new AddEquipmentViewModel();
                }
                return instance;
            }
        }
        private AddEquipmentViewModel()
        {

            this.AddNewEquipmentToRoom = new RelayCommand(Execute_AddNewEquipmentCommand);
            this.NavigateToPreviousPage = new RelayCommand(Execute_NavigateToPreviousPageCommand);
         
        }


        private void Execute_AddNewEquipmentCommand(object obj)
        {
           
            EquipmentValidationDTO.Validate();
            if (EquipmentValidationDTO.IsValid)
            {
                if (EquipmentValidationDTO.Name.Equals("bolnicki krevet"))
                {
                    for(int i = 0; i < Convert.ToInt32(EquipmentValidationDTO.Quantity) ; i++)
                    {
                        BedController.Instance.AddBed(new Bed(SelectedRoom.Id, false));
                        SelectedRoom.BedNumber = Convert.ToInt32(EquipmentValidationDTO.Quantity);
                        RoomController.Instance.UpdateRoom(SelectedRoom);

                    }

                }
             
                    EquiptType equiptType = (EquiptType)SelectedEnum;
                    Equipment equipment = new Equipment(equiptType, EquipmentValidationDTO.Name, Convert.ToInt32(EquipmentValidationDTO.Quantity),
                        EquipmentValidationDTO.ProducerName);

                    RoomController.Instance.AddEquipment(SelectedRoom, equipment);
                    EquipmentViewModel.Instance.SelectedEquipment = null;
                    EquipmentValidationDTO = new EquipmentValidationDTO();
                    this.NavService.Navigate(
                    new Uri("ManagerView1/EquipmentView.xaml", UriKind.Relative));
               
            }
        }

        private void Execute_NavigateToPreviousPageCommand(object obj)
        {
            EquipmentViewModel.Instance.SelectedEquipment = null;
            EquipmentValidationDTO = new EquipmentValidationDTO();
            this.NavService.GoBack();
        }

    }
}
