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
        private int colNum = 0;
        public ObservableCollection<Model.DoctorAppointment> Appointments { get; set; }
        public Model.Doctor doctor { get; set; }
        public DoctorHomePage()
        {
            InitializeComponent();
            this.DataContext = this;
            //doctor = new Storages.UserFileStorage.GetByEmail(email);
            doctor = new Model.Doctor();
            Appointments = new ObservableCollection<Model.DoctorAppointment>();
            foreach (Model.DoctorAppointment appointment in doctor.doctorAppointment)
            {
                Appointments.Add(appointment);
            }
            
        }
        private void generateColumns(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            colNum++;
            if (colNum == 3)
                e.Column.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
        }

        public void ExitBtnClick(object sender, RoutedEventArgs e)
        {
            bool dialog = (bool)new ExitMess("Da li ste sigurni da želite da se odjavite?").ShowDialog();
           if (dialog)
            {
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
    }
}
