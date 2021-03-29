using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Report: DoctorAppointment
    {
        public DoctorAppointment DoctorApp { get; set; }

        public bool Recipe { get; set; }

        public bool Test { get; set; }


        private Report(DoctorAppointment ap, String cause, String detail, bool recipe=false, bool test=false) : base(ap.DateAndTime, ap.Type, ap.Reserved, ap.Room, ap.Doctor, ap.patient)
        {
            this.DoctorApp = ap;
            this.Cause = cause;
            this.Detail = detail;
            this.Recipe = recipe;
            this.Test = test;
        }

        public void AddReport(DoctorAppointment doc, String cause, String detail, bool recipe = false, bool test = false)
        {
            Report rep = new Report(doc, cause, detail, recipe, test);
            doc.patient.medicalHistory.AddReport(rep);
        }

    }
}
