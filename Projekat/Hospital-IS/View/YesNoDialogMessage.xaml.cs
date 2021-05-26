using Hospital_IS.View.PatientViewModels;
using System;
using System.Windows;

namespace Hospital_IS.View
{
    /// <summary>
    /// Interaction logic for YesNoDialogMessage.xaml
    /// </summary>
    public partial class YesNoDialogMessage : Window
    {
        public YesNoDialogMessageViewModel DialogMessageViewModel { get; set; }
        public YesNoDialogMessage(String message)
        {
            DialogMessageViewModel = new YesNoDialogMessageViewModel(message);
            this.DataContext = DialogMessageViewModel;
            InitializeComponent();           
        }
        /*
        private void EvaluateHospital(object sender, RoutedEventArgs e)
        {
            PatientHospitalEvaluationWindow hospitalEvaluation = new PatientHospitalEvaluationWindow();
            hospitalEvaluation.HospitalEvaluationViewModel.OnRequestClose += (s, e) => hospitalEvaluation.Close();
            hospitalEvaluation.Show();
            this.Close();
        }

        private void NegativeAnswer(object sender, RoutedEventArgs e)
        {
            this.Close();
        }*/
    }
}

