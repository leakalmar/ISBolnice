using Hospital_IS.Storages;
using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service
{
    class DoctorService
    {
        private FSDoctor dfs = new FSDoctor();
        public List<Doctor> AllDoctors { get; set; }

        private static DoctorService instance = null;
        public static DoctorService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DoctorService();
                }
                return instance;
            }
        }

        private DoctorService()
        {
            AllDoctors = dfs.GetAll();
        }

        public List<int> GetDoctorIDs()
        {
            List<int> allDoctorIDs = new List<int>();
            foreach (Employee employee in AllDoctors)
            {
                allDoctorIDs.Add(employee.Id);
            }
            return allDoctorIDs;
        }
    }
}
