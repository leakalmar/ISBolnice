using Model;
using Storages;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Hospital_IS.View
{
    /// <summary>
    /// Interaction logic for UCPatientChart.xaml
    /// </summary>
    public partial class UCPatientChart : UserControl
    {
        public UCReport report;
        public DoctorAppointment Appointment;
        AppointmentFileStorage afs = new AppointmentFileStorage();

        public UCPatientChart(DoctorAppointment appointment, bool start = false)
        {
            InitializeComponent();
            if (start)
            {
                end.Visibility = Visibility.Visible;
                back.Visibility = Visibility.Collapsed;
                reportBtn.Visibility = Visibility.Visible;
            }


            Appointment = appointment;
            PersonalData.DataContext = appointment.Patient;
            report = new UCReport();
            patientInfo.Content = report;

        }

        public void Report_DoubleClicked(object sender, MouseButtonEventArgs e)
        {
            //Model.Report report = (Model.Report)patientHistory.SelectedItem;
            //Window window = new Report(report);
            //window.Show();
        }

        public void ExitBtnClick(object sender, RoutedEventArgs e)
        {
            //Treba settovati appointment na zavrsen
            bool dialog = (bool)new ExitMess("Da li ste sigurni da želite da završite pregled?").ShowDialog();
            if (dialog)
            {
                DoctorHomePage.Instance.Home.Content = new UserControlHomePage();
            }
        }

        private void appointmentBtn(object sender, RoutedEventArgs e)
        {
            Window w = new DoctorSetAppointment(Appointment);
            w.Show();
        }

        private void updateBtn_Click(object sender, RoutedEventArgs e)
        {
            Window w = new DoctorSetAppointment(Appointment, true);
            w.Show();
        }

        private void CancleBtn_Click(object sender, RoutedEventArgs e)
        {
            Hospital.Instance.allAppointments.Remove(Appointment);
            DoctorHomePage.Instance.DoctorAppointment.Remove(Appointment);
            MainWindow login = new MainWindow();
            AppointmentFileStorage afs = new AppointmentFileStorage();
            afs.SaveAppointment(Hospital.Instance.allAppointments);
            DoctorHomePage.Instance.Home.Content = new UserControlHomePage();
        }

        private void futureApp_DoubleClicked(object sender, MouseButtonEventArgs e)
        {
            //DoctorAppointment future = (DoctorAppointment)patientAppointments.SelectedItem;
            //Window window = new DoctorSetAppointment(future, true);
            //window.Show();
        }

        private void TabBtnClick(object sender, RoutedEventArgs e)
        {
            int index = int.Parse(((Button)e.Source).Uid);

           

            bool started = false;
            if (end.Visibility == Visibility.Visible)
            {
                started = true;
                marker.Margin = new Thickness(120 * index, 0, 0, 0);
            }
            else
            {
                marker.Margin = new Thickness(120 * index - 120, 0, 0, 0);
            }

            switch (index)
            {
                case 0:
                    patientInfo.Content = report;
                    break;
                case 1:
                    patientInfo.Content = new UCGeneralInfo(Appointment.Patient, started);
                    break;
                case 2:
                    patientInfo.Content = new UCHistory(Appointment.Patient);
                    break;
                case 3:
                    patientInfo.Content = new UCScheduledApp(Appointment.Patient, started);
                    break;
                case 4:
                    patientInfo.Content = new UCTherapy(Appointment.Patient, started);
                    break;
                case 5:
                    patientInfo.Content = new UCTest(Appointment.Patient, started);
                    break;
            }
        }
    }
}
