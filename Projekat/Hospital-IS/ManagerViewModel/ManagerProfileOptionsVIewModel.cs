using Hospital_IS.ManagerView1;
using System;
using System.Windows.Navigation;

namespace Hospital_IS.ManagerViewModel
{
    public class ManagerProfileOptionsVIewModel : ViewModel
    {
        private RelayCommand navigateToPreviuosPageCommand;
        private RelayCommand navigateToNotificationPageCommand;
        private RelayCommand navigateToPreviousMainPage;
        private RelayCommand navigateToFeedbackCommand;
        private RelayCommand openHelpWindow;
        private Uri previousMainPage;
        private NavigationService navService;

        public Uri PreviousMainPage
        {
            get
            {
                return previousMainPage;
            }
            set
            {
                if (value != previousMainPage)
                {

                    previousMainPage = value;
                    OnPropertyChanged("PreviousMainPage");

                }
            }
        }

        public RelayCommand OpenHelpWindow
        {
            get { return openHelpWindow; }
            set
            {
                openHelpWindow = value;
            }
        }
        public RelayCommand NavigateToFeedbackCommand
        {
            get { return navigateToFeedbackCommand; }
            set
            {
                navigateToFeedbackCommand = value;
            }
        }
        public RelayCommand NavigateToPreviousMainPage
        {
            get { return navigateToPreviousMainPage; }
            set
            {
                navigateToPreviousMainPage = value;
            }
        }
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

        private static ManagerProfileOptionsVIewModel instance = null;
        public static ManagerProfileOptionsVIewModel Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ManagerProfileOptionsVIewModel();
                }
                return instance;
            }
        }

        private ManagerProfileOptionsVIewModel()
        {
            
         
            this.NavigateToPreviousPageCommand = new RelayCommand(Execute_NavigateToPreviousPageCommand);
            this.NavigateToNotificationPageCommand = new RelayCommand(Execute_NavigateToNotificationPageCommand);
            this.NavigateToPreviousMainPage = new RelayCommand(Execute_NavigateToPreviousMainPage);
            this.NavigateToFeedbackCommand = new RelayCommand(Execute_NavigateToFeedbackCommand);
            this.OpenHelpWindow = new RelayCommand(Execute_OpenHelpWindowCommand);


        }

        private void Execute_OpenHelpWindowCommand(object obj)
        {

            HelpWindow helpWindow = new HelpWindow();
            helpWindow.ShowDialog();
        }


        private void Execute_NavigateToFeedbackCommand(object obj)
        {
            FeedbackViewModel.Instance.NavService = this.NavService;
            this.NavService.Navigate(
                new Uri("ManagerView1/FeedbackView.xaml", UriKind.Relative));
        }


        private void Execute_NavigateToPreviousMainPage(object obj)
        {
            
            this.NavService.Navigate(PreviousMainPage);
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
