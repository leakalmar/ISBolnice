using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital_IS.DTOs
{
    public class HospitalEvaluationDTO
    {
        public int Grade { get; set; }
        public string Comment { get; set; }
        public DateTime Date { get; set; }
        public int PatientId { get; set; }

        public HospitalEvaluationDTO(int grade, string comment, DateTime date, int patientId)
        {
            this.Grade = grade;
            this.Comment = comment;
            this.Date = date;
            this.PatientId = patientId;
        }
    }
}
