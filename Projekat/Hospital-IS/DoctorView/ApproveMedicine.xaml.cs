using Controllers;
using Hospital_IS.DoctorViewModel;
using Model;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Hospital_IS.DoctorView
{
    public partial class ApproveMedicine : UserControl
    {
        private ApproveMedicineViewModel viewModel;

        public ApproveMedicineViewModel _ViewModel
        {
            get { return viewModel; }
            set { viewModel = value; }
        }

        public ApproveMedicine()
        {
            InitializeComponent();
            this._ViewModel = new ApproveMedicineViewModel();
            this.DataContext = _ViewModel;

        }
    }
}
