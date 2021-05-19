using Controllers;
using Model;
using Storages;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;

namespace Hospital_IS.DoctorView
{
    public partial class UCPatientChart : UserControl
    {
        public Patient Patient;
        public DoctorAppointment Appointment;

        public Report ReportObject { get; set; }
        public UCReport ReportView { get; set; }
        public UCGeneralInfo General { get; set; }
        public UCHistory History { get; set; }
        public UCScheduledApp App { get; set; }
        public UCTherapy Therapy{get;set;}
        public UCTest Test { set; get; }
        public int Last { get; set; }

        public UCPatientChart(NavigationService navigation)
        {

        }

        public UCPatientChart(DoctorAppointment appointment,bool start = false)
        {
            InitializeComponent();
            ReportView = new UCReport(this);
            Appointment = appointment;
            Patient = Appointment.Patient;
            General = new UCGeneralInfo(this);
            History = new UCHistory(this);
            App = new UCScheduledApp(this);
            Therapy = new UCTherapy(this);
            Test = new UCTest(this);

            if (start)
            {
                
                end.Visibility = Visibility.Visible;
                back.Visibility = Visibility.Collapsed;
                reportBtn.Visibility = Visibility.Visible;                
                patientInfo.Children.Add(ReportView);
                Last = 0;
            }
            else
            {
                patientInfo.Children.Add(General);
                Last = 1;
            }


            
            PersonalData.DataContext = Patient;
            

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
                    patientInfo.Children.Remove(ReportView);
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
                    patientInfo.Children.Add(ReportView);
                    Last = 0;
                    break;
                case 1:
                    General.Started = started;
                    patientInfo.Children.Add(General);
                    Last = 1;
                    break;
                case 2:
                    History.Started = started;
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
            if(e.Key == Key.Escape)
            {
                //DoctorHomePage.Instance.Home.Children.Remove(this);
                //DoctorAppointmentController.Instance.RemoveAppointment(Appointment);
                //Appointment.Report.Anamnesis = Report.reportDetail.Text;
                //DoctorAppointmentController.Instance.AddAppointment(Appointment);
                //DoctorHomePage.Instance.Home.Children.Add(this);
            }
        }

        private void patientInfo_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (patientInfo.Visibility == Visibility.Collapsed)
            {
               // DoctorHomePage.Instance.Home.Children.Remove(this);
            }
        }

        private void EndApp_KeyDown(object sender, KeyEventArgs e)
        {
            DoctorAppointmentController.Instance.EndAppointment(Appointment);
            ChartController.Instance.AddReport(Appointment, ReportView.reportDetail.Text, ReportView.Prescriptions.Count, Patient);
            ChartController.Instance.AddPrescriptions(new List<Prescription>(ReportView.Prescriptions), Patient);

           // DoctorHomePage.Instance.Home.Children.Remove(this);
           // DoctorHomePage.Instance.Home.Children.Add(new UCAppDetail(null));
        }

        //Kada se vraca u AppDetail treba da refreshuje i njega i pocetnu stranu inace se ne zakazu novo zakazani termini! Jer nisam stavila da se dodaju u onu listu
        // koja je u HomePage.DoctorAppointments 
        private void EndAppointment_Click(object sender, RoutedEventArgs e)
        {
            DoctorAppointmentController.Instance.EndAppointment(Appointment);
            ChartController.Instance.AddReport(Appointment, ReportView.reportDetail.Text, ReportView.Prescriptions.Count, Patient);
            ChartController.Instance.AddPrescriptions(new List<Prescription>(ReportView.Prescriptions), Patient);

            //DoctorHomePage.Instance.Home.Children.Remove(this);
            UCAppDetail r = new UCAppDetail(null);
            //DoctorHomePage.Instance.Home.Children.Add(r);
        }

        private void back_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (back.IsVisible)
            {
                //DoctorHomePage.Instance.Home.Children.Remove(this);
                //DoctorHomePage.Instance.Home.Children.Add(new UCAppDetail());

            }
        }
    }
}
