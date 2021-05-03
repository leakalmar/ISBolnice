using Controllers;
using Model;
using Storages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Input;

namespace Hospital_IS.DoctorView
{
    public partial class UCNewApp : UserControl
    {
        public DoctorAppointment Appointment { get; }
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
            AppointmetType type = AppointmetType.CheckUp;
            if (types.SelectedItem.Equals(AppointmetType.Operation.ToString())){
                type = AppointmetType.Operation;
            }
            String[] parts = duration.Text.Split(".");
            if (doc == null)
            {
                foreach (Doctor d in MainWindow.Doctors)
                {
                    if (d.Id.Equals(DoctorHomePage.Instance.Doctor.Id))
                    {
                        doctors.SelectedItem = d;
                        doc = d;
                    }
                }
            }
            if (room == null)
            {
                room = DoctorHomePage.Instance.PrimaryRoom;
            }
            if (dates.Count == 0)
            {
                calendar.SelectedDate = DateTime.Now;
                dates = calendar.SelectedDates;
            }


            int hours = 0;
            int minutes = 0;
            if (type == AppointmetType.Operation)
            {
                if ((string)parts.GetValue(0) != "")
                {
                    hours = int.Parse((string)parts.GetValue(0));
                    if (parts.Length == 2)
                    {
                        minutes = 30;
                    }
                }
                else
                {
                    return;
                }
            }
            TimeSpan durationTimeSpan = new TimeSpan(hours, minutes, 0);
            appointments.DataContext = DoctorAppointmentController.Instance.SuggestAppointmetsToDoctor(dates,room.RoomId, type, durationTimeSpan, Appointment.Patient);
        }

        private void setAppointment_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                DoctorAppointment selected = (DoctorAppointment)appointments.SelectedItem;
                selected.Reserved = true;
                DoctorAppointmentController.Instance.AddAppointment(selected);
                DoctorHomePage.Instance.DoctorAppointment.Add(selected);

                DoctorHomePage.Instance.Home.Children.Remove(this);
                DoctorHomePage.Instance.Home.Children.Add(new UCPatientChart(Appointment, true));
            }
        }

        private void appointments_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DoctorAppointment selected = (DoctorAppointment)appointments.SelectedItem;
            if(selected.Reserved == true)
            {
                return;
            }
            selected.Reserved = true;
            DoctorAppointmentController.Instance.AddAppointment(selected);
            DoctorHomePage.Instance.DoctorAppointment.Add(selected);

            DoctorHomePage.Instance.Home.Children.Remove(this);
            DoctorHomePage.Instance.Home.Children.Add(new UCPatientChart(Appointment, true));
        }
    }
    
}
