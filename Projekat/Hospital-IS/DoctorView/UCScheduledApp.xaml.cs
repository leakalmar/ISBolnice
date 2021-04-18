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

namespace Hospital_IS.DoctorView
{
    /// <summary>
    /// Interaction logic for UCScheduledApp.xaml
    /// </summary>
    public partial class UCScheduledApp : UserControl
    {
        AppointmentFileStorage afs = new AppointmentFileStorage();
        public UCScheduledApp(Patient patient, bool started)
        {
            InitializeComponent();
            dataGrid.DataContext = afs.GetAllByPatient(patient.Id);

            if (!started)
            {
                add.Visibility = Visibility.Collapsed;
            }
        }

    }
}
