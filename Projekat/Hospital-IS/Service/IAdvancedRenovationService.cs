using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital_IS.Service
{
    public interface IAdvancedRenovationService
    {
        void ExecuteAdvancedRoomRenovation(AdvancedRenovation advancedRenovation);
        void MakeAdvancedRenovation(AdvancedRenovation advancedRenovation);
    }
}
