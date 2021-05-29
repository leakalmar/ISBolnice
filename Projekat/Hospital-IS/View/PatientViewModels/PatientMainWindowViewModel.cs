using Controllers;
using Hospital_IS.Model;
using Model;
using System;
using System.Windows;
using System.Windows.Threading;

namespace Hospital_IS.View.PatientViewModels
{
    public class PatientMainWindowViewModel : BindableBase
    {
        public MyICommand<string> NavCommand { get; private set; }
        private HomePatientViewModel homePatientViewModel;
        private AppointmentPatientViewModel appointmentViewModel;
        private AllAppointmentsViewModel allAppointmentsViewModel;
        private TherapyPatientViewModel therapyPatientViewModel;
        private PatientNotificationsViewModel notificationsViewModel;
        private PatientUpdateProfileViewModel updateProfileViewModel;
        private BindableBase currentViewModel;
        public static Patient Patient{ get; set;}

        public PatientMainWindowViewModel()
        {
            Patient = MainWindow.PatientUser;
            NavCommand = new MyICommand<string>(OnNav);
            homePatientViewModel = new HomePatientViewModel();
            appointmentViewModel = new AppointmentPatientViewModel();
            allAppointmentsViewModel = new AllAppointmentsViewModel();
            therapyPatientViewModel = new TherapyPatientViewModel();
            notificationsViewModel = new PatientNotificationsViewModel();
            updateProfileViewModel = new PatientUpdateProfileViewModel();
            CurrentViewModel = homePatientViewModel;
            CheckDailyNotifications();
            DispatcherTimer dispatcherTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMinutes(1)
            };
            dispatcherTimer.Tick += Timer_Tick;
            dispatcherTimer.Start();
        }
        
        private void Timer_Tick(object sender, EventArgs e)
        {
            DateTime time = DateTime.Now;

            foreach (Therapy therapy in ChartController.Instance.GetTherapiesByPatient(Patient))
            {
                int usageHourDifference = (int)24 / therapy.TimesADay;
                for (int i = 0; i < therapy.TimesADay; i++)
                {
                    if (time.AddHours(2).Hour == (therapy.FirstUsageTime + i * usageHourDifference) && time.Minute == 00)
                    {
                        MessageBox.Show("Za 2 sata treba da popijete lek: " + therapy.Medicine.Name);
                    }
                }
            }

            foreach (PatientNote note in PatientController.Instance.GetNotesByPatient(Patient.Id))
            {
                if (time.Hour == note.NotificationTime && time.Minute == 00 && note.IsNotifyChecked)
                {
                    MessageBox.Show("Podsetnik: " + note.NoteContent);
                }
            }
        }

        private  void CheckDailyNotifications()
        {
            DateTime time = DateTime.Now;

            foreach (Therapy therapy in ChartController.Instance.GetTherapiesByPatient(Patient))
            {
                int usageHourDifference = (int)24 / therapy.TimesADay;
                for (int i = 0; i < therapy.TimesADay; i++)
                {
                    if (time.Hour >= (therapy.FirstUsageTime + i * usageHourDifference) && time.Hour <= (therapy.FirstUsageTime + i * usageHourDifference) + 2)
                    {
                        MessageBox.Show("Trebalo je da popijete lek " + therapy.Medicine.Name + " u " + (therapy.FirstUsageTime + i * usageHourDifference) + "h.");
                    }
                }
            }

            foreach (PatientNote note in PatientController.Instance.GetNotesByPatient(Patient.Id))
            {
                if (time.Hour >= note.NotificationTime && time.Hour <= note.NotificationTime + 2 && note.IsNotifyChecked)
                {
                    MessageBox.Show("Podsetnik podešen za " + note.NotificationTime + "h: " + note.NoteContent);
                }
            }
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
                case "updateProfile":
                    CurrentViewModel = updateProfileViewModel;
                    break;
            }
        }

    }
}
