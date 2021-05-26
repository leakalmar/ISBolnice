﻿using Hospital_IS.DTOs;
using Model;
using Service;
using System;
using System.Collections.Generic;

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

        public List<Doctor> GetAllDoctorsByIds(List<int> senderIds)
        {
            return DoctorService.Instance.GetAllDoctorsByIds(senderIds);
        }

        public List<Doctor> GetAll()
        {
            return DoctorService.Instance.GetAll();
        }

        public List<Doctor> GetDoctorsBySpecilty(Specialty specialty)
        {
            return DoctorService.Instance.GetDoctorsBySpecialty(specialty.Name);
        }

        public String GetDoctorsNameAndSurname(int senderId)
        {
            return DoctorService.Instance.GetDoctorNameAndSurname(senderId);
        }

        public void AddDoctor(Doctor doctor)
        {
            DoctorService.Instance.AddDoctor(doctor);
        }

        public void UpdateDoctort(Doctor doctor)
        {
            DoctorService.Instance.UpdateDoctort(doctor);
        }

        public void DeleteDoctor(DoctorDTO doctorDTO)
        {
            Doctor doctor = DoctorService.Instance.GetDoctorByID(doctorDTO.Id);
            DoctorService.Instance.DeleteDoctor(doctor);
        }
    }
}
