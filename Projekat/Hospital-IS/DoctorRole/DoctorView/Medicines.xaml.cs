using Hospital_IS.DoctorViewModel;
using System.Windows.Controls;

namespace Hospital_IS.DoctorRole.DoctorView
{
    public partial class Medicines : UserControl
    {
        private MedicinesModelView viewModel;

        public MedicinesModelView _ViewModel
        {
            get { return viewModel; }
            set { viewModel = value; }
        }

        public Medicines()
        {
            InitializeComponent();
            this._ViewModel = new MedicinesModelView();
            this.DataContext = _ViewModel;

        }

    }
}
