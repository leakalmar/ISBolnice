using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class AntiTroll
    {
        public DateTime TrollCheckStartDate { get; set; }
        public int AppointmentCounterInTimeRange { get; set; }
        public bool IsTroll { get; set; }

        public AntiTroll()
        {
        }
    }
}
