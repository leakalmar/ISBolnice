using Hospital_IS.DTOs;
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

        public void AddDoctor(DoctorDTO doctorDTO)
        {
            Doctor doctor = new Doctor(doctorDTO.Id, doctorDTO.Name, doctorDTO.Surname, doctorDTO.BirthDate, doctorDTO.Email, 
                doctorDTO.Password, doctorDTO.Address, 80000.0, DateTime.Now, null, SpecializationService.Instance.GetSpecialtyByName(doctorDTO.Specialty), 0);
            DoctorService.Instance.AddDoctor(doctor);
        }

        public void UpdateDoctor(DoctorDTO doctorDTO)
        {
            Doctor doctor = new Doctor(doctorDTO.Id, doctorDTO.Name, doctorDTO.Surname, doctorDTO.BirthDate, doctorDTO.Email,
               doctorDTO.Password, doctorDTO.Address, 80000.0, DateTime.Now, null, SpecializationService.Instance.GetSpecialtyByName(doctorDTO.Specialty), 0);
            doctor.WorkShift = doctorDTO.WorkShift;
            doctor.VacationTimeStart = doctorDTO.VacationTimeStart;
            //doctor.DaysOff.AddRange(doctorDTO.DaysOff);
            DoctorService.Instance.UpdateDoctor(doctor);
        }

        public void DeleteDoctor(DoctorDTO doctorDTO)
        {
            Doctor doctor = DoctorService.Instance.GetDoctorByID(doctorDTO.Id);
            DoctorService.Instance.DeleteDoctor(doctor);
        }
    }
}
