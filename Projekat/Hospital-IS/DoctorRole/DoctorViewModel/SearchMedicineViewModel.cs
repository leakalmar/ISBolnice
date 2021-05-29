using Controllers;
using DTOs;
using Hospital_IS.DoctorRole.Commands;
using Hospital_IS.DoctorRole.DoctorView;
using Hospital_IS.DTOs.SecretaryDTOs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Navigation;

namespace Hospital_IS.DoctorViewModel
{
    public class SearchMedicineViewModel : BindableBase
    {
        #region Feilds
        private PatientDTO patient;
        private ObservableCollection<MedicineDTO> medicineList;
        private ObservableCollection<PrescriptionDTO> prescriptions;
        private MedicineDTO selectedMedicine;
        private NavigationService mainNavigationService;
        private DateTime datePrescribed;
        private bool potentiallyAllergicMassage;

        public NavigationService MainNavigationService
        {
            get { return mainNavigationService; }
            set
            {
                mainNavigationService = value;
            }
        }

        public ObservableCollection<MedicineDTO> MedicineList

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
                this.MedicineList = new ObservableCollection<MedicineDTO>(MedicineController.Instance.GenerateListOfMedicines(Patient.Alergies, new List<PrescriptionDTO>(prescriptions)));
            }
        }

        public MedicineDTO SelectedMedicine

        {
            get { return selectedMedicine; }
            set
            {
                selectedMedicine = value;
                OnPropertyChanged("SelectedMedicine");
                if(value != null)
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
        #endregion

        #region Commands
        private RelayCommand addRemoveMedicineCommand;


        public RelayCommand AddRemoveMedicineCommand
        {
            get { return addRemoveMedicineCommand; }
            set
            {
                addRemoveMedicineCommand = value;
            }
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
        #endregion

        #region Methods
        private void RemovePrescription()
        {
            foreach(PrescriptionDTO p in Prescriptions)
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
            PatientChart.Instance._ViewModel.ReportView._ViewModel.Prescriptions = Prescriptions;
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
            PatientChart.Instance._ViewModel.ReportView._ViewModel.Prescriptions = Prescriptions;
        }

        private static void PatientAllergicMassage()
        {
            ExitMess mess = new ExitMess("Pacijent je alergican na izabrani lek!");
            mess.btnCancle.Visibility = Visibility.Collapsed;
            mess.btnOk.Content = "U redu";
            mess.ShowDialog();
        }
        #endregion

        #region Constructior
        public SearchMedicineViewModel()
        {
            this.AddRemoveMedicineCommand = new RelayCommand(Execute_AddRemoveMedicineCommand, CanExecute_Command);
        }
        #endregion
    }
}
