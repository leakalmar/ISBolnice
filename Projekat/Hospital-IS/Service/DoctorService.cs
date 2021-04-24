using Hospital_IS.Storages;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service
{
    class DoctorService
    {
        private FSDoctor dfs = new FSDoctor();

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

        }
    }
}
