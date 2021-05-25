using Controllers;
using Hospital_IS.DoctorViewModel;
using Model;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Hospital_IS.DoctorView
{
    public partial class Appointments : UserControl
    {
        private AppointmentsViewModel viewModel;

        public AppointmentsViewModel _ViewModel
        {
            get { return viewModel; }
            set { viewModel = value; }
        }

        public Appointments()
        {
            this._ViewModel = new AppointmentsViewModel();
            InitializeComponent();
            _ViewModel.InsideNavigation = this.frame.NavigationService;
            this.DataContext = _ViewModel;
        }
    }
}
