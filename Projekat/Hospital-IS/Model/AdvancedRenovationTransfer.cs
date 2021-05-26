using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class AdvancedRenovation
    {
        public Room RoomFirst { get; set; }
        public Room RoomSecond { get; set; }
        public Room RenovationResultRoom { get; set; }
        public Boolean IsSplit { get; set; }
        public Boolean IsMerge { get; set; }

        public Boolean isMade { get; set; }
        public DateTime RenovationEnd { get; set; }

        public AdvancedRenovation(Room roomFirst, Room roomSecond, Room renovationResultRoom, bool isSplit, bool isMerge, DateTime renovationEnd)
        {
            RoomFirst = roomFirst;
            RoomSecond = roomSecond;
            RenovationResultRoom = renovationResultRoom;
            IsSplit = isSplit;
            IsMerge = isMerge;
            RenovationEnd = renovationEnd;
        }
    }
}
