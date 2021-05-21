using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Navigation;

namespace Hospital_IS.ManagerViewModel
{
    public class ManagerProfileOptionsVIewModel : ViewModel
    {
        private RelayCommand navigateToPreviuosPageCommand;
        private RelayCommand navigateToNotificationPageCommand;
        private NavigationService navService;


        public RelayCommand NavigateToNotificationPageCommand
        {
            get { return navigateToNotificationPageCommand; }
            set
            {
                navigateToNotificationPageCommand = value;
            }
        }
        public RelayCommand NavigateToPreviousPageCommand
        {
            get { return navigateToPreviuosPageCommand; }
            set
            {
                navigateToPreviuosPageCommand = value;
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

        public ManagerProfileOptionsVIewModel(NavigationService navigationService)
        {
            this.navService = navigationService;
            this.NavigateToPreviousPageCommand = new RelayCommand(Execute_NavigateToPreviousPageCommand);
            this.NavigateToNotificationPageCommand = new RelayCommand(Execute_NavigateToNotificationPageCommand);

        }

        private void Execute_NavigateToPreviousPageCommand(object obj)
        {
            this.NavService.GoBack();
        }

        private void Execute_NavigateToNotificationPageCommand(object obj)
        {
            this.NavService.Navigate(
                 new Uri("ManagerView1/NotificationView.xaml", UriKind.Relative));
        }

    }
}
