using Controllers;
using DTOs;
using Hospital_IS.Commands;
using Hospital_IS.DoctorView;
using Model;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Navigation;

namespace Hospital_IS.DoctorViewModel
{
    public class SearchMedicineViewModel : BindableBase
    {
        #region Feilds
        private Patient patient;
        private ObservableCollection<MedicineDTO> medicineList;
        private ObservableCollection<Prescription> prescriptions;
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

        public ObservableCollection<Prescription> Prescriptions

        {
            get { return prescriptions; }
            set
            {
                prescriptions = value;
                OnPropertyChanged("Prescriptions");
                this.MedicineList = new ObservableCollection<MedicineDTO>(MedicineController.Instance.GenerateListOfMedicines(Patient));
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
                    PotentiallyAllergicMassage = PatientController.Instance.CheckIfAllergicToComponent(patient, SelectedMedicine.Medicine);
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

        public Patient Patient
        {
            get { return patient; }
            set
            {
                patient = value;
                OnPropertyChanged("Patient");
                this.MedicineList = new ObservableCollection<MedicineDTO>(MedicineController.Instance.GenerateListOfMedicines(Patient));
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
            foreach(Prescription p in Prescriptions)
            {
                if (p.Medicine.Equals(SelectedMedicine.Medicine))
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
            Prescriptions.Add(new Prescription(SelectedMedicine.Medicine, DatePrescribed));
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
