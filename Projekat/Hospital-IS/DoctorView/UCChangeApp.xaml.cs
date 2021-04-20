using Model;
using Storages;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace Hospital_IS.DoctorView
{
    /// <summary>
    /// Interaction logic for UCChangeApp.xaml
    /// </summary>
    public partial class UCChangeApp : UserControl
    {
        ContentControl panel;

        private DoctorAppointment _appointment;

        public DoctorAppointment Appointment
        {
            get { return _appointment; }
            set
            {
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
            
            panel = details;
        }

        private void filter_changed(object sender, SelectionChangedEventArgs e)
        {
            Doctor doc = (Doctor)doctors.SelectedItem;
            Room room = (Room)rooms.SelectedItem;
            SelectedDatesCollection dates = calendar.SelectedDates;
            if (doc == null)
            {
                foreach (Doctor d in MainWindow.Doctors)
                {
                    if (d.Id.Equals(Appointment.Doctor.Id))
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
                    if (r.RoomId.Equals(Appointment.Room))
                    {
                        rooms.SelectedItem = r;
                        room = r;
                    }
                }
            }
            if(dates == null)
            {
                calendar.SelectedDate = DateTime.Now.Date;
                dates = calendar.SelectedDates;
            }
            ObservableCollection<DoctorAppointment> appList = new ObservableCollection<DoctorAppointment>();
            

            int hour = 0;
            foreach (DateTime d in dates)
            {
                for (int i = 0; i < 16; i++)
                {
                    if (i % 2 == 0)
                    {
                        appList.Add(new DoctorAppointment(new DateTime(d.Year, d.Month, d.Day, 8 + i / 2, 0, 0), AppointmetType.CheckUp, false, room.RoomId, doc, Appointment.Patient));
                        hour = 8 + i / 2;
                    }
                    else
                    {
                        appList.Add(new DoctorAppointment(new DateTime(d.Year, d.Month, d.Day, hour, 30, 0), AppointmetType.CheckUp, false, room.RoomId, doc, Appointment.Patient));
                    }

                }
            }
            ICollectionView view = new CollectionViewSource { Source = appList }.View;
            view.Filter = null;
            view.Filter = delegate (object item)
            {
                bool found = false;
                foreach (DoctorAppointment dapp in Hospital.Instance.GetAllAppointmentsByDoctor(doc))
                {
                    found = ((DoctorAppointment)item).Room.Equals(room.RoomId) & ((DoctorAppointment)item).DateAndTime.Equals(dapp.DateAndTime);
                }

                if (!found) 
                {
                    foreach (Appointment app in Hospital.Instance.GetAllAppByRoom(room))
                    {
                        found = app.AppointmentStart > ((DoctorAppointment)item).DateAndTime & app.AppointmentEnd < ((DoctorAppointment)item).DateAndTime;
                    }
                }
                

                //ako ga je pronasao znaci da ne treba da prikazuje
                return !found;
            };
            app.DataContext = view;

        }

        //private void room_changed(object sender, SelectionChangedEventArgs e)
        //{
        //    SelectedDatesCollection dates = calendar.SelectedDates;
        //    ObservableCollection<DoctorAppointment> appList = new ObservableCollection<DoctorAppointment>();

        //    Doctor doc = (Doctor)doctors.SelectedItem;
        //    Room room = (Room)rooms.SelectedItem;
        //    if (doc == null || room == null){
        //        foreach(Doctor d in MainWindow.Doctors)
        //        {
        //            if (d.Id.Equals(Appointment.Doctor.Id))
        //            {
        //                doctors.SelectedItem = d;
        //                doc = (Doctor)doctors.SelectedItem;
        //            }
        //        }
        //        foreach (Room r in MainWindow.Rooms)
        //        {
        //            if (r.RoomId.Equals(Appointment.Room))
        //            {
        //                rooms.SelectedItem = r;
        //                room = (Room)rooms.SelectedItem;
        //            }
        //        }
        //    }

        //    int hour = 0;
        //        foreach (DateTime d in dates)
        //    {
        //        for (int i = 0; i < 16; i++)
        //        {
        //            if (i % 2 == 0)
        //            {
        //                appList.Add(new DoctorAppointment(new DateTime(d.Year, d.Month, d.Day, 8 + i/2, 0, 0), AppointmetType.CheckUp, false, room.RoomId, doc, Appointment.Patient));
        //                hour = 8 + i / 2;
        //            }
        //            else
        //            {
        //                appList.Add(new DoctorAppointment(new DateTime(d.Year, d.Month, d.Day, hour, 30, 0), AppointmetType.CheckUp, false, room.RoomId, doc, Appointment.Patient));
        //            }

        //        }
        //    }


        //    AvailableAppoitments.Clear();
        //    bool flag = false;
        //    foreach (DoctorAppointment ap in appList)
        //    {
        //        foreach (DoctorAppointment hospital in Hospital.Instance.GetAllAppByRoom(room))
        //        {
        //            if (ap.DateAndTime == hospital.DateAndTime)
        //                flag = true;
        //        }
        //        if (flag == false)
        //        {
        //            AvailableAppoitments.Add(ap);
        //        }
        //        else
        //        {
        //            flag = false;
        //        }
        //    }

        //    app.DataContext = AvailableAppoitments;
        //}

        //private void calendar_changed(object sender, SelectionChangedEventArgs e)
        //{
        //    SelectedDatesCollection dates = calendar.SelectedDates;
        //    ObservableCollection<DoctorAppointment> appList = new ObservableCollection<DoctorAppointment>();

        //    Doctor doc = (Doctor)doctors.SelectedItem;
        //    Room room = (Room)rooms.SelectedItem;
        //    if (doc == null || room == null)
        //    {
        //        foreach (Doctor d in MainWindow.Doctors)
        //        {
        //            if (d.Id.Equals(Appointment.Doctor.Id))
        //            {
        //                doctors.SelectedItem = d;
        //                doc = d;
        //            }
        //        }
        //        foreach (Room r in MainWindow.Rooms)
        //        {
        //            if (r.RoomId.Equals(Appointment.Room))
        //            {
        //                rooms.SelectedItem = r;
        //                room = r;
        //            }
        //        }
        //    }

        //    int hour = 0;
        //    foreach (DateTime d in dates)
        //    {
        //        for (int i = 0; i < 16; i++)
        //        {
        //            if (i % 2 == 0)
        //            {
        //                appList.Add(new DoctorAppointment(new DateTime(d.Year, d.Month, d.Day, 8 + i/2, 0, 0), AppointmetType.CheckUp, false, room.RoomId, doc, Appointment.Patient));
        //                hour = 8 + i / 2;
        //            }
        //            else
        //            {
        //                appList.Add(new DoctorAppointment(new DateTime(d.Year, d.Month, d.Day, hour, 30, 0), AppointmetType.CheckUp, false, room.RoomId, doc, Appointment.Patient));
        //            }

        //        }
        //    }


        //    AvailableAppoitments.Clear();
        //    bool flag = false;
        //    foreach (DoctorAppointment ap in appList)
        //    {
        //        foreach (DoctorAppointment hospital in Hospital.Instance.allAppointments)
        //        {
        //            if (ap.DateAndTime == hospital.DateAndTime)
        //                flag = true;
        //        }
        //        if (flag == false)
        //        {
        //            AvailableAppoitments.Add(ap);
        //        }
        //        else
        //        {
        //            flag = false;
        //        }
        //    }

        //    app.DataContext = AvailableAppoitments;
        //}

        private void ChangeApp_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                DoctorAppointment docApp = (DoctorAppointment)app.SelectedItem;

                Hospital.Instance.RemoveAppointment(Appointment);


                DoctorHomePage.Instance.DoctorAppointment.Remove(Appointment);
                Appointment.Reserved = false;

                Hospital.Instance.AddAppointment(docApp);
                DoctorHomePage.Instance.DoctorAppointment.Add(docApp);
                docApp.Reserved = true;

                MainWindow login = new MainWindow();
                AppointmentFileStorage afs = new AppointmentFileStorage();
                afs.SaveAppointment(Hospital.Instance.allAppointments);

                panel.Visibility = Visibility.Collapsed;

            }
        }
    }
}
