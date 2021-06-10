using DTOs;
using Enums;
using Hospital_IS.Controllers;
using Hospital_IS.DTOs;
using Hospital_IS.DTOs.SecretaryDTOs;
using Hospital_IS.Enums;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Hospital_IS.SecretaryView
{
    /// <summary>
    /// Interaction logic for ScheduleAppointment.xaml
    /// </summary>
    public partial class ScheduleAppointment : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public DoctorAppointmentDTO DocAppointment { get; set; } = new DoctorAppointmentDTO();
        public ObservableCollection<PatientDTO> Patients { get; set; } = new ObservableCollection<PatientDTO>();
        public ObservableCollection<DoctorDTO> Doctors { get; set; } = new ObservableCollection<DoctorDTO>();
        public ObservableCollection<RoomDTO> Rooms { get; set; } = new ObservableCollection<RoomDTO>();

        public ObservableCollection<int> Hours { get; set; } = new ObservableCollection<int>();
        public ObservableCollection<DoctorAppointmentDTO> ScheduledAppointments { get; set; } = new ObservableCollection<DoctorAppointmentDTO>();

        public UCAppointmentsView uca;

        public PatientDTO patient = null;
        public PatientView pv;
        public DoctorDTO doctor = null;
        public DoctorView dv;

        private int _appDuration;
        public int AppDuration
        {
            get { return _appDuration; }
            set
            {
                if (value != _appDuration)
                {
                    _appDuration = value;
                    OnPropertyChanged("AppDuration");
                }
            }
        }

        public ScheduleAppointment(UCAppointmentsView uca)
        {
            InitializeComponent();
            this.uca = uca;

            Patients = new ObservableCollection<PatientDTO>(SecretaryManagementController.Instance.GetAllRegisteredPatients());
            Doctors = new ObservableCollection<DoctorDTO>(SecretaryManagementController.Instance.GetAllDoctors());
            dpAppDate.SelectedDate = DateTime.Now;

            this.DataContext = this;
        }
        public ScheduleAppointment(UCAppointmentsView uca, PatientDTO patient, PatientView pv)
        {
            InitializeComponent();
            this.uca = uca;
            this.patient = patient;
            this.pv = pv;
            Patients = new ObservableCollection<PatientDTO>(SecretaryManagementController.Instance.GetAllRegisteredPatients());
            Doctors = new ObservableCollection<DoctorDTO>(SecretaryManagementController.Instance.GetAllDoctors());
            dpAppDate.SelectedDate = DateTime.Now;

            cbPatient.SelectedItem = patient;
            cbPatient.IsEnabled = false;

            this.DataContext = this;
        }

        public ScheduleAppointment(UCAppointmentsView uca, DoctorDTO doctor, DoctorView dv)
        {
            InitializeComponent();
            this.uca = uca;
            this.doctor = doctor;
            this.dv = dv;
            Patients = new ObservableCollection<PatientDTO>(SecretaryManagementController.Instance.GetAllRegisteredPatients());
            Doctors = new ObservableCollection<DoctorDTO>(SecretaryManagementController.Instance.GetAllDoctors());
            dpAppDate.SelectedDate = DateTime.Now;

            cbDoctor.SelectedItem = doctor;
            cbDoctor.IsEnabled = false;

            this.DataContext = this;
        }

        private void NewAppointment(object sender, RoutedEventArgs e)
        {
            if (cbAppType.SelectedIndex == -1 || cbPatient.SelectedIndex == -1 || cbDoctor.SelectedIndex == -1 
                || cbRoom.SelectedIndex == -1 || cbHours.SelectedIndex == -1 || cbMinutes.SelectedIndex == -1 
                || string.IsNullOrEmpty(txtAppDuration.Text))
            {
                MessageBox.Show("Morate da popunite sva obavezna polja!");
                return;
            }


            // doktor
            DocAppointment.Doctor = Doctors[cbDoctor.SelectedIndex];

            // soba
            DocAppointment.Room = Rooms[cbRoom.SelectedIndex].RoomId;
            
            //  pacijent
            DocAppointment.Patient = Patients[cbPatient.SelectedIndex];

            // tip pregleda
            if (cbAppType.SelectedIndex == 0)
            {
                DocAppointment.Type = AppointmentType.CheckUp;
                DocAppointment.AppTypeText = "Pregled";
            }
            else if (cbAppType.SelectedIndex == 1)
            {
                DocAppointment.Type = AppointmentType.Operation;
                DocAppointment.AppTypeText = "Operacija";
            }

            DocAppointment.Reserved = true;

            DocAppointment.AppointmentStart = (DateTime)dpAppDate.SelectedDate;
            DocAppointment.AppointmentStart = DocAppointment.AppointmentStart.AddHours(Hours[cbHours.SelectedIndex]);
            if (cbMinutes.SelectedIndex == 1)
                DocAppointment.AppointmentStart = DocAppointment.AppointmentStart.AddMinutes(30);

            int appDuration = Int32.Parse(txtAppDuration.Text);
            DocAppointment.AppointmentEnd = DocAppointment.AppointmentStart.AddMinutes(appDuration);

            uca.doctorAppointmentTarget.AddDoctorAppointment(DocAppointment);

            uca.RefreshGrid();

            if (patient != null)
                pv.RefreshGrid();
            if (doctor != null)
                dv.RefreshGrid();

            this.Close();
        }

        private void Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void VerifyAppointment(object sender, RoutedEventArgs e)
        {
            if (cbDoctor.SelectedIndex != -1 && cbRoom.SelectedIndex != -1 && cbPatient.SelectedIndex != -1 
                && dpAppDate.SelectedDate != null && cbHours.SelectedIndex != -1 && cbMinutes.SelectedIndex != -1 && !string.IsNullOrEmpty(txtAppDuration.Text)) 
            {
                // doktor
                DocAppointment.Doctor = Doctors[cbDoctor.SelectedIndex];

                // soba
                DocAppointment.Room = Rooms[cbRoom.SelectedIndex].RoomId;

                //pacijent
                DocAppointment.Patient = Patients[cbPatient.SelectedIndex];

                // datum, vreme i trajanje pregleda
                try
                {
                    DocAppointment.AppointmentStart = (DateTime)dpAppDate.SelectedDate;
                    DocAppointment.AppointmentStart = DocAppointment.AppointmentStart.AddHours(Hours[cbHours.SelectedIndex]);
                    if (cbMinutes.SelectedIndex == 1)
                        DocAppointment.AppointmentStart = DocAppointment.AppointmentStart.AddMinutes(30);

                    int appDuration = Int32.Parse(txtAppDuration.Text);
                    DocAppointment.AppointmentEnd = DocAppointment.AppointmentStart.AddMinutes(appDuration);

                }
                catch (Exception ex)
                {
                }

                EnableAppointmentConfirmation(uca.doctorAppointmentTarget.VerifyAppointment(DocAppointment));
            }
        }

        private void EnableAppointmentConfirmation(bool isValid)
        {
            if (isValid)
            {
                btnConfirm.IsEnabled = true;
                //txtStartOfApp.Background = new SolidColorBrush(Colors.White);
                //txtEndOfApp.Background = new SolidColorBrush(Colors.White);
            }
            else
            {
                //txtStartOfApp.Background = new SolidColorBrush(Colors.Red);
                //txtEndOfApp.Background = new SolidColorBrush(Colors.Red);
                btnConfirm.IsEnabled = false;
            }
        }

        private void btnEmergency_Click(object sender, RoutedEventArgs e)
        {
            ScheduleEmergencyAppointment sea;
            if (patient == null)
            {
                sea = new ScheduleEmergencyAppointment(this);
            }
            else 
            {
                sea = new ScheduleEmergencyAppointment(this, patient);
            }
            this.Visibility = Visibility.Collapsed;
            sea.ShowDialog();
        }

        private void cbAppType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbAppType.SelectedIndex == 0)
            {
                txtAppDuration.IsEnabled = false;
                cbRoom.SelectedIndex = -1;
                txtAppDuration.Text = "30";
                Rooms = new ObservableCollection<RoomDTO>(DoctorAppointmentManagementController.Instance.GetRoomByType(RoomType.ConsultingRoom));
                cbRoom.ItemsSource = Rooms;
            }
            else
            {
                txtAppDuration.IsEnabled = true;
                cbRoom.SelectedIndex = -1;
                txtAppDuration.Text = "";
                Rooms = new ObservableCollection<RoomDTO>(DoctorAppointmentManagementController.Instance.GetRoomByType(RoomType.OperationRoom));
                cbRoom.ItemsSource = Rooms;
            }
        }

        private void cbDoctor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ScheduledAppointments = new ObservableCollection<DoctorAppointmentDTO>(uca.doctorAppointmentTarget.GetFutureAppointmentsForDoctor(Doctors[cbDoctor.SelectedIndex].Id));
            GenerateAppointmentTimes(Doctors[cbDoctor.SelectedIndex]);
            if (dpAppDate.SelectedDate != null) {
                ICollectionView view = new CollectionViewSource { Source = ScheduledAppointments }.View;
                view.Filter = delegate (object item)
                {
                    return CheckAppointmentDate((DoctorAppointmentDTO)item);
                };

                dataGridAppointments.ItemsSource = view;
            }
        }
        private bool CheckAppointmentDate(DoctorAppointmentDTO docAppointment)
        {
            bool sameDay = false;
            if (dpAppDate.SelectedDate == null)
                sameDay = false;
            else
            {
                DateTime appDate = (DateTime)dpAppDate.SelectedDate;
                if (appDate.Date.Equals(docAppointment.AppointmentStart.Date))
                    sameDay = true;
            }

            return sameDay;
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

        private void GenerateAppointmentTimes(DoctorDTO doctor)
        {
            Hours.Clear();
            if (doctor.WorkShift.Equals(WorkDayShift.FirstShift))
            {
                for (int i = 8; i <= 13; i++)
                    Hours.Add(i);
            }
            else
            {
                for (int i = 14; i <= 20; i++)
                    Hours.Add(i);
            }

            cbHours.ItemsSource = Hours;
        }

        private void dpAppDate_LostFocus(object sender, RoutedEventArgs e)
        {
            DateTime date = DateTime.Today;
            if (dpAppDate.SelectedDate != null)
                date = (DateTime)dpAppDate.SelectedDate;

            if (date < DateTime.Today) 
            {
                dpAppDate.SelectedDate = DateTime.Today;
            }
        }

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
