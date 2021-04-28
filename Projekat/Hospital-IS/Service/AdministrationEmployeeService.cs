using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service
{
    public class AdministrationEmployeeService
    {

        public List<Employee> AllEmployees { get; set; }

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
            
        }
    }
}
