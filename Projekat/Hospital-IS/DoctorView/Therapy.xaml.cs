using System.Windows;
using System.Windows.Controls;

namespace Hospital_IS.DoctorView
{
    public partial class Therapy : UserControl
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
        private PatientChart PatientChart;

        public Therapy()
        {

        }
        public Therapy(PatientChart patientChart)
        {
            InitializeComponent();
            PatientChart = patientChart;
            //dataGrid.DataContext = PatientChart.Patient;

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
