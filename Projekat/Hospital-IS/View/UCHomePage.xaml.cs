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
        public ObservableCollection<DoctorAppointment> appointments;
        public UserControlHomePage()
        {
            InitializeComponent();
            docotrAppointments.DataContext = DoctorHomePage.Instance.DoctorAppointment;
        }

        private void Patient_Selected(object sender, KeyEventArgs e)
        {
            DoctorAppointment appointment= (DoctorAppointment)docotrAppointments.SelectedItem;
            DoctorHomePage.Instance.Home.Content = new UCPatientChart(appointment);
        }

        public void Patient_DoubleClicked(object sender, MouseButtonEventArgs e)
        {
            DoctorAppointment ap = (DoctorAppointment)docotrAppointments.SelectedItem;

            //if (ap.Equals(first) && ap.DateAndTime.Date.Equals(DateTime.Now.Date))
            //{
            //    DoctorHomePage.Instance.Home.Content = new UCPatientChart(ap, true, true);
            //}
            //else if (ap.DateAndTime > DateTime.Now.AddDays(1))
            //{
            //    DoctorHomePage.Instance.Home.Content = new UCPatientChart(ap, false, false);
            //}
            //else
            //{
            //    DoctorHomePage.Instance.Home.Content = new UCPatientChart(ap,false, true);
            //}

            DoctorHomePage.Instance.Home.Content = new UCAppDetail(ap);


        }
    }
}
