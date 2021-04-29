using Controllers;
using DTOs;
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
    public partial class PatientAppointmentEvaluation : Window
    {
        public PatientAppointmentEvaluationDTO AppointmentEvaluation { get; set; }
        public PatientAppointmentEvaluation(DoctorAppointment doctorAppointment)
        {
            InitializeComponent();
            AppointmentEvaluation = new PatientAppointmentEvaluationDTO(doctorAppointment.AppointmentStart.Date, doctorAppointment.Patient.Id, doctorAppointment.Doctor.Id);
        }

        private void EvaluateAppointment(object sender, RoutedEventArgs e)
        {
            AppointmentEvaluation.Grade = Grades.SelectedIndex + 1;
            AppointmentEvaluation.Comment = Comment.Text;
            PatientAppointmentEvaluationController.Instance.AddAppointmentEvaluation(AppointmentEvaluation);
            this.Close();
        }
    }
}
