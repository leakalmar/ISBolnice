using Controllers;
using Hospital_IS.DoctorViewModel;
using Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Hospital_IS.DoctorView
{
    public partial class UCNewApp : UserControl
    {
        private NewAppViewModel viewModel;

        public NewAppViewModel _ViewModel
        {
            get { return viewModel; }
            set { viewModel = value; }
        }

        public UCNewApp()
        {
            InitializeComponent();
            this._ViewModel = new NewAppViewModel();
            this.DataContext = _ViewModel;
        }

    }

}
