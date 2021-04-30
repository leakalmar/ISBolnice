using Controllers;
using Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Hospital_IS.DoctorView
{
    public partial class UCNewApp : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        private String _documentMessage;

        public String DocumentMessage
        {
            get { return _documentMessage; }
            set
            {
                if (value != _documentMessage)
                {
                    _documentMessage = value;

                }
                DoctorAppointment selected = (DoctorAppointment)appointments.SelectedItem;

                if (!DoctorHomePage.Instance.Doctor.Id.Equals(selected.Doctor.Id))
                {
                    if (value.Any(x => !char.IsLetter(x)) || value == "")
                    {
                        save.IsEnabled = false;
                    }
                    else
                    {
                        save.IsEnabled = true;
                    }
                }
                else
                {
                    save.IsEnabled = true;
                }
                OnPropertyChanged("DocumentMessage");
            }
        }

        public DoctorAppointment Appointment { get; }
        public bool Emergency = false;

        public UCNewApp(DoctorAppointment appointment)
        {
            InitializeComponent();
            Emergency = false;
            cause.BorderBrush = Brushes.PaleVioletRed;
            cause.DataContext = this;
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
                    break;
                }
            }

            foreach (Doctor d in MainWindow.Doctors)
            {
                if (d.Id.Equals(DoctorHomePage.Instance.Doctor.Id))
                {
                    doctors.SelectedItem = d;
                    break;
                }
            }


            foreach (Room r in MainWindow.Rooms)
            {
                if (r.RoomId.Equals(DoctorHomePage.Instance.PrimaryRoom.RoomId))
                {
                    rooms.SelectedItem = r;
                    break;
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
                Doctor doctor = (Doctor)doctors.SelectedItem;
                Room room = (Room)rooms.SelectedItem;
                SelectedDatesCollection dates = calendar.SelectedDates;
                AppointmetType type = FindType();


                changeVisibilityOfFields(type, doctor);
                List<DoctorAppointment> allAppointments = DoctorAppointmentController.Instance.GetSuggestedAndReservedByDoctor(dates, Emergency, room.RoomId, type, (TimeSpan)duration.Value, Appointment.Patient, doctor);

                ICollectionView view = new CollectionViewSource { Source = ConvertList(allAppointments) }.View;
                view.SortDescriptions.Clear();
                view.SortDescriptions.Add(new SortDescription("Appointment.AppointmentStart", ListSortDirection.Ascending));

                appointments.DataContext = view;
            }

        }

        private List<AppointmentRow> ConvertList(List<DoctorAppointment> allAppointments)
        {
            List<AppointmentRow> list = new List<AppointmentRow>();
            foreach (DoctorAppointment da in allAppointments)
            {
                list.Add(new AppointmentRow(da, Emergency));
            }

            return list;
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
            DoctorAppointment selected = (DoctorAppointment)((AppointmentRow)appointments.SelectedItem).Appointment;
            var row = appointments.ItemContainerGenerator.ContainerFromItem(selected) as DataGridRow;


            schedule.Visibility = Visibility.Collapsed;
            date.Content = selected.AppointmentStart.Date;
            time.Content = selected.AppointmentStart.TimeOfDay;
            documentSpecialty.Content = selected.Doctor.Specialty.Name;

            documentDoctor.Content = selected.Doctor.Name.ToString() + " " + ShortSurname(selected.Doctor);
            thisDoctor.Content = DoctorHomePage.Instance.Doctor.Name.ToString() + " " + ShortSurname(DoctorHomePage.Instance.Doctor);
            today.Content = DateTime.Now.Date;

            document.Visibility = Visibility.Visible;
            cause.Focus();


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

            DoctorAppointment selected = (DoctorAppointment)((AppointmentRow)appointments.SelectedItem).Appointment;

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

        private void emergency_Click(object sender, RoutedEventArgs e)
        {
            if(Emergency == false)
            {
                Image image = new Image();
                image.Source = new BitmapImage(new Uri(@"pack://application:,,,/Resources/redsiren.png"));
                emergency.Content = image;
                Emergency = true;
            }
            else
            {
                Image image = new Image();
                image.Source = new BitmapImage(new Uri(@"pack://application:,,,/Resources/siren.png"));
                emergency.Content = image;
                Emergency = false;
            }
            filterAppointments();
        }
    }

}
