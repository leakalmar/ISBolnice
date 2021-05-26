using Model;
using Storages;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace Service
{
    public class AdvancedRenovationService
    {
        private AdvancedRenovationStorage afs = new AdvancedRenovationStorage();
        public List<AdvancedRenovation> AllAdvancedRenovations { get; set; }

        private static AdvancedRenovationService instance = null;
        public static AdvancedRenovationService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new AdvancedRenovationService();
                }
                return instance;
            }
        }

        private AdvancedRenovationService()
        {
            AllAdvancedRenovations = afs.GetAll();
        }

        public void AddNewAdvancedRenovation(AdvancedRenovation advancedRenovation)
        {
            AllAdvancedRenovations.Add(advancedRenovation);
            afs.Save(AllAdvancedRenovations);
        }

        
        public void RemoveAdvancedRenovation(AdvancedRenovation advancedRenovation)
        {

            AllAdvancedRenovations.Remove(advancedRenovation);
            afs.Save(AllAdvancedRenovations);
        }
    }
}
