using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Hospital_IS.DoctorView
{
    /// <summary>
    /// Interaction logic for UCReport.xaml
    /// </summary>
    public partial class UCReport : UserControl
    {
        Patient Patient { get; set; }
        DoctorAppointment Appointment { get; set; }
        public UCReport(DoctorAppointment appointment)
        {
            InitializeComponent();
            Patient = appointment.Patient;
            Appointment = appointment;
            reportDetail.Text = Appointment.Report.Amnesis;
            
            medicines.DataContext = appointment.Patient.MedicalHistory.GetByAppointment(Appointment);

        }

        private void Perscription_Click(object sender, RoutedEventArgs e)
        {
            (this.Parent as Grid).Visibility = Visibility.Collapsed;
            Appointment.Report.Amnesis = reportDetail.Text;
            DoctorHomePage.Instance.Home.Children.Add(new UCSearchMedicine(Appointment));
        }

        private void delete_(object sender, KeyEventArgs e)
        {

        }

        private void delete_prescription(object sender, KeyEventArgs e)
        {
            Prescription selected = (Prescription)medicines.SelectedItem;
            if (e.Key == Key.Delete)
            {
                foreach (Prescription p in Appointment.Patient.MedicalHistory.GetByAppointment(Appointment))
                {
                    if (p.Medicine.Name.Equals(selected.Medicine.Name))
                    {
                        Appointment.Patient.MedicalHistory.RemovePrescription(p);
                    }
                }
            }
            medicines.DataContext = Appointment.Patient.MedicalHistory.GetByAppointment(Appointment);
        }
    }
}
