using Controllers;
using Model;
using Storages;
using System;
using System.Collections.ObjectModel;
using System.Windows;

namespace Hospital_IS.View
{
    /// <summary>
    /// Interaction logic for AllAppointments.xaml
    /// </summary>
    public partial class AllAppointments : Window
    {
        public ObservableCollection<DoctorAppointment> DoctorAppointment { get; set; }

        public AllAppointments()
        {
            InitializeComponent();
            DoctorAppointment = new ObservableCollection<DoctorAppointment>(DoctorAppointmentController.Instance.GetAllAppointmentsByPatient(HomePatient.Instance.Patient.Id));
            this.DataContext = this;
        }

        private void home(object sender, RoutedEventArgs e)
        {
            HomePatient.Instance.Show();
            this.Close();
        }


        private void reserveApp(object sender, RoutedEventArgs e)
        {
            AppointmentPatient ap = new AppointmentPatient();
            ap.Show();
            this.Close();
        }

        private void showTherapy(object sender, RoutedEventArgs e)
        {
            TherapyPatient doc = new TherapyPatient();
            doc.Show();
            this.Close();
        }

        private void logout(object sender, RoutedEventArgs e)
        {
            MainWindow login = new MainWindow();
            login.Show();
            this.Close();
        }

        private void showNotifications(object sender, RoutedEventArgs e)
        {
            PatientNotifications notifications = new PatientNotifications();
            notifications.Show();
            this.Close();
        }

        private void showRow(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            DoctorAppointment docApp = (DoctorAppointment)dataGridAppointment.SelectedItem;
            Date.Text = docApp.AppointmentStart.ToString("dd.MM.yyyy.");
            Doctor.Content = docApp.Doctor.Name + docApp.Doctor.Surname;
            AppointmentType.Content = docApp.AppTypeText;
            Room.Content = docApp.Room;
            //Details.Text = 
            //Evaluation.Content = 
            //Comment.Text=
            if (docApp.AppointmentStart <= DateTime.Today)
            {
                evaluateApp.Visibility = Visibility.Visible;
            }
            AppointmentInfo.Visibility = Visibility.Visible;
        }

        private void showEvaluationWindow(object sender, RoutedEventArgs e)
        {
            if (PatientAppointmentEvaluationController.Instance.IsAppointmentEvaluated((DoctorAppointment)dataGridAppointment.SelectedItem))
            {
                MessageBox.Show("Pregled je već ocenjen!");
            }
            else
            {
                PatientAppointmentEvaluation appointmentEvaluation = new PatientAppointmentEvaluation((DoctorAppointment)dataGridAppointment.SelectedItem);
                appointmentEvaluation.Show();
            }
           
        }
    }
}
