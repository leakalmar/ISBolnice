using Controllers;
using Hospital_IS.View.PatientViewModels;
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
using System.Windows.Shapes;

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
