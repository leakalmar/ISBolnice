using Controllers;
using Hospital_IS.DTOs;
using Hospital_IS.View.PatientViewModels;
using Model;
using Service;
using Storages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace Hospital_IS.View
{
    /// <summary>
    /// Interaction logic for AppointmentPatient.xaml
    /// </summary>
    public partial class AppointmentPatientView : UserControl
    {
        private AppointmentPatientViewModel appointmentPatientViewModel;

        public AppointmentPatientView()
        {
            InitializeComponent();
            //this.DataContext = this;
            //appointmentPatientViewModel = new AppointmentPatientViewModel();
            //this.DataContext = appointmentPatientViewModel;
            //AvailableAppointments = new ObservableCollection<DoctorAppointment>();
            //Doctors.DataContext = Hospital.Instance.Doctors;
            //DateTime today = DateTime.Today;
            Calendar.DisplayDateStart = DateTime.Today;
            //Calendar.SelectedDate = today;
        }
        /*
        private void home(object sender, RoutedEventArgs e)
        {

            PatientMainWindowView.Instance.Show();
            this.Close();

        }

        private void allApp(object sender, RoutedEventArgs e)
        {/*
            AllAppointments all = new AllAppointments();
            all.Show();
            this.Close();
        }
        /*
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
        private void ShowAvailableApp(object sender, RoutedEventArgs e)
        {            
            doctor = (Doctor)Doctors.SelectedItem;
            date = Calendar.SelectedDate.Value;
            bool timePriority = false;
            if (TimePriority.IsChecked == true)
            {
                timePriority = true;
            }
            
            AvailableAppointments.Clear();
            PossibleAppointmentForPatientDTO possibleAppointment = new PossibleAppointmentForPatientDTO(TimeSlot.Text, doctor, PatientMainWindowViewModel.Patient, date, timePriority);
            List<DoctorAppointment> docApps = DoctorAppointmentController.Instance.SuggestAppointmentsToPatient(possibleAppointment);
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
                if (!PatientController.Instance.IsPatientTroll(PatientMainWindowViewModel.Patient, docApp))
                {
                    PatientMainWindowView.Instance.DoctorAppointment.Add(docApp);
                    DoctorAppointmentController.Instance.AddAppointment(docApp);
                    docApp.Reserved = true;
                    //AvailableAppointments.Remove(docApp);
                }
                else
                {
                    MessageBox.Show("Zbog učestalog zakazivanja ili izmene termina, ne možete zakazati termin!");
                }               
            }
        }
        */
        public void RescheduleAppointment(DoctorAppointment docApp)
        {
            int maximumDayDifference = 3;
            DateTime date = docApp.AppointmentStart.Date;
            Calendar.DisplayDateEnd = date.AddDays(maximumDayDifference);
            Calendar.DisplayDateStart = date;
            Calendar.SelectedDate = date;
            change.Visibility = Visibility.Visible;         //Dugme za izmenu termina pregleda
            reserve.Visibility = Visibility.Collapsed;      //Dugme za zakazivanje pregleda
            
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
            appointmentPatientViewModel.SetRescheduleAppointmentView(docApp);
        }
        /*
        private void RescheduleAppointmentButton(object sender, RoutedEventArgs e)
        {
            DoctorAppointment docApp = (DoctorAppointment)listOfAppointments.SelectedItem;
            if (docApp == null)
            {
                MessageBox.Show("Izaberite termin!");
            }
            else
            {
                if (!PatientController.Instance.IsPatientTroll(PatientMainWindowViewModel.Patient, docApp))
                {
                    DoctorAppointmentController.Instance.UpdateAppointment(PatientMainWindowView.Instance.rescheduledApp, docApp);
                    PatientMainWindowView.Instance.DoctorAppointment.Remove(PatientMainWindowView.Instance.rescheduledApp);
                    PatientMainWindowView.Instance.DoctorAppointment.Add(docApp);
                    docApp.Reserved = true;
                    //AvailableAppointments.Remove(docApp);
                }
                else
                {
                    MessageBox.Show("Zbog učestalog zakazivanja ili izmene termina, ne možete zakazati termin!");
                }
            }          
        }
        *//*
        private void logout(object sender, RoutedEventArgs e)
        {
            MainWindow login = new MainWindow();
            login.Show();
            this.Close();
        }
    */
    }
}
