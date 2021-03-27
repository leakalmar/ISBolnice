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
using System.Windows.Shapes;


namespace Hospital_IS.View
{
    /// <summary>
    /// Interaction logic for HomePatient.xaml
    /// </summary>
    public partial class HomePatient : Window
    {
      
        public HomePatient()
        {
            InitializeComponent();          
        }

        private void reserveApp(object sender, RoutedEventArgs e)
        {
            AppointmentPatient ap = new AppointmentPatient();
            ap.Show();
        }

        private void allApp(object sender, RoutedEventArgs e)
        {

            AllAppointments ap = new AllAppointments();
            ap.Show();
        }

        private void showDoc(object sender, RoutedEventArgs e)
        {
            DocumentationPatient dp = new DocumentationPatient();
            dp.Show();
        }
    }
}
