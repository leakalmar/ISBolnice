using Controllers;
using DTOs;
using Hospital_IS.DoctorRole.Commands;
using Hospital_IS.DoctorRole.DoctorView;
using Hospital_IS.DTOs;
using Hospital_IS.DTOs.SecretaryDTOs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;

//MVVM
namespace Hospital_IS.DoctorViewModel
{
    public class SearchMedicineViewModel : BindableBase
    {
        #region Feilds
        private PatientDTO patient;
        private ICollectionView medicineList;
        private ObservableCollection<PrescriptionDTO> prescriptions;
        private MedicineDTO selectedMedicine;
        private DateTime datePrescribed;
        private bool potentiallyAllergicMassage;
        private String searchText;

        public ICollectionView MedicineList

        {
            get { return medicineList; }
            set
            {
                medicineList = value;
                OnPropertyChanged("MedicineList");
            }
        }

        public ObservableCollection<PrescriptionDTO> Prescriptions

        {
            get { return prescriptions; }
            set
            {
                prescriptions = value;
                OnPropertyChanged("Prescriptions");
                List<MedicineDTO> list = MedicineController.Instance.GenerateListOfMedicines(Patient.Alergies, new List<PrescriptionDTO>(prescriptions));
                this.MedicineList = new CollectionViewSource { Source = list }.View;

            }
        }

        public MedicineDTO SelectedMedicine

        {
            get { return selectedMedicine; }
            set
            {
                selectedMedicine = value;
                OnPropertyChanged("SelectedMedicine");
                if (value != null)
                {
                    PotentiallyAllergicMassage = PatientController.Instance.CheckIfAllergicToComponent(Patient.Alergies, SelectedMedicine.Name);
                }
            }
        }

        public bool PotentiallyAllergicMassage
        {
            get { return potentiallyAllergicMassage; }
            set
            {
                potentiallyAllergicMassage = value;
                OnPropertyChanged("PotentiallyAllergicMassage");
            }
        }

        public PatientDTO Patient
        {
            get { return patient; }
            set
            {
                patient = value;
                OnPropertyChanged("Patient");
            }
        }

        public DateTime DatePrescribed
        {
            get { return datePrescribed; }
            set
            {
                datePrescribed = value;
                OnPropertyChanged("DatePrescribed");
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
        private RelayCommand addRemoveMedicineCommand;
        private RelayCommand gotFocusCommand;
        private RelayCommand lostFocusCommand;


        public RelayCommand AddRemoveMedicineCommand
        {
            get { return addRemoveMedicineCommand; }
            set { addRemoveMedicineCommand = value; }
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
        private bool CanExecute_Command(object obj)
        {
            return true;
        }

        private void Execute_AddRemoveMedicineCommand(object obj)

        {
            if (SelectedMedicine.Allergic == true)
            {
                PatientAllergicMassage();
                return;

            }

            if (SelectedMedicine.Check == false)
            {
                AddPrescription();
            }
            else
            {
                RemovePrescription();
            }

            OnPropertyChanged("Prescriptions");
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
        private void RemovePrescription()
        {
            foreach (PrescriptionDTO p in Prescriptions)
            {
                if (p.Medicine.Name.Equals(SelectedMedicine.Name))
                {
                    Prescriptions.Remove(p);
                    break;
                }
            }

            foreach (MedicineDTO medicine in medicineList)
            {
                if (medicine.Equals(SelectedMedicine))
                {
                    medicine.Check = false;
                }
            }
            PatientChartViewModel.Instance.ReportViewModel.Prescriptions = Prescriptions;
        }

        private void FilterMedicines()
        {
            if (MedicineList != null)
            {
                List<MedicineDTO> list = MedicineController.Instance.GenerateListOfMedicines(Patient.Alergies, new List<PrescriptionDTO>(prescriptions));
                ICollectionView view = new CollectionViewSource { Source = list }.View;
                view.Filter = null;
                if(SearchText != null && SearchText != "")
                {
                    view.Filter = delegate (object item)
                    {
                        MedicineDTO medicine = item as MedicineDTO;

                        return CheckIfMedicineMeetsSearchCriteria(medicine);
                    };
                }
                MedicineList = view;
                SelectedMedicine = list[0];
            }
        }
        private void AddPrescription()
        {
            Prescriptions.Add(new PrescriptionDTO(MedicineController.Instance.GetByName(SelectedMedicine.Name), DatePrescribed));
            foreach (MedicineDTO medicine in medicineList)
            {
                if (medicine.Equals(SelectedMedicine))
                {
                    medicine.Check = true;
                }
            }
            PatientChartViewModel.Instance.ReportViewModel.Prescriptions = Prescriptions;
        }

        private static void PatientAllergicMassage()
        {
            ExitMess mess = new ExitMess("Pacijent je alergican na izabrani lek!", "info");
            mess.btnCancle.Visibility = Visibility.Collapsed;
            mess.btnOk.Content = "U redu";
            mess.ShowDialog();
        }

        private bool CheckIfMedicineMeetsSearchCriteria(MedicineDTO medicine)
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

        private bool CompositionCriteria(MedicineDTO medicine)
        {
            foreach (MedicineComponentDTO c in medicine.Composition)
            {
                if (c.Component.Contains(SearchText))
                {
                    return true;
                }
            }
            return false;
        }
        #endregion

        #region Constructior
        public SearchMedicineViewModel()
        {
            SearchText = "Pretraži po nazivu ili sastavu..";
            this.AddRemoveMedicineCommand = new RelayCommand(Execute_AddRemoveMedicineCommand, CanExecute_Command);
            this.GotFocusCommand = new RelayCommand(Execute_GotFocusCommand, CanExecute_Command);
            this.LostFocusCommand = new RelayCommand(Execute_LostFocusCommand, CanExecute_Command);
        }
        #endregion
    }
}
