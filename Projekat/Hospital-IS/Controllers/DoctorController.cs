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

        public List<Doctor> GetDoctorsBySpecilty(string name)
        {
            return DoctorService.Instance.GetDoctorsBySpecialty(name);
        }

        public String GetDoctorsNameAndSurname(int senderId)
        {
            return DoctorService.Instance.GetDoctorNameAndSurname(senderId);
        }

        public Doctor GetDoctorById(int doctorId)
        {
            return DoctorService.Instance.GetDoctorByID(doctorId);
        }
    }
}
