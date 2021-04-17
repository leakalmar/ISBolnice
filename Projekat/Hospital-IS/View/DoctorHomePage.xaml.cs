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

namespace Hospital_IS.View
{
    public partial class DoctorHomePage : Window
    {
        private static DoctorHomePage instance = null;
        public Model.Doctor Doctor { get; set; }
        public ObservableCollection<DoctorAppointment> DoctorAppointment { get; set; } = new ObservableCollection<DoctorAppointment>();

        FSDoctor dfs = new FSDoctor();

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
            WindowControls.SetInicials(this);
            Doctor = MainWindow.DoctortUser;
            this.DataContext = new ViewModel.HomePage();



        }

        public void ExitBtnClick(object sender, RoutedEventArgs e)
        {
            bool dialog = (bool)new ExitMess("Da li ste sigurni da želite da se odjavite?").ShowDialog();
           if (dialog)
            {
                dfs.UpdateDoctor(Doctor);
                MainWindow login = new MainWindow();
                login.Show();
                this.Hide();
                AppointmentFileStorage afs = new AppointmentFileStorage();
                afs.SaveAppointment(Hospital.Instance.allAppointments);
                this.Close();
                
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

            switch (index)
            {
                case 0:
                    Home.Content = new UserControlHomePage();
                    break;
                case 1:
                    Home.Content = new UCAppointments();
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
        }
    }
}
