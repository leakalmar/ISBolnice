﻿using System;
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
            this.SourceRoomId = sourceRoomId;
            this.DestinationRoomId = destinationRoomId;
            this.Equip = equip;
            this.Quantity = quantity;
            this.TransferEnd = transferEnd;
            this.isMade = isMade;
        }

        public Transfer()
        {

        }
    }
}
