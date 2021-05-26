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
        public List<AdvancedRenovation> AllAdvancedTransfers { get; set; }

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
            AllAdvancedTransfers = afs.GetAll();
        }

        public void AddNewAdvancedRenovation(AdvancedRenovation advancedRenovation)
        {
            AllAdvancedTransfers.Add(advancedRenovation);
            afs.Save(AllAdvancedTransfers);
        }

        
        public void RemoveAdvancedRenovation(AdvancedRenovation advancedRenovation)
        {
            
            AllAdvancedTransfers.Remove(advancedRenovation);
            afs.Save(AllAdvancedTransfers);
        }
    }
}
