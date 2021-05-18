using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital_IS.View.PatientViewModels
{
    public class PatientMainWindowViewModel : BindableBase
    {
        public MyICommand<string> NavCommand { get; private set; }
        private HomePatientViewModel homePatientViewModel = new HomePatientViewModel();
        private AppointmentPatientViewModel appointmentViewModel = new AppointmentPatientViewModel();
        private AllAppointmentsViewModel allAppointmentsViewModel = new AllAppointmentsViewModel();
        private TherapyPatientViewModel therapyPatientViewModel = new TherapyPatientViewModel();
        private PatientNotificationsViewModel notificationsViewModel = new PatientNotificationsViewModel();
        private BindableBase currentViewModel;

        public PatientMainWindowViewModel()
        {
            NavCommand = new MyICommand<string>(OnNav);
            CurrentViewModel = homePatientViewModel;
        }

        public BindableBase CurrentViewModel
        {
            get { return currentViewModel; }
            set
            {
                SetProperty(ref currentViewModel, value);
            }
        }

        private void OnNav(string destination)
        {
            switch (destination)
            {
                case "home":
                    CurrentViewModel = homePatientViewModel;
                    break;
                case "reserve":
                    CurrentViewModel = appointmentViewModel;
                    break;
                case "allApp":
                    CurrentViewModel = allAppointmentsViewModel;
                    break;
                case "therapy":
                    CurrentViewModel = therapyPatientViewModel;
                    break;
                case "notifications":
                    CurrentViewModel = notificationsViewModel;
                    break;
            }
        }
    }
}
