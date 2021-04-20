using Model;
using Storages;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Hospital_IS.DoctorView
{
    /// <summary>
    /// Interaction logic for UCPatientChart.xaml
    /// </summary>
    public partial class UCPatientChart : UserControl
    {
        private UCReport report;
        private DoctorAppointment Appointment;
        AppointmentFileStorage afs = new AppointmentFileStorage();
        private UCReport Report { get; set; }
        private UCGeneralInfo General { get; set; }
        private UCHistory History { get; set; }
        private UCScheduledApp App { get; set; }
        private UCTherapy Therapy{get;set;}
        private UCTest Test { set; get; }
        private int Last { get; set; }


        public UCPatientChart(DoctorAppointment appointment, bool start = false)
        {
            InitializeComponent();

            if (appointment.Report == null & start)
            {
                appointment.Report = new Model.Report(appointment.DateAndTime);
            }
            Appointment = appointment;
            Report = new UCReport(appointment);
            General = new UCGeneralInfo(Appointment.Patient);
            History = new UCHistory(Appointment.Patient);
            App = new UCScheduledApp(Appointment.Patient);
            Therapy = new UCTherapy(Appointment.Patient);
            Test = new UCTest(Appointment.Patient);

            if (start)
            {
                
                end.Visibility = Visibility.Visible;
                back.Visibility = Visibility.Collapsed;
                reportBtn.Visibility = Visibility.Visible;                
                patientInfo.Children.Add(Report);
                Last = 0;
            }
            else
            {
                patientInfo.Children.Add(General);
                Last = 1;
            }


            
            PersonalData.DataContext = appointment.Patient;
            

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
                DoctorHomePage.Instance.Home.Children.Remove(this);
                DoctorHomePage.Instance.Home.Children.Add(DoctorHomePage.Instance.HomePage);
            }
        }

        private void appointmentBtn(object sender, RoutedEventArgs e)
        {
            //Window w = new DoctorSetAppointment(Appointment);
            //w.Show();
        }

        private void updateBtn_Click(object sender, RoutedEventArgs e)
        {
            //Window w = new DoctorSetAppointment(Appointment, true);
            //w.Show();
        }

        private void CancleBtn_Click(object sender, RoutedEventArgs e)
        {
            Hospital.Instance.allAppointments.Remove(Appointment);
            DoctorHomePage.Instance.DoctorAppointment.Remove(Appointment);
            MainWindow login = new MainWindow();
            AppointmentFileStorage afs = new AppointmentFileStorage();
            afs.SaveAppointment(Hospital.Instance.allAppointments);
            DoctorHomePage.Instance.Home.Children.Add(DoctorHomePage.Instance.HomePage);
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

            switch (Last)
            {
                case 0:
                    patientInfo.Children.Remove(Report);
                    break;
                case 1:
                    patientInfo.Children.Remove(General);
                    break;
                case 2:
                    patientInfo.Children.Remove(History);
                    break;
                case 3:
                    patientInfo.Children.Remove(App);
                    break;
                case 4:
                    patientInfo.Children.Remove(Therapy);
                    break;
                case 5:
                    patientInfo.Children.Remove(Test);
                    break;
                default:
                    break;
            }
            switch (index)
            {
                case 0:
                    patientInfo.Children.Add(Report);
                    Last = 0;
                    break;
                case 1:
                    General.Started = started;
                    patientInfo.Children.Add(General);
                    Last = 1;
                    break;
                case 2:
                    patientInfo.Children.Add(History);
                    Last = 2;
                    break;
                case 3:
                    App.Started = started;
                    patientInfo.Children.Add(App);
                    Last = 3;
                    break;
                case 4:
                    Therapy.Started = started;
                    patientInfo.Children.Add(Therapy);
                    Last = 4;
                    break;
                case 5:
                    Test.Started = started;
                    patientInfo.Children.Add(Test);
                    Last = 5;
                    break;
            }
        }

        private void back_clicked(object sender, KeyEventArgs e)
        {
            DoctorHomePage.Instance.Home.Children.Remove(this);
        }

        private void patientInfo_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (patientInfo.Visibility == Visibility.Collapsed)
            {
                DoctorHomePage.Instance.Home.Children.Remove(this);
            }
        }

        private void EndApp_KeyDown(object sender, KeyEventArgs e)
        {
            Appointment.Report.Amnesis = Report.reportDetail.Text;
            afs.UpdateAppointment(Appointment);
            DoctorHomePage.Instance.Home.Children.Remove(this);
            DoctorHomePage.Instance.Home.Children.Add(new UCAppDetail(null));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Appointment.Report.Amnesis = Report.reportDetail.Text;
            if(Appointment.Patient.MedicalHistory.GetByAppointment(Appointment).Count != 0)
            {
                Appointment.Report.HaveRecipe = true;
            }
            afs.UpdateAppointment(Appointment);
            DoctorHomePage.Instance.Home.Children.Remove(this);
            UCAppDetail r = new UCAppDetail(null);
            if(r.docotrAppointments.Items.Count == 0)
            {
                int index = DoctorHomePage.Instance.Home.Children.IndexOf(DoctorHomePage.Instance.HomePage);
                DoctorHomePage.Instance.Home.Children[index].Visibility = Visibility.Visible;
            }
            else
            {
                DoctorHomePage.Instance.Home.Children.Add(r);
            }
        }
    }
}
