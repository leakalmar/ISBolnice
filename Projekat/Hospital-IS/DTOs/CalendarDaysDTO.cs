using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital_IS.DTOs
{
    public class CalendarDaysDTO
    {
        public DateTime Date { get; set; }
        public string AppointmentInformation { get; set; }
        public bool IsToday { get; set; }
        public bool IsTargetMonth { get; set; }

        public CalendarDaysDTO(DateTime date, bool isTargetMonth)
        {
            this.Date = date;
            this.IsTargetMonth = isTargetMonth;
        }
    }
}
