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
    public class UpdateEquipmentViewModel:ViewModel
    {
        private EquipmentValidationDTO equipmentValidationDTO = new EquipmentValidationDTO();
        private int selectedEnum = 0;
        private NavigationService navService;
        private RelayCommand updateEquipmentCommand;
        private RelayCommand navigateToPreviousPage;
        public Room SelectedRoom { get; set; }
        public Equipment Equipment { get; set; }

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

        public RelayCommand UpdateEquipmentCommand
        {
            get { return updateEquipmentCommand; }
            set
            {
                updateEquipmentCommand = value;
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

        private static UpdateEquipmentViewModel instance = null;
        public static UpdateEquipmentViewModel Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new UpdateEquipmentViewModel();
                }
                return instance;
            }
        }
        private UpdateEquipmentViewModel()
        {

            this.UpdateEquipmentCommand = new RelayCommand(Execute_UpdateEquipmentCommand);
            this.NavigateToPreviousPage = new RelayCommand(Execute_NavigateToPreviousPageCommand);

        }

        public void SetEquipment(Equipment equipment)
        {
            EquipmentValidationDTO.Name = equipment.Name;
            EquipmentValidationDTO.ProducerName = equipment.ProducerName;
            EquipmentValidationDTO.Quantity = equipment.Quantity.ToString();
            SelectedEnum = (int)equipment.EquipType;
            this.Equipment = equipment;
        }

        private void  Execute_UpdateEquipmentCommand(object obj)
        {

            EquipmentValidationDTO.Validate();
            if (EquipmentValidationDTO.IsValid)
            {

                EquiptType equiptType = (EquiptType)SelectedEnum;
                Equipment equipment = new Equipment(equiptType, EquipmentValidationDTO.Name, Convert.ToInt32(EquipmentValidationDTO.Quantity),
                    EquipmentValidationDTO.ProducerName);
                equipment.EquiptId = this.Equipment.EquiptId;
                RoomController.Instance.UpdateEquipment(SelectedRoom, equipment);
                EquipmentViewModel.Instance.SelectedEquipment = null;
                EquipmentValidationDTO = new EquipmentValidationDTO();
                this.NavService.Navigate(
                new Uri("ManagerView1/EquipmentView.xaml", UriKind.Relative));
            }
        }

        private void Execute_NavigateToPreviousPageCommand(object obj)
        {
            EquipmentValidationDTO = new EquipmentValidationDTO();
            EquipmentValidationDTO = new EquipmentValidationDTO();
            this.NavService.GoBack();

        }
    }
}
