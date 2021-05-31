﻿using Controllers;
using Hospital_IS.DoctorRole.Commands;
using Hospital_IS.DoctorRole.DoctorView;
using Model;
using System.Collections.ObjectModel;

namespace Hospital_IS.DoctorViewModel
{
    public class TherapyViewModel : BindableBase
    {
        #region Fields
        private bool started;
        private ObservableCollection<Therapy> therapies;
        public bool Started
        {
            get { return started; }
            set
            {
                started = value;
                OnPropertyChanged("Started");
            }
        }

        public ObservableCollection<Therapy> Therapies
        {
            get { return therapies; }
            set
            {
                therapies = value;
                OnPropertyChanged("Therapies");
            }
        }
        #endregion

        #region Commands
        private RelayCommand newTherapyCommand;

        public RelayCommand NewTherapyCommand
        {
            get { return newTherapyCommand; }
            set
            {
                newTherapyCommand = value;
                OnPropertyChanged("NewTherapyCommand");
            }
        }
        #endregion

        #region Actions
        private void Execute_NewTherapyCommand(object obj)
        {
            DoctorMainWindow.Instance._ViewModel.PatientChartView._ViewModel.InsideNavigationService.Navigate(new TherapyNew());
        }

        private bool CanExecute_NavigateCommand(object obj)
        {
            return true;
        }
        #endregion

        #region Constructor
        public TherapyViewModel()
        {
            this.NewTherapyCommand = new RelayCommand(Execute_NewTherapyCommand, CanExecute_NavigateCommand);
            this.Therapies = new ObservableCollection<Therapy>(ChartController.Instance.GetTherapiesByPatient(DoctorMainWindow.Instance._ViewModel.PatientChartView._ViewModel.Patient));
        }
        #endregion
    }
}
