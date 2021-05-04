using System;
using System.Collections.ObjectModel;

namespace Model
{
    public class Report
    {
        public String Anamnesis { get; set; }
        public bool HaveRecipe { get; set; }

        public bool HaveTest { get; set; }
        public DateTime ReportId { get; set; }

        public String DoctorName { get; set; }
        public String DoctorSurname { get; set; }
        public AppointmetType Type { get; set; }
        public String Cause { get; set; }

        public Report(DateTime appointmentDate, String doctorName, String doctorSurname, AppointmetType type, String cause) {
            this.HaveRecipe = false;
            this.HaveTest = false;
            this.ReportId = appointmentDate;
            this.DoctorName = doctorName;
            this.DoctorSurname = doctorSurname;
            this.Type = type;
            this.Cause = cause;
        }


    }
}
