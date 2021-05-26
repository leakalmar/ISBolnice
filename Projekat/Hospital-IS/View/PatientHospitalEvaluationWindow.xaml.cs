using Hospital_IS.View.PatientViewModels;
using System.Windows;

namespace Hospital_IS.View
{
    /// <summary>
    /// Interaction logic for PatientHospitalEvaluation.xaml
    /// </summary>
    public partial class PatientHospitalEvaluationWindow : Window
    {
        public PatientHospitalEvaluationWindowViewModel HospitalEvaluationViewModel { get; set;}
        public PatientHospitalEvaluationWindow()
        {
            HospitalEvaluationViewModel = new PatientHospitalEvaluationWindowViewModel();
            this.DataContext = HospitalEvaluationViewModel;
            InitializeComponent();
        }
        /*
        private void EvaluateHospital(object sender, RoutedEventArgs e)
        {
            PatientHospitalEvaluationController.Instance.AddAppointmentEvaluation(Grades.SelectedIndex+1, Comment.Text, DateTime.Today, PatientMainWindowViewModel.Patient.Id);
            this.Close();
        }*/
    }
}
