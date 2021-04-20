using Model;
using Storages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Hospital_IS.DoctorView
{
    /// <summary>
    /// Interaction logic for UCNewApp.xaml
    /// </summary>
    public partial class UCNewApp : UserControl
    {
        public DoctorAppointment Appointment { get; }
        private AppointmentFileStorage afs { get; } = new AppointmentFileStorage();
        public UCNewApp(DoctorAppointment appointment)
        {
            InitializeComponent();
            Appointment = appointment;
            doctors.DataContext = MainWindow.Doctors;
            rooms.DataContext = MainWindow.Rooms;

            string[] list = Enum.GetNames(typeof(AppointmetType));
            string[] docApp = new string[2];
            docApp[0] = list[0];
            docApp[1] = list[1];
            types.ItemsSource = docApp;
            types.SelectedIndex = 1;
        }



        private void filter_changed(object sender, SelectionChangedEventArgs e)
        {
            Doctor doc = (Doctor)doctors.SelectedItem;
            Room room = (Room)rooms.SelectedItem;
            SelectedDatesCollection dates = calendar.SelectedDates;
            String type = (string)types.SelectedItem;
            String dur = duration.Text;
            String[] parts = dur.Split(".");
            if (doc == null)
            {
                foreach (Doctor d in MainWindow.Doctors)
                {
                    if (d.Id.Equals(DoctorHomePage.Instance.GetDoctor().Id))
                    {
                        doctors.SelectedItem = d;
                        doc = d;
                    }
                }
            }
            if (room == null)
            {
                foreach (Room r in MainWindow.Rooms)
                {
                    if (r.RoomId.Equals(DoctorHomePage.Instance.GetDoctor().PrimaryRoom))
                    {
                        rooms.SelectedItem = r;
                        room = r;
                    }
                }
            }
            if (dates.Count == 0)
            {
                calendar.SelectedDate = DateTime.Now;
                dates = calendar.SelectedDates;
            }
            ObservableCollection<DoctorAppointment> appList = new ObservableCollection<DoctorAppointment>();


            foreach (DateTime d in dates)
            {
                DateTime last = new DateTime(d.Year, d.Month, d.Day, 8, 00, 00);
                System.Diagnostics.Debug.WriteLine(last.ToString());
                System.Diagnostics.Debug.WriteLine("D   " + d.ToString());
                while (last.TimeOfDay < new DateTime(DateTime.Now.Date.Year, DateTime.Now.Date.Month, DateTime.Now.Date.Day, 20, 00, 00).TimeOfDay)
                {
                    if (type == "CheckUp")
                    {
                        DoctorAppointment dt = new DoctorAppointment(new DateTime(d.Year, d.Month, d.Day, last.Hour, last.Minute, 0), new DateTime(d.Year, d.Month, d.Day, last.AddMinutes(30).Hour, last.AddMinutes(30).Minute, 0), AppointmetType.CheckUp, room.RoomId, doc, Appointment.Patient);
                        appList.Add(dt);
                        last = last.AddMinutes(30);
                    }
                    else
                    {
                        int hours = 0;
                        int minutes = 0;
                        if (parts.GetValue(0) != "")
                        {
                            hours = int.Parse((string)parts.GetValue(0));
                            minutes = 0;
                            if (parts.Length == 2)
                            {
                                minutes = 30;
                            }

                            DoctorAppointment dt = new DoctorAppointment(new DateTime(d.Year, d.Month, d.Day, last.Hour, last.Minute, 0), new DateTime(d.Year, d.Month, d.Day, last.AddHours(hours).Hour, last.AddMinutes(minutes).Minute, 0), AppointmetType.Operation, room.RoomId, doc, Appointment.Patient);
                            appList.Add(dt);
                            System.Diagnostics.Debug.WriteLine(dt.AppointmentStart.ToString());
                            last = last.AddHours(int.Parse((string)parts.GetValue(0)));
                            if (parts.Length == 2)
                            {
                                last = last.AddMinutes(30);
                            }
                        }
                        else
                        {
                            appointments.DataContext = new ObservableCollection<DoctorAppointment>();
                            return;
                        }
                       
                        
                    }

                }
            }

            //ObservableCollection<DoctorAppointment > ret = Hospital.Instance.CheckDoctorAppointments(appList,room.RoomId);
            if(type == "CheckUp")
            {
                foreach (DoctorAppointment d in appList)
                {
                    foreach (DoctorAppointment ap in Hospital.Instance.GetAllAppointmentsByDoctor(DoctorHomePage.Instance.GetDoctor()))
                    {
                        if (ap.Room == d.Room && ap.AppointmentStart == d.AppointmentStart && ap.AppointmentEnd == ap.AppointmentEnd)
                        {
                            d.Reserved = true;
                        }
                    }
                }
                appointments.DataContext = appList;
            }
            else
            {
                ObservableCollection<DoctorAppointment > ret = Hospital.Instance.CheckDoctorAppointments(appList,room.RoomId,dates);
                appointments.DataContext = ret;
            }

        }

        private void setAppointment_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                DoctorAppointment selected = (DoctorAppointment)appointments.SelectedItem;
                selected.Reserved = true;
                Hospital.Instance.AddAppointment(selected);
                DoctorHomePage.Instance.DoctorAppointment.Add(selected);
                ObservableCollection<DoctorAppointment> list = afs.GetAll();
                list.Add(selected);
                afs.SaveAppointment(list);

                DoctorHomePage.Instance.Home.Children.Remove(this);
                DoctorHomePage.Instance.Home.Children.Add(new UCPatientChart(Appointment, true));
            }
        }

        private void appointments_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DoctorAppointment selected = (DoctorAppointment)appointments.SelectedItem;
            selected.Reserved = true;
            Hospital.Instance.AddAppointment(selected);
            DoctorHomePage.Instance.DoctorAppointment.Add(selected);
            ObservableCollection<DoctorAppointment> list = afs.GetAll();
            list.Add(selected);
            afs.SaveAppointment(list);

            DoctorHomePage.Instance.Home.Children.Remove(this);
            DoctorHomePage.Instance.Home.Children.Add(new UCPatientChart(Appointment, true));
        }
    }
    
}
