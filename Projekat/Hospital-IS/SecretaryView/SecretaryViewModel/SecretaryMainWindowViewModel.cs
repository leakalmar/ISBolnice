using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace Hospital_IS.SecretaryView.SecretaryViewModel
{
    public class SecretaryMainWindowViewModel : BindableBase
    {
        public MyICommand<string> NavCommand { get; private set; }
        private BindableBase currentViewModel;
        private UCPatientsViewModel patientViewModel;
        public SecretaryMainWindowViewModel()
        {
            NavCommand = new MyICommand<string>(OnNav);
            patientViewModel = new UCPatientsViewModel();
            CurrentViewModel = patientViewModel;
        }
        public BindableBase CurrentViewModel
        {
            get { return currentViewModel; }
            set
            {
                SetProperty(ref currentViewModel, value);
                OnPropertyChanged("CurrentViewModel");
            }
        }

        private void OnNav(string destination)
        {
            switch (destination)
            {
                case "patients":
                    CurrentViewModel = patientViewModel;
                    break;
                case "appointments":
                    //CurrentViewModel = appointmentViewModel;
                    break;
                case "doctors":
                    //CurrentViewModel = allAppointmentsViewModel;
                    break;
                case "notifications":
                    //CurrentViewModel = therapyPatientViewModel;
                    break;
            }
        }
    }
}
