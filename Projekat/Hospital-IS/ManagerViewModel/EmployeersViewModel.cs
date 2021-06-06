using Hospital_IS.DTOs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Navigation;

namespace Hospital_IS.ManagerViewModel
{
    public class EmployeersViewModel: ViewModel
    {
        private RelayCommand navigateToWorkerRegistration;
        private RelayCommand navigateToWorkerInsight;
        private RelayCommand navigateToPreviousPage;
        private RelayCommand navigateToEmployeerDetailsPage;
        private NavigationService navService;
        private WorkDayDTO workWeek;
        private ObservableCollection<WorkDayDTO> workWeeks;
        private RelayCommand navigateToRoomPage;
        private RelayCommand navigateToEquipmentPage;
        private RelayCommand navigateToMedicinePage;
        private RelayCommand navigateToBranchPage;
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

        public RelayCommand NavigateToBranchPage
        {
            get { return navigateToBranchPage; }
            set
            {
                navigateToBranchPage = value;
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


        public ObservableCollection<WorkDayDTO> WorkWeeks
        {
            get
            {
                return workWeeks;
            }
            set
            {
                if (value != workWeeks)
                {

                    workWeeks = value;
                    OnPropertyChanged("WorkWeeks");

                }
            }
        }
        public WorkDayDTO WorkWeek
        {
            get { return workWeek; }
            set
            {
                workWeek = value;
                OnPropertyChanged("WorkWeek");
            }
        }

        public RelayCommand NavigateToEmployeerDetailsPage
        {
            get { return navigateToEmployeerDetailsPage; }
            set
            {
                navigateToEmployeerDetailsPage = value;
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

        public RelayCommand NavigateToWorkerInsight
        {
            get { return navigateToWorkerInsight; }
            set
            {
                navigateToWorkerInsight = value;
            }
        }

        public RelayCommand NavigateToWorkerRegistration
        {
            get { return navigateToWorkerRegistration; }
            set
            {
                navigateToWorkerRegistration = value;
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

        public EmployeersViewModel(NavigationService navigationService)
        {
            WorkWeek = new WorkDayDTO("14h-20h", "08-20h", "slobodan", "08h-16h", "20h-03h", "00h-20h");
            WorkWeeks = new ObservableCollection<WorkDayDTO>();
            WorkWeeks.Add(WorkWeek);
            this.NavService = navigationService;
            this.NavigateToWorkerRegistration = new RelayCommand(Execute_NavigateToAddWorkerPageCommand);
            this.NavigateToWorkerInsight = new RelayCommand(Execute_NavigateToWorkerInsightPageCommand);
            this.NavigateToPreviousPage = new RelayCommand(Execute_NavigateToPreviousPageCommand);
            this.NavigateToEmployeerDetailsPage = new RelayCommand(Execute_NavigateToWorkerDetailsPageCommand);
            this.NavigateToRoomPage = new RelayCommand(Execute_NavigateToRoomPageCommand);
            this.NavigateToEquipmentPage = new RelayCommand(Execute_NavigateToEquipmentPageCommand);
            this.NavigateToMedicinePage = new RelayCommand(Execute_NavigateToMedicinePageCommand);
            this.NavigateToManagerProfilePage = new RelayCommand(Execute_NavigateToManagerProfilePageCommand);
            this.NavigateToBranchPage = new RelayCommand(Execute_NavigateToBranchPageCommand);

        }


        private void Execute_NavigateToWorkerDetailsPageCommand(object obj)
        {
            this.NavService.Navigate(
          new Uri("ManagerView1/EmployeerDetailsView.xaml", UriKind.Relative));

        }


        private void Execute_NavigateToAddWorkerPageCommand(object obj)
        {      
                this.NavService.Navigate(
              new Uri("ManagerView1/AddWorkerView.xaml", UriKind.Relative));
          
        }

        private void Execute_NavigateToWorkerInsightPageCommand(object obj)
        {

            this.NavService.Navigate(
          new Uri("ManagerView1/EmployyerInsight.xaml", UriKind.Relative));

        }

        private void Execute_NavigateToPreviousPageCommand(object obj)
        {

            this.NavService.GoBack();

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

        private void Execute_NavigateToBranchPageCommand(object obj)
        {
            this.NavService.Navigate(
                new Uri("ManagerView1/BranchView.xaml", UriKind.Relative));
        }


        private void Execute_NavigateToManagerProfilePageCommand(object obj)
        {
            ManagerProfileOptionsVIewModel.Instance.PreviousMainPage = this.NavService.CurrentSource;
            this.NavService.Navigate(
                new Uri("ManagerView1/ManagerProfileOptionsView.xaml", UriKind.Relative));
        }

    }
}
