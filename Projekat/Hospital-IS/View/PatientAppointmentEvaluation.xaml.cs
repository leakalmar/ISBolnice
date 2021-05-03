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
            if (PatientAppointmentEvaluationController.Instance.ShowHospitalEvaluation(HomePatient.Instance.Patient.Id))
            {
                //Napraviti prozor za pitanje da li zeli da oceni bolnicu,ako zeli napravi prozor sa anketom/ocenjivanjem bolnice
                YesNoDialogMessage yesNoDialog = new YesNoDialogMessage("Da li želite da ocenite bolnicu?");
                yesNoDialog.Show();
                this.Close();
            }
            this.Close();
        }
    }
}
