using Hospital_IS.Storages;
using Model;
using Storages;
using System;
using System.Collections.Generic;
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

namespace Hospital_IS.View
{
    /// <summary>
    /// Interaction logic for UCAppointments.xaml
    /// </summary>
    /// 

    public partial class UCAppointments : UserControl
    {
        private FSDoctor dfs = new FSDoctor();
        private RoomStorage rs = new RoomStorage();
        public UCAppointments()
        {
            InitializeComponent();
            doctors.DataContext = dfs.GetAll();
            rooms.DataContext = rs.GetAll();
            string[] list = Enum.GetNames(typeof(AppointmetType));
            string[] docApp = new string[2];
            docApp[0] = list[0];
            docApp[1] = list[1];
            type.ItemsSource = docApp;
            docotrAppointments.DataContext = DoctorHomePage.Instance.DoctorAppointment;
            from.SelectedDate = DateTime.Now.Date;
            to.SelectedDate = DateTime.Now.Date.AddDays(7);
            doctors.SelectedValue = DoctorHomePage.Instance.Doctor;
            rooms.SelectedItem = (ComboBoxItem)rooms.ItemContainerGenerator.ContainerFromItem(DoctorHomePage.Instance.Doctor);
            type.SelectedIndex = 0;
        }

        private void App_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DoctorAppointment ap = (DoctorAppointment)docotrAppointments.SelectedItem;
            docotrAppointments.Height = 195;
            //details.Content = new UCChangeApp(ap);
        }
    }
}
