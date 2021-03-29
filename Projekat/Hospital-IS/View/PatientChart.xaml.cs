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
using System.Windows.Shapes;

namespace Hospital_IS.View
{
    /// <summary>
    /// Interaction logic for PatientChart.xaml
    /// </summary>
    public partial class PatientChart : Window
    {
        public Model.Patient patient { get; set; }

        public ObservableCollection<Model.DoctorAppointment> History { get; set; }
        public ObservableCollection<Model.DoctorAppointment> Future { get; set; }

        public PatientChart()
        {
            ObservableCollection<String> alergije = new ObservableCollection<String>();
            alergije.Add("Tetanus");
            alergije.Add("Paracetamol");
            Model.Room r1 = new Model.Room(Model.RoomType.ConsultingRoom, false, true, 2, 25);
            Model.Patient p1 = new Model.Patient(001, "Simona", "Vukmanov Simokov", DateTime.Now.Date, "Petra Drapsina 8", "simona@gmail.com", "123", DateTime.Now, "neki poslodavac", alergije);
            List<Model.WorkDay> dani = new List<Model.WorkDay>
            {
                new Model.WorkDay("Pon", DateTime.Now, DateTime.Now)
            };
            Model.Specialty spec = new Model.Specialty("Dermatolog");
            Model.Doctor doc = new Model.Doctor(111, "Dragana", "Vukmanov Simokov", DateTime.Now, "dragana@gmail.com", "123", "Brace Radica 30", 60000.0, DateTime.Now, dani,spec);
            p1.AddDoctorAppointment(new Model.DoctorAppointment(new DateTime(2020, 05, 03, 12, 0, 0),Model.AppointmetType.CheckUp, true, r1, doc,p1));
            p1.AddDoctorAppointment(new Model.DoctorAppointment(new DateTime(2018, 07, 20, 9, 0, 0), Model.AppointmetType.CheckUp, true, r1, doc,p1));
            p1.AddDoctorAppointment(new Model.DoctorAppointment(new DateTime(2016, 11, 19, 16, 0, 0), Model.AppointmetType.CheckUp, true, r1, doc,p1));


            InitializeComponent();
            this.patient = p1;

            History = new ObservableCollection<Model.DoctorAppointment>();
            Future = new ObservableCollection<Model.DoctorAppointment>();
            FindAppointments();

            PersonalData.DataContext = patient;
            HistoryGrid.DataContext = patient;
            patientAppointments.DataContext = this;
            patientHistory.DataContext = this;

        }

        public void FindAppointments()
        {
            foreach (Model.DoctorAppointment doc in patient.DoctorAppointment)
            {
                if(doc.DateAndTime < DateTime.Now)
                {
                    History.Add(doc);
                }
                else if(doc.DateAndTime > DateTime.Now)
                {
                    Future.Add(doc);
                }

            }
        }

    }

    
}
