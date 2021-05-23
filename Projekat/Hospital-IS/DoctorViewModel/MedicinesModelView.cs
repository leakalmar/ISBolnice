using Controllers;
using Hospital_IS.Commands;
using Hospital_IS.DoctorView;
using Model;
using System.Collections.Generic;

namespace Hospital_IS.DoctorViewModel
{
    public class MedicinesModelView : BindableBase
    {
        #region Fields
        private List<Medicine> medicines;
        private Medicine selectedMedicine;
        
        public List<Medicine> Medicines
        {
            get { return medicines; }
            set
            {
                medicines = value;
                OnPropertyChanged("Medicines");
            }
        }

        public Medicine SelectedMedicine
        {
            get { return selectedMedicine; }
            set
            {
                selectedMedicine = value;
                OnPropertyChanged("SelectedMedicine");
            }
        }
        #endregion

        #region Commands
        private RelayCommand viewMedicineCommand;

        public RelayCommand ViewMedicineCommand
        {
            get { return viewMedicineCommand; }
            set { viewMedicineCommand = value; }
        }

        #endregion

        #region Actions
        private void Execute_ViewMedicineCommand(object obj)
        {
            DoctorMainWindow.Instance._ViewModel.NavigateToUpdateMedicineCommand.Execute(SelectedMedicine);
        }

        private bool CanExecute_Command(object obj)
        {
            return true;
        }
        #endregion

        #region Constructor
        public MedicinesModelView()
        {
            this.ViewMedicineCommand = new RelayCommand(Execute_ViewMedicineCommand, CanExecute_Command);
            this.Medicines = MedicineController.Instance.GetAll();
        }
        #endregion
    }
}
