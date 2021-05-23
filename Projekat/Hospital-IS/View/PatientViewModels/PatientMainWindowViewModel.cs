﻿using Controllers;
using Model;
using System;
using System.Collections.Generic;
using System.Text;
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
            CurrentViewModel = homePatientViewModel;
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
            }
        }

        public void changeApp(DoctorAppointment docApp)
        {
            MessageBox.Show("OK11111");
            AppointmentPatientViewModel a = new AppointmentPatientViewModel();
            a.SetRescheduleAppointmentView(docApp);
            CurrentViewModel = a;
        }

        public HomePatientViewModel HomePatientViewModel
        {
            get { return homePatientViewModel; }
            set { }
        }
    }
}