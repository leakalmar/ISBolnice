using Controllers;
using DoctorView;
using Hospital_IS.DoctorView;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Navigation;

namespace Hospital_IS.ManagerViewModel
{
    public class MedicineViewModel:ViewModel
    {
        private string name = "Unesite ime lijeka";
        private string sideEffects = "Unesite nezeljene posljedice";
        private string usage = "Unesite terapijske indikacije";
        private ObservableCollection<MedicineComponent> composition;
        private ObservableCollection<ReplaceMedicineName> replaceMedicines;
        private ObservableCollection<Medicine> medicines;
        private NavigationService navService;
        private RelayCommand navigateToRoomPageCommand;
        private RelayCommand navigateToMedicineInsightPage;
        private RelayCommand navigateToPreviousPage;
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
                return sideEffects;
            }
            set
            {
                if (value != sideEffects)
                {

                    sideEffects = value;
                    OnPropertyChanged("Usage");

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
                    OnPropertyChanged("Composition");

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
                    OnPropertyChanged("Composition");

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
            this.NavigateToRoomPageCommand = new RelayCommand(Execute_NavigateToRoomPageCommand, CanExecute_NavigateCommand);
            this.NavigateToMedicineInsightPage = new RelayCommand(Execute_NavigateToMedicineInsightPage, CanExecute_IfMedicineIsSelected);
            this.NavigateToPreviousPage = new RelayCommand(Execute_NavigateToPreviousPage, CanExecute_NavigateCommand);

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
            Composition = null;
            ReplaceMedicines = null;
            this.NavService.GoBack();

        }

        private void Execute_NavigateToMedicineInsightPage(object obj)
        {
            Composition = new ObservableCollection<MedicineComponent>(SelectedMedicine.Composition);
            ReplaceMedicines = new ObservableCollection<ReplaceMedicineName>(SelectedMedicine.ReplaceMedicine);
            this.NavService.Navigate(
                    new Uri("ManagerView1/MedicineInsightView.xaml", UriKind.Relative));
        }

        private void Execute_NavigateToRoomPageCommand(object obj)
        {
            this.NavService.Navigate(
                    new Uri("ManagerView1/RoomView.xaml", UriKind.Relative));
        }




    }
}
