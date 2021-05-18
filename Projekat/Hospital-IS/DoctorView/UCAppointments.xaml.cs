using Controllers;
using Model;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace Hospital_IS.DoctorView
{
    public partial class UCAppointments : UserControl
    {
        private UCChangeApp ChangeApp { get; set; }
        public UCAppointments()
        {
            InitializeComponent();
            rooms.DataContext = RoomController.Instance.GetAllRooms();
            string[] list = Enum.GetNames(typeof(AppointmentType));
            string[] docApp = new string[2];
            docApp[0] = list[0];
            docApp[1] = list[1];
            type.ItemsSource = docApp;
            //doctorAppointments.DataContext = DoctorMainWindow.Instance._ViewModel.DoctorAppointments;
            from.SelectedDate = DateTime.Now.Date;
            to.SelectedDate = DateTime.Now.Date.AddDays(7);
            type.SelectedIndex = 1;
            rooms.SelectedItem = RoomController.Instance.GetRoomById(DoctorMainWindow.Instance._ViewModel.DoctorPrimaryRoom);

            DoctorAppointment ap = (DoctorAppointment)doctorAppointments.SelectedItem;
            ChangeApp = new UCChangeApp(details);
            details.Content = ChangeApp;
        }

        private void App_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ChangeApp.Appointment = (DoctorAppointment)doctorAppointments.SelectedItem;
            details.Visibility = Visibility.Visible;
        }

        private void conten_changed(object sender, DependencyPropertyChangedEventArgs e)
        {
            if(details.Visibility == Visibility.Visible)
            {
                doctorAppointments.Height = 195;
                if(doctorAppointments.SelectedItem != null)
                {
                    ChangeApp.Appointment = (DoctorAppointment)doctorAppointments.SelectedItem;
                }
            }
            else
            {
                doctorAppointments.Height = 430;
            }
        }

        private void filter_changed(object sender, SelectionChangedEventArgs e)
        {
            Room room = (Room)rooms.SelectedItem;
            DateTime startDate = (DateTime)from.SelectedDate;
            DateTime endDate;
            if(to.SelectedDate == null)
            {
                endDate = DateTime.Now.Date.AddDays(7);
            }
            else
            {
                endDate = (DateTime)to.SelectedDate;
            }

            if (room == null)
            {
                room = RoomController.Instance.GetRoomById(DoctorMainWindow.Instance._ViewModel.DoctorPrimaryRoom);
            }
            String selected = (String)type.SelectedItem;

           /* ICollectionView view = new CollectionViewSource { Source = DoctorMainWindow.Instance._ViewModel.DoctorAppointments }.View;
            view.Filter = null;
            view.Filter = delegate (object item)
            {
                return ((DoctorAppointment)item).Type.ToString().Equals(selected) & ((DoctorAppointment)item).Room == room.RoomId & ((DoctorAppointment)item).AppointmentStart.Date <= endDate.Date & ((DoctorAppointment)item).AppointmentStart.Date >= startDate.Date;
            };

            doctorAppointments.DataContext = view;*/
        }

        private void Delete_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Delete)
            {
                DoctorAppointment app = (DoctorAppointment)doctorAppointments.SelectedItem;
                if(app.AppointmentStart > DateTime.Now.AddDays(1))
                {
                    bool dialog = (bool)new ExitMess("Da li želite da otkažete termin?").ShowDialog();
                    if (dialog)
                    {
                        DoctorAppointmentController.Instance.RemoveAppointment(app);
                       // DoctorMainWindow.Instance._ViewModel.DoctorAppointments.Remove(app);
                        app.Reserved = false;
                    }
                }
            }
        }
    }
}
