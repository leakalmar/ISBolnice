using Model;
using Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace Controllers
{
    public class SpecializationController
    {
        private static SpecializationController instance = null;
        public static SpecializationController Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SpecializationController();
                }
                return instance;
            }
        }

        private SpecializationController()
        {
        }

        public List<Specialty> GetAll()
        {
            return SpecializationService.Instance.GetAll();
        }

    }
}
