using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Model
{
    public class Report: DoctorAppointment
    {
        public DoctorAppointment DoctorApp { get; set; }

        public bool HaveRecipe { get; set; }

        public bool HaveTest { get; set; }

        public Prescription Prescription { get; set; }
        public ObservableCollection<Test> Test { get; set; }


        public Report(DoctorAppointment ap, String cause, String detail, bool recipe=false, bool test=false) : base(ap.DateAndTime, ap.Type, ap.Reserved, ap.Room, ap.Doctor, ap.Patient)
        {
            this.DoctorApp = ap;
            this.Cause = cause;
            this.Detail = detail;
            this.HaveRecipe = recipe;
            this.HaveTest = test;
            this.Test = new ObservableCollection<Test>();
        }

        public void AddTest(Test test)
        {
            Test.Add(test);
        }

    }
}
