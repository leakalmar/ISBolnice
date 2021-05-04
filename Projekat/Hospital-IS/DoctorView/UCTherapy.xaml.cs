using Model;
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
    /// Interaction logic for UCTherapy.xaml
    /// </summary>
    public partial class UCTherapy : UserControl
    {
        private bool _started;

        public bool Started
        {
            get { return _started; }
            set
            {
                _started = value;
                if (!Started)
                {
                    add.Visibility = Visibility.Collapsed;
                }
                else
                {
                    add.Visibility = Visibility.Visible;
                }
            }
        }
        private UCPatientChart PatientChart;
        public UCTherapy(UCPatientChart patientChart)
        {
            InitializeComponent();
            PatientChart = patientChart;
            dataGrid.DataContext = PatientChart.Patient;

            if (!Started)
            {
                add.Visibility = Visibility.Collapsed;
            }
            else
            {
                add.Visibility = Visibility.Visible;
            }
        }
    }
}
