using Model;
using Storages;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service
{
    public class AdministrationEmployeeService
    {

        public List<Employee> AllEmployees { get; set; }

        public EmployeeFileStorage efs = new EmployeeFileStorage();

        private static AdministrationEmployeeService instance = null;
        public static AdministrationEmployeeService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new AdministrationEmployeeService();
                }
                return instance;
            }
        }

        private AdministrationEmployeeService()
        {
            AllEmployees = efs.GetAll();
        }

        public List<int> GetEmployeIDs()
        {
            List<int> allEmployeeIDs = new List<int>();
            foreach(Employee employee in AllEmployees)
            {
                allEmployeeIDs.Add(employee.Id);
            }
            return allEmployeeIDs;
        }

    }
}
