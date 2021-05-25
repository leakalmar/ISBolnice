using Model;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Hospital_IS.View
{
    /// <summary>
    /// Interaction logic for AppointmentPatient.xaml
    /// </summary>
    public partial class AppointmentPatientView : UserControl
    {

        public AppointmentPatientView()
        {
            InitializeComponent();
            //this.DataContext = new AppointmentPatientViewModel();
            Calendar.DisplayDateStart = DateTime.Today;
        }
    }
}
