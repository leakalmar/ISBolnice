using Hospital_IS.Storages;
using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service
{
    public class SpecializationService
    {
        private SpecializationFileStorage sfs = new SpecializationFileStorage();
        public List<Specialty> AllSpecialties { get; set; }

        private static SpecializationService instance = null;
        public static SpecializationService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SpecializationService();
                }
                return instance;
            }
        }

        private SpecializationService()
        {
            AllSpecialties = sfs.GetAll();
        }

        public List<Specialty> GetAll()
        {
            return AllSpecialties;
        }
    }
}
