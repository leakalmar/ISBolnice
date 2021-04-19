using System;
using System.Collections.Generic;

namespace Model
{
    public class Employee : User
    {
        public Double Salary { get; set; }
        public DateTime EmploymentDate { get; set; }

        public Employee(int id, string name, string surname, DateTime birthDate, string address, string email, string password, double salary, DateTime employmentDate, List<WorkDay> workDays) : base(id, name, surname, birthDate, address, email, password)
        {
            this.Salary = salary;
            this.EmploymentDate = employmentDate;
            this.WorkDay = workDays;
        }

        public Employee()
        {
        }

        public List<WorkDay> WorkDay
        {
            get; set;
        }

        public void AddWorkDay(WorkDay newWorkDay)
        {
            if (newWorkDay == null)
                return;
            if (this.WorkDay == null)
                this.WorkDay = new List<WorkDay>();
            if (!this.WorkDay.Contains(newWorkDay))
                this.WorkDay.Add(newWorkDay);
        }

        public void RemoveWorkDay(WorkDay oldWorkDay)
        {
            if (oldWorkDay == null)
                return;
            if (this.WorkDay != null)
                if (this.WorkDay.Contains(oldWorkDay))
                    this.WorkDay.Remove(oldWorkDay);
        }

        public void RemoveAllWorkDay()
        {
            if (WorkDay != null)
                WorkDay.Clear();
        }

    }
}