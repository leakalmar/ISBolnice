using System;

namespace Model
{
    public class Specialty
    {
        public String role { get; set; }

        public Doctor doctor;

        public Doctor Doctor
        {
            get
            {
                return doctor;
            }
            set
            {
                this.doctor = value;
            }
        }

    }
}