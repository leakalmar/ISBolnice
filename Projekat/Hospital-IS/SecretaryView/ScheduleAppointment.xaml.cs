﻿using Hospital_IS.Storages;
using Model;
using Storages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows;
using System.Windows.Media;

namespace Hospital_IS.SecretaryView
{
    /// <summary>
    /// Interaction logic for ScheduleAppointment.xaml
    /// </summary>
    public partial class ScheduleAppointment : Window
    {
        public DoctorAppointment DocAppointment { get; set; } = new DoctorAppointment();
        AppointmentFileStorage afs = new AppointmentFileStorage();
        private UCAppointmentsView uca;
        public ObservableCollection<Patient> Patients { get; set; } = new ObservableCollection<Patient>();
        public ObservableCollection<Doctor> Doctors { get; set; } = new ObservableCollection<Doctor>();
        public ObservableCollection<Room> Rooms { get; set; } = new ObservableCollection<Room>();

        public ScheduleAppointment(UCAppointmentsView uca)
        {
            InitializeComponent();
            this.uca = uca;

            PatientFileStorage pfs = new PatientFileStorage();
            List<Patient> patients = pfs.GetAll();
            Patients = new ObservableCollection<Patient>(patients);

            FSDoctor fsd = new FSDoctor();
            Doctors = fsd.GetAll();

            RoomStorage rs = new RoomStorage();
            Rooms = rs.GetAll();


            this.DataContext = this;
        }

        private void NewAppointment(object sender, RoutedEventArgs e)
        {
            //  pacijent
            DocAppointment.Patient = Patients[cbPatient.SelectedIndex];

            //  doktor
            DocAppointment.Doctor = Doctors[cbDoctor.SelectedIndex];

            // soba
            if (cbRoom.IsEnabled)
                DocAppointment.Room = Rooms[cbRoom.SelectedIndex].RoomNumber;
            else
                DocAppointment.Room = findRoomNumber(DocAppointment.Doctor.PrimaryRoom);

            // tip pregleda
            if (cbAppType.SelectedIndex == 0)
            {
                DocAppointment.Type = AppointmetType.CheckUp;
                DocAppointment.AppTypeText = "Pregled";
            }
            else if (cbAppType.SelectedIndex == 1)
            {
                DocAppointment.Type = AppointmetType.Operation;
                DocAppointment.AppTypeText = "Operacija";
            }

            // datum, vreme i trajanje pregleda
            try
            {
                DateTime appDate = DateTime.ParseExact(txtAppDate.Text, "dd.MM.yyyy.", CultureInfo.InvariantCulture);
                DocAppointment.DateAndTime = appDate;

                DateTime appStart = DateTime.ParseExact(txtStartOfApp.Text, "HH:mm", CultureInfo.InvariantCulture);
                DocAppointment.AppointmentStart = appDate.Date.Add(appStart.TimeOfDay);

                if (cbAppType.SelectedIndex == 0)
                {
                    DocAppointment.AppointmentEnd = DocAppointment.AppointmentStart.AddMinutes(30);
                }
                else if (cbAppType.SelectedIndex == 1)
                {
                    DateTime appEnd = DateTime.ParseExact(txtEndOfApp.Text, "HH:mm", CultureInfo.InvariantCulture);
                    DocAppointment.AppointmentEnd = appDate.Date.Add(appEnd.TimeOfDay);
                }

                DocAppointment.DateAndTime = appDate.Date.Add(appStart.TimeOfDay);
            }
            catch (Exception ex)
            {
            }

            uca.Appointments.Add(DocAppointment);

            afs.SaveAppointment(uca.Appointments);

            this.Close();
        }





        private void Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void cbAppType_LostFocus(object sender, RoutedEventArgs e)
        {
            if (cbAppType.SelectedIndex == 0)
            {
                txtEndOfApp.IsEnabled = false;
                cbRoom.IsEnabled = false;
            }
            else 
            {
                txtEndOfApp.IsEnabled = true;
                cbRoom.IsEnabled = true;
            }
        }

