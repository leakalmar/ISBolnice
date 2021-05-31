﻿using Controllers;
using Hospital_IS.DoctorRole.Commands;
using Hospital_IS.DoctorRole.DoctorView;
using Model;
using System;

namespace Hospital_IS.DoctorRole.DoctorViewModel
{
    public class TestNewViewModel : BindableBase
    {
        #region Fields
        private string testName;
        private string testResults;

        public string TestName
        {
            get { return testName; }
            set
            {
                testName = value;
                OnPropertyChanged("TestName");
            }
        }

        public string TestResults
        {
            get { return testResults; }
            set
            {
                testResults = value;
                OnPropertyChanged("TestResults");
            }
        }
        #endregion
        #region Commands
        private RelayCommand saveCommand;
        private RelayCommand cancelCommand;

        public RelayCommand SaveCommand
        {
            get { return saveCommand; }
            set
            {
                saveCommand = value;
                OnPropertyChanged("SaveCommand");
            }
        }

        public RelayCommand CancelCommand
        {
            get { return cancelCommand; }
            set
            {
                cancelCommand = value;
                OnPropertyChanged("CancelCommand");
            }
        }
        #endregion

        #region Actions
        private void Execute_SaveCommand(object obj)
        {
            Test newTest = new Test(TestName, DateTime.Now, TestResults);
            ChartController.Instance.AddTest(newTest, DoctorMainWindow.Instance._ViewModel.PatientChartView._ViewModel.Patient);
            DoctorMainWindow.Instance._ViewModel.PatientChartView._ViewModel.ChangeCommand.Execute("5");
        }

        private void Execute_CancelCommand(object obj)
        {
            DoctorMainWindow.Instance._ViewModel.PatientChartView._ViewModel.ChangeCommand.Execute("5");
        }

        private bool CanExecute_Command(object obj)
        {
            return true;
        }
        #endregion

        #region Constructor
        public TestNewViewModel()
        {
            this.CancelCommand = new RelayCommand(Execute_CancelCommand, CanExecute_Command);
            this.SaveCommand = new RelayCommand(Execute_SaveCommand, CanExecute_Command);
        }
        #endregion
    }
}
