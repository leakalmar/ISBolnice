using Model;
using Storages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;


namespace Hospital_IS.View
{
    /// <summary>
    /// Interaction logic for HomePatient.xaml
    /// </summary>
    public partial class HomePatient : Window
    {
        private static HomePatient instance = null;
        public static HomePatient Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new HomePatient();
                }
                return instance;
            }
        }
        
        public Patient Patient { get; set; }
        public DoctorAppointment changedApp;
        public ObservableCollection<DoctorAppointment> DoctorAppointment { get; set; }
        public HomePatient()
        {
            InitializeComponent();

            Patient = MainWindow.PatientUser;
            this.DataContext = this;
            PersonalData.DataContext = Patient;
        }

        private void reserveApp(object sender, RoutedEventArgs e)
        {
            
            AppointmentPatient ap = new AppointmentPatient();
            ap.Show();
            this.Hide();
        }

        private void allApp(object sender, RoutedEventArgs e)
        {

            AllAppointments ap = new AllAppointments();
            ap.Show();
            this.Hide();
        }

        private void showDoc(object sender, RoutedEventArgs e)
        {
            DocumentationPatient doc = new DocumentationPatient();
            doc.Show();
            this.Hide();
        }

        private void deleteAppointment(object sender, RoutedEventArgs e)
        {
            DoctorAppointment doctorApp = (DoctorAppointment)dataGridAppointment.SelectedItem;
            if (doctorApp == null)
            {
                MessageBox.Show("Izaberite termin!");
            }
            else
            {
                Hospital.Instance.RemoveAppointment(doctorApp);
                DoctorAppointment.Remove(doctorApp);
                doctorApp.Reserved = false;
            }
            
        }

        private void changeAppointment(object sender, RoutedEventArgs e)
        {
            changedApp = (DoctorAppointment)dataGridAppointment.SelectedItem;
            if (changedApp == null)
            {
                MessageBox.Show("Izaberite termin!");
            }
            else
            {
                AppointmentPatient ap = new AppointmentPatient();
                ap.Show();
                ap.changeAppointment(changedApp);
            }
        }
        
        private void logout(object sender, RoutedEventArgs e)
        {
            MainWindow login = new MainWindow();
            login.Show();
            this.Hide();
            AppointmentFileStorage afs = new AppointmentFileStorage();
            afs.SaveAppointment(Hospital.Instance.allAppointments);
        }
    }
}