        private void txtEndOfApp_LostFocus(object sender, RoutedEventArgs e)
        {
            ObservableCollection<DoctorAppointment> appsByRoom = new ObservableCollection<DoctorAppointment>();
            if (cbAppType.SelectedIndex == 0)
                appsByRoom = afs.GetAllByRoom(Doctors[cbDoctor.SelectedIndex].PrimaryRoom);
            else if (cbAppType.SelectedIndex == 1)
                appsByRoom = afs.GetAllByRoom(Rooms[cbRoom.SelectedIndex].RoomId);

            ObservableCollection<DoctorAppointment> appsByDoctor = afs.GetAllByDoctor(Doctors[cbDoctor.SelectedIndex].Id);

            confirmAppointmentDate(checkAppointment(appsByRoom, appsByDoctor));
        }

        private void txtStartOfApp_LostFocus(object sender, RoutedEventArgs e)
        {
            if (cbAppType.SelectedIndex == 0 && !string.IsNullOrEmpty(txtStartOfApp.Text))
            {
                DateTime appStart = DateTime.ParseExact(txtStartOfApp.Text, "HH:mm", System.Globalization.CultureInfo.InvariantCulture);
                DateTime appEnd = appStart.AddMinutes(30);
                txtEndOfApp.Text = appEnd.ToString("t", DateTimeFormatInfo.InvariantInfo);



                ObservableCollection<DoctorAppointment> appsByDoctor = afs.GetAllByDoctor(Doctors[cbDoctor.SelectedIndex].Id);
                ObservableCollection<DoctorAppointment> appsByRoom = new ObservableCollection<DoctorAppointment>();
                if (cbAppType.SelectedIndex == 0)
                    appsByRoom = afs.GetAllByRoom(Doctors[cbDoctor.SelectedIndex].PrimaryRoom);
                else if (cbAppType.SelectedIndex == 1)
                    appsByRoom = afs.GetAllByRoom(Rooms[cbRoom.SelectedIndex].RoomId);

                confirmAppointmentDate(checkAppointment(appsByRoom, appsByDoctor));
            }
        }

        private void confirmAppointmentDate(bool isValid)
        {
            if (isValid)
            {
                btnConfirm.IsEnabled = true;
                txtStartOfApp.Background = new SolidColorBrush(Colors.White);
                txtEndOfApp.Background = new SolidColorBrush(Colors.White);
            }
            else
            {
                txtStartOfApp.Background = new SolidColorBrush(Colors.Red);
                txtEndOfApp.Background = new SolidColorBrush(Colors.Red);
                btnConfirm.IsEnabled = false;
            }
        }

        private bool checkAppointment(ObservableCollection<DoctorAppointment> RoomAppointments, ObservableCollection<DoctorAppointment> DoctorAppointments)
        {
            DateTime start;
            DateTime end;
            DateTime date = DateTime.ParseExact(txtAppDate.Text, "dd.MM.yyyy.", CultureInfo.InvariantCulture);
            start = DateTime.ParseExact(txtStartOfApp.Text, "HH:mm", CultureInfo.InvariantCulture);
            start = date.Date.Add(start.TimeOfDay);

            end = DateTime.ParseExact(txtEndOfApp.Text, "HH:mm", CultureInfo.InvariantCulture);
            end = date.Date.Add(end.TimeOfDay);

            foreach (Appointment appointment in RoomAppointments)
            {

                bool between = IsBetweenDates(start, end, appointment);
                if (between || start < appointment.AppointmentStart && end > appointment.AppointmentEnd)
                {
                    return false;
                }

            }

            if (DoctorAppointments.Count == 0)
                return true;

            

            foreach (Appointment appointment in DoctorAppointments)
            {

                bool between = IsBetweenDates(start, end, appointment);
                if (between || (start <= appointment.AppointmentStart && end >= appointment.AppointmentEnd))
                {
                    return false;
                }

            }

            return true;

        }

        private static bool IsBetweenDates(DateTime start, DateTime end, Appointment appointment)
        {
            return (start >= appointment.AppointmentStart && start < appointment.AppointmentEnd) || (end > appointment.AppointmentStart && end <= appointment.AppointmentEnd);
        }

        private int findRoomNumber(int roomId)
        {
            foreach (Room room in Rooms)
            {
                if (room.RoomId == roomId)
                    return room.RoomNumber;
            }
            return 0;
        }
    }
}
