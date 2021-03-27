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
    /// Interaction logic for AppointmentPatient.xaml
    /// </summary>
    public partial class AppointmentPatient : Window
    {
        public AppointmentPatient()
        {
            InitializeComponent();
        }

        private void home(object sender, RoutedEventArgs e)
        {
            HomePatient home = new HomePatient();
            home.Show();
        }

        private void allApp(object sender, RoutedEventArgs e)
        {
            AllAppointments all = new AllAppointments();
            all.Show();
        }

        private void showDoc(object sender, RoutedEventArgs e)
        {
            DocumentationPatient doc = new DocumentationPatient();
            doc.Show();
        }
    }
}
