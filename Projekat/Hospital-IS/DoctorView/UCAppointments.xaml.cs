using Hospital_IS.DoctorView;
using Hospital_IS.Storages;
using Model;
using Storages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Hospital_IS.DoctorView
{
    /// <summary>
    /// Interaction logic for UCAppointments.xaml
    /// </summary>
    /// 

    public partial class UCAppointments : UserControl
    {
        private UCChangeApp ChangeApp { get; set; }
        public UCAppointments()
        {
            InitializeComponent();
            rooms.DataContext = MainWindow.Rooms;
            string[] list = Enum.GetNames(typeof(AppointmetType));
            string[] docApp = new string[2];
            docApp[0] = list[0];
            docApp[1] = list[1];
            type.ItemsSource = docApp;
            docotrAppointments.DataContext = DoctorHomePage.Instance.DoctorAppointment;
            from.SelectedDate = DateTime.Now.Date;
            to.SelectedDate = DateTime.Now.Date.AddDays(7);
            type.SelectedIndex = 1;
            System.Diagnostics.Debug.WriteLine(DoctorHomePage.Instance.GetPrimaryRoom());
            rooms.SelectedItem = DoctorHomePage.Instance.GetPrimaryRoom();

            DoctorAppointment ap = (DoctorAppointment)docotrAppointments.SelectedItem;
            ChangeApp = new UCChangeApp(details);
            details.Content = ChangeApp;
        }

        private void App_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            details.Visibility = Visibility.Visible;
        }

        private void conten_changed(object sender, DependencyPropertyChangedEventArgs e)
        {
            if(details.Visibility == Visibility.Visible)
            {
                docotrAppointments.Height = 195;
                ChangeApp.Appointment = (DoctorAppointment)docotrAppointments.SelectedItem;
            }
            else if(details.Visibility == Visibility.Collapsed)
            {
                docotrAppointments.Height = 430;
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
                room = DoctorHomePage.Instance.GetPrimaryRoom();
            }
            String selected = (String)type.SelectedItem;

            ICollectionView view = CollectionViewSource.GetDefaultView(DoctorHomePage.Instance.DoctorAppointment);
            view.Filter = null;
            view.Filter = delegate (object item)
            {
                return ((DoctorAppointment)item).Type.ToString().Equals(selected) & ((DoctorAppointment)item).Room == room.RoomId & ((DoctorAppointment)item).DateAndTime.Date <= endDate.Date & ((DoctorAppointment)item).DateAndTime.Date >= startDate.Date;
            };
        }

        private void Delete_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Delete)
            {
                DoctorAppointment app = (DoctorAppointment)docotrAppointments.SelectedItem;
                if(app.DateAndTime > DateTime.Now.AddDays(1))
                {
                    bool dialog = (bool)new ExitMess("Da li želite da otkažete termin?").ShowDialog();
                    if (dialog)
                    {
                        Hospital.Instance.RemoveAppointment(app);
                        DoctorHomePage.Instance.DoctorAppointment.Remove(app);
                        app.Reserved = false;
                        AppointmentFileStorage afs = new AppointmentFileStorage();
                        afs.SaveAppointment(Hospital.Instance.allAppointments);
                    }
                }
            }
        }
    }
}
