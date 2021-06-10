using Enums;
using Hospital_IS.DTOs;
using Hospital_IS.Service;
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
            return AdvancedRenovationService.Instance.AllAdvancedRenovations;
        }

        public void MakeAdvancedRenovation(AdvancedRenovation advancedRenovation)
        {
            IAdvancedRenovationService advancedRenovationService;
            if (advancedRenovation.Type == AdvancedRenovationType.MERGE)
            {
                advancedRenovationService = new AdvancedRenovationMergeService();
                advancedRenovationService.MakeAdvancedRenovation(advancedRenovation);
            }
            else if (advancedRenovation.Type == AdvancedRenovationType.SPLIT)
            {
                advancedRenovationService = new AdvancedRenovationSplitService();
                advancedRenovationService.MakeAdvancedRenovation(advancedRenovation);
            }
        }

        public void RemoveAdvancedRenovation(AdvancedRenovation advancedRenovation)
        {
            AdvancedRenovationService.Instance.RemoveAdvancedRenovation(advancedRenovation);
        }


        public void ExecuteAdvancedRoomRenovation(AdvancedRenovation advancedRenovation)
        {
            IAdvancedRenovationService advancedRenovationService;
            if(advancedRenovation.Type == AdvancedRenovationType.MERGE)
            {
                advancedRenovationService = new AdvancedRenovationMergeService();
                advancedRenovationService.ExecuteAdvancedRoomRenovation(advancedRenovation);
            }
            else if(advancedRenovation.Type == AdvancedRenovationType.SPLIT)
            {
                advancedRenovationService = new AdvancedRenovationSplitService();
                advancedRenovationService.ExecuteAdvancedRoomRenovation(advancedRenovation);
            }
        }

        public bool CheckIfTransferIsAfterRenovation(RenovationAppointmentDTO renovationAppointmentDTO)
        {
            return AdvancedRenovationService.Instance.CheckIfTransferIsAfterRenovation(renovationAppointmentDTO);
        }
    }
}
