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
using Hospital_IS.Storages;
using Model;
using Storages;

namespace Hospital_IS.View
{
    public partial class DoctorHomePage : Window
    {
        private static DoctorHomePage instance = null;
        private FSDoctor dfs = new FSDoctor();
        private PatientFileStorage pfs = new PatientFileStorage();

        public static DoctorHomePage Instance
        {
            get
            {
                if (instance == null)
                {
                    Doctor d = Hospital.Instance.Doctors[0];
                    instance = new DoctorHomePage(d);
                }
                return instance;
            }
        }
        public Doctor Doctor { get; set; }
        private DoctorHomePage(Doctor doctor)
        {
            InitializeComponent();

            Patient p1 = Hospital.Instance.allPatients[0];
            Patient p2 = Hospital.Instance.allPatients[1];
            Patient p3 = Hospital.Instance.allPatients[2];

            DoctorAppointment da1 = new Model.DoctorAppointment(new DateTime(2021, 03, 31, 9, 0, 0), Model.AppointmetType.CheckUp, true, doctor.PrimaryRoom, doctor, p1);
            DoctorAppointment da2 = new Model.DoctorAppointment(new DateTime(2021, 04, 10, 16, 0, 0), Model.AppointmetType.CheckUp, true, doctor.PrimaryRoom, doctor, p1);


            DoctorAppointment pastDa = new Model.DoctorAppointment(new DateTime(2020, 05, 03, 12, 0, 0), Model.AppointmetType.CheckUp, true, doctor.PrimaryRoom, doctor, p1);
            Model.Report report = new Model.Report(pastDa, "Ovde ide uzrok", "Ovde idu detalji pregleda", false, false);
            p1.MedicalHistory.AddReport(report);


            Model.Prescription per = new Model.Prescription();
            per.AddMedicine("Paracetamol");
            per.AddMedicine("Brufen");
            report.Prescription = per;


            Model.Test test = new Model.Test("Dijagnoza", report.DoctorApp.DateAndTime, "neki test", "rezultati");
            report.AddTest(test);


            p1.AddDoctorAppointment(da1);
            p1.AddDoctorAppointment(da2);
            p1.AddDoctorAppointment(pastDa);

            doctor.AddDoctorAppointment(da1);
            doctor.AddDoctorAppointment(da2);
            doctor.AddDoctorAppointment(pastDa);

            this.Doctor = doctor;
            this.DataContext = new ViewModel.HomePage();
        }

        public void ExitBtnClick(object sender, RoutedEventArgs e)
        {
            bool dialog = (bool)new ExitMess("Da li ste sigurni da želite da se odjavite?").ShowDialog();
           if (dialog)
            {
                this.Close();
                //dfs.Save(Hospital.Instance.Doctors);
                //foreach (Patient p in Hospital.Instance.allPatients)
               // {
                 //   pfs.SavePatient(p);
               // }
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
