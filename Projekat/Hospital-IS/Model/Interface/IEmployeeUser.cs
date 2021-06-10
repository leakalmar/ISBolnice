using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital_IS.Model.Interface
{
    interface IEmployeeUser : IUser
    {
        public Double Salary { get; set; }
        public DateTime EmploymentDate { get; set; }
        public List<WorkDay> WorkDay { get; set; }
    }
}
