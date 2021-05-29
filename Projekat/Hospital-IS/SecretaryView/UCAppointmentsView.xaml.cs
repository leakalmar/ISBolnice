using DTOs;
using Hospital_IS.Controllers;
using Hospital_IS.DTOs;
using Hospital_IS.DTOs.SecretaryDTOs;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Hospital_IS.SecretaryView
{
    /// <summary>
    /// Interaction logic for UCAppointmentsView.xaml
    /// </summary>
    public partial class UCAppointmentsView : UserControl
    {
        public ObservableCollection<DoctorAppointmentDTO> Appointments { get; set; }
        public ObservableCollection<RoomDTO> Rooms { get; set; }
        public ObservableCollection<DoctorDTO> Doctors { get; set; }

        public UCAppointmentsView()
        {
            InitializeComponent();
            RefreshGrid();
            Rooms = new ObservableCollection<RoomDTO>(DoctorAppointmentManagementController.Instance.GetAllRooms());
            Doctors = new ObservableCollection<DoctorDTO>(SecretaryManagementController.Instance.GetAllDoctors());
            dpFrom.SelectedDate = DateTime.Now.Date;
            dpTo.SelectedDate = DateTime.Now.Date.AddDays(7);


            this.DataContext = this;

        }

        public void RefreshGrid()
        {
            if (Appointments != null)
                Appointments.Clear();

            DoctorAppointmentManagementController.Instance.ReloadAppointments();
            Appointments = new ObservableCollection<DoctorAppointmentDTO>(DoctorAppointmentManagementController.Instance.GetAll());

            /*foreach (DoctorAppointment appointment in Appointments)
            {
                if (string.IsNullOrEmpty(appointment.AppTypeText))
                {
                    if (appointment.Type == AppointmentType.CheckUp)
                        appointment.AppTypeText = "Pregled";
                    else if (appointment.Type == AppointmentType.Operation)
                        appointment.AppTypeText = "Operacija";
                }
            }*/

            dataGridAppointments.ItemsSource = Appointments;
        }

        private void ShowAppointment(object sender, RoutedEventArgs e)
        {
            if ((DoctorAppointmentDTO)dataGridAppointments.SelectedItem != null)
            {
                DoctorAppointmentDTO appointment = (DoctorAppointmentDTO)dataGridAppointments.SelectedItem;
                AppointmentView av = new AppointmentView(appointment);
                av.Show();
            }
        }

        private void UpdateAppointment(object sender, RoutedEventArgs e)
        {
            if ((DoctorAppointmentDTO)dataGridAppointments.SelectedItem != null)
            {
                DoctorAppointmentDTO appointment = (DoctorAppointmentDTO)dataGridAppointments.SelectedItem;
                UpdateAppointment ua = new UpdateAppointment(appointment, this);
                ua.Show();
            }
        }

        private void DeleteAppointment(object sender, RoutedEventArgs e)
        {
            if ((DoctorAppointmentDTO)dataGridAppointments.SelectedItem != null)
            {
                CancelAppointment ca = new CancelAppointment(this);
                ca.Show();
            }
        }

        private void ScheduleAppointment(object sender, RoutedEventArgs e)
        {
            ScheduleAppointment sa = new ScheduleAppointment(this);
            sa.Show();
        }

        private void filter_changed(object sender, SelectionChangedEventArgs e)
        {
            DoctorDTO doctor = (DoctorDTO)cbDoctor.SelectedItem;
            RoomDTO room = (RoomDTO)cbRoom.SelectedItem;
            DateTime startDate = (DateTime)dpFrom.SelectedDate;
            DateTime endDate;
            if (dpTo.SelectedDate == null)
            {
                endDate = DateTime.Now.Date.AddDays(7);
            }
            else
            {
                endDate = (DateTime)dpTo.SelectedDate;
            }

            ICollectionView view = new CollectionViewSource { Source = Appointments }.View;
            view.Filter = delegate (object item)
            {
                if (room == null && doctor == null)
                    return ((DoctorAppointmentDTO)item).AppointmentStart.Date <= endDate.Date & ((DoctorAppointmentDTO)item).AppointmentStart.Date >= startDate.Date;
                else if (room == null && doctor != null)
                    return ((DoctorAppointmentDTO)item).Doctor.Id == doctor.Id &
                    ((DoctorAppointmentDTO)item).AppointmentStart.Date <= endDate.Date & ((DoctorAppointmentDTO)item).AppointmentStart.Date >= startDate.Date;
                else if (doctor == null && room != null)
                    return ((DoctorAppointmentDTO)item).Room == room.RoomId &
                    ((DoctorAppointmentDTO)item).AppointmentStart.Date <= endDate.Date & ((DoctorAppointmentDTO)item).AppointmentStart.Date >= startDate.Date;
                else
                    return ((DoctorAppointmentDTO)item).Doctor.Id == doctor.Id & ((DoctorAppointmentDTO)item).Room == room.RoomId &
                    ((DoctorAppointmentDTO)item).AppointmentStart.Date <= endDate.Date & ((DoctorAppointmentDTO)item).AppointmentStart.Date >= startDate.Date;
            };

            dataGridAppointments.ItemsSource = view;


        }
    }
}
