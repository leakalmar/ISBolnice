﻿using Model;
using Service;
using Storages;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace Hospital_IS.Service
{
    class AdvancedRenovationSplitService : IAdvancedRenovationService
    {
       

        public void ExecuteAdvancedRoomRenovation(AdvancedRenovation advancedRenovation)
        {
            advancedRenovation.RoomFirst.SurfaceArea = advancedRenovation.RoomFirst.SurfaceArea / 2;
            Room updateRoom = new Room(advancedRenovation.RoomFirst.Id, advancedRenovation.RoomFirst.RoomFloor, advancedRenovation.RoomFirst.SurfaceArea / 2,
                advancedRenovation.RoomFirst.BedNumber, advancedRenovation.RoomFirst.Type);
            RoomService.Instance.UpdateRoom(updateRoom);
            Room room = new Room(advancedRenovation.RenovationResultRoom.RoomNumber, advancedRenovation.RenovationResultRoom.RoomFloor, advancedRenovation.RenovationResultRoom.SurfaceArea,
                 0, advancedRenovation.RenovationResultRoom.Type);
            RoomService.Instance.AddRoom(room);
        }

        public void MakeAdvancedRenovation(AdvancedRenovation advancedRenovation)
        {
            AdvancedRenovationService.Instance.AddNewAdvancedRenovation(advancedRenovation);
        }

      
    }
}
