using Model;
using Storages;
using System;
using System.Collections.ObjectModel;
using System.Windows;

namespace Hospital_IS.View
{
    /// <summary>
    /// Interaction logic for AppointmentPatient.xaml
    /// </summary>
    public partial class AppointmentPatient : Window
    {
        public ObservableCollection<DoctorAppointment> AvailableAppoitments { get; set; }
        public ObservableCollection<Doctor> TempDoctors { get; set; }
        public WorkDay day;
        public Doctor doctor;
        public DateTime date;
        public Patient patient;


        public AppointmentPatient()
        {
            InitializeComponent();
            this.DataContext = this;
            AvailableAppoitments = new ObservableCollection<DoctorAppointment>();
            TempDoctors = Hospital.Instance.Doctors;
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

        private void showAvailableApp(object sender, RoutedEventArgs e)
        {
            doctor = (Doctor)Doctors.SelectedItem;
            date = Calendar.SelectedDate.Value;
            int slotStart = 8;

            if (TimeSlot.Text.Equals("8:00-11:00"))
            {
                slotStart = 8;
            }
            else if (TimeSlot.Text.Equals("11:00-14:00"))
            {
                slotStart = 11;
            }
            else if (TimeSlot.Text.Equals("14:00-17:00"))
            {
                slotStart = 14;
            }
            else if (TimeSlot.Text.Equals("17:00-20:00"))
            {
                slotStart = 17;
            }
            
            ObservableCollection<DoctorAppointment> appList = new ObservableCollection<DoctorAppointment>();
            DoctorAppointment app1 = new DoctorAppointment(new DateTime(date.Year, date.Month, date.Day, slotStart, 0, 0), AppointmetType.CheckUp, false, doctor.PrimaryRoom, doctor, HomePatient.Instance.Patient);
            appList.Add(app1);
            DoctorAppointment app2 = new DoctorAppointment(new DateTime(date.Year, date.Month, date.Day, slotStart, 30, 0), AppointmetType.CheckUp, false, doctor.PrimaryRoom, doctor, HomePatient.Instance.Patient);
            appList.Add(app2);
            DoctorAppointment app3 = new DoctorAppointment(new DateTime(date.Year, date.Month, date.Day, slotStart + 1, 0, 0), AppointmetType.CheckUp, false, doctor.PrimaryRoom, doctor, HomePatient.Instance.Patient);
            appList.Add(app3);
            DoctorAppointment app4 = new DoctorAppointment(new DateTime(date.Year, date.Month, date.Day, slotStart + 1, 30, 0), AppointmetType.CheckUp, false, doctor.PrimaryRoom, doctor, HomePatient.Instance.Patient);
            appList.Add(app4);
            DoctorAppointment app5 = new DoctorAppointment(new DateTime(date.Year, date.Month, date.Day, slotStart + 2, 0, 0), AppointmetType.CheckUp, false, doctor.PrimaryRoom, doctor, HomePatient.Instance.Patient);
            appList.Add(app5);
            DoctorAppointment app6 = new DoctorAppointment(new DateTime(date.Year, date.Month, date.Day, slotStart + 2, 30, 0), AppointmetType.CheckUp, false, doctor.PrimaryRoom, doctor, HomePatient.Instance.Patient);
            appList.Add(app6);

            if (TimePriority.IsChecked == true)
            {
                foreach (Doctor doc in Hospital.Instance.Doctors)
                {
                    if (!doc.Id.Equals(doctor.Id))
                    {
                        DoctorAppointment appTime1 = new DoctorAppointment(new DateTime(date.Year, date.Month, date.Day, slotStart, 0, 0), AppointmetType.CheckUp, false, doctor.PrimaryRoom, doctor, HomePatient.Instance.Patient);
                        appList.Add(appTime1);
                        DoctorAppointment appTime2 = new DoctorAppointment(new DateTime(date.Year, date.Month, date.Day, slotStart, 30, 0), AppointmetType.CheckUp, false, doctor.PrimaryRoom, doctor, HomePatient.Instance.Patient);
                        appList.Add(appTime2);
                        DoctorAppointment appTime3 = new DoctorAppointment(new DateTime(date.Year, date.Month, date.Day, slotStart + 1, 0, 0), AppointmetType.CheckUp, false, doctor.PrimaryRoom, doctor, HomePatient.Instance.Patient);
                        appList.Add(appTime3);
                        DoctorAppointment appTime4 = new DoctorAppointment(new DateTime(date.Year, date.Month, date.Day, slotStart + 1, 30, 0), AppointmetType.CheckUp, false, doctor.PrimaryRoom, doctor, HomePatient.Instance.Patient);
                        appList.Add(appTime4);
                        DoctorAppointment appTime5 = new DoctorAppointment(new DateTime(date.Year, date.Month, date.Day, slotStart + 2, 0, 0), AppointmetType.CheckUp, false, doctor.PrimaryRoom, doctor, HomePatient.Instance.Patient);
                        appList.Add(appTime5);
                        DoctorAppointment appTime6 = new DoctorAppointment(new DateTime(date.Year, date.Month, date.Day, slotStart + 2, 30, 0), AppointmetType.CheckUp, false, doctor.PrimaryRoom, doctor, HomePatient.Instance.Patient);
                        appList.Add(appTime6);
                    }
                }
            }

            AvailableAppoitments.Clear();
            bool flag = false;
            foreach (DoctorAppointment ap in appList)
            {
                foreach (DoctorAppointment hospital in Hospital.Instance.allAppointments)
                {
                    if (ap.DateAndTime == hospital.DateAndTime && ap.Doctor.Id.Equals(hospital.Doctor.Id))
                        flag = true;
                }
                if (flag == false)
                {
                    AvailableAppoitments.Add(ap);
                }
                else
                {
                    flag = false;
                }
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
                HomePatient.Instance.DoctorAppointment.Add(docApp);
                Hospital.Instance.AddAppointment(docApp);
                docApp.Reserved = true;
                AvailableAppoitments.Remove(docApp);
            }
        }

        public void changeAppointment(DoctorAppointment docApp)
        {
            doctor = docApp.Doctor;
            date = docApp.DateAndTime;
            Calendar.SelectedDate = date;
            Calendar.DisplayDateStart = date;
            Calendar.DisplayDateEnd = new DateTime(date.Year, date.Month, date.Day + 3);
            change.Visibility = Visibility.Visible;
            reserve.Visibility = Visibility.Collapsed;
            int slotStart = 8;
            
            if (date.Hour < 11)
            {
                TimeSlot.SelectedIndex = 0;
                slotStart = 8;
            }
            else if (date.Hour < 14 && date.Hour >= 11)
            {
                TimeSlot.SelectedIndex = 1;
                slotStart = 11;
            }
            else if (date.Hour < 17 && date.Hour >= 14)
            {
                TimeSlot.SelectedIndex = 2;
                slotStart = 14;
            }
            else if (date.Hour < 20 && date.Hour >= 17)
            {
                TimeSlot.SelectedIndex = 3;
                slotStart = 17;
            }

            ObservableCollection<DoctorAppointment> appList = new ObservableCollection<DoctorAppointment>();
            DoctorAppointment app1 = new DoctorAppointment(new DateTime(date.Year, date.Month, date.Day, slotStart, 0, 0), AppointmetType.CheckUp, false, doctor.PrimaryRoom, doctor, HomePatient.Instance.Patient);
            appList.Add(app1);
            DoctorAppointment app2 = new DoctorAppointment(new DateTime(date.Year, date.Month, date.Day, slotStart, 30, 0), AppointmetType.CheckUp, false, doctor.PrimaryRoom, doctor, HomePatient.Instance.Patient);
            appList.Add(app2);
            DoctorAppointment app3 = new DoctorAppointment(new DateTime(date.Year, date.Month, date.Day, slotStart + 1, 0, 0), AppointmetType.CheckUp, false, doctor.PrimaryRoom, doctor, HomePatient.Instance.Patient);
            appList.Add(app3);
            DoctorAppointment app4 = new DoctorAppointment(new DateTime(date.Year, date.Month, date.Day, slotStart + 1, 30, 0), AppointmetType.CheckUp, false, doctor.PrimaryRoom, doctor, HomePatient.Instance.Patient);
            appList.Add(app4);
            DoctorAppointment app5 = new DoctorAppointment(new DateTime(date.Year, date.Month, date.Day, slotStart + 2, 0, 0), AppointmetType.CheckUp, false, doctor.PrimaryRoom, doctor, HomePatient.Instance.Patient);
            appList.Add(app5);
            DoctorAppointment app6 = new DoctorAppointment(new DateTime(date.Year, date.Month, date.Day, slotStart + 2, 30, 0), AppointmetType.CheckUp, false, doctor.PrimaryRoom, doctor, HomePatient.Instance.Patient);
            appList.Add(app6);

            AvailableAppoitments.Clear();
            bool flag = false;
            foreach (DoctorAppointment ap in appList)
            {
                foreach (DoctorAppointment hospital in Hospital.Instance.allAppointments)
                {
                    if (ap.DateAndTime == hospital.DateAndTime)
                        flag = true;
                }
                if (flag == false)
                {
                    AvailableAppoitments.Add(ap);
                }
                else
                {
                    flag = false;
                }
            }
        }

        private void changeAppointmentButton(object sender, RoutedEventArgs e)
        {
            DoctorAppointment docApp = (DoctorAppointment)listOfAppointments.SelectedItem; ;
            HomePatient.Instance.DoctorAppointment.Remove(HomePatient.Instance.changedApp);
            Hospital.Instance.RemoveAppointment(HomePatient.Instance.changedApp);
            HomePatient.Instance.DoctorAppointment.Add(docApp);
            Hospital.Instance.allAppointments.Add(docApp);
            docApp.Reserved = true;
            AvailableAppoitments.Remove(docApp);
        }

        private void logout(object sender, RoutedEventArgs e)
        {
            MainWindow login = new MainWindow();
            login.Show();
            this.Close();
            AppointmentFileStorage afs = new AppointmentFileStorage();
            afs.SaveAppointment(Hospital.Instance.allAppointments);
        }
    }
}
