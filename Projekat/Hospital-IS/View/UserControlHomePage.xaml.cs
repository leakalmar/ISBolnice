using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
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
    /// Interaction logic for UserControlHomePage.xaml
    /// </summary>
    public partial class UserControlHomePage : UserControl
    {
        public Doctor Doctor { get; set; }
        public UserControlHomePage()
        {
            InitializeComponent();
            List<WorkDay> dani = new List<WorkDay>
            {
                new WorkDay(Day.Monday, DateTime.Now, DateTime.Now)
            };
            Specialty spec = new Specialty("Dermatolog");
            Doctor doc = new Doctor(111, "Dragana", "Vukmanov Simokov", DateTime.Now, "dragana@gmail.com", "123", "Brace Radica 30", 60000.0, DateTime.Now, dani, spec, null);
            ObservableCollection<String> alergije = new ObservableCollection<String>();
            alergije.Add("Tetanus");
            alergije.Add("Paracetamol");
            Model.Patient p1 = new Model.Patient(001, "Simona", "Vukmanov Simokov", DateTime.Now.Date, "Petra Drapsina 8", "simona@gmail.com", "123", DateTime.Now, "neki poslodavac", alergije);
            Room r = new Room(RoomType.ConsultingRoom, false, true, 2, 25);
            DoctorAppointment appointment1 = new Model.DoctorAppointment(new DateTime(2020, 05, 03, 12, 0, 0), Model.AppointmetType.CheckUp, true, r, doc, p1);
            p1.AddDoctorAppointment(new Model.DoctorAppointment(new DateTime(2018, 07, 20, 9, 0, 0), Model.AppointmetType.CheckUp, true, r, doc, p1));
            p1.AddDoctorAppointment(new Model.DoctorAppointment(new DateTime(2016, 11, 19, 16, 0, 0), Model.AppointmetType.CheckUp, true, r, doc, p1));
            Model.Report report = new Model.Report(appointment1, "Ovde ide uzrok", "Ovde idu detalji pregleda", false, false);
            Model.Prescription per = new Model.Prescription();
            per.AddMedicine("Paracetamol");
            per.AddMedicine("Brufen");
            report.Prescription = per;
            Model.Test test = new Model.Test("Dijagnoza",report.DoctorApp.DateAndTime, "neki test", "rezultati");
            report.AddTest(test);
            p1.MedicalHistory.AddReport(report);

            DoctorAppointment a1 = new DoctorAppointment(new DateTime(2020, 05, 05), AppointmetType.CheckUp, true, r, doc, p1);
            DoctorAppointment a2 = new DoctorAppointment(new DateTime(2021, 07, 05), AppointmetType.Operation, true, r, doc, p1);
            DoctorAppointment a3 = new DoctorAppointment(DateTime.Now.AddHours(1), AppointmetType.CheckUp, true, r, doc, p1);
            doc.AddDoctorAppointment(a1);
            doc.AddDoctorAppointment(a2);
            doc.AddDoctorAppointment(a3);
            this.Doctor = doc;

            ObservableCollection<DoctorAppointment> collection = new ObservableCollection<Model.DoctorAppointment>();
            foreach (DoctorAppointment ap in doc.DoctorAppointment)
            {

                DateTime temp = ap.DateAndTime;
                if (temp.AddMinutes(60) > DateTime.Now)
                {
                    collection.Add(ap);
                }
            }

            docotrAppointments.DataContext = collection;
        }

        private void Patient_Selected(object sender, KeyEventArgs e)
        {
            DoctorAppointment appointment= (DoctorAppointment)docotrAppointments.SelectedItem;
            Window window = new PatientChart(appointment);
            window.Show();
        }

        public void Patient_DoubleClicked(object sender, MouseButtonEventArgs e)
        {
            DoctorAppointment ap = (DoctorAppointment)docotrAppointments.SelectedItem;
            DoctorAppointment first = (DoctorAppointment)docotrAppointments.SelectedItems[0];

            if (ap.Equals(first) && ap.DateAndTime.Date.Equals(DateTime.Now.Date))
            {
                Window window = new PatientChart(ap, true, true);
                window.Show();
            }
            else if (ap.DateAndTime > DateTime.Now.AddDays(1))
            {
                Window window = new PatientChart(ap, false, false);
                window.Show();
            }
            else
            {
                Window window = new PatientChart(ap,false, true);
                window.Show();
            }
            
        }
    }
}
