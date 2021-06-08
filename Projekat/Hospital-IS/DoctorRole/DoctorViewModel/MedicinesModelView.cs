using Controllers;
using Hospital_IS.DoctorRole.Commands;
using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Data;

//MVVM
namespace Hospital_IS.DoctorViewModel
{
    public class MedicinesModelView : BindableBase
    {
        #region Fields
        private ICollectionView medicines;
        private Medicine selectedMedicine;
        private String searchText;

        public ICollectionView Medicines
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
        public String SearchText
        {
            get { return searchText; }
            set
            {
                searchText = value;
                OnPropertyChanged("SearchText");
                FilterMedicines();
            }
        }
        #endregion

        #region Commands
        private RelayCommand viewMedicineCommand;
        private RelayCommand gotFocusCommand;
        private RelayCommand lostFocusCommand;

        public RelayCommand ViewMedicineCommand
        {
            get { return viewMedicineCommand; }
            set { viewMedicineCommand = value; }
        }
        public RelayCommand GotFocusCommand
        {
            get { return gotFocusCommand; }
            set { gotFocusCommand = value; }
        }
        public RelayCommand LostFocusCommand
        {
            get { return lostFocusCommand; }
            set { lostFocusCommand = value; }
        }
        #endregion

        #region Actions
        private void Execute_ViewMedicineCommand(object obj)
        {
            DoctorNavigationController.Instance.NavigateToUpdateMedicineCommand(SelectedMedicine);
        }

        private bool CanExecute_Command(object obj)
        {
            return true;
        }
        private void Execute_GotFocusCommand(object obj)
        {
            if (SearchText.Equals("Pretraži po nazivu ili sastavu.."))
            {
                SearchText = string.Empty;
            }
        }
        private void Execute_LostFocusCommand(object obj)
        {
            if (string.IsNullOrEmpty(SearchText))
            {
                SearchText = "Pretraži po nazivu ili sastavu..";
            }
        }
        #endregion

        #region Methods
        private void FilterMedicines()
        {
            if (Medicines != null)
            {
                List<Medicine> list = MedicineController.Instance.GetAll();
                ICollectionView view = new CollectionViewSource { Source = list }.View;
                view.Filter = null;
                if (SearchText != null && SearchText != "")
                {
                    view.Filter = delegate (object item)
                    {
                        Medicine medicine = item as Medicine;

                        return CheckIfMedicineMeetsSearchCriteria(medicine);
                    };
                }
                Medicines = view;
                SelectedMedicine = list[0];
            }
        }
        private bool CheckIfMedicineMeetsSearchCriteria(Medicine medicine)
        {
            if (!SearchText.Equals("Pretraži po nazivu ili sastavu.."))
            {
                return medicine.Name.ToLower().Contains(SearchText) || CompositionCriteria(medicine);
            }
            else
            {
                return true;
            }
        }

        private bool CompositionCriteria(Medicine medicine)
        {
            foreach (MedicineComponent c in medicine.Composition)
            {
                if (c.Component.Contains(SearchText))
                {
                    return true;
                }
            }
            return false;
        }
        #endregion

        #region Constructor
        public MedicinesModelView()
        {
            this.ViewMedicineCommand = new RelayCommand(Execute_ViewMedicineCommand, CanExecute_Command);
            List<Medicine> list = MedicineController.Instance.GetAll();
            ICollectionView view = new CollectionViewSource { Source = list }.View;
            this.Medicines = view;
            this.GotFocusCommand = new RelayCommand(Execute_GotFocusCommand, CanExecute_Command);
            this.LostFocusCommand = new RelayCommand(Execute_LostFocusCommand, CanExecute_Command);
        }
        #endregion
    }
}
