﻿using Model;
using Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace Controllers
{
    public class DoctorController
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

        public List<Doctor> GetAll()
        {
            return DoctorService.Instance.AllDoctors;
        }

        public List<Doctor> GetDoctorsBySpecilty(Specialty specialty)
        {
            return DoctorService.Instance.GetDoctorsBySpecialty(specialty.Name);
        }
    }
}
