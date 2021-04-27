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
        public List<Doctor> allDoctors { get; set; }

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
            allDoctors = dfs.GetAll();
        }
    }
}
