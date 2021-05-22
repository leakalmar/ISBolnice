using Controllers;
using Hospital_IS.Controllers;
using Hospital_IS.DoctorViewModel;
using Model;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Hospital_IS.DoctorView
{
    public partial class UCIssueInstruction : UserControl
    {
        private IssueInstructionViewModel viewModel;

        public IssueInstructionViewModel _ViewModel
        {
            get { return viewModel; }
            set { viewModel = value; }
        }

        public UCIssueInstruction()
        {
            InitializeComponent();
            this._ViewModel = new IssueInstructionViewModel();
            this.DataContext = _ViewModel;
        }
    }
}
