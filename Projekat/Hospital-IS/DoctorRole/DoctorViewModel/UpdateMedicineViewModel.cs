using Controllers;
using Hospital_IS.DoctorRole.Commands;
using Model;
using System.Collections.ObjectModel;
using System.Linq;

//MVVM
namespace Hospital_IS.DoctorViewModel
{
    public class UpdateMedicineViewModel : BindableBase
    {
        #region Fields
        private string newUsage;
        private string newSideEffects;
        private ObservableCollection<MedicineComponent> compositionOfMedicine;
        private ObservableCollection<ReplaceMedicineName> replaceMedicines;
        private MedicineComponent selectedComponent;
        private ReplaceMedicineName selectedReplace;
        private Medicine medicineWhichIsUpdated;

        public string NewUsage
        {
            get { return newUsage; }
            set
            {
                newUsage = value;
                OnPropertyChanged("NewUsage");
            }
        }
        public string NewSideEffects
        {
            get { return newSideEffects; }
            set
            {
                newSideEffects = value;
                OnPropertyChanged("NewSideEffects");
            }
        }

        public ObservableCollection<MedicineComponent> CompositionOfMedicine
        {
            get { return compositionOfMedicine; }
            set
            {
                compositionOfMedicine = value;
                OnPropertyChanged("CompositionOfMedicine");
            }
        }

        public ObservableCollection<ReplaceMedicineName> ReplaceMedicines
        {
            get { return replaceMedicines; }
            set
            {
                replaceMedicines = value;
                OnPropertyChanged("ReplaceMedicines");
            }
        }

        public MedicineComponent SelectedComponent
        {
            get { return selectedComponent; }
            set
            {
                selectedComponent = value;
                OnPropertyChanged("SelecteedComponent");
            }
        }

        public ReplaceMedicineName SelectedReplace
        {
            get { return selectedReplace; }
            set
            {
                selectedReplace = value;
                OnPropertyChanged("SelectedReplace");
            }
        }

        public Medicine MedicineWhichIsUpdated
        {
            get { return medicineWhichIsUpdated; }
            set
            {
                medicineWhichIsUpdated = value;
                ReplaceMedicines = new ObservableCollection<ReplaceMedicineName>(medicineWhichIsUpdated.ReplaceMedicine);
                CompositionOfMedicine = new ObservableCollection<MedicineComponent>(medicineWhichIsUpdated.Composition);
                NewSideEffects = medicineWhichIsUpdated.SideEffects;
                newUsage = medicineWhichIsUpdated.Usage;
                OnPropertyChanged("MedicineWhichIsUpdated");
            }
        }


        #endregion

        #region Commands
        private RelayCommand saveChangesCommand;
        private RelayCommand addCompositionCommand;
        private RelayCommand addReplaceCommand;
        private RelayCommand backCommand;

        public RelayCommand SaveChangesCommand
        {
            get { return saveChangesCommand; }
            set { saveChangesCommand = value; }
        }

        public RelayCommand AddCompositionCommand
        {
            get { return addCompositionCommand; }
            set { addCompositionCommand = value; }
        }

        public RelayCommand AddReplaceCommand
        {
            get { return addReplaceCommand; }
            set { addReplaceCommand = value; }
        }

        public RelayCommand BackCommand
        {
            get { return backCommand; }
            set { backCommand = value; }
        }
        #endregion

        #region Actions
        private void Execute_SaveChangesCommand(object obj)
        {
            MedicineWhichIsUpdated.Composition = CompositionOfMedicine.ToList();
            MedicineWhichIsUpdated.ReplaceMedicine = ReplaceMedicines.ToList();
            MedicineWhichIsUpdated.SideEffects = NewSideEffects;
            MedicineWhichIsUpdated.Usage = NewUsage;

            MedicineController.Instance.UpdateMedicine(MedicineWhichIsUpdated);
            DoctorNavigationController.Instance.NavigateToMedicinesCommand();
        }

        private void Execute_AddCompositionCommand(object obj)
        {
            MedicineComponent medicineComponent = new MedicineComponent("");
            CompositionOfMedicine.Add(medicineComponent);
            SelectedComponent = medicineComponent;
        }

        private void Execute_AddReplaceCommand(object obj)
        {
            ReplaceMedicineName newMedicine = new ReplaceMedicineName("");
            ReplaceMedicines.Add(newMedicine);
            SelectedReplace = newMedicine;
        }

        private void Execute_BackCommand(object obj)
        {
            DoctorNavigationController.Instance.NavigateToMedicinesCommand();
        }

        private bool CanExecute_Command(object obj)
        {
            return true;
        }

        #endregion

        #region Constructor
        public UpdateMedicineViewModel()
        {
            this.SaveChangesCommand = new RelayCommand(Execute_SaveChangesCommand, CanExecute_Command);
            this.AddCompositionCommand = new RelayCommand(Execute_AddCompositionCommand, CanExecute_Command);
            this.AddReplaceCommand = new RelayCommand(Execute_AddReplaceCommand, CanExecute_Command);
            this.BackCommand = new RelayCommand(Execute_BackCommand, CanExecute_Command);
        }

        #endregion
    }
}
