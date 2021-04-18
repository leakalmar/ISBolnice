using Hospital_IS.Storages;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Hospital_IS.DoctorView
{
    /// <summary>
    /// Interaction logic for UCChangeApp.xaml
    /// </summary>
    public partial class UCChangeApp : UserControl
    {
        ContentControl panel;
        private ObservableCollection<DoctorAppointment> AvailableAppoitments { get; set; }

        private DoctorAppointment _appointment;

        public DoctorAppointment Appointment
        {
            get { return _appointment; }
            set {
                _appointment = value;
                foreach (Doctor d in MainWindow.Doctors)
                {
                    if (d.Id.Equals(value.Doctor.Id))
                    {
                        doctors.SelectedItem = d;
                        break;
                    }
                }
                foreach (Room r in MainWindow.Rooms)
                {
                    if (r.RoomId.Equals(value.Room))
                    {
                        rooms.SelectedItem = r;
                        break;
                    }
                }
                calendar.SelectedDate = DateTime.Now.Date;
            }
        }


        public UCChangeApp(ContentControl details)
        {
            InitializeComponent();
            doctors.DataContext = MainWindow.Doctors;
            rooms.DataContext = MainWindow.Rooms;
            AvailableAppoitments = new ObservableCollection<DoctorAppointment>();
            panel = details;
        }

        private void doctor_changed(object sender, SelectionChangedEventArgs e)
        {
            SelectedDatesCollection dates = calendar.SelectedDates;
            ObservableCollection<DoctorAppointment> appList = new ObservableCollection<DoctorAppointment>();
            Doctor doc = (Doctor)doctors.SelectedItem;

            int hour = 0;
            foreach (DateTime d in dates)
            {
                for (int i = 0; i < 16; i++)
                {
                    if (i % 2 == 0)
                    {
                        appList.Add(new DoctorAppointment(new DateTime(d.Year, d.Month, d.Day, 8 + i / 2, 0, 0), AppointmetType.CheckUp, false, doc.PrimaryRoom, doc, Appointment.Patient));
                        hour = 8 + i / 2;
                    }
                    else
                    {
                        appList.Add(new DoctorAppointment(new DateTime(d.Year, d.Month, d.Day, hour, 30, 0), AppointmetType.CheckUp, false, doc.PrimaryRoom, doc, Appointment.Patient));
                    }

                }
            }
            
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

            app.DataContext = AvailableAppoitments;
        }

        private void room_changed(object sender, SelectionChangedEventArgs e)
        {
            SelectedDatesCollection dates = calendar.SelectedDates;
            ObservableCollection<DoctorAppointment> appList = new ObservableCollection<DoctorAppointment>();
            
            Doctor doc = (Doctor)doctors.SelectedItem;
            Room room = (Room)rooms.SelectedItem;
            if (doc == null || room == null){
                foreach(Doctor d in MainWindow.Doctors)
                {
                    if (d.Id.Equals(Appointment.Doctor.Id))
                    {
                        doctors.SelectedItem = d;
                        doc = (Doctor)doctors.SelectedItem;
                    }
                }
                foreach (Room r in MainWindow.Rooms)
                {
                    if (r.RoomId.Equals(Appointment.Room))
                    {
                        rooms.SelectedItem = r;
                        room = (Room)rooms.SelectedItem;
                    }
                }
            }

            int hour = 0;
                foreach (DateTime d in dates)
            {
                for (int i = 0; i < 16; i++)
                {
                    if (i % 2 == 0)
                    {
                        appList.Add(new DoctorAppointment(new DateTime(d.Year, d.Month, d.Day, 8 + i/2, 0, 0), AppointmetType.CheckUp, false, room.RoomId, doc, Appointment.Patient));
                        hour = 8 + i / 2;
                    }
                    else
                    {
                        appList.Add(new DoctorAppointment(new DateTime(d.Year, d.Month, d.Day, hour, 30, 0), AppointmetType.CheckUp, false, room.RoomId, doc, Appointment.Patient));
                    }

                }
            }


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

            app.DataContext = AvailableAppoitments;
        }

        private void calendar_changed(object sender, SelectionChangedEventArgs e)
        {
            SelectedDatesCollection dates = calendar.SelectedDates;
            ObservableCollection<DoctorAppointment> appList = new ObservableCollection<DoctorAppointment>();

            Doctor doc = (Doctor)doctors.SelectedItem;
            Room room = (Room)rooms.SelectedItem;
            if (doc == null || room == null)
            {
                foreach (Doctor d in MainWindow.Doctors)
                {
                    if (d.Id.Equals(Appointment.Doctor.Id))
                    {
                        doctors.SelectedItem = d;
                        doc = d;
                    }
                }
                foreach (Room r in MainWindow.Rooms)
                {
                    if (r.RoomId.Equals(Appointment.Room))
                    {
                        rooms.SelectedItem = r;
                        room = r;
                    }
                }
            }

            int hour = 0;
            foreach (DateTime d in dates)
            {
                for (int i = 0; i < 16; i++)
                {
                    if (i % 2 == 0)
                    {
                        appList.Add(new DoctorAppointment(new DateTime(d.Year, d.Month, d.Day, 8 + i/2, 0, 0), AppointmetType.CheckUp, false, room.RoomId, doc, Appointment.Patient));
                        hour = 8 + i / 2;
                    }
                    else
                    {
                        appList.Add(new DoctorAppointment(new DateTime(d.Year, d.Month, d.Day, hour, 30, 0), AppointmetType.CheckUp, false, room.RoomId, doc, Appointment.Patient));
                    }

                }
            }


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

            app.DataContext = AvailableAppoitments;
        }

        private void DataGridRow_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void DataGridRow_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                DoctorAppointment docApp = (DoctorAppointment)app.SelectedItem;

                Hospital.Instance.RemoveAppointment(Appointment);


                DoctorHomePage.Instance.DoctorAppointment.Remove(Appointment);
                Appointment.Reserved = false;
                AvailableAppoitments.Add(Appointment);

                Hospital.Instance.AddAppointment(docApp);
                DoctorHomePage.Instance.DoctorAppointment.Add(docApp);
                docApp.Reserved = true;
                AvailableAppoitments.Remove(docApp);

                MainWindow login = new MainWindow();
                AppointmentFileStorage afs = new AppointmentFileStorage();
                afs.SaveAppointment(Hospital.Instance.allAppointments);

                panel.Visibility = Visibility.Collapsed;

            }
        }
    }
}
