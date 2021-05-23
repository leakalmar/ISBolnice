﻿using Model;
using System.Windows.Controls;
using Hospital_IS.DoctorViewModel;
using System.Windows.Navigation;

namespace Hospital_IS.DoctorView
{
    public partial class AppDetail : UserControl
    {
        private AppDetailViewModel viewModel;

        public AppDetailViewModel _ViewModel
        {
            get { return viewModel; }
            set { viewModel = value; }
        }


        public AppDetail()
        {
            InitializeComponent();
            AppDetailViewModel vm = new AppDetailViewModel();
            this._ViewModel = vm;
            this.DataContext = vm;
            
        }
    }
}