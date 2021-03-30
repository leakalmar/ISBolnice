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
using Model;
using Storages;

namespace Hospital_IS.View
{
    public partial class DoctorHomePage : Window
    {
        private static DoctorHomePage instance = null;

        public static DoctorHomePage Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DoctorHomePage();
                }
                return instance;
            }
        }
        public Model.Doctor Doctor { get; set; }
        private DoctorHomePage()
        {
            InitializeComponent();
            List<WorkDay> dani = new List<WorkDay>();
            dani.Add(new WorkDay(Day.Monday, new DateTime(2020, 02, 02, 8, 0, 0), new DateTime(2020, 02, 02, 20, 0, 0)));
            dani.Add(new WorkDay(Day.Tuesday, new DateTime(2020, 02, 02, 8, 0, 0), new DateTime(2020, 02, 02, 20, 0, 0)));
            dani.Add(new WorkDay(Day.Wednesday, new DateTime(2020, 02, 02, 8, 0, 0), new DateTime(2020, 02, 02, 20, 0, 0)));
            dani.Add(new WorkDay(Day.Thursday, new DateTime(2020, 02, 02, 8, 0, 0), new DateTime(2020, 02, 02, 20, 0, 0)));
            dani.Add(new WorkDay(Day.Friday, new DateTime(2020, 02, 02, 8, 0, 0), new DateTime(2020, 02, 02, 20, 0, 0)));
            dani.Add(new WorkDay(Day.Saturday, new DateTime(2020, 02, 02, 8, 0, 0), new DateTime(2020, 02, 02, 20, 0, 0)));
            dani.Add(new WorkDay(Day.Sunday, new DateTime(2020, 02, 02, 8, 0, 0), new DateTime(2020, 02, 02, 20, 0, 0)));
            Specialty spec = new Specialty("Dermatolog");
            Room r = new Room(RoomType.ConsultingRoom, false, true, 2, 25);
            Doctor doc = new Doctor(101, "Dragana", "Vukmanov Simokov", new DateTime(1980, 05, 12), "doktor@gmail.com", "doktor", "Brace Radica 30", 70000.0, DateTime.Now, dani, spec, r);


            ObservableCollection<String> alergije = new ObservableCollection<String>();
            alergije.Add("Tetanus");
            alergije.Add("Paracetamol");
            Model.Patient p1 = new Model.Patient(001, "Simona", "Vukmanov Simokov", DateTime.Now.Date, "Petra Drapsina 8", "simona@gmail.com", "123", DateTime.Now, "neki poslodavac", alergije);
            p1.AddDoctorAppointment(new Model.DoctorAppointment(new DateTime(2018, 07, 20, 9, 0, 0), Model.AppointmetType.CheckUp, true, r, doc, p1));
            p1.AddDoctorAppointment(new Model.DoctorAppointment(new DateTime(2016, 11, 19, 16, 0, 0), Model.AppointmetType.CheckUp, true, r, doc, p1));


            DoctorAppointment appointment1 = new Model.DoctorAppointment(new DateTime(2020, 05, 03, 12, 0, 0), Model.AppointmetType.CheckUp, true, r, doc, p1);
            Model.Report report = new Model.Report(appointment1, "Ovde ide uzrok", "Ovde idu detalji pregleda", false, false);
            p1.MedicalHistory.AddReport(report);

            Model.Prescription per = new Model.Prescription();
            per.AddMedicine("Paracetamol");
            per.AddMedicine("Brufen");
            report.Prescription = per;

            Model.Test test = new Model.Test("Dijagnoza", report.DoctorApp.DateAndTime, "neki test", "rezultati");
            report.AddTest(test);


            DoctorAppointment a1 = new DoctorAppointment(new DateTime(2020, 05, 05), AppointmetType.CheckUp, true, new Room(RoomType.ConsultingRoom, true, true, 1, 1), doc, p1);
            DoctorAppointment a2 = new DoctorAppointment(new DateTime(2021, 07, 05), AppointmetType.Operation, true, new Room(RoomType.ConsultingRoom, true, true, 1, 1), doc, p1);
            DoctorAppointment a3 = new DoctorAppointment(DateTime.Now, AppointmetType.CheckUp, true, new Room(RoomType.ConsultingRoom, true, true, 1, 1), doc, p1);
            doc.AddDoctorAppointment(a1);
            doc.AddDoctorAppointment(a2);
            doc.AddDoctorAppointment(a3);
            this.Doctor = doc;

            this.Doctor = doc;
            this.DataContext = new ViewModel.HomePage();
        }

        public void ExitBtnClick(object sender, RoutedEventArgs e)
        {
            bool dialog = (bool)new ExitMess("Da li ste sigurni da želite da se odjavite?").ShowDialog();
           if (dialog)
            {
                Hospital h = Hospital.Instance;
                this.Close();
            }
         }

        public void MinimizeBtnClick(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        public void MaximizeBtnClick(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Maximized;
        }

        private void HomePage_Clicked(object sender, RoutedEventArgs e)
        {
            DataContext = new ViewModel.HomePage();
        }
    }
}
