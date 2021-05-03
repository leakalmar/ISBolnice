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
    public partial class UCNewApp : UserControl
    {

        public ObservableCollection<Room> Rooms { get; set; }
        public DoctorAppointment Appointment { get; }
        public bool Emergency = false;

        public UCNewApp(DoctorAppointment appointment)
        {
            InitializeComponent();
            Emergency = false;
            Appointment = appointment;
            Rooms = new ObservableCollection<Room>(RoomController.Instance.getRoomByType(RoomType.ConsultingRoom));

            doctors.DataContext = MainWindow.Doctors;
            rooms.DataContext = Rooms;
            duration.Value = new TimeSpan(0);

            string[] list = Enum.GetNames(typeof(AppointmetType));
            string[] docApp = new string[2];
            docApp[0] = list[0];
            docApp[1] = list[1];

            specialization.ItemsSource = MainWindow.Specialties;
            InitializeComoboBoxes();

        }

        private void InitializeComoboBoxes()
        {
            types.SelectedIndex = 0;
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
                List<DateTime> dates = new List<DateTime>(calendar.SelectedDates);
                AppointmetType type = FindType();
                Specialty specialty = (Specialty)specialization.SelectedItem;

                changeVisibilityOfFields(type, doctor);

                if (Emergency)
                {
                    List<SuggestedEmergencyAppDTO> allEmergencyAppointments = DoctorAppointmentController.Instance.GetSuggestedEmergencyAppsForDoctor(dates, Emergency, room, type, (TimeSpan)duration.Value, Appointment.Patient, doctor);
                    ICollectionView view = new CollectionViewSource { Source = allEmergencyAppointments }.View;
                    view.GroupDescriptions.Add((new PropertyGroupDescription("SuggestedAppointment.Doctor.Surname")));
                    if (doctor.Id != -1)
                    {
                        view.Filter = null;
                        view.Filter = delegate (object item)
                        {
                            return ((SuggestedEmergencyAppDTO)item).SuggestedAppointment.Doctor.Id == doctor.Id;
                        };
                    }


                    emergencyAppointments.DataContext = view;
                    appointmentsGroupBox.Visibility = Visibility.Collapsed;
                    emergencyGroupBox.Visibility = Visibility.Visible;
                }
                else
                {
                    List<DoctorAppointment> allAppointments = DoctorAppointmentController.Instance.GetSuggestedAppointmentsByDoctor(dates, Emergency, room, type, (TimeSpan)duration.Value, Appointment.Patient, doctor);
                    ICollectionView view = new CollectionViewSource { Source = ConvertList(allAppointments) }.View;
                    view.SortDescriptions.Clear();
                    view.SortDescriptions.Add(new SortDescription("Appointment.AppointmentStart", ListSortDirection.Ascending));

                    appointments.DataContext = view;
                    emergencyGroupBox.Visibility = Visibility.Collapsed;
                    appointmentsGroupBox.Visibility = Visibility.Visible;
                }


            }

        }


        private void types_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (FindType() == AppointmetType.CheckUp)
            {
                Rooms = new ObservableCollection<Room>(RoomController.Instance.getRoomByType(RoomType.ConsultingRoom));
                rooms.ItemsSource = Rooms;
            }
            else
            {
                Rooms = new ObservableCollection<Room>(RoomController.Instance.getRoomByType(RoomType.OperationRoom));
                rooms.ItemsSource = Rooms;
            }
            rooms.SelectedItem = Rooms[0];
            filterAppointments();

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
            if (((ComboBoxItem)types.SelectedItem).Content.Equals("Pregled"))
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
            if (Emergency)
            {
                List<Doctor> docs = new List<Doctor>();
                docs.Add(new Doctor(-1, "Svi", "doktori", DateTime.Now, null, null, null, 0, DateTime.Now, null, selectedSpecialty, 0));
                docs.AddRange(DoctorController.Instance.GetDoctorsBySpecilty(selectedSpecialty));
                doctors.DataContext = docs;
            }
            else
            {
                doctors.DataContext = DoctorController.Instance.GetDoctorsBySpecilty(selectedSpecialty);
            }


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
                types.SelectedIndex = 0;
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
            if ((doctor.Specialty.Name.Equals(DoctorHomePage.Instance.Doctor.Specialty.Name) && type == AppointmetType.CheckUp) || (Emergency && type == AppointmetType.CheckUp))
            {
                rooms.Visibility = Visibility.Visible;
                types.Visibility = Visibility.Visible;
                duration.Visibility = Visibility.Collapsed;
                lblRoom.Visibility = Visibility.Visible;
                lblType.Visibility = Visibility.Visible;
                lblDuration.Visibility = Visibility.Collapsed;
            }
            else if ((doctor.Specialty.Name.Equals(DoctorHomePage.Instance.Doctor.Specialty.Name) && type == AppointmetType.Operation) || Emergency)
            {
                rooms.Visibility = Visibility.Visible;
                types.Visibility = Visibility.Visible;
                duration.Visibility = Visibility.Visible;
                lblRoom.Visibility = Visibility.Visible;
                lblType.Visibility = Visibility.Visible;
                lblDuration.Visibility = Visibility.Visible;
            }
            else if (!doctor.Specialty.Name.Equals(DoctorHomePage.Instance.Doctor.Specialty.Name))
            {
                rooms.Visibility = Visibility.Collapsed;
                types.Visibility = Visibility.Collapsed;
                duration.Visibility = Visibility.Collapsed;
                lblRoom.Visibility = Visibility.Collapsed;
                lblType.Visibility = Visibility.Collapsed;
                lblDuration.Visibility = Visibility.Collapsed;
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
            this.Visibility = Visibility.Collapsed;
            DoctorHomePage.Instance.Home.Children.Add(new UCIssueInstruction(this));
        }

        private void emergencyApp_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            SuggestedEmergencyAppDTO selected = (SuggestedEmergencyAppDTO)emergencyAppointments.SelectedItem;

            foreach (RescheduledAppointmentDTO rescheduled in selected.RescheduledAppointments)
            {
                DoctorAppointmentController.Instance.UpdateAppointment(rescheduled.OldDocAppointment, rescheduled.DocAppointment);  //notifikacije ???
            }
            DoctorAppointmentController.Instance.AddAppointment(selected.SuggestedAppointment);

            DoctorHomePage.Instance.Home.Children.Remove(this);
            DoctorHomePage.Instance.Home.Children.Add(new UCPatientChart(Appointment, true));

        }

        private void emergency_Click(object sender, RoutedEventArgs e)
        {
            if (Emergency == false)
            {
                Specialty selectedSpecialty = (Specialty)specialization.SelectedItem;
                List<Doctor> docs = new List<Doctor>();
                docs.Add(new Doctor(-1, "Svi", "doktori", DateTime.Now, null, null, null, 0, DateTime.Now, null, selectedSpecialty, 0));
                docs.AddRange(DoctorController.Instance.GetDoctorsBySpecilty(selectedSpecialty));
                doctors.DataContext = docs;

                Image image = new Image();
                image.Source = new BitmapImage(new Uri(@"pack://application:,,,/Resources/redsiren.png"));
                emergency.Content = image;
                Emergency = true;
            }
            else
            {
                Specialty selectedSpecialty = (Specialty)specialization.SelectedItem;
                doctors.DataContext = DoctorController.Instance.GetDoctorsBySpecilty(selectedSpecialty);
                types.SelectedIndex = 0;

                Image image = new Image();
                image.Source = new BitmapImage(new Uri(@"pack://application:,,,/Resources/siren.png"));
                emergency.Content = image;
                Emergency = false;
            }
            filterAppointments();
        }
    }

}
