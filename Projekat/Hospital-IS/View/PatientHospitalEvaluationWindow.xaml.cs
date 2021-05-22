using Hospital_IS.Controllers;
using Hospital_IS.View.PatientViewModels;
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
