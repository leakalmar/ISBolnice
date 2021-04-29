﻿using Controllers;
using Model;
using Service;
using Storages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace Hospital_IS.View
{
    /// <summary>
    /// Interaction logic for AppointmentPatient.xaml
    /// </summary>
    public partial class AppointmentPatient : Window
    {
        public ObservableCollection<DoctorAppointment> AvailableAppointments { get; set; }
        private Doctor doctor;
        private DateTime date;

        public AppointmentPatient()
        {
            InitializeComponent();
            this.DataContext = this;
            AvailableAppointments = new ObservableCollection<DoctorAppointment>();
            Doctors.DataContext = Hospital.Instance.Doctors;
            DateTime today = DateTime.Today;
            Calendar.DisplayDateStart = today;
            Calendar.SelectedDate = today;
        }

        private void home(object sender, RoutedEventArgs e)
        {

            HomePatient.Instance.Show();
            this.Close();

        }

        private void allApp(object sender, RoutedEventArgs e)
        {
            AllAppointments all = new AllAppointments();
            all.Show();
            this.Close();
        }

        private void showTherapy(object sender, RoutedEventArgs e)
        {
            TherapyPatient doc = new TherapyPatient();
            doc.Show();
            this.Close();
        }

        private void showNotifications(object sender, RoutedEventArgs e)
        {
            PatientNotifications notifications = new PatientNotifications();
            notifications.Show();
            this.Close();
        }
        //Drugi doktor je hardcode-ovan u FSDoctor klasi,samo radi pokazivanja funkcionalnosti(Samo ga otkomentarisati pri pokretanju da bi se prikazao)
        private void showAvailableApp(object sender, RoutedEventArgs e)
        {            
            doctor = (Doctor)Doctors.SelectedItem;
            date = Calendar.SelectedDate.Value;
            bool timePriority = false;
            if (TimePriority.IsChecked == true)
            {
                timePriority = true;
            }
            
            AvailableAppointments.Clear();
            List<DoctorAppointment> docApps = DoctorAppointmentController.Instance.SuggestAppointmentsToPatient(TimeSlot.Text, doctor, HomePatient.Instance.Patient, date, timePriority);
            foreach (DoctorAppointment doctorAppointment in docApps)
            {
                AvailableAppointments.Add(doctorAppointment);
            }
        }

        private void reserveAppointment(object sender, RoutedEventArgs e)
        {
            DoctorAppointment docApp = (DoctorAppointment)listOfAppointments.SelectedItem;
            if (docApp == null)
            {
                MessageBox.Show("Izaberite termin!");
            }
            else
            {
                if (!PatientController.Instance.IsPatientTroll(HomePatient.Instance.Patient, docApp))
                {
                    HomePatient.Instance.DoctorAppointment.Add(docApp);
                    DoctorAppointmentController.Instance.AddAppointment(docApp);
                    docApp.Reserved = true;
                    AvailableAppointments.Remove(docApp);
                }
                else
                {
                    MessageBox.Show("Zbog učestalog zakazivanja ili izmene termina, ne možete zakazati termin!");
                }               
            }
        }

        public void changeAppointment(DoctorAppointment docApp)
        {
            Doctors.SelectedItem = docApp.Doctor;
            date = docApp.AppointmentStart;
            Calendar.SelectedDate = date;
            Calendar.DisplayDateStart = date;
            Calendar.DisplayDateEnd = date.AddDays(3);
            change.Visibility = Visibility.Visible;
            reserve.Visibility = Visibility.Collapsed;
            
            if (date.Hour < 11)
            {
                TimeSlot.SelectedIndex = 0;
            }
            else if (date.Hour < 14 && date.Hour >= 11)
            {
                TimeSlot.SelectedIndex = 1;
            }
            else if (date.Hour < 17 && date.Hour >= 14)
            {
                TimeSlot.SelectedIndex = 2;
            }
            else if (date.Hour < 20 && date.Hour >= 17)
            {
                TimeSlot.SelectedIndex = 3;
            }
            
            AvailableAppointments.Clear();
            List<DoctorAppointment> docApps = DoctorAppointmentController.Instance.SuggestAppointmentsToPatient(TimeSlot.Text, docApp.Doctor, HomePatient.Instance.Patient, date, false);
            foreach (DoctorAppointment doctorAppointment in docApps)
            {
                AvailableAppointments.Add(doctorAppointment);
            }
            
        }

        private void changeAppointmentButton(object sender, RoutedEventArgs e)
        {
            DoctorAppointment docApp = (DoctorAppointment)listOfAppointments.SelectedItem;
            if (docApp == null)
            {
                MessageBox.Show("Izaberite termin!");
            }
            else
            {
                if (!PatientController.Instance.IsPatientTroll(HomePatient.Instance.Patient, docApp))
                {
                    DoctorAppointmentController.Instance.UpdateAppointment(HomePatient.Instance.changedApp, (DoctorAppointment)listOfAppointments.SelectedItem);
                    HomePatient.Instance.DoctorAppointment.Remove(HomePatient.Instance.changedApp);
                    HomePatient.Instance.DoctorAppointment.Add(docApp);
                    docApp.Reserved = true;
                    AvailableAppointments.Remove(docApp);
                }
                else
                {
                    MessageBox.Show("Zbog učestalog zakazivanja ili izmene termina, ne možete zakazati termin!");
                }
            }          
        }

        private void logout(object sender, RoutedEventArgs e)
        {
            MainWindow login = new MainWindow();
            login.Show();
            this.Close();
        }

    }
}
