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
    /// Interaction logic for YesNoDialogMessage.xaml
    /// </summary>
    public partial class YesNoDialogMessage : Window
    {
        public YesNoDialogMessage(String message)
        {
            InitializeComponent();
            Message.Text = message;
        }

        private void EvaluateHospital(object sender, RoutedEventArgs e)
        {
            PatientHospitalEvaluationWindow hospitalEvaluation = new PatientHospitalEvaluationWindow();
            hospitalEvaluation.Show();
            this.Close();
        }

        private void NegativeAnswer(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

