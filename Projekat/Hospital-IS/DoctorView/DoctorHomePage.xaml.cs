using Hospital_IS.Storages;
using Model;
using Storages;
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
using Hospital_IS.DoctorView;

namespace Hospital_IS.DoctorView
{
    public partial class DoctorHomePage : Window
    {
        private static DoctorHomePage instance = null;
        private Doctor doctor;
        public UserControlHomePage HomePage;
        public UCAppointments Appointments;
        private int Last { get; set; } = 0;

        public Doctor GetDoctor()
        {
            return doctor;
        }

        public void SetDoctor(Doctor value)
        {
            doctor = value;
        }

        private ObservableCollection<DoctorAppointment> _doctorAppointments = new ObservableCollection<DoctorAppointment>();

        public ObservableCollection<DoctorAppointment> DoctorAppointment
        {
            get { return  _doctorAppointments; }
            set {  _doctorAppointments =  value; }
        }


        FSDoctor dfs = new FSDoctor();
        private Room primaryRoom;

        public Room GetPrimaryRoom()
        {
            return primaryRoom;
        }

        public void SetPrimaryRoom(Room value)
        {
            primaryRoom = value;
        }

        public static DoctorHomePage Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DoctorHomePage();
                }
                return instance;
            }
        }
        public DoctorHomePage()
        {
            InitializeComponent();
            SetDoctor(MainWindow.DoctortUser);
            foreach (Room r in MainWindow.Rooms)
            {
                if (r.RoomId.Equals(doctor.PrimaryRoom))
                {
                    SetPrimaryRoom(r);
                    break;
                }
            }
            start.IsChecked = true;



        }

        public void ExitBtnClick(object sender, RoutedEventArgs e)
        {
            bool dialog = (bool)new ExitMess("Da li ste sigurni da želite da se odjavite?").ShowDialog();
           if (dialog)
            {
                dfs.UpdateDoctor(GetDoctor());
                MainWindow login = new MainWindow();
                login.Show();
                this.Hide();
                AppointmentFileStorage afs = new AppointmentFileStorage();
                afs.SaveAppointment(Hospital.Instance.allAppointments);
                this.Hide();
                
            }
         }

        public void MinBtnClick(object sender, RoutedEventArgs e)
        {
            WindowControls.DoMinimize(this);
        }

        public void MaxBtnClick(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            WindowControls.DoMaximize(this, btn);
        }

        public void FullBtnClick(object sender, RoutedEventArgs e)
        {
            WindowControls.DoFullscreen(this);
        }

        private void Meni_Clicked(object sender, RoutedEventArgs e)
        {
            int index = int.Parse(((RadioButton)e.Source).Uid);

            switch (Last)
            {
                case 0:
                    Home.Children.Remove(HomePage);
                    break;
                case 1:
                    Home.Children.Remove(Appointments);
                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    break;
                case 5:
                    break;
                case 6:
                    break;
                case 7:
                    break;
            }

            switch (index)
            {
                case 0:
                    Home.Children.Add(HomePage);
                    Last = 0;
                    break;
                case 1:
                    Home.Children.Add(Appointments);
                    Last = 1;
                    break;
                case 2:
                    Last = 2;
                    break;
                case 3:
                    Last = 3;
                    break;
                case 4:
                    Last = 4;
                    break;
                case 5:
                    Last = 5;
                    break;
                case 6:
                    Last = 6;
                    break;
                case 7:
                    Last = 7;
                    break;
            }
        }

    }
}
