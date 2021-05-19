using Model;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Hospital_IS.DoctorView;
using System.ComponentModel;
using System.Windows.Data;
using Hospital_IS.DoctorViewModel;
using Microsoft.VisualBasic;


namespace Hospital_IS.DoctorView
{
    public partial class UCHomePage : UserControl
    {
        private HomePageViewModel viewModel;
        public HomePageViewModel _ViewModel
        {
            get { return viewModel; }
            set
            {
                viewModel = value;
            }
        }
        public UCHomePage(NavigationService navigation)
        {
            InitializeComponent();
            this.viewModel = new HomePageViewModel(navigation);
            this.DataContext = this.viewModel;
            this.Focus();
        }
    }
}
