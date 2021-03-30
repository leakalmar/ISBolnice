using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
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
using Model;

namespace Hospital_IS.View
{
    /// <summary>
    /// Interaction logic for UserControlHomePage.xaml
    /// </summary>
    public partial class UserControlHomePage : UserControl
    {
        public Doctor Doctor { get; set; }
        public UserControlHomePage()
        {
            InitializeComponent();
            this.Doctor = DoctorHomePage.Instance.Doctor;
            docotrAppointments.DataContext = Doctor.DoctorAppointment;
        }

        private void Patient_Selected(object sender, KeyEventArgs e)
        {
            DoctorAppointment appointment= (DoctorAppointment)docotrAppointments.SelectedItem;
            Window window = new PatientChart(appointment);
            window.Show();
        }

        public void Patient_DoubleClicked(object sender, MouseButtonEventArgs e)
        {
            DoctorAppointment ap = (DoctorAppointment)docotrAppointments.SelectedItem;
            DoctorAppointment first = (DoctorAppointment)docotrAppointments.SelectedItems[0];

            if (ap.Equals(first) && ap.DateAndTime.Date.Equals(DateTime.Now.Date))
            {
                Window window = new PatientChart(ap, true, true);
                window.Show();
            }
            else if (ap.DateAndTime > DateTime.Now.AddDays(1))
            {
                Window window = new PatientChart(ap, false, false);
                window.Show();
            }
            else
            {
                Window window = new PatientChart(ap,false, true);
                window.Show();
            }
            
        }
    }
}
