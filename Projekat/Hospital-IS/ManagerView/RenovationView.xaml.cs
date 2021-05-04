using Controllers;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Hospital_IS.ManagerView
{
    /// <summary>
    /// Interaction logic for RenovationView.xaml
    /// </summary>
    public partial class RenovationView : Window
    {
        public static ObservableCollection<Appointment> allAppointments;

        public Room CurrentRoom { get; set; }
        public RenovationView(Room room)
        {
            allAppointments = new ObservableCollection<Appointment>(AppointmentController.Instance.GetAppByRoomId(room.RoomId));
            InitializeComponent();
            CurrentRoom = room;
            RenovationDateGrid.DataContext = allAppointments;

        }

        private void AffirmRenovation_Click(object sender, RoutedEventArgs e)
        {
           


            bool isNull = false;
            if (RenovationStart.Value == null || RenovationEnd.Value == null)
            {
                MessageBox.Show("Izaberite datum i vreme");
                isNull = true;
            }

            if (!isNull)
            {
                DateTime renovationStart = (DateTime)RenovationStart.Value;
                DateTime renovationEnd = (DateTime)RenovationEnd.Value;
                if (renovationStart >= renovationEnd || renovationStart.Date < DateTime.Today.AddMonths(1).Date || renovationEnd.Date < DateTime.Today.AddMonths(1).Date)
                {
                    MessageBox.Show("Zakazite odgovarajuvi termin");
                }
                else
                {
                    String description;
                    if(DescriptionField.Text == null)
                    {
                        description = "";
                    }
                    else
                    {
                        description = DescriptionField.Text;
                    }
                    bool isSuccces = AppointmentController.Instance.MakeRenovationAppointment(renovationStart, renovationEnd, description, CurrentRoom.RoomId);
                    if (isSuccces)
                    {
                        MessageBox.Show("Uspjesno zakazan termin");
                        RenovationDateGrid.DataContext = new ObservableCollection<Appointment>(AppointmentController.Instance.GetAppByRoomId(CurrentRoom.RoomId));
                    }
                    else
                    {
                        MessageBox.Show("Nespjeno zakazan termin");

                    }
                }
            }
            


        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            RoomOptions roomOptions = new RoomOptions();
            roomOptions.Show();
            this.Close();
        }
    }
}
