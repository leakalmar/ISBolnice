using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class MedicineNotification
    {
        public String Title { get; set; }
        public Medicine Medicine { get; set; }
        public List<int> DoctorIds { get; set; }
        public String Note { get; set; }

        public MedicineNotification(string title, Medicine medicine, List<int> doctorIds)
        {
            Title = title;
            Medicine = medicine;
            DoctorIds = doctorIds;
        }
    }
}
