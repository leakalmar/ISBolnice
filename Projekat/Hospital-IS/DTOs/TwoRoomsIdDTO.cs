using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital_IS.DTOs
{
    public class TwoRoomsIdDTO
    {
        public int RoomIdSource { get; set; }
        public int RoomIdDestination { get; set; }

        public TwoRoomsIdDTO(int roomIdSource, int roomIdDestination)
        {
            RoomIdSource = roomIdSource;
            RoomIdDestination = roomIdDestination;
        }
    }
}
