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
    }
}
