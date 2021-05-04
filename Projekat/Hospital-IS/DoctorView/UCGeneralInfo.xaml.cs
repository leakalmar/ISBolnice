using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for UCGeneralInfo.xaml
    /// </summary>
    public partial class UCGeneralInfo : UserControl
    {


        private bool _started;

        public bool Started
        {
            get { return _started; }
            set { 
                _started = value;
                if (!Started)
                {
                    change.Visibility = Visibility.Collapsed;
                }
                else
                {
                    change.Visibility = Visibility.Visible;
                }
            }
        }

        public UCGeneralInfo(UCPatientChart patientChart)
        {
            InitializeComponent();
            info.DataContext = patientChart.Patient;

            if (!Started)
            {
                change.Visibility = Visibility.Collapsed;
            }
            else
            {
                change.Visibility = Visibility.Visible;
            }
        }

    }
}
