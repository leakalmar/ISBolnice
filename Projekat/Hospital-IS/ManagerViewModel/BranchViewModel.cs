using Hospital_IS.DTOs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Navigation;

namespace Hospital_IS.ManagerViewModel
{
    public class BranchViewModel: ViewModel
    {

        private ObservableCollection<BranchDTO> branches;
        private ObservableCollection<BranchWorkerDTO> workers;
        private ObservableCollection<BranchEquipmentDTO> equipments;
        private NavigationService navService;
        private RelayCommand navigateToBranchWorkers;
        private RelayCommand navigateToPreviousPage;
        private RelayCommand navigateToBranchEquipment;
        private RelayCommand navigateToRoomPage;
        private RelayCommand navigateToEquipmentPage;
        private RelayCommand navigateToMedicinePage;
        private RelayCommand navigateToEmployeePage;
        private RelayCommand navigateToManagerProfilePage;



        public RelayCommand NavigateToRoomPage
        {
            get { return navigateToRoomPage; }
            set
            {
                navigateToRoomPage = value;
            }
        }
        public RelayCommand NavigateToEquipmentPage
        {
            get { return navigateToEquipmentPage; }
            set
            {
                navigateToEquipmentPage = value;
            }
        }

        public RelayCommand NavigateToMedicinePage
        {
            get { return navigateToMedicinePage; }
            set
            {
                navigateToMedicinePage = value;
            }
        }

        public RelayCommand NavigateToEmployeePage
        {
            get { return navigateToEmployeePage; }
            set
            {
                navigateToEmployeePage = value;
            }
        }

        public RelayCommand NavigateToManagerProfilePage
        {
            get { return navigateToManagerProfilePage; }
            set
            {
                navigateToManagerProfilePage = value;
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
        public RelayCommand NavigateToBranchWorkers
        {
            get { return navigateToBranchWorkers; }
            set
            {
                navigateToBranchWorkers = value;
            }
        }

        public RelayCommand NavigateToBranchEquipment
        {
            get { return navigateToBranchEquipment; }
            set
            {
                navigateToBranchEquipment = value;
            }
        }

        public ObservableCollection<BranchEquipmentDTO> Equipments
        {
            get
            {
                return equipments;
            }
            set
            {
                if (value != equipments)
                {

                    equipments = value;
                    OnPropertyChanged("Equipments");

                }
            }
        }

        public ObservableCollection<BranchWorkerDTO> Workers
        {
            get
            {
                return workers;
            }
            set
            {
                if (value != workers)
                {

                    workers = value;
                    OnPropertyChanged("Workers");

                }
            }
        }
        public ObservableCollection<BranchDTO> Branches
        {
            get
            {
                return branches;
            }
            set
            {
                if (value != branches)
                {

                    branches = value;
                    OnPropertyChanged("Branches");

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

        public BranchViewModel(NavigationService navigationService)
        {
            NavService = navigationService;
            BranchDTO branch = new BranchDTO("Klinika Zdravo", "Novi Sad");
            BranchDTO branch2 = new BranchDTO("Klinka Zdravo", "Kragujevac");
            BranchWorkerDTO branchWorker = new BranchWorkerDTO("Teodora", "Maruna", "Kardiolog");
            BranchEquipmentDTO branchEquipmentDTO = new BranchEquipmentDTO("makaze", "Dinamicka", "50");
            BranchEquipmentDTO branchEquipmentDTO1 = new BranchEquipmentDTO("op. krevet", "Staticka", "50");



            Branches = new ObservableCollection<BranchDTO>();
            Workers = new ObservableCollection<BranchWorkerDTO>();
            Equipments = new ObservableCollection<BranchEquipmentDTO>();
            Equipments.Add(branchEquipmentDTO1);
            Equipments.Add(branchEquipmentDTO);
            Branches.Add(branch);
            Branches.Add(branch2);
            Workers.Add(branchWorker);
            this.NavigateToBranchWorkers = new RelayCommand(Execute_NavigateToBranchPageCommand);
            this.NavigateToPreviousPage = new RelayCommand(Execute_NavigateToPreviousPageCommand);
            this.NavigateToBranchEquipment = new RelayCommand(Execute_NavigateToBranchEquipmentPageCommand);
            this.NavigateToRoomPage = new RelayCommand(Execute_NavigateToRoomPageCommand);
            this.NavigateToEquipmentPage = new RelayCommand(Execute_NavigateToEquipmentPageCommand);
            this.NavigateToMedicinePage = new RelayCommand(Execute_NavigateToMedicinePageCommand);
            this.NavigateToManagerProfilePage = new RelayCommand(Execute_NavigateToManagerProfilePageCommand);
            this.NavigateToEmployeePage = new RelayCommand(Execute_NavigateToEmployeePageCommand);
          
        }

        private void Execute_NavigateToBranchPageCommand(object obj)
        {
            this.NavService.Navigate(
                new Uri("ManagerView1/BranchWorkerInsight.xaml", UriKind.Relative));
        }

        private void Execute_NavigateToBranchEquipmentPageCommand(object obj)
        {
            this.NavService.Navigate(
                new Uri("ManagerView1/BranchEquipmentInsight.xaml", UriKind.Relative));
        }

        private void Execute_NavigateToRoomPageCommand(object obj)
        {
           
            this.NavService.Navigate(
                new Uri("ManagerView1/RoomView.xaml", UriKind.Relative));
        }
        private void Execute_NavigateToEquipmentPageCommand(object obj)
        {
            EquipmentViewModel.Instance.NavService = this.NavService;
            this.NavService.Navigate(
                new Uri("ManagerView1/EquipmentView.xaml", UriKind.Relative));
        }
        private void Execute_NavigateToMedicinePageCommand(object obj)
        {
            MedicineViewModel.Instance.NavService = this.NavService;
            this.NavService.Navigate(
                new Uri("ManagerView1/MainMedicineView.xaml", UriKind.Relative));
        }

        private void Execute_NavigateToEmployeePageCommand(object obj)
        {
            this.NavService.Navigate(
                new Uri("ManagerView1/EmployeersView.xaml", UriKind.Relative));
        }


        private void Execute_NavigateToManagerProfilePageCommand(object obj)
        {
            ManagerProfileOptionsVIewModel.Instance.PreviousMainPage = this.NavService.CurrentSource;
            this.NavService.Navigate(
                new Uri("ManagerView1/ManagerProfileOptionsView.xaml", UriKind.Relative));
        }


        private void Execute_NavigateToPreviousPageCommand(object obj)
        {
            this.NavService.GoBack();
        }

    }
}
