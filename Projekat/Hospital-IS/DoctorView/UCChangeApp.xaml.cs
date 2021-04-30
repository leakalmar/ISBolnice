using Controllers;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Hospital_IS.DoctorView
{
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
            if (rooms.SelectedItem == null || doctors.SelectedItem == null || calendar.SelectedDate == null)
            {
                return;
            }
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
            if (dates == null)
            {
                calendar.SelectedDate = DateTime.Now.Date;
                dates = calendar.SelectedDates;
            }

            TimeSpan duration = Appointment.AppointmentEnd - Appointment.AppointmentStart;
            List<DoctorAppointment> list = DoctorAppointmentController.Instance.SuggestAppointmetsToDoctor(dates, room.RoomId, Appointment.Type, duration, Appointment.Patient, doc);
            ObservableCollection<DoctorAppointment> possibleAppointments = new ObservableCollection<DoctorAppointment>(list);

            /*ICollectionView view = new CollectionViewSource { Source = possibleAppointments }.View;
            view.Filter = null;
            view.Filter = delegate (object item)
            {
                bool found = false;
                foreach (DoctorAppointment dapp in DoctorAppointmentController.Instance.GetAllByDoctor(doc.Id))
                {
                    found = ((DoctorAppointment)item).Room.Equals(room.RoomId) & ((DoctorAppointment)item).AppointmentStart.Equals(dapp.AppointmentStart);
                }

                if (!found) 
                {
                    foreach (Appointment app in Hospital.Instance.GetAllAppByRoom(room))
                    {
                        found = app.AppointmentStart > ((DoctorAppointment)item).AppointmentStart & app.AppointmentEnd < ((DoctorAppointment)item).AppointmentStart;
                    }
                }
                

                //ako ga je pronasao znaci da ne treba da prikazuje
                return !found;
            };*/
            app.DataContext = possibleAppointments;

        }

        private void ChangeApp_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                DoctorAppointment newDoctorAppointment = (DoctorAppointment)app.SelectedItem;
                newDoctorAppointment.Reserved = true;
                DoctorAppointmentController.Instance.UpdateAppointment(Appointment, newDoctorAppointment);

                DoctorHomePage.Instance.DoctorAppointment.Remove(Appointment);
                DoctorHomePage.Instance.DoctorAppointment.Add(newDoctorAppointment);

                panel.Visibility = Visibility.Collapsed;
            }
        }
    }
}
