using Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital_IS.DTOs
{
    public class RenovationDTO
    {
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public AppointmentType AppType { get; set; }

        public RenovationDTO(DateTime dateStart, DateTime dateEnd, AppointmentType appType)
        {
            DateStart = dateStart;
            DateEnd = dateEnd;
            AppType = appType;
        }
    }
}
