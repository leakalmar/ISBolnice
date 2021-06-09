using Enums;
using Hospital_IS.Model.Interface;
using System;
using System.Collections.Generic;

namespace Model
{
    public class Employee : IEmployeeUser
    {
        public EducationCategory Education { get; set; }
        public int Id { get; set; }
        public String Name { get; set; }
        public String Surname { get; set; }
        public DateTime BirthDate { get; set; }
        public String Phone { get; set; }
        public String Email { get; set; }
        public String Gender { get; set; }
        public String Relationship { get; set; }
        public String Password { get; set; }
        public String Address { get; set; }
        public Double Salary { get; set; }
        public DateTime EmploymentDate { get; set; }
        public List<WorkDay> WorkDay { get; set;}

        public Employee(int id, string name, string surname, DateTime birthDate, string address, string email, string password, double salary, DateTime employmentDate, List<WorkDay> workDays)
        {
            this.Id = id;
            this.Name = name;
            this.Surname = surname;
            this.BirthDate = birthDate;
            this.Address = address;
            this.Email = email;
            this.Password = password;
            this.Salary = salary;
            this.EmploymentDate = employmentDate;
            this.WorkDay = workDays;
        }

        public Employee()
        {
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