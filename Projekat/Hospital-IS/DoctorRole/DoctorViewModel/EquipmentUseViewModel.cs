using Enums;
using Hospital_IS.DoctorRole.Commands;
using Hospital_IS.DTOs;
using Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;

//MVVM
namespace Hospital_IS.DoctorRole.DoctorViewModel
{
    public class EquipmentUseViewModel : BindableBase
    {
        #region Feilds
        private string searchText;
        private int quantity;
        private List<Equipment> equipmentList;
        private ObservableCollection<UsedEquipmentDTO> enteredEquipment;
        private Equipment selectedEquipment;

        public string SearchText
        {
            get { return searchText; }
            set
            {
                searchText = value;
                OnPropertyChanged("SearchText");
            }
        }

        public int Quantity
        {
            get { return quantity; }
            set
            {
                quantity = value;
                OnPropertyChanged("Quantity");
            }
        }

        public List<Equipment> EquipmentList
        {
            get { return equipmentList; }
            set
            {
                equipmentList = value;
                OnPropertyChanged("EquipmentList");
            }
        }

        public ObservableCollection<UsedEquipmentDTO> EnteredEquipment
        {
            get { return enteredEquipment; }
            set
            {
                enteredEquipment = value;
                OnPropertyChanged("EnteredEquipment");
            }
        }

        public Equipment SelectedEquipment
        {
            get { return selectedEquipment; }
            set
            {
                selectedEquipment = value;
                OnPropertyChanged("SelectedEquipment");
            }
        }
        #endregion

        #region Commands;
        private RelayCommand newInputCommand;

        public RelayCommand NewInputCommand
        {
            get { return newInputCommand; }
            set { newInputCommand = value; }
        }
        #endregion

        #region Actions
        private bool CanExecute_Command(object obj)
        {
            return true;
        }

        private void Execute_NewInputCommand(object obj)
        {
            EnteredEquipment.Add(new UsedEquipmentDTO(SelectedEquipment.Name, Quantity));
        }
        #endregion

        #region Constructor
        public EquipmentUseViewModel()
        {
            this.Quantity = 1;
            this.EnteredEquipment = new ObservableCollection<UsedEquipmentDTO>();
            this.EquipmentList = new List<Equipment>();
            EquipmentList.Add(new Equipment(EquiptType.Dynamic, "Makaze", 10, "Proizvodjac1"));
            EquipmentList.Add(new Equipment(EquiptType.Dynamic, "Gaze", 10, "Proizvodjac2"));
            EquipmentList.Add(new Equipment(EquiptType.Dynamic, "Rukavice", 10, "Proizvodjac3"));
            EquipmentList.Add(new Equipment(EquiptType.Dynamic, "Igle", 10, "Proizvodjac4"));
            this.NewInputCommand = new RelayCommand(Execute_NewInputCommand, CanExecute_Command);
        }
        #endregion
    }
}
