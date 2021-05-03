﻿using Hospital_IS.Storages;
using Model;
using System;
using System.Collections.Generic;

namespace Service
{
    public class DoctorService
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



        public List<int> GetDoctorIds()
        {
            List<int> allDoctorIds = new List<int>();
            foreach (Employee employee in AllDoctors)
            {
                allDoctorIds.Add(employee.Id);
            }
            return allDoctorIds;
        }

        public List<Doctor> GetAllDoctorsByIds(List<int> senderIds)
        {
            List<Doctor> doctors = new List<Doctor>();
           foreach(Doctor doctor in AllDoctors)
            {
                if (senderIds.Contains(doctor.Id))
                {
                    doctors.Add(doctor);
                }
            }
            return doctors;
        }
    }
}
