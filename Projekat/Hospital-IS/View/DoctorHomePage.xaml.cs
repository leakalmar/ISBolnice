using Hospital_IS.Storages;
using Model;
using Storages;
using System.Collections.ObjectModel;
using System.Windows;

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

        public void MinimizeBtnClick(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        public void MaximizeBtnClick(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Maximized;
        }

        private void HomePage_Clicked(object sender, RoutedEventArgs e)
        {
            DataContext = new ViewModel.HomePage();
        }
    }
}
