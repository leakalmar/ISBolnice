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
                foreach (Doctor d in DoctorController.Instance.GetAll())
                {
                    if (d.Id.Equals(value.Doctor.Id))
                    {
                        doctors.SelectedItem = d;
                        break;
                    }
                }
                foreach (Room r in RoomController.Instance.GetAllRooms())
                {
                    if (r.RoomId.Equals(value.Room))
                    {
                        rooms.SelectedItem = r;
                        break;
                    }
                }
                calendar.SelectedDate = DateTime.Now.Date;
                FindAppointments((Doctor)doctors.SelectedItem, (Room)rooms.SelectedItem, new List<DateTime>(calendar.SelectedDates));
            }
        }


        public UCChangeApp(ContentControl details)
        {
            InitializeComponent();
            doctors.DataContext = DoctorController.Instance.GetAll();
            rooms.DataContext = RoomController.Instance.GetAllRooms();

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
            List<DateTime> dates = new List<DateTime>(calendar.SelectedDates);
            if (doc == null)
            {
                foreach (Doctor d in DoctorController.Instance.GetAll())
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
                foreach (Room r in RoomController.Instance.GetAllRooms())
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
                dates = new List<DateTime>(calendar.SelectedDates);
            }

            FindAppointments(doc, room, dates);

        }

        private void FindAppointments(Doctor doc, Room room, List<DateTime> dates)
        {
            TimeSpan duration = Appointment.AppointmentEnd - Appointment.AppointmentStart;
            List<DoctorAppointment> list = DoctorAppointmentController.Instance.SuggestAppointmetsToDoctor(dates, Appointment.IsUrgent, room, Appointment.Type, duration, Appointment.Patient, doc);
            ObservableCollection<DoctorAppointment> possibleAppointments = new ObservableCollection<DoctorAppointment>(list);
            app.DataContext = possibleAppointments;
        }

        private void ChangeApp_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                DoctorAppointment newDoctorAppointment = (DoctorAppointment)app.SelectedItem;
                newDoctorAppointment.Reserved = true;
                DoctorAppointmentController.Instance.UpdateAppointment(Appointment, newDoctorAppointment);

                DoctorMainWindow.Instance._ViewModel.DoctorAppointments.Remove(Appointment);
                DoctorMainWindow.Instance._ViewModel.DoctorAppointments.Add(newDoctorAppointment);

                panel.Visibility = Visibility.Collapsed;
            }
        }
    }
}
