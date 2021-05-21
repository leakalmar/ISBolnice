using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Navigation;

namespace Hospital_IS.ManagerViewModel
{
    public class ManagerProfileOptionsVIewModel : ViewModel
    {
        private RelayCommand navigateToPreviuosPageCommand;
        private NavigationService navService;

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

        }

        private void Execute_NavigateToPreviousPageCommand(object obj)
        {
            this.NavService.GoBack();
        }

    }
}
