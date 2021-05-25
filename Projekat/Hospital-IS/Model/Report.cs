using DTOs;
using Newtonsoft.Json;
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
        public AppointmentType Type { get; set; }
        public String Cause { get; set; }

        [JsonConstructor]
        public Report(DateTime appointmentDate, String doctorName, String doctorSurname, AppointmentType type, String cause) {
            this.HaveRecipe = false;
            this.HaveTest = false;
            this.ReportId = appointmentDate;
            this.DoctorName = doctorName;
            this.DoctorSurname = doctorSurname;
            this.Type = type;
            this.Cause = cause;
        }

        public Report(ReportDTO reportDTO)
        {
            this.Anamnesis = reportDTO.Anemnesis;
            if(reportDTO.CountPresciption > 0)
            {
                this.HaveRecipe = true;
            }
            else
            {
                this.HaveRecipe = false;
            }
            if (reportDTO.CountTests > 0)
            {
                this.HaveTest = true;
            }
            else
            {
                this.HaveTest = false;
            }
            this.ReportId = reportDTO.AppointmentStart;
            this.DoctorName = reportDTO.DoctorName;
            this.DoctorSurname = reportDTO.DoctorSurname;
            this.Type = reportDTO.Type;
            this.Cause = reportDTO.AppointmentCause;
        }


    }
}
