using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Transfer
    {

        public int SourceRoomId { get; set; }
        public int DestinationRoomId { get; set; }
        public Equipment Equip { get; set;}
        public int Quantity { get; set; }

        public DateTime TransferEnd { get; set; }

        public Boolean isMade { get; set; }

        public Transfer(int sourceRoomId, int destinationRoomId, Equipment equip, int quantity, DateTime transferEnd,Boolean isMade)
        {
            SourceRoomId = sourceRoomId;
            DestinationRoomId = destinationRoomId;
            Equip = equip;
            Quantity = quantity;
            TransferEnd = transferEnd;
            isMade = isMade;
        }

        public Transfer()
        {

        }
    }
}
