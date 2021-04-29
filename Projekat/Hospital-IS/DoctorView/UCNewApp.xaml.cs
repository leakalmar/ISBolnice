using Controllers;
using Model;
using Storages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;

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
            specialization.ItemsSource = MainWindow.Specialties;
            InitializeComoboBoxes();

        }

        private void InitializeComoboBoxes()
        {
            types.SelectedIndex = 1;
            duration.Value = new TimeSpan(0);

            foreach (Specialty s in MainWindow.Specialties)
            {
                if (s.Name.Equals(DoctorHomePage.Instance.Doctor.Specialty.Name))
                {
                    specialization.SelectedItem = s;
                }
            }

            foreach (Doctor d in MainWindow.Doctors)
            {
                if (d.Id.Equals(DoctorHomePage.Instance.Doctor.Id))
                {
                    doctors.SelectedItem = d;
                }
            }


            foreach (Room r in MainWindow.Rooms)
            {
                if (r.RoomId.Equals(DoctorHomePage.Instance.PrimaryRoom.RoomId))
                {
                    rooms.SelectedItem = r;
                }
            }
            calendar.SelectedDate = DateTime.Now;
        }

        private void filter_changed(object sender, SelectionChangedEventArgs e)
        {
            filterAppointments();
        }

        private void filterAppointments()
        {
            if (types.SelectedItem == null || specialization.SelectedItem == null || rooms.SelectedItem == null || doctors.SelectedItem == null)
            {
                return;
            }
            else
            {
                Doctor doc = (Doctor)doctors.SelectedItem;
                Room room = (Room)rooms.SelectedItem;
                SelectedDatesCollection dates = calendar.SelectedDates;
                AppointmetType type = FindType();
                //TimeSpan durationTimeSpan = ParseInputOfDuration(type);

                changeVisibilityOfFields(type, doc);
                appointments.DataContext = DoctorAppointmentController.Instance.SuggestAppointmetsToDoctor(dates, room.RoomId, type, (TimeSpan)duration.Value, Appointment.Patient);
            }

        }

        private AppointmetType FindType()
        {
            AppointmetType type;
            if (types.SelectedItem.Equals(AppointmetType.CheckUp.ToString()))
            {
                type = AppointmetType.CheckUp;
            }
            else
            {
                type = AppointmetType.Operation;
            }

            return type;
        }

        private TimeSpan ParseInputOfDuration(AppointmetType type)
        {
            String[] parts = duration.Text.Split(".");
            int hours = 0, minutes = 0;
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
            }
            return new TimeSpan(hours, minutes, 0);
        }

        private void specialization_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Specialty selectedSpecialty = (Specialty)specialization.SelectedItem;
            doctors.DataContext = DoctorController.Instance.GetDoctorsBySpecilty(selectedSpecialty);

            if (selectedSpecialty.Name.Equals(DoctorHomePage.Instance.Doctor.Specialty.Name))
            {
                foreach (Doctor doctor in MainWindow.Doctors)
                {
                    if (doctor.Id.Equals(DoctorHomePage.Instance.Doctor.Id))
                    {
                        doctors.SelectedItem = doctor;
                    }
                }
            }
            else
            {
                doctors.SelectedIndex = 0;
                types.SelectedIndex = 1;
            }

            if (types.SelectedItem != null && doctors.SelectedItem != null)
            {
                changeVisibilityOfFields(FindType(), (Doctor)doctors.SelectedItem);
            }
        }
        private void duration_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            filterAppointments();
        }

        private void changeVisibilityOfFields(AppointmetType type, Doctor doctor)
        {
            if (!doctor.Specialty.Name.Equals(DoctorHomePage.Instance.Doctor.Specialty.Name))
            {
                rooms.Visibility = Visibility.Collapsed;
                types.Visibility = Visibility.Collapsed;
                duration.Visibility = Visibility.Collapsed;
                lblRoom.Visibility = Visibility.Collapsed;
                lblType.Visibility = Visibility.Collapsed;
                lblDuration.Visibility = Visibility.Collapsed;
            }
            if (doctor.Specialty.Name.Equals(DoctorHomePage.Instance.Doctor.Specialty.Name) && type == AppointmetType.CheckUp)
            {
                rooms.Visibility = Visibility.Visible;
                types.Visibility = Visibility.Visible;
                duration.Visibility = Visibility.Collapsed;
                lblRoom.Visibility = Visibility.Visible;
                lblType.Visibility = Visibility.Visible;
                lblDuration.Visibility = Visibility.Collapsed;
            }
            if (doctor.Specialty.Name.Equals(DoctorHomePage.Instance.Doctor.Specialty.Name) && type == AppointmetType.Operation)
            {
                rooms.Visibility = Visibility.Visible;
                types.Visibility = Visibility.Visible;
                duration.Visibility = Visibility.Visible;
                lblRoom.Visibility = Visibility.Visible;
                lblType.Visibility = Visibility.Visible;
                lblDuration.Visibility = Visibility.Visible;
            }
        }    

        private void setAppointment_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
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
            
            schedule.Visibility = Visibility.Collapsed;
            DoctorAppointment selected = (DoctorAppointment)appointments.SelectedItem;
            date.Content = selected.AppointmentStart.Date;
            time.Content = selected.AppointmentStart.TimeOfDay;
            documentSpecialty.Content = selected.Doctor.Specialty.Name;
            
            documentDoctor.Content = selected.Doctor.Name.ToString() + " " + ShortSurname(selected.Doctor);
            thisDoctor.Content = DoctorHomePage.Instance.Doctor.Name.ToString() +" "+ ShortSurname(selected.Doctor);
            today.Content = DateTime.Now.Date;
            
            document.Visibility = Visibility.Visible;


        }

        private string ShortSurname(Doctor doctor)
        {
            String newSurname = doctor.Surname;
            if (doctor.Surname.Length > 11)
            {
                String[] surnames = doctor.Surname.Split(" ");
                newSurname = surnames[0].ToCharArray()[0].ToString() + ". " + surnames[1];
            }
            return newSurname;
        }

        private void save_Click(object sender, RoutedEventArgs e)
        {
            DoctorAppointment selected = (DoctorAppointment)appointments.SelectedItem;
            if (selected.Reserved == true)
            {
                return;
            }
            selected.Reserved = true;
            selected.AppointmentCause = cause.Text;
            DoctorAppointmentController.Instance.AddAppointment(selected);
            DoctorHomePage.Instance.DoctorAppointment.Add(selected);

            DoctorHomePage.Instance.Home.Children.Remove(this);
            DoctorHomePage.Instance.Home.Children.Add(new UCPatientChart(Appointment, true));
        }

        private void cancle_Click(object sender, RoutedEventArgs e)
        {
            schedule.Visibility = Visibility.Visible;
            document.Visibility = Visibility.Collapsed;
        }
    }

}
