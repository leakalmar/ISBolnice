using Hospital_IS.Storages;
using Model;
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

        public List<int> GetDoctorIDs()
        {
            List<int> allDoctorIDs = new List<int>();
            foreach (Employee employee in AllDoctors)
            {
                allDoctorIDs.Add(employee.Id);
            }
            return allDoctorIDs;
        }

        public List<Doctor> GetDoctorsBySpecialty(string specialtyName)
        {
            List<Doctor> doctors = new List<Doctor>();

            foreach (Doctor doctor in AllDoctors) 
            {
                if (specialtyName.Equals(doctor.Specialty.Name))
                    doctors.Add(doctor);

            }
            return doctors;
        }

    }
}
