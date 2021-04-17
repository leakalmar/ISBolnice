using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Therapy
    {
        public Medicine Medicine { get; set; }
        public int Quantity { get; set; }
        public int TimesADay { get; set; }
        public DateTime TherapyStart { get; set; }
        public DateTime TherapyEnd { get; set; }

        public Therapy(Medicine medicine, int quantity, int timesADay, DateTime start, DateTime end)
        {
            Medicine = medicine;
            Quantity = quantity;
            TimesADay = timesADay;
            TherapyStart = start;
            TherapyEnd = end;
        }
    }
}
