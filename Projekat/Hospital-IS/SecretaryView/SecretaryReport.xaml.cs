using DTOs;
using Hospital_IS.Adapter;
using Hospital_IS.Controllers;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace Hospital_IS.SecretaryView
{
    /// <summary>
    /// Interaction logic for SecretaryReport.xaml
    /// </summary>
    public partial class SecretaryReport : Window
    {
        public ObservableCollection<DoctorAppointmentDTO> Appointments { get; set; }
        public IDoctorAppointmentTarget doctorAppointmentTarget = SecretaryMainWindow.Instance.target;
        public SecretaryReport()
        {
            InitializeComponent();
            var monday = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + (int)DayOfWeek.Monday);
            Appointments = new ObservableCollection<DoctorAppointmentDTO>(doctorAppointmentTarget.GetAllDoctorAppointmentsForCurrentWeek(monday));
            txtDate.Text = DateTime.Now.ToString("dd.MM.yyyy.");
            this.DataContext = this;
        }

        private void PrintReport(object sender, RoutedEventArgs e)
        {
            try
            {
                IsEnabled = false;
                PrintDialog pd = new PrintDialog();
                if (pd.ShowDialog() == true)
                {
                    pd.PrintVisual(report, "Izveštaj");
                }
            }
            finally 
            {
                IsEnabled = true;
            }
        }
    }
}
