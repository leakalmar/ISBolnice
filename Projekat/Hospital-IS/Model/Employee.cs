using System;
using System.Collections.Generic;

namespace Model
{
    public class Employee : User
    {
        public Double Salary { get; set; }
        public DateTime EmploymentDate { get; set; }
        public List<WorkDay> workDay { get; set; }

        public Employee(int id, string name, string surname, DateTime birthDate, string email, string password, string address, double salary, DateTime employmentDate, List<WorkDay> workDays) : base(id, name, surname, birthDate, email, password, address)
        {
            this.Salary = salary;
            this.EmploymentDate = employmentDate;
            this.WorkDay = workDays;
        }

        public List<WorkDay> WorkDay
        {
            get
            {
                if (workDay == null)
                    workDay = new List<WorkDay>();
                return workDay;
            }
            set
            {
                RemoveAllWorkDay();
                if (value != null)
                {
                    foreach (WorkDay oWorkDay in value)
                        AddWorkDay(oWorkDay);
                }
            }
        }

        public void AddWorkDay(WorkDay newWorkDay)
        {
            if (newWorkDay == null)
                return;
            if (this.workDay == null)
                this.workDay = new List<WorkDay>();
            if (!this.workDay.Contains(newWorkDay))
                this.workDay.Add(newWorkDay);
        }

        public void RemoveWorkDay(WorkDay oldWorkDay)
        {
            if (oldWorkDay == null)
                return;
            if (this.workDay != null)
                if (this.workDay.Contains(oldWorkDay))
                    this.workDay.Remove(oldWorkDay);
        }

        public void RemoveAllWorkDay()
        {
            if (workDay != null)
                workDay.Clear();
        }

    }
}