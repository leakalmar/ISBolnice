using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for UCNewApp.xaml
    /// </summary>
    public partial class UCNewApp : UserControl
    {
        public UCNewApp()
        {
            InitializeComponent();
            doctors.DataContext = MainWindow.Doctors;
            rooms.DataContext = MainWindow.Rooms;
            string[] list = Enum.GetNames(typeof(AppointmetType));
            string[] docApp = new string[2];
            docApp[0] = list[0];
            docApp[1] = list[1];
            types.ItemsSource = docApp;
        }

        private void duration_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex reg = new Regex("[^0-9]+.[0|5]");
            e.Handled = reg.IsMatch(e.Text);
        }

        private void filter_changed(object sender, SelectionChangedEventArgs e)
        {
            Doctor doc = (Doctor)doctors.SelectedItem;
            Room room = (Room)rooms.SelectedItem;
            SelectedDatesCollection dates = calendar.SelectedDates;
            if (doc == null)
            {
                foreach (Doctor d in MainWindow.Doctors)
                {
                    if (d.Id.Equals(DoctorHomePage.Instance.GetDoctor().Id))
                    {
                        doctors.SelectedItem = d;
                        doc = d;
                    }
                }
            }
            if (room == null)
            {
                foreach (Room r in MainWindow.Rooms)
                {
                    if (r.RoomId.Equals(DoctorHomePage.Instance.GetDoctor().Id))
                    {
                        rooms.SelectedItem = r;
                        room = r;
                    }
                }
            }
            if (dates == null)
            {
                calendar.SelectedDate = DateTime.Now.Date;
                dates = calendar.SelectedDates;
            }


        }
    }
}
