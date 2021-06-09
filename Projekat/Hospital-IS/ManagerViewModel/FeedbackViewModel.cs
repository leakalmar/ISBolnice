using Hospital_IS.Model;
using Hospital_IS.Service;
using System;
using System.Windows;
using System.Windows.Navigation;

namespace Hospital_IS.ManagerViewModel
{
    public class FeedbackViewModel:ViewModel
    {
        private NavigationService navService;
        private String note = new String("");
        private RelayCommand sendFeedbackNotification;
        private RelayCommand navigateToPreviousPage;

        public RelayCommand SendFeedbackNotification
        {
            get { return sendFeedbackNotification; }
            set
            {
                sendFeedbackNotification = value;
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

        public NavigationService NavService
        {
            get { return navService; }
            set
            {
                navService = value;
            }
        }

        public String Note
        {
            get
            {
                return note;
            }
            set
            {
                if (value != note)
                {
                    note = value;
                    OnPropertyChanged("Note");
                }
            }
        }

        private static FeedbackViewModel instance = null;
        public static FeedbackViewModel Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new FeedbackViewModel();
                }
                return instance;
            }
        }
        private FeedbackViewModel()
        {
            this.SendFeedbackNotification = new RelayCommand(Execute_SendFeedbackNotificationCommand, CanExecute_SendFeedbackNotificationCommand);
            this.NavigateToPreviousPage = new RelayCommand(Execute_NavigateToPreviousPage);

        }

        private void Execute_SendFeedbackNotificationCommand(object obj)
        {
            FeedbackMessage feedbackMessage = new FeedbackMessage(Note, DateTime.Now);
            FeedbackMessageService.Instance.AddFeedbackMessage(feedbackMessage);
            MessageBox.Show("Uspjesno poslat feedback");
            Note = "";

        }

        private bool CanExecute_SendFeedbackNotificationCommand(object obj)
        {
            return !(Note.Length == 0);
        }


        private void Execute_NavigateToPreviousPage(object obj)
        {
            this.NavService.GoBack();
        }

       
    }
}
