﻿using Hospital_IS.DoctorRole.Commands;
using Hospital_IS.DoctorRole.DoctorView;
using Hospital_IS.DTOs.SecretaryDTOs;

namespace Hospital_IS.DoctorViewModel
{
    public class GeneralInfoViewModel : BindableBase
    {
        #region Feilds
        private PatientDTO patient;
        private bool started;

        public PatientDTO Patient
        {
            get { return patient; }
            set
            {
                patient = value;
                OnPropertyChanged("Patient");
            }
        }

        public bool Started
        {
            get { return started; }
            set
            {
                started = value;
                OnPropertyChanged("Started");
            }
        }
        #endregion

        #region Commands
        private RelayCommand changeInfoCommand;

        public RelayCommand ChangeInfoCommand
        {
            get { return changeInfoCommand; }
            set
            {
                changeInfoCommand = value;
            }
        }
        #endregion

        #region Actions

        private void Execute_ChangeInfoCommand(object obj)
        {
            DoctorMainWindow.Instance._ViewModel.PatientChartView._ViewModel.InsideNavigationService.Navigate(new GeneralInfoChange());
        }

        private bool CanExecute_NavigateCommand(object obj)
        {
            return true;
        }
        #endregion

        #region Constructor
        public GeneralInfoViewModel()
        {
            this.ChangeInfoCommand = new RelayCommand(Execute_ChangeInfoCommand, CanExecute_NavigateCommand);
            Patient = DoctorMainWindow.Instance._ViewModel.PatientChartView._ViewModel.Patient;
        }
        #endregion

    }


}
