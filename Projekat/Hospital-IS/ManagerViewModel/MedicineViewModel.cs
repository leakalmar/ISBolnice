using Controllers;
using Hospital_IS.DTOs;
using Hospital_IS.ManagerView1;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Navigation;

namespace Hospital_IS.ManagerViewModel
{
    public class MedicineViewModel:ViewModel
    {
        private string name;
        private string sideEffects;
        private string usage;
        private ObservableCollection<MedicineComponent> composition;
        private ObservableCollection<ReplaceMedicineName> replaceMedicines;
        private ObservableCollection<Medicine> medicines;
        private ObservableCollection<MedicineComponentDTO> compositionDTO;
        private ObservableCollection<ReplaceMedicineNameDTO> replaceMedicineNameDTOs;
        private NavigationService navService;
        private RelayCommand navigateToRoomPageCommand;
        private RelayCommand navigateToMedicineInsightPage;
        private RelayCommand navigateToPreviousPage;
        private RelayCommand navigateToUpdateMedicinePage;
        private RelayCommand navigateMedicineRegistrationPage;
        private RelayCommand registrateNewMedicineCommand;
        private RelayCommand updateMedicineCommand;
        private RelayCommand deleteMedicineCommand;
        private Medicine selectedMedicine;

        public Medicine SelectedMedicine
        {
            get
            {
                return selectedMedicine;
            }
            set
            {
                if (value != selectedMedicine)
                {

                    selectedMedicine = value;
                    OnPropertyChanged("SelectedMedicine");

                }
            }
        }

        public RelayCommand DeleteMedicineCommand
        {
            get { return deleteMedicineCommand; }
            set
            {
                deleteMedicineCommand = value;
            }
        }


        public RelayCommand UpdateMedicineCommand
        {
            get { return updateMedicineCommand; }
            set
            {
                updateMedicineCommand = value;
            }
        }

        public RelayCommand RegistrateNewMedicineCommand
        {
            get { return registrateNewMedicineCommand; }
            set
            {
                registrateNewMedicineCommand = value;
            }
        }

        public RelayCommand NavigateMedicineRegistrationPage
        {
            get { return navigateMedicineRegistrationPage; }
            set
            {
                navigateMedicineRegistrationPage = value;
            }
        }

