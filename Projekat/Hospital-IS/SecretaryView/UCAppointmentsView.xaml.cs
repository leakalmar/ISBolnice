using Controllers;
using DTOs;
using Hospital_IS.Adapter;
using Hospital_IS.Controllers;
using Hospital_IS.DTOs;
using Hospital_IS.DTOs.SecretaryDTOs;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

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

        public IDoctorAppointmentTarget doctorAppointmentTarget;

        public UCAppointmentsView(IDoctorAppointmentTarget target)
        {
            InitializeComponent();
            doctorAppointmentTarget = target;
            Appointments = new ObservableCollection<DoctorAppointmentDTO>(doctorAppointmentTarget.GetAll());
            dataGridAppointments.ItemsSource = Appointments;
            Rooms = new ObservableCollection<RoomDTO>(DoctorAppointmentManagementController.Instance.GetAllRooms());
            Doctors = new ObservableCollection<DoctorDTO>(SecretaryManagementController.Instance.GetAllDoctors());
            dpFrom.SelectedDate = DateTime.Now.Date;
            dpTo.SelectedDate = DateTime.Now.Date.AddDays(7);
            RefreshGrid();

            this.DataContext = this;

        }

        public void RefreshGrid()
        {
            if (Appointments != null)
                Appointments.Clear();

            DoctorAppointmentController.Instance.ReloadDoctorAppointments();
            Appointments = new ObservableCollection<DoctorAppointmentDTO>(doctorAppointmentTarget.GetAll());

            ICollectionView view = new CollectionViewSource { Source = Appointments }.View;
            view.Filter = delegate (object item)
            {
                return CheckAppointment((DoctorAppointmentDTO)item);
            };

            dataGridAppointments.ItemsSource = view;
        }

        private void ShowAppointment(object sender, RoutedEventArgs e)
        {
            if ((DoctorAppointmentDTO)dataGridAppointments.SelectedItem != null)
            {
                DoctorAppointmentDTO appointment = (DoctorAppointmentDTO)dataGridAppointments.SelectedItem;
                AppointmentView av = new AppointmentView(appointment);
                av.Show();
            }
            else
                MessageBox.Show("Izaberite termin!");
        }

        private void UpdateAppointment(object sender, RoutedEventArgs e)
        {
            if ((DoctorAppointmentDTO)dataGridAppointments.SelectedItem != null)
            {
                DoctorAppointmentDTO appointment = (DoctorAppointmentDTO)dataGridAppointments.SelectedItem;
                UpdateAppointment ua = new UpdateAppointment(appointment, this);
                ua.ShowDialog();
            }
            else
                MessageBox.Show("Izaberite termin!");
        }

        private void ScheduleAppointment(object sender, RoutedEventArgs e)
        {
            ScheduleAppointment sa = new ScheduleAppointment(this);
            sa.ShowDialog();
        }

        private void filter_changed(object sender, SelectionChangedEventArgs e)
        {

            ICollectionView view = new CollectionViewSource { Source = Appointments }.View;
            view.Filter = delegate (object item)
            {
                return CheckAppointment((DoctorAppointmentDTO)item);
            };

            dataGridAppointments.ItemsSource = view;
        }

        private bool CheckAppointment(DoctorAppointmentDTO docAppointment)
        {
            DoctorDTO doctor = (DoctorDTO)cbDoctor.SelectedItem;
            RoomDTO room = (RoomDTO)cbRoom.SelectedItem;
            if (dpFrom.SelectedDate == null)
                dpFrom.SelectedDate = DateTime.Now;
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

            if (room == null && doctor == null)
                return docAppointment.AppointmentStart.Date <= endDate.Date & docAppointment.AppointmentStart.Date >= startDate.Date;
            else if (room == null && doctor != null)
                return docAppointment.Doctor.Id == doctor.Id &
                docAppointment.AppointmentStart.Date <= endDate.Date & docAppointment.AppointmentStart.Date >= startDate.Date;
            else if (doctor == null && room != null)
                return docAppointment.Room == room.RoomId &
                docAppointment.AppointmentStart.Date <= endDate.Date & docAppointment.AppointmentStart.Date >= startDate.Date;
            else
                return docAppointment.Doctor.Id == doctor.Id & docAppointment.Room == room.RoomId &
                docAppointment.AppointmentStart.Date <= endDate.Date & docAppointment.AppointmentStart.Date >= startDate.Date;
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            ICollectionView view = new CollectionViewSource { Source = Appointments }.View;
            view.Filter = delegate (object item)
            {
                DoctorAppointmentDTO docAppointmemt = item as DoctorAppointmentDTO;
                return CheckIfAppointmentMeetsSearchCriteria(docAppointmemt) & CheckAppointment(docAppointmemt);
            };
            dataGridAppointments.ItemsSource = view;
        }

        private bool CheckIfAppointmentMeetsSearchCriteria(DoctorAppointmentDTO docAppointmemt)
        {
            string[] search = txtSearch.Text.ToLower().Split(" ");
            if (txtSearch.Text.Equals("Pretraži...")||txtSearch.Text.Equals("Search..."))
                search[0] = string.Empty;

            if (search.Length <= 1 && docAppointmemt != null)
                return docAppointmemt.Patient.Name.ToLower().Contains(search[0]) | docAppointmemt.Patient.Surname.ToLower().Contains(search[0]) |
                    docAppointmemt.AppTypeText.ToLower().Contains(search[0]);
            else
            {
                bool firstName = true;
                bool lastName = true;
                bool appType = true;
                int cnt = 0;

                if (docAppointmemt != null)
                    for (int i = 0; i < search.Length; i++)
                    {
                        if (docAppointmemt.Patient.Name.ToLower().Contains(search[i]) && firstName)
                        {
                            firstName = false;
                            cnt++;
                            continue;
                        }
                        if (docAppointmemt.Patient.Surname.ToLower().Contains(search[i]) && lastName)
                        {
                            lastName = false;
                            cnt++;
                            continue;
                        }
                        if (docAppointmemt.AppTypeText.ToLower().Contains(search[i]) && appType)
                        {
                            appType = false;
                            cnt++;
                            continue;
                        }
                    }

                return cnt == search.Length;
            }

        }

        private void SearchBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtSearch.Text.Equals("Pretraži...")||txtSearch.Text.Equals("Search..."))
            {
                txtSearch.Text = string.Empty;
                txtSearch.Foreground = new SolidColorBrush(Colors.Black);
            }
        }

        private void SearchBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtSearch.Text))
            {
                txtSearch.Foreground = new SolidColorBrush(Colors.Gray);
                if (SecretaryMainWindow.Instance.miSerbian.IsChecked)
                    txtSearch.Text = "Pretraži...";
                else
                    txtSearch.Text = "Search...";
            }
        }

        private void ResetFillters(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button.Name.ToString().Equals("btnResetDoctors"))
                cbDoctor.SelectedItem = null;
            else
                cbRoom.SelectedItem = null;
        }

        private void FormReport(object sender, RoutedEventArgs e)
        {
            SecretaryReport sr = new SecretaryReport();
            sr.ShowDialog();
        }
    }
}
