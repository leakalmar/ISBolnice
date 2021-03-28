using System;

namespace Model
{
    public class Specialty
    {
        public String Role { get; set; }

        public Doctor Doctor { get; set; }

        public Specialty(string role)
        {
            this.Role = role;
        }

    }
}