        public RelayCommand NavigateToUpdateMedicinePage
        {
            get { return navigateToUpdateMedicinePage; }
            set
            {
                navigateToUpdateMedicinePage = value;
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
        public RelayCommand NavigateToMedicineInsightPage
        {
            get { return navigateToMedicineInsightPage; }
            set
            {
                navigateToMedicineInsightPage = value;
            }
        }
        public RelayCommand NavigateToRoomPageCommand
        {
            get { return navigateToRoomPageCommand; }
            set
            {
                navigateToRoomPageCommand = value;
            }
        }

        public String Name 
        {
            get
            {
                return name;
            }
            set
            {
                if (value != name)
                {

                    name = value;
                    OnPropertyChanged("Name");

                }
            }
        }
        public String SideEffects
        {
            get
            {
                return sideEffects;
            }
            set
            {
                if (value != sideEffects)
                {

                    sideEffects = value;
                    OnPropertyChanged("SideEffects");

                }
            }
        }

        public String Usage
        {
            get
            {
                return usage;
            }
            set
            {
                if (value != usage)
                {

                    usage = value;
                    OnPropertyChanged("Usage");

                }
            }
        }
        
        public ObservableCollection<ReplaceMedicineNameDTO> ReplaceMedicineNameDTOs
        {
            get
            {
                return replaceMedicineNameDTOs;
            }
            set
            {
                if (value != replaceMedicineNameDTOs)
                {

                    replaceMedicineNameDTOs = value;
                    OnPropertyChanged("ReplaceMedicineNameDTOs");

                }
            }
        }
        public ObservableCollection<MedicineComponentDTO> CompositionDTO
        {
            get
            {
                return compositionDTO;
            }
            set
            {
                if (value != compositionDTO)
                {

                    compositionDTO = value;
                    OnPropertyChanged("CompositionDTO");

                }
            }
        }

        public ObservableCollection<Medicine> Medicines
        {
            get
            {
                return medicines;
            }
            set
            {
                if (value != medicines)
                {

                    medicines = value;
                    OnPropertyChanged("Medicines");

                }
            }
        }
        public ObservableCollection<MedicineComponent> Composition
        {
            get
            {
                return composition;
            }
            set
            {
                if (value != composition)
                {

                    composition = value;
                    OnPropertyChanged("Composition");

                }
            }
        }

        public ObservableCollection<ReplaceMedicineName> ReplaceMedicines
        {
            get
            {
                return replaceMedicines;
            }
            set
            {
                if (value != replaceMedicines)
                {

                    replaceMedicines = value;
                    OnPropertyChanged("ReplaceMedicines");

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

        private static MedicineViewModel instance = null;
        public static MedicineViewModel Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MedicineViewModel();
                }
                return instance;
            }
        }
        private MedicineViewModel()
        {

            
            Medicines = new ObservableCollection<Medicine>(MedicineController.Instance.GetAll());
            ReplaceMedicineNameDTOs = new ObservableCollection<ReplaceMedicineNameDTO>();
            CompositionDTO = new ObservableCollection<MedicineComponentDTO>();
            this.NavigateToRoomPageCommand = new RelayCommand(Execute_NavigateToRoomPageCommand, CanExecute_NavigateCommand);
            this.NavigateToMedicineInsightPage = new RelayCommand(Execute_NavigateToMedicineInsightPage, CanExecute_IfMedicineIsSelected);
            this.NavigateToPreviousPage = new RelayCommand(Execute_NavigateToPreviousPage, CanExecute_NavigateCommand);
            this.NavigateToUpdateMedicinePage = new RelayCommand(Execute_NavigateToMedicineUpdatePage, CanExecute_IfMedicineIsSelected);
            this.NavigateMedicineRegistrationPage = new RelayCommand(Execute_NavigateToMedicineRegistrationPage, CanExecute_NavigateCommand);
            this.RegistrateNewMedicineCommand = new RelayCommand(Execute_RegistrateNewMedicine);
            this.UpdateMedicineCommand = new RelayCommand(Execute_UpdateMedicine);
            this.DeleteMedicineCommand = new RelayCommand(Execute_DeleteMedicine, CanExecute_IfMedicineIsSelected);

        }

        private bool CanExecute_NavigateCommand(object obj)
        {
            return true;
        }
        private bool CanExecute_IfMedicineIsSelected(object obj)
        {
            return !(SelectedMedicine == null);
        }

        private void Execute_NavigateToPreviousPage(object obj)
        {
            Composition = new ObservableCollection<MedicineComponent>();
            ReplaceMedicines = new ObservableCollection<ReplaceMedicineName>();
            Name = null;
            Usage = null;
            SideEffects = null;
            CompositionDTO = new ObservableCollection<MedicineComponentDTO>();
            ReplaceMedicineNameDTOs = new ObservableCollection<ReplaceMedicineNameDTO>();
            this.NavService.GoBack();

        }

        private void Execute_DeleteMedicine(object obj)
        {

            MedicineController.Instance.DeleteMedicine(SelectedMedicine);
            Medicines = new ObservableCollection<Medicine>(MedicineController.Instance.GetAll());

        }
        private void Execute_UpdateMedicine(object obj)
        {

            List<MedicineComponent> medicineComponents = new List<MedicineComponent>();
            ConvertMedcineComponetDTOsToList(medicineComponents, CompositionDTO);
            List<ReplaceMedicineName> replaceMedicineNames = new List<ReplaceMedicineName>();
            ConvertReplaceMedicineNameDTOsToList(replaceMedicineNames, ReplaceMedicineNameDTOs);

            MedicineController.Instance.UpdateMedicine(new Medicine(Name, medicineComponents, SideEffects, Usage, replaceMedicineNames));
            Medicines = new ObservableCollection<Medicine>(MedicineController.Instance.GetAll());
            CompositionDTO = new ObservableCollection<MedicineComponentDTO>();
            ReplaceMedicineNameDTOs = new ObservableCollection<ReplaceMedicineNameDTO>();
            this.NavService.GoBack();
        }

        private void Execute_RegistrateNewMedicine(object obj)
        {

            List<MedicineComponent> medicineComponents = new List<MedicineComponent>();
            ConvertMedcineComponetDTOsToList(medicineComponents,CompositionDTO);
            List<ReplaceMedicineName> replaceMedicineNames = new List<ReplaceMedicineName>();
            ConvertReplaceMedicineNameDTOsToList(replaceMedicineNames,ReplaceMedicineNameDTOs);
            Medicine medicine = new Medicine(Name, medicineComponents, SideEffects, Usage, replaceMedicineNames);

           
           
            CompositionDTO = new ObservableCollection<MedicineComponentDTO>();
            ReplaceMedicineNameDTOs = new ObservableCollection<ReplaceMedicineNameDTO>();

            RecipientViewModel.Instance.NotificationMedicine = medicine;
            ChooseReciepientForNotification chooseRecipient = new ChooseReciepientForNotification();
            chooseRecipient.SendReNotification.Visibility = Visibility.Collapsed;
            chooseRecipient.ShowDialog();
        }

        private void ConvertMedcineComponetDTOsToList(List<MedicineComponent> medicineComponents,ObservableCollection<MedicineComponentDTO> medicineComponentDTOs)
        {
           foreach(MedicineComponentDTO componentDTO in medicineComponentDTOs)
            {
                MedicineComponent medicineComponent = new MedicineComponent(componentDTO.Component);
                medicineComponents.Add(medicineComponent);
            }
        }

        private void ConvertReplaceMedicineNameDTOsToList(List<ReplaceMedicineName> medicineNames, ObservableCollection<ReplaceMedicineNameDTO> medicineNameDTOs)
        {
            foreach (ReplaceMedicineNameDTO replaceMedicine in medicineNameDTOs)
            {
                ReplaceMedicineName replaceMedicineName = new ReplaceMedicineName(replaceMedicine.Name);
                medicineNames.Add(replaceMedicineName);
            }
        }


        private void Execute_NavigateToMedicineRegistrationPage(object obj)
        {
           
            this.NavService.Navigate(
                    new Uri("ManagerView1/MedicineRegistrationView.xaml", UriKind.Relative));
        }


        private void Execute_NavigateToMedicineInsightPage(object obj)
        {
            Composition = new ObservableCollection<MedicineComponent>(SelectedMedicine.Composition);
            ReplaceMedicines = new ObservableCollection<ReplaceMedicineName>(SelectedMedicine.ReplaceMedicine);
            this.NavService.Navigate(
                    new Uri("ManagerView1/MedicineInsightView.xaml", UriKind.Relative));
        }

        private void Execute_NavigateToMedicineUpdatePage(object obj)
        {
            ConvertReplaceMedicineListToDTOList(SelectedMedicine.ReplaceMedicine, ReplaceMedicineNameDTOs);
            ConvertMedcineComponetListToDTOList(SelectedMedicine.Composition, CompositionDTO);
            Name = SelectedMedicine.Name;
            SideEffects = SelectedMedicine.SideEffects;
            Usage = SelectedMedicine.Usage;
            this.NavService.Navigate(
                    new Uri("ManagerView1/MedicineUpdateView.xaml", UriKind.Relative));
        }

        private void ConvertMedcineComponetListToDTOList(List<MedicineComponent> medicineComponents, ObservableCollection<MedicineComponentDTO> medicineComponentDTOs)
        {
            foreach (MedicineComponent component in medicineComponents)
            {
                MedicineComponentDTO medicineComponentDTO = new MedicineComponentDTO(component.Component);
                medicineComponentDTOs.Add(medicineComponentDTO);
            }
        }

        private void ConvertReplaceMedicineListToDTOList(List<ReplaceMedicineName> medicineNames, ObservableCollection<ReplaceMedicineNameDTO> medicineNameDTOs)
        {
            foreach (ReplaceMedicineName replaceMedicine in medicineNames)
            {
                ReplaceMedicineNameDTO replaceMedicineNameDTO = new ReplaceMedicineNameDTO(replaceMedicine.Name);
                medicineNameDTOs.Add(replaceMedicineNameDTO);
            }
        }


        private void Execute_NavigateToRoomPageCommand(object obj)
        {
            this.NavService.Navigate(
                    new Uri("ManagerView1/RoomView.xaml", UriKind.Relative));
        }




    }
}
