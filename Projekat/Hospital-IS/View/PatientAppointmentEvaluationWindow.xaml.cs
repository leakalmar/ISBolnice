using Hospital_IS.View.PatientViewModels;
using System.Windows;

namespace Hospital_IS.View
{
    /// <summary>
    /// Interaction logic for PatientAppointmentEvaluation.xaml
    /// </summary>
    public partial class PatientAppointmentEvaluationWindow : Window
    {
        public PatientAppointmentEvaluationWindowViewModel AppointmentEvaluation { get; set; }
        public PatientAppointmentEvaluationWindow(int doctorAppointmentId)
        {
            AppointmentEvaluation = new PatientAppointmentEvaluationWindowViewModel(doctorAppointmentId);
            this.DataContext = AppointmentEvaluation;
            InitializeComponent();
            
        }
    }
}
