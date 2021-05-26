using Model;
using Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital_IS.Controllers
{
    public class AdvancedRenovationController
    {


        private static AdvancedRenovationController instance = null;
        public static AdvancedRenovationController Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new AdvancedRenovationController();
                }
                return instance;
            }
        }

        private AdvancedRenovationController()
        {

        }

        public List<AdvancedRenovation> GetAll()
        {
            return AdvancedRenovationService.Instance.AllAdvancedTransfers;
        }

        public void MakeAdvancedRenovation(AdvancedRenovation advancedRenovation)
        {
             AdvancedRenovationService.Instance.AddNewAdvancedRenovation(advancedRenovation);
        }

        public void RemoveAdvancedRenovation(AdvancedRenovation advancedRenovation)
        {
            AdvancedRenovationService.Instance.RemoveAdvancedRenovation(advancedRenovation);
        }
    }
}
