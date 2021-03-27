using System;
using System.Collections.Generic;

namespace Model
{
    public class Secretary : Employee
    {
        public Secretary(int id, string name, string surname, DateTime birthDate, string email, string password, string address, double salary, DateTime employmentDate, List<WorkDay> workDays) : base(id, name, surname, birthDate, email, password, address, salary, employmentDate, workDays)
        {
        }

        public Patient RegisterPatient()
        {
            throw new NotImplementedException();
        }

        public void UpdateInformation(Patient patient)
        {
            throw new NotImplementedException();
        }

        public Boolean BookAppointment()
        {
            throw new NotImplementedException();
        }

        public Boolean UpdateAppointment(Appointment appointment)
        {
            throw new NotImplementedException();
        }

    }
}