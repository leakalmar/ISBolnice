using DTOs;
using Enums;
using Hospital_IS.Controllers;
using Hospital_IS.Enums;
using Model;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Hospital_IS.SecretaryView
{
    /// <summary>
    /// Interaction logic for UpdateAppointment.xaml
    /// </summary>
    public partial class UpdateAppointment : Window
    {
        public DoctorAppointmentDTO OldDocAppointment { get; set; }

        public DoctorAppointmentDTO NewDocAppointment { get; set; }

        public UCAppointmentsView uca;

        public ObservableCollection<int> Hours { get; set; } = new ObservableCollection<int>();
        public ObservableCollection<DoctorAppointmentDTO> ScheduledAppointments { get; set; } = new ObservableCollection<DoctorAppointmentDTO>();

        public UpdateAppointment(DoctorAppointmentDTO appointment, UCAppointmentsView uca)
        {
            InitializeComponent();
            OldDocAppointment = appointment;
            this.uca = uca;
            ScheduledAppointments = new ObservableCollection<DoctorAppointmentDTO>(uca.doctorAppointmentTarget.GetFutureAppointmentsForDoctor(OldDocAppointment.Doctor.Id));

            this.DataContext = this;

            GenerateAppointmentTimes();

            if (OldDocAppointment.Type == AppointmentType.CheckUp)
            {
                txtAppType.Text = "Pregled";
                txtAppDuration.IsEnabled = false;
            }
            else if (OldDocAppointment.Type == AppointmentType.Operation)
            {
                txtAppType.Text = "Operacija";
            }

            SetDoctorAppointmentInfo();

            NewDocAppointment = new DoctorAppointmentDTO(OldDocAppointment);
        }

        private void SetDoctorAppointmentInfo()
        {
            dpAppDate.SelectedDate = OldDocAppointment.AppointmentStart;

            cbHours.SelectedValue = Int32.Parse(OldDocAppointment.AppointmentStart.ToString("HH"));
            if (OldDocAppointment.AppointmentStart.ToString("mm").Equals("00"))
                cbMinutes.SelectedIndex = 0;
            else
                cbMinutes.SelectedIndex = 1;

            TimeSpan appDuration = OldDocAppointment.AppointmentEnd - OldDocAppointment.AppointmentStart;

            txtAppDuration.Text = appDuration.TotalMinutes.ToString();

            txtAppCause.Text = OldDocAppointment.AppointmentCause;

            ICollectionView view = new CollectionViewSource { Source = ScheduledAppointments }.View;
            view.Filter = delegate (object item)
            {
                return CheckAppointmentDate((DoctorAppointmentDTO)item);
            };

            dataGridAppointments.ItemsSource = view;

        }

        private void ChangeAppointment(object sender, RoutedEventArgs e)
        {
            NewDocAppointment.AppointmentStart = (DateTime)dpAppDate.SelectedDate;
            NewDocAppointment.AppointmentStart = NewDocAppointment.AppointmentStart.AddHours(Hours[cbHours.SelectedIndex]);
            if (cbMinutes.SelectedIndex == 1)
                NewDocAppointment.AppointmentStart = NewDocAppointment.AppointmentStart.AddMinutes(30);

            int appDuration = Int32.Parse(txtAppDuration.Text);
            NewDocAppointment.AppointmentEnd = NewDocAppointment.AppointmentStart.AddMinutes(appDuration);

            NewDocAppointment.AppointmentCause = txtAppCause.Text;

            NotificationController.Instance.SendNAppointmentRescheduledNotification(OldDocAppointment, NewDocAppointment);

            uca.doctorAppointmentTarget.UpdateDoctorAppointment(OldDocAppointment, NewDocAppointment);
            uca.RefreshGrid();
            this.Close();
        }

        private void Close(object sender, RoutedEventArgs e)
        {
            uca.RefreshGrid();
            this.Close();
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            uca.RefreshGrid();
        }

        private void VerifyAppointment(object sender, RoutedEventArgs e)
        {
            // datum, vreme i trajanje pregleda
            try
            {
                NewDocAppointment.AppointmentStart = (DateTime)dpAppDate.SelectedDate;
                NewDocAppointment.AppointmentStart = NewDocAppointment.AppointmentStart.AddHours(Hours[cbHours.SelectedIndex]);
                if (cbMinutes.SelectedIndex == 1)
                    NewDocAppointment.AppointmentStart = NewDocAppointment.AppointmentStart.AddMinutes(30);

                int appDuration = Int32.Parse(txtAppDuration.Text);
                NewDocAppointment.AppointmentEnd = NewDocAppointment.AppointmentStart.AddMinutes(appDuration);

            }
            catch (Exception ex)
            {
            }

            EnableAppointmentConfirmation(uca.doctorAppointmentTarget.VerifyAppointment(NewDocAppointment));
        }

        private void EnableAppointmentConfirmation(bool isValid)
        {
            if (isValid)
            {
                btnConfirm.IsEnabled = true;
               // txtStartOfApp.Background = new SolidColorBrush(Colors.White);
               // txtEndOfApp.Background = new SolidColorBrush(Colors.White);
            }
            else
            {
               // txtStartOfApp.Background = new SolidColorBrush(Colors.Red);
               // txtEndOfApp.Background = new SolidColorBrush(Colors.Red);
                btnConfirm.IsEnabled = false;
            }
        }

        private void CancelAppointment(object sender, RoutedEventArgs e)
        {
            DeleteAppointmentView dav = new DeleteAppointmentView(OldDocAppointment, this);
            dav.ShowDialog();
        }


        private void GenerateAppointmentTimes()
        {
            Hours.Clear();
            if (OldDocAppointment.Doctor.WorkShift.Equals(WorkDayShift.FirstShift))
            {
                for (int i = 8; i <= 13; i++)
                    Hours.Add(i);
            }
            else {
                for (int i = 14; i <= 20; i++)
                    Hours.Add(i);
            }

            cbHours.ItemsSource = Hours;
        }

        private void ShowScheduledAppointments(object sender, SelectionChangedEventArgs e)
        {
            ICollectionView view = new CollectionViewSource { Source = ScheduledAppointments }.View;
            view.Filter = delegate (object item)
            {
                return CheckAppointmentDate((DoctorAppointmentDTO)item);
            };

            dataGridAppointments.ItemsSource = view;
        }

        private bool CheckAppointmentDate(DoctorAppointmentDTO docAppointment)
        {
            bool sameDay = false;
            if (dpAppDate.SelectedDate == null)
                sameDay = false;
            else
            {
                DateTime appDate = (DateTime)dpAppDate.SelectedDate;
                if (appDate.Date.Equals(docAppointment.AppointmentStart.Date) && !OldDocAppointment.Id.Equals(docAppointment.Id))
                    sameDay = true;
            }

            return sameDay;
        }

        private void UndoAllChanges(object sender, RoutedEventArgs e)
        {
            SetDoctorAppointmentInfo();
        }
    }
}
