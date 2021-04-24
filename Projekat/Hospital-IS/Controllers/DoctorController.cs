using System;
using System.Collections.Generic;
using System.Text;

namespace Controllers
{
    class DoctorController
    {
        private static DoctorController instance = null;
        public static DoctorController Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DoctorController();
                }
                return instance;
            }
        }

        private DoctorController()
        {

        }
    }
}
