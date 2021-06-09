using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital_IS.DTOs
{
    public class TherapyReportDTO
    {
        public int PatientId { get; set; }
        public DateTime ReportStart { get; set; }
        public DateTime ReportEnd { get; set; }

        public TherapyReportDTO(int patientId, DateTime reportStart, DateTime reportEnd)
        {
            this.PatientId = patientId;
            this.ReportStart = reportStart;
            this.ReportEnd = reportEnd;
        }
    }
}
