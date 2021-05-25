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
    public class NotificationViewModel: ViewModel
    {

        private ObservableCollection<MedicineNotification> notifications;
        private MedicineNotification selectedNotification;
        private NavigationService navService;
        private RelayCommand navigateToSelectedNotification;
        private RelayCommand navigateToPreviousPage;
        private RelayCommand deleteMedicineNotificationCommand;
        private RelayCommand navigateToMedicineInsightPageCommmand;
        private RelayCommand navigateToMedicineUpdatePageCommand;
        private RelayCommand navigateToNotificationInformationPage;
        private RelayCommand navigateToChooseRecipientWindow;
        private Uri previousMainPage;
        private string doctorName;
        private string name;
        private string sideEffects;
        private string usage;
        private ObservableCollection<MedicineComponentDTO> compositionDTO;
        private ObservableCollection<ReplaceMedicineNameDTO> replaceMedicineNameDTOs;



     
        public MedicineNotification SelectedNotification
        {
            get
            {
                return selectedNotification;
            }
            set
            {
                if (value != selectedNotification)
                {

                    selectedNotification = value;
                    OnPropertyChanged("SelectedNotification");

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

        public String DoctorName
        {
            get
            {
                return doctorName;
            }
            set
            {
                if (value != doctorName)
                {

                    doctorName = value;
                    OnPropertyChanged("DoctorName");

                }
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




     
        public RelayCommand NavigateToChooseRecipientWindow
        {
            get { return navigateToChooseRecipientWindow; }
            set
            {
                navigateToChooseRecipientWindow = value;
            }
        }
        public RelayCommand NavigateToMedicineUpdatePageCommand
        {
            get { return navigateToMedicineUpdatePageCommand; }
            set
            {
                navigateToMedicineUpdatePageCommand = value;
            }
        }

        public RelayCommand NavigateToNotificationInformationPage
        {
            get { return navigateToNotificationInformationPage; }
            set
            {
                navigateToNotificationInformationPage = value;
            }
        }


        public RelayCommand NavigateToMedicineInsightPageCommmand
        {
            get { return navigateToMedicineInsightPageCommmand; }
            set
            {
                navigateToMedicineInsightPageCommmand = value;
            }
        }



        public RelayCommand NavigateToSelectedNotification
        {
            get { return navigateToSelectedNotification; }
            set
            {
                navigateToSelectedNotification = value;
            }
        }

        public RelayCommand DeleteMedicineNotificationCommand
        {
            get { return deleteMedicineNotificationCommand; }
            set
            {
                deleteMedicineNotificationCommand = value;
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


        public ObservableCollection<MedicineNotification> Notifications
        {
            get
            {
                return notifications;
            }
            set
            {
                if (value != notifications)
                {

                    notifications = value;
                    OnPropertyChanged("Notifications");

                }
            }
        }



        private static NotificationViewModel instance = null;
        public static NotificationViewModel Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new NotificationViewModel();
                }
                return instance;
            }
        }
        private NotificationViewModel()
        {
            ReplaceMedicineNameDTOs = new ObservableCollection<ReplaceMedicineNameDTO>();
            CompositionDTO = new ObservableCollection<MedicineComponentDTO>();
            Notifications = new ObservableCollection<MedicineNotification>(MedicineNotificationController.Instance.GetAllByDoctorId(6));
            this.NavigateToSelectedNotification = new RelayCommand(Execute_NavigateToNotificationInforamationPage, CanExecute_IfNotificationIsSelected);
            this.DeleteMedicineNotificationCommand = new RelayCommand(Execute_DeleteMedicineNotification, CanExecute_Navigation);
            this.NavigateToPreviousPage = new RelayCommand(Execute_NavigateToPreviousPage, CanExecute_Navigation);

            this.NavigateToMedicineInsightPageCommmand = new RelayCommand(Execute_NavigateToMedicineInsightPage);
            this.NavigateToMedicineUpdatePageCommand = new RelayCommand(Execute_NavigateToMedicineUpdatePage, CanExecute_Navigation);
            this.NavigateToChooseRecipientWindow = new RelayCommand(Execute_NavigateToChooseRecipientWindow);
         

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

        private void ConvertMedcineComponetDTOsToList(List<MedicineComponent> medicineComponents, ObservableCollection<MedicineComponentDTO> medicineComponentDTOs)
        {
            foreach (MedicineComponentDTO componentDTO in medicineComponentDTOs)
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




        private bool CanExecute_IfNotificationIsSelected(object obj)
        {
           
            return !(SelectedNotification == null);
        }

        private bool CanExecute_Navigation(object obj)
        {
            return true;
        }

       

        private void Execute_NavigateToPreviousPage(object obj)
        {
            SelectedNotification = null; 
            this.NavService.Navigate(new Uri("ManagerView1/ManagerProfileOptionsView.xaml", UriKind.Relative));
        }

        private void Execute_NavigateToChooseRecipientWindow(object obj)
        {
            List<MedicineComponent> medicineComponents = new List<MedicineComponent>();
            ConvertMedcineComponetDTOsToList(medicineComponents, CompositionDTO);
            List<ReplaceMedicineName> replaceMedicineNames = new List<ReplaceMedicineName>();
            ConvertReplaceMedicineNameDTOsToList(replaceMedicineNames, ReplaceMedicineNameDTOs);
            Medicine medicine = new Medicine(Name, medicineComponents, SideEffects, Usage, replaceMedicineNames);

            CompositionDTO = new ObservableCollection<MedicineComponentDTO>();
            ReplaceMedicineNameDTOs = new ObservableCollection<ReplaceMedicineNameDTO>();

            RecipientViewModel.Instance.Notification = SelectedNotification;
            RecipientViewModel.Instance.NotificationMedicine = medicine;
            RecipientViewModel.Instance.NavService = navService;
            ChooseReciepientForNotification chooseReciepient = new ChooseReciepientForNotification();
            chooseReciepient.Send.Visibility = Visibility.Collapsed;
            chooseReciepient.ShowDialog();

        }

        private void Execute_NavigateToMedicineUpdatePage(object obj)
        {
            ConvertReplaceMedicineListToDTOList(SelectedNotification.Medicine.ReplaceMedicine, ReplaceMedicineNameDTOs);
            ConvertMedcineComponetListToDTOList(SelectedNotification.Medicine.Composition, CompositionDTO);
            Name = SelectedNotification.Medicine.Name;
            SideEffects = SelectedNotification.Medicine.SideEffects;
            Usage = SelectedNotification.Medicine.Usage;

            this.NavService.Navigate(
                   new Uri("ManagerView1/NotificationMedicineUpdate.xaml", UriKind.Relative));
        }



        private void Execute_NavigateToNotificationInforamationPage(object obj)
        { 


           List<Doctor> doctors = new List<Doctor>(DoctorController.Instance.GetAllDoctorsByIds(SelectedNotification.SenderId));
           foreach(Doctor d in doctors)
            {
                DoctorName = d.Name + " " + d.Surname;
            }

            this.NavService.Navigate(
                   new Uri("ManagerView1/NotificationInformationView.xaml", UriKind.Relative));
        }

        private void Execute_NavigateToMedicineInsightPage(object obj)
        {
            MedicineViewModel.Instance.SelectedMedicine = this.SelectedNotification.Medicine;
            MedicineViewModel.Instance.NavService = this.NavService;
            this.NavService.Navigate(
                   new Uri("ManagerView1/MedicineInsightView.xaml", UriKind.Relative));
        }

        private void Execute_DeleteMedicineNotification(object obj)
        {
            MedicineNotificationController.Instance.DeleteNotification(SelectedNotification);
            Notifications = new ObservableCollection<MedicineNotification>(MedicineNotificationController.Instance.GetAllByDoctorId(6));
            this.NavService.GoBack();
                   
        }



    }
}
