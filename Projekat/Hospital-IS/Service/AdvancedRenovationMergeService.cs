using Model;
using Service;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace Hospital_IS.Service
{
    public class AdvancedRenovationMergeService : IAdvancedRenovationService
    {
        public void ExecuteAdvancedRoomRenovation(AdvancedRenovation advancedRenovation)
        {
            RoomService.Instance.RemoveRoom(advancedRenovation.RoomFirst);
            RoomService.Instance.RemoveRoom(advancedRenovation.RoomSecond);
            Room room = new Room(advancedRenovation.RenovationResultRoom.RoomNumber, advancedRenovation.RenovationResultRoom.RoomFloor, advancedRenovation.RenovationResultRoom.SurfaceArea,
                0, advancedRenovation.RenovationResultRoom.Type);
            RoomService.Instance.AddRoom(room);
        }
    }
}
