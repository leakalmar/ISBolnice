using System;
using System.Collections.ObjectModel;

namespace Model
{
    public class Report
    {
        public String Amnesis { get; set; }
        public bool HaveRecipe { get; set; }

        public bool HaveTest { get; set; }
        public String ReportId { get; set; }

        public Report(DateTime appointmentDate) {
            this.HaveRecipe = false;
            this.HaveTest = false;
            this.ReportId = appointmentDate.ToString();
        }


    }
}
