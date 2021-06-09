using Hospital_IS.DoctorRole.Commands;
using Hospital_IS.ManagerView1;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Navigation;

namespace Hospital_IS.ManagerHelp
{
    public class HelpViewModel
    {
        private RelayCommand navigateToPreviousPage;
        private RelayCommand naviagateToHelpPage2;
        private RelayCommand naviagateToHelpPage3;
        private RelayCommand naviagateToHelpPage4;
        private RelayCommand naviagateToHelpPage5;
        private RelayCommand naviagateToHelpPage6;
        private RelayCommand closeHelpWindow;
        private NavigationService navService;
        public Boolean isShowed { get; set; } = false;

        public RelayCommand CloseHelpWindow
        {
            get { return closeHelpWindow; }
            set
            {
                closeHelpWindow = value;
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
        public RelayCommand NaviagateToHelpPage2
        {
            get { return naviagateToHelpPage2; }
            set
            {
                naviagateToHelpPage2 = value;
            }
        }
        public RelayCommand NaviagateToHelpPage3
        {
            get { return naviagateToHelpPage3; }
            set
            {
                naviagateToHelpPage3 = value;
            }
        }

        public RelayCommand NaviagateToHelpPage4
        {
            get { return naviagateToHelpPage4; }
            set
            {
                naviagateToHelpPage4 = value;
            }
        }

        public RelayCommand NaviagateToHelpPage5
        {
            get { return naviagateToHelpPage5; }
            set
            {
                naviagateToHelpPage5 = value;
            }
        }

        public RelayCommand NaviagateToHelpPage6
        {
            get { return naviagateToHelpPage6; }
            set
            {
                naviagateToHelpPage6 = value;
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

        private static HelpViewModel instance = null;
        public static HelpViewModel Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new HelpViewModel();
                }
                return instance;
            }
        }
        private HelpViewModel()
        {
          
            this.NavigateToPreviousPage = new RelayCommand(Execute_NavigateToPreviousPage);
            this.NaviagateToHelpPage2 = new RelayCommand(Execute_NavigateToHelpPage2);
            this.NaviagateToHelpPage3 = new RelayCommand(Execute_NavigateToHelpPage3);
            this.NaviagateToHelpPage4 = new RelayCommand(Execute_NavigateToHelpPage4);
            this.NaviagateToHelpPage5 = new RelayCommand(Execute_NavigateToHelpPage5);
            this.NaviagateToHelpPage6 = new RelayCommand(Execute_NavigateToHelpPage6);
            this.CloseHelpWindow = new RelayCommand(Execute_CloseWindow);

        }

        private void Execute_NavigateToPreviousPage(object obj)
        {
            this.NavService.GoBack();
        }
        private void Execute_CloseWindow(object obj)
        {
            HelpWindow helpWindow = (HelpWindow)obj;
            helpWindow.Close();
            MessageBox.Show("Ugodan rad u nasoj aplikaciji");
        }

        private void Execute_NavigateToHelpPage2(object obj)
        {
          
            this.NavService.Navigate(
                new Uri("ManagerHelp/ManagerHelpPage2.xaml", UriKind.Relative));
        }

        private void Execute_NavigateToHelpPage3(object obj)
        {

            this.NavService.Navigate(
                new Uri("ManagerHelp/ManagerHelpPage3.xaml", UriKind.Relative));
        }

        private void Execute_NavigateToHelpPage4(object obj)
        {

            this.NavService.Navigate(
                new Uri("ManagerHelp/ManagerHelpPage4.xaml", UriKind.Relative));
        }

        private void Execute_NavigateToHelpPage5(object obj)
        {

            this.NavService.Navigate(
                new Uri("ManagerHelp/ManagerHelpPage5.xaml", UriKind.Relative));
        }

        private void Execute_NavigateToHelpPage6(object obj)
        {

            this.NavService.Navigate(
                new Uri("ManagerHelp/ManagerHelpPage6.xaml", UriKind.Relative));
        }

    }
}
