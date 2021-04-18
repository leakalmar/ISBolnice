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
using Hospital_IS.Storages;
using Model;
using Storages;
using Hospital_IS.DoctorView;

namespace Hospital_IS.DoctorView
{
    /// <summary>
    /// Interaction logic for PatientChart.xaml
    /// </summary>
    public partial class PatientChart : Window
    {
        public DoctorAppointment Appointment;
        AppointmentFileStorage afs = new AppointmentFileStorage();
        ObservableCollection<DoctorAppointment> patientDocApp { get; set; }

        public PatientChart(DoctorAppointment appointment, bool activButton = false, bool endAppointmentBtn = false)
        {
            InitializeComponent();
            reportBtn.IsEnabled = activButton;
            prescriptionBtn.IsEnabled = activButton;
            testBtn.IsEnabled = activButton;
            newAppointmentBtn.IsEnabled = activButton;
            endBtn.IsEnabled = activButton;


            if (activButton)
            {
                cancleBtn.Visibility = Visibility.Hidden;
                updateBtn.Visibility = Visibility.Hidden;
                if (endAppointmentBtn)
                {
                    endBtn.Visibility = Visibility.Visible;
                }
                else
                {
                    endBtn.Visibility = Visibility.Hidden;
                }
                
            }
            else
            {
                cancleBtn.Visibility = Visibility.Visible;
                updateBtn.Visibility = Visibility.Visible;
                endBtn.Visibility = Visibility.Hidden;
            }


            patientDocApp = afs.GetAllByPatient(appointment.Patient.Id);
            Appointment = appointment;
            PersonalData.DataContext = appointment.Patient;
            HistoryGrid.DataContext = appointment.Patient;
            patientAppointments.DataContext = patientDocApp;
            patientHistory.DataContext = appointment.Patient;

        }

        public void Report_DoubleClicked(object sender, MouseButtonEventArgs e)
        {
            Model.Report report = (Model.Report)patientHistory.SelectedItem;
            Window window = new Report(report);
            window.Show();
        }

        public void ExitBtnClick(object sender, RoutedEventArgs e)
        {
            //Treba settovati appointment na zavrsen
            bool dialog = (bool)new ExitMess("Da li ste sigurni da želite da zavrsite pregled?").ShowDialog();
            if (dialog)
            {
                this.Close();
            }
        }

        public void MinimizeBtnClick(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        public void MaximizeBtnClick(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Maximized;
        }

        private void appointmentBtn(object sender, RoutedEventArgs e)
        {
            Window w = new DoctorSetAppointment(Appointment);
            w.Show();
        }

        private void updateBtn_Click(object sender, RoutedEventArgs e)
        {
            Window w = new DoctorSetAppointment(Appointment,true);
            w.Show();
        }

        private void CancleBtn_Click(object sender, RoutedEventArgs e)
        {
            Hospital.Instance.allAppointments.Remove(Appointment);
            DoctorHomePage.Instance.DoctorAppointment.Remove(Appointment);
            MainWindow login = new MainWindow();
            AppointmentFileStorage afs = new AppointmentFileStorage();
            afs.SaveAppointment(Hospital.Instance.allAppointments);
            this.Close();
        }

        private void futureApp_DoubleClicked(object sender, MouseButtonEventArgs e)
        {
            DoctorAppointment future = (DoctorAppointment)patientAppointments.SelectedItem;
            Window window = new DoctorSetAppointment(future,true);
            window.Show();
        }
    }

    
}
