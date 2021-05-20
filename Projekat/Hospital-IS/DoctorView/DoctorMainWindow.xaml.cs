﻿using System.Windows;
using Hospital_IS.DoctorViewModel;

namespace Hospital_IS.DoctorView
{
    public partial class DoctorMainWindow : Window
    {
        private static DoctorMainWindow instance = null;
        private DoctorMainWindowModel viewModel;
        public static DoctorMainWindow Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DoctorMainWindow();
                }
                return instance;
            }
        }

        public DoctorMainWindowModel _ViewModel
        {
            get { return viewModel; }
            set
            {
                viewModel = value;
            }
        }

        public DoctorMainWindow()
        {
            InitializeComponent();
            this.viewModel = new DoctorMainWindowModel(this.Home.NavigationService);
            this.DataContext = this.viewModel;
            HomePageSelected.IsChecked = true;
        }

    }
}