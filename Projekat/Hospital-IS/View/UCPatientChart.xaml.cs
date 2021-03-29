using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Model;

namespace Hospital_IS.View
{
    /// <summary>
    /// Interaction logic for UCPatientChart.xaml
    /// </summary>
    public partial class UCPatientChart : UserControl
    {
        public Model.Patient Patient { get; set; }

        public UCPatientChart(Patient patient)
        {
            InitializeComponent();
            this.Patient = patient;

            ObservableCollection<Model.DoctorAppointment> History = new ObservableCollection<Model.DoctorAppointment>();
            ObservableCollection<Model.DoctorAppointment> Future = new ObservableCollection<Model.DoctorAppointment>();
            FindAppointments(History, Future);


            PersonalData.DataContext = patient;
            HistoryGrid.DataContext = patient;
            patientAppointments.DataContext = Future;
            patientHistory.DataContext = History;

        }

        public void FindAppointments(ObservableCollection<Model.DoctorAppointment> history, ObservableCollection<Model.DoctorAppointment> future)
        {
            foreach (Model.DoctorAppointment doc in Patient.DoctorAppointment)
            {
                if (doc.DateAndTime < DateTime.Now)
                {
                    history.Add(doc);
                }
                else if (doc.DateAndTime > DateTime.Now)
                {
                    future.Add(doc);
                }

            }
        }
    }
}